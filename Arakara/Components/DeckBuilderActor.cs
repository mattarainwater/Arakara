using Arakara.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using Nez;
using Microsoft.Xna.Framework;
using Arakara.Common;

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

        public DeckBuilderActor(string name, int maxHP, Faction faction, List<Card> cards) :
            base(name, maxHP, faction)
        {
            _deck = cards;
            _hand = new List<Card>();
            _discardPile = new List<Card>();
            _handEntities = new List<Entity>();

            ShuffleDeck();
        }

        public void PlayCard(Card card)
        {
            _selectedCard = card;
            State = BattleState.AwaitingDecision;
        }

        protected override void OnStartOfTurn(BattleController controller)
        {
            DrawCards();
            State = BattleState.AwaitingDecision;
            Immune = false;
        }

        protected override void OnAwaitingDecision(BattleController controller)
        {
            if(_selectedCard != null)
            {
                controller.MakeTargetables(this, _selectedCard.Action.Targeting);
                State = BattleState.Targeting;
            }
        }

        protected override void OnTargeting(BattleController controller)
        {
            if(_selectedTarget != null)
            {
                _selectedCard.Action.Effect.Perform(this, _selectedTarget, controller);
                State = BattleState.EndOfTurn;
            }
        }

        protected override void OnEndOfTurn(BattleController controller)
        {
            Reset();
            State = BattleState.NotTurn;
        }

        private void UpgradeCards()
        {
            var nonSelectedCards = _hand.Where(x => x == _selectedCard);
            foreach(var card in nonSelectedCards)
            {
                var newDamage = (int)(((DamageEffect)card.Action.Effect).Damage * 1.5);
                if (card.Grade == Grade.Bronze)
                {
                    card.Grade = Grade.Silver;
                    ((DamageEffect)card.Action.Effect).Damage = newDamage;
                }
                else if(card.Grade == Grade.Silver)
                {
                    card.Grade = Grade.Gold;
                    ((DamageEffect)card.Action.Effect).Damage = newDamage;
                }
            }
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
            _selectedTarget = null;
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
            var cardEntity = entity.scene.createEntity("card " + index, new Vector2(transform.position.X + (100 * (index - 1)), transform.position.Y - 125));
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

            for (var i = 0; i < deckList.Length; i++)
            {
                var j = Nez.Random.range(0, i);
                var entryI = deckList[i];
                var entryJ = deckList[j];
                deckList[i] = entryJ;
                deckList[j] = entryI;
            }

            _deck = deckList.ToList();
            _discardPile = new List<Card>();
        }
    }
}
