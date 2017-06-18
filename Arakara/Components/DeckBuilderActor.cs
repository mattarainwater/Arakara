using Arakara.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using Nez;
using Microsoft.Xna.Framework;
using Arakara.Common;
using Arakara.Battle.Card;
using Nez.Sprites;

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
        private CardUpgrader<TEnum> _cardUpgrader;
        private bool _drawing;
        private Sprite<TEnum> _animator;
        private TEnum _idleAnimation;

        public DeckBuilderActor(string name, int maxHP, Faction faction, List<Card<TEnum>> cards, float dodgeChance, float critChance, float speed, TEnum idleAnimation) :
            base(name, maxHP, faction, dodgeChance, critChance, speed)
        {
            Deck = cards;
            _hand = new List<Card<TEnum>>();
            _discardPile = new List<Card<TEnum>>();
            _handEntities = new List<Entity>();

            ShuffleDeck();

            _cardUpgrader = new CardUpgrader<TEnum>();
            _idleAnimation = idleAnimation;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            _animator = entity.getComponent<Sprite<TEnum>>();
            _animator.play(_idleAnimation);
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

        private void UpgradeCards()
        {
            var nonSelectedCards = _hand.Where(x => x == _selectedCard);
            _cardUpgrader.UpgradeCards(nonSelectedCards);
        }

        private void Reset()
        {
            _discardPile.AddRange(_hand);
            _handEntities.ForEach(entity => entity.destroy());
            _handEntities = new List<Entity>();
            UpgradeCards();
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
            var verts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(100, 0),
                new Vector2(100, 100),
                new Vector2(0, 100),
            };
            cardEntity.addComponent(new SimplePolygon(verts, Color.Black));
            cardEntity.addComponent(new Text(Graphics.instance.bitmapFont, card.Action.Name, new Vector2(5, 5), Color.White));
            cardEntity.addComponent(new Text(Graphics.instance.bitmapFont, card.Action.Effect.FormatDescription(), new Vector2(5, 30), Color.White));
            cardEntity.addComponent(new CardClicker<TEnum>(card, this));
            cardEntity.addCollider(new BoxCollider(new Rectangle(0, 0, 100, 100)));
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
