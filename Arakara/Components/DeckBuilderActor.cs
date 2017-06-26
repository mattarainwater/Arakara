using Arakara.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using Nez;
using Microsoft.Xna.Framework;
using Arakara.Common;
using Arakara.Battle.Card;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;

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
        }

        public void PlayCard(Card<TEnum> card)
        {
            _selectedCard = card;
            Controller.MakeTargetables(this, _selectedCard.Action.Targeting);
        }

        protected override void OnStartOfTurn()
        {
            if(!_drawing)
            {
                _drawing = true;
                DrawCards();
                Core.schedule(_handSize * .25f, t => {
                    _drawing = false;
                    State = BattleState.DuringTurn;
                    Immune = false;
                    var firstCardEntity = _handEntities.First();
                    var cardSelector = firstCardEntity.getComponent<CardSelector<TEnum>>();
                    cardSelector.Selected = true;
                    PlayCard(cardSelector.Card);
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

            cardEntity.addComponent(new CardSelector<TEnum>(card, _defaultCardTexture, _hoverCardTexture));
            cardEntity.addCollider(new BoxCollider(new Rectangle(10, 0, 100, 125)));

            _handEntities.Add(cardEntity);
        }

        private void ShuffleDeck()
        {
            var deckList = _discardPile.Concat(Deck).ToArray();
            deckList.shuffle();
            Deck = deckList.ToList();
            _discardPile = new List<Card<TEnum>>();
        }
    }
}
