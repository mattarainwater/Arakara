using Arakara.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using Nez;
using Microsoft.Xna.Framework;
using Arakara.Common;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez.Textures;

namespace Arakara.Components
{
    public class DeckBuilderActor<TEnum> : BattleActor where TEnum : struct, IComparable, IFormattable
    {
        public List<Card<TEnum>> Deck { get; set; }
        private List<Card<TEnum>> _hand;
        private List<Card<TEnum>> _discardPile;
        private List<Entity> _handEntities;
        private Card<TEnum> _selectedCard;
        private int _handSize = 3;
        private bool _drawing;
        private Sprite<TEnum> _animator;
        private TEnum _idleAnimation;

        private Texture2D _defaultCardTexture;
        private Texture2D _hoverCardTexture;

        private VirtualButton _selectInput;
        private VirtualButton _backInput;
        private VirtualButton _leftInput;
        private VirtualButton _rightInput;

        private Selector _cardSelector;
        private Selector _targetSelector;

        private int _selectedCardIndex = 0;

        public DeckBuilderActor(string name, int maxHP, Faction faction, List<Card<TEnum>> cards, float dodgeChance, float critChance, float speed, TEnum idleAnimation) :
            base(name, maxHP, faction, dodgeChance, critChance, speed)
        {
            Deck = cards;
            _hand = new List<Card<TEnum>>();
            _discardPile = new List<Card<TEnum>>();
            _handEntities = new List<Entity>();

            ShuffleDeck();

            _idleAnimation = idleAnimation;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            _animator = entity.getComponent<Sprite<TEnum>>();
            _animator.play(_idleAnimation);
            _defaultCardTexture = entity.scene.contentManager.Load<Texture2D>("card");
            _hoverCardTexture = entity.scene.contentManager.Load<Texture2D>("card_dark");

            var cardSelectorEntity = entity.scene.createEntity("cardSelector");
            _cardSelector = cardSelectorEntity.addComponent(new Selector(
                onFocus: OnCardFocus,
                onBlur: OnCardBlur,
                onSelect: OnCardSelect));

            var targetSelectorEntity = entity.scene.createEntity("targetSelector");
            _targetSelector = targetSelectorEntity.addComponent(new Selector(
                onFocus: OnTargetFocus,
                onBlur: OnTargetBlur,
                onSelect: OnTargetSelect));

            SetupInput();
        }

        public override void onRemovedFromEntity()
        {
            _selectInput.deregister();
            _backInput.deregister();
            _leftInput.deregister();
            _rightInput.deregister();
        }

