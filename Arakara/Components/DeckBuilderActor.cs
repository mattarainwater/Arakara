using Arakara.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using Nez;
using Microsoft.Xna.Framework;

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

        public DeckBuilderActor(int maxHP, Faction faction) :
            base(maxHP, faction)
        {
            _deck = new List<Card>()
            {
                new Card
                {
                    Delay =  5,
                    Effect = Effect.Attack,
                    Name = "Quick Slash",
                    Magnitude = 5,
                    Targeting = Targeting.Enemies,
                    Text = "Deal 5 damage"
                },
                new Card
                {
                    Delay =  5,
                    Effect = Effect.Attack,
                    Name = "Quick Slash",
                    Magnitude = 5,
                    Targeting = Targeting.Enemies,
                    Text = "Deal 5 damage"
                },
                new Card
                {
                    Delay =  5,
                    Effect = Effect.Attack,
                    Name = "Quick Slash",
                    Magnitude = 5,
                    Targeting = Targeting.Enemies,
                    Text = "Deal 5 damage"
                },
                new Card
                {
                    Delay =  5,
                    Effect = Effect.Attack,
                    Name = "Quick Slash",
                    Magnitude = 5,
                    Targeting = Targeting.Enemies,
                    Text = "Deal 5 damage"
                },
                new Card
                {
                    Delay =  10,
                    Effect = Effect.Attack,
                    Name = "Heavy Slash",
                    Magnitude = 10,
                    Targeting = Targeting.Enemies,
                    Text = "Deal 10 damage"
                },
                new Card
                {
                    Delay =  10,
                    Effect = Effect.Attack,
                    Name = "Heavy Slash",
                    Magnitude = 10,
                    Targeting = Targeting.Enemies,
                    Text = "Deal 10 damage"
                },
                new Card
                {
                    Delay =  20,
                    Effect = Effect.Heal,
                    Name = "Pocket Potion",
                    Magnitude = 20,
                    Targeting = Targeting.Self,
                    Text = "Heal 20 HP"
                },
                new Card
                {
                    Delay =  20,
                    Effect = Effect.Heal,
                    Name = "Pocket Potion",
                    Magnitude = 20,
                    Targeting = Targeting.Self,
                    Text = "Heal 20 HP"
                },
                new Card
                {

                    Delay =  5,
                    Effect = Effect.Defense,
                    Name = "Evade",
                    Magnitude = 10,
                    Targeting = Targeting.Self,
                    Text = "Evade until next turn"
                },
            };
            _hand = new List<Card>();
            _discardPile = new List<Card>();
            _handEntities = new List<Entity>();

            ShuffleDeck();
        }

        public override void update()
        {
            if(State == BattleState.StartOfTurn)
            {
                DrawCards();
                State = BattleState.AwaitingDecision;
            }
            if(State == BattleState.Targeting)
            {
                var i = 0;
            }
            if(State == BattleState.EndOfTurn)
            {
                Delay = _selectedCard.Delay;
                _discardPile.AddRange(_hand);
                _hand = new List<Card>();
            }
        }

        public void PlayCard(Card card)
        {
            _selectedCard = card;
            State = BattleState.Targeting;
        }

        private void DrawCards()
        {
            for(var i = 0; i < _handSize; i++)
            {
                if (!_deck.Any())
                {
                    ShuffleDeck();
                }
                _hand.Add(_deck.First());
                _deck.Remove(_deck.First());
                CreateCardEntity(i, _deck.First());
            }
        }

        private void CreateCardEntity(int index, Card card)
        {
            var cardEntity = entity.scene.createEntity("card " + index, new Vector2(transform.position.X + (200 * (index - 1)), transform.position.Y - 250));
            cardEntity.tag = 5;
            var verts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(150, 0),
                new Vector2(150, 200),
                new Vector2(0, 200),
            };
            cardEntity.addComponent(new SimplePolygon(verts, Color.Black));
            cardEntity.addComponent(new Text(Graphics.instance.bitmapFont, card.Name, new Vector2(5, 5), Color.White));
            cardEntity.addComponent(new Text(Graphics.instance.bitmapFont, card.Text, new Vector2(5, 30), Color.White));
            cardEntity.addComponent(new CardClicker(card));
            cardEntity.addCollider(new BoxCollider(new Rectangle(0, 0, 150, 200)));
            _handEntities.Add(cardEntity);
        }

        private void ShuffleDeck()
        {
            var deckList = _discardPile.Concat(_deck).ToArray();

            for(var i = 0; i < deckList.Length; i++)
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
