using Arakara.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using Nez;
using Microsoft.Xna.Framework;
using Arakara.Common;
using Arakara.Battle.Card;

namespace Arakara.Components
{
    public class DeckBuilderActor : BattleActor
    {
        private List<Card> _deck;
        private List<Card> _hand;
        private List<Card> _discardPile;
        private List<Entity> _handEntities;
        private Card _selectedCard;
        private int _handSize = 3;
        private CardUpgrader _cardUpgrader;

        public DeckBuilderActor(string name, int maxHP, Faction faction, List<Card> cards, float dodgeChance, float critChance) :
            base(name, maxHP, faction, dodgeChance, critChance)
        {
            _deck = cards;
            _hand = new List<Card>();
            _discardPile = new List<Card>();
            _handEntities = new List<Entity>();

            ShuffleDeck();

            _cardUpgrader = new CardUpgrader();
        }

        public void PlayCard(Card card)
        {
            _selectedCard = card;
            Controller.MakeTargetables(this, _selectedCard.Action.Targeting);
        }

        protected override void OnStartOfTurn()
        {
            DrawCards();
            State = BattleState.DuringTurn;
            Immune = false;
        }

        protected override void DuringTurn()
        {
            if(_selectedCard != null && _selectedTargets != null)
            {
                _selectedCard.Action.Effect.Perform(this, _selectedTargets, Controller);
                State = BattleState.EndOfTurn;
            }
        }

        protected override void OnEndOfTurn()
        {
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
            Delay = _selectedCard.Action.Speed;
            _discardPile.AddRange(_hand);
            _handEntities.ForEach(entity => entity.destroy());
            _handEntities = new List<Entity>();
            UpgradeCards();
            _hand = new List<Card>();
            _selectedCard = null;
            _selectedTargets = null;
        }

        private void DrawCards()
        {
            for (var i = 0; i < _handSize; i++)
            {
                if (!_deck.Any())
                {
                    ShuffleDeck();
                }
                _hand.Add(_deck.First());
                CreateCardEntity(i, _deck.First());
                _deck.Remove(_deck.First());
            }
        }

        private void CreateCardEntity(int index, Card card)
        {
            var cardEntity = entity.scene.createEntity("card " + index, new Vector2(transform.position.X + (100 * (index - 1)), transform.position.Y - 200));
            cardEntity.tag = EntityTags.CARDCLICKER_TAG;
            var verts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(75, 0),
                new Vector2(75, 100),
                new Vector2(0, 100),
            };
            cardEntity.addComponent(new SimplePolygon(verts, Color.Black));
            cardEntity.addComponent(new Text(Graphics.instance.bitmapFont, card.Action.Name, new Vector2(5, 5), Color.White));
            cardEntity.addComponent(new Text(Graphics.instance.bitmapFont, card.Action.Effect.GetDescription(), new Vector2(5, 30), Color.White));
            cardEntity.addComponent(new Text(Graphics.instance.bitmapFont, "Delay: " + card.Action.Speed, new Vector2(5, 60), Color.White));
            cardEntity.addComponent(new CardClicker(card, this));
            cardEntity.addCollider(new BoxCollider(new Rectangle(0, 0, 75, 100)));
            _handEntities.Add(cardEntity);
        }

        private void ShuffleDeck()
        {
            var deckList = _discardPile.Concat(_deck).ToArray();
            deckList.shuffle();
            _deck = deckList.ToList();
            _discardPile = new List<Card>();
        }
    }
}
