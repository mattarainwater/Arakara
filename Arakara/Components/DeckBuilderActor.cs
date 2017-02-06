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
        private List<Entity> _targetableEntities;

        private Card _selectedCard;

        private int _handSize = 3;

        public DeckBuilderActor(string name, int maxHP, Faction faction, List<Card> cards) :
            base(name, maxHP, faction)
        {
            _deck = cards;
            _hand = new List<Card>();
            _discardPile = new List<Card>();
            _handEntities = new List<Entity>();
            _targetableEntities = new List<Entity>();

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
                MakeTargetables(controller.Actors);
                State = BattleState.Targeting;
            }
        }

        protected override void OnTargeting(BattleController controller)
        {
            if(_selectedTarget != null)
            {
                switch (_selectedCard.Effect)
                {
                    case Effect.Attack:
                        _selectedTarget.CurrentHP -= _selectedCard.Magnitude;
                        break;
                    case Effect.Defense:
                        Immune = true;
                        break;
                    case Effect.Heal:
                        _selectedTarget.CurrentHP += _selectedCard.Magnitude;
                        if (_selectedTarget.CurrentHP > _selectedTarget.MaxHP)
                        {
                            _selectedTarget.CurrentHP = _selectedTarget.MaxHP;
                        }
                        break;
                    default:
                        break;
                }
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
                if(card.Grade == Grade.Bronze)
                {
                    card.Grade = Grade.Silver;
                    card.Magnitude = (int)(card.Magnitude * 1.5);
                }
                else if(card.Grade == Grade.Silver)
                {
                    card.Grade = Grade.Gold;
                    card.Magnitude = (int)(card.Magnitude * 1.5);
                }
            }
        }

        private void Reset()
        {
            Delay = _selectedCard.Delay;
            _discardPile.AddRange(_hand);
            _handEntities.ForEach(entity => entity.destroy());
            _handEntities = new List<Entity>();
            _targetableEntities.ForEach(entity => entity.destroy());
            _targetableEntities = new List<Entity>();
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
            var cardEntity = entity.scene.createEntity("card " + index, new Vector2(transform.position.X + (200 * (index - 1)), transform.position.Y - 250));
            cardEntity.tag = EntityTags.CARDCLICKER_TAG;
            var verts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(75, 0),
                new Vector2(75, 100),
                new Vector2(0, 100),
            };
            cardEntity.addComponent(new SimplePolygon(verts, Color.Black));
            cardEntity.addComponent(new Text(Graphics.instance.bitmapFont, card.Name, new Vector2(5, 5), Color.White));
            cardEntity.addComponent(new Text(Graphics.instance.bitmapFont, card.Text, new Vector2(5, 30), Color.White));
            cardEntity.addComponent(new Text(Graphics.instance.bitmapFont, "Delay: " + card.Delay, new Vector2(5, 60), Color.White));
            cardEntity.addComponent(new CardClicker(card, this));
            cardEntity.addCollider(new BoxCollider(new Rectangle(0, 0, 75, 100)));
            _handEntities.Add(cardEntity);
        }

        private void MakeTargetables(List<BattleActor> actors)
        {
            _targetableEntities.ForEach(entity => entity.destroy());
            _targetableEntities = new List<Entity>();
            var targetableActors = GetTargetableActors(_selectedCard.Targeting, actors);
            for (var i = 0; i < targetableActors.Count(); i++)
            {
                var actor = targetableActors[i];
                var actorEntity = actor.entity;
                var targetableEntity = entity.scene.createEntity("target " + i, new Vector2(actorEntity.transform.position.X, actorEntity.transform.position.Y - 25));
                targetableEntity.tag = EntityTags.TARGETABLE_TAG;
                var verts = new Vector2[3]
                {
                    new Vector2(65, 0),
                    new Vector2(85, 0),
                    new Vector2(75, 10),
                };
                targetableEntity.addComponent(new SimplePolygon(verts, Color.LightPink));
                targetableEntity.addCollider(new BoxCollider(new Rectangle(0, 25, 150, 200)));
                targetableEntity.addComponent(new Targetable(actor, this));
                _targetableEntities.Add(targetableEntity);
            }
        }

        private List<BattleActor> GetTargetableActors(Targeting targeting, List<BattleActor> actors)
        {
            switch (targeting)
            {
                case Targeting.Allies:
                    return actors.Where(x => x.Faction.Id == Faction.Id).ToList();
                case Targeting.Enemies:
                    return actors.Where(x => x.Faction.Id != Faction.Id).ToList();
                case Targeting.Self:
                    return actors.Where(x => x == this).ToList();
                default:
                    return null;
            }
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