        private void SetupInput()
        {
            _selectInput = new VirtualButton();
            _selectInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.J));
            _selectInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.A));

            _backInput = new VirtualButton();
            _backInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.L));
            _backInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.B));

            _leftInput = new VirtualButton();
            _leftInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.A));
            _leftInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.Left));
            _leftInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.DPadLeft));
            _leftInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.LeftThumbstickLeft));

            _rightInput = new VirtualButton();
            _rightInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.D));
            _rightInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.Right));
            _rightInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.DPadRight));
            _rightInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.LeftThumbstickRight));
        }

        public void PlayCard(Card<TEnum> card)
        {
            _selectedCard = card;
            var targets = Controller.MakeTargetables(this, _selectedCard.Action.Targeting);
            foreach(var target in targets)
            {
                _targetSelector.AddEntity(target);
            }
        }

        protected override void OnStartOfTurn()
        {
            _selectedCardIndex = 0;
            if (!_drawing)
            {
                _drawing = true;
                DrawCards();
                Core.schedule(_handSize * .25f, t => {
                    _drawing = false;
                    State = BattleState.DuringTurn;
                    Immune = false;
                });
            }
        }

        protected override void DuringTurn()
        {
            if(_selectedCard != null && _selectedTargets != null)
            {
                if(!EqualityComparer<TEnum>.Default.Equals(_animator.currentAnimation, _selectedCard.Action.Animation))
                {
                    _animator.play(_selectedCard.Action.Animation);
                    _animator.onAnimationCompletedEvent = (t) => {
                        _selectedCard.Action.Effect.Perform(this, _selectedTargets, Controller);
                        _animator.onAnimationCompletedEvent = null;
                        State = BattleState.EndOfTurn;
                    };
                }
            }
            if(_selectedCard == null)
            {
                if (_rightInput.isPressed)
                {
                    _cardSelector.MoveNext();
                }
                else if (_leftInput.isPressed)
                {
                    _cardSelector.MoveBack();
                }
                else if (_selectInput.isPressed)
                {
                    _cardSelector.SelectHoveredEntity();
                }
            }
            else
            {
                if (_rightInput.isPressed)
                {
                    _targetSelector.MoveNext();
                }
                else if (_leftInput.isPressed)
                {
                    _targetSelector.MoveBack();
                }
                else if (_selectInput.isPressed)
                {
                    _targetSelector.SelectHoveredEntity();
                }
                else if (_backInput.isPressed)
                {
                    _selectedCard = null;
                    Controller.RemoveTargetables();
                    _targetSelector.Reset();
                }
            }
        }

        protected override void OnEndOfTurn()
        {
            _animator.play(_idleAnimation);
            Reset();
            State = BattleState.NotTurn;
        }

        private void Reset()
        {
            _discardPile.AddRange(_hand);
            _handEntities.ForEach(entity => entity.destroy());
            _handEntities = new List<Entity>();
            _hand = new List<Card<TEnum>>();
            _selectedCard = null;
            _selectedTargets = null;
            _cardSelector.Reset();
            _targetSelector.Reset();
        }

        private void DrawCards()
        {
            for (var i = 0; i < _handSize; i++)
            {
                var index = i;
                Core.schedule(i * .25f, t => DrawCard(index));
            }
        }

        private void DrawCard(int index)
        {
            if (!Deck.Any())
            {
                ShuffleDeck();
            }
            _hand.Add(Deck.First());
            CreateCardEntity(index, Deck.First());
            Deck.Remove(Deck.First());
        }

        private void CreateCardEntity(int index, Card<TEnum> card)
        {
            var cardEntity = entity.scene.createEntity("card " + index, new Vector2(transform.position.X + (125 * (index - 1)), transform.position.Y - 175));
            cardEntity.tag = EntityTags.CARDCLICKER_TAG;

            var sprite = new Sprite(_defaultCardTexture);
            sprite.origin = Vector2.Zero;
            sprite.setRenderLayer(100);
            cardEntity.addComponent(sprite);

            var nameText = new Text(CommonResources.DefaultBitmapFont, card.Action.Name, new Vector2(20, 7), Color.White);
            nameText.setRenderLayer(50);
            cardEntity.addComponent(nameText);

            var cardText = new Text(CommonResources.DefaultBitmapFont, card.Action.Effect.FormatDescription(), new Vector2(20, 40), Color.White);
            cardText.setRenderLayer(50);
            cardEntity.addComponent(cardText);

            var buyValueText = new Text(CommonResources.DefaultBitmapFont, card.BuyValue.ToString(), new Vector2(97, 107), Color.White);
            buyValueText.setRenderLayer(50);
            cardEntity.addComponent(buyValueText);

            cardEntity.addComponent(card);
            cardEntity.addCollider(new BoxCollider(new Rectangle(10, 0, 100, 125)));

            _handEntities.Add(cardEntity);
            _cardSelector.AddEntity(cardEntity);
        }

        private void ShuffleDeck()
        {
            var deckList = _discardPile.Concat(Deck).ToArray();
            deckList.shuffle();
            Deck = deckList.ToList();
            _discardPile = new List<Card<TEnum>>();
        }

        private void OnCardFocus(Entity entity)
        {
            entity.getComponent<Sprite>().subtexture = new Subtexture(_hoverCardTexture);
        }

        private void OnCardBlur(Entity entity)
        {
            entity.getComponent<Sprite>().subtexture = new Subtexture(_defaultCardTexture);
        }

        private void OnCardSelect(Entity entity)
        {
            var card = entity.getComponent<Card<TEnum>>();
            PlayCard(card);
        }

        private void OnTargetFocus(Entity entity)
        {
            var simplePolygon = entity.getComponent<TargetPolygon>();
            simplePolygon.setColor(Color.Red);
        }

        private void OnTargetBlur(Entity entity)
        {
            var simplePolygon = entity.getComponent<TargetPolygon>();
            simplePolygon.setColor(Color.LightPink);
        }

        private void OnTargetSelect(Entity entity)
        {
            var battleActor = entity.getComponent<BattleActor>();
            Controller.CurrentActor.SelectTarget(battleActor);
        }
    }
}
