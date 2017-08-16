using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Nez;
using Arakara.Common;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using Arakara.Factories.Entities;

namespace Arakara.Battle.Phases.DeckBuilder
{
    public class DrawCardsPhase : Phase
    {
        private bool _drawing;
        private int _handSize;
        private DeckBuilderActor _deckBuilderActor;
        private CardEntityFactory _factory;

        public DrawCardsPhase(DeckBuilderActor actor)
            : base(actor)
        {
            _handSize = 3;
            _deckBuilderActor = actor;
            _drawing = false;
            _factory = new CardEntityFactory();
        }

        protected override void initialize()
        {
            var cursed = _deckBuilderActor.Statuses.Contains("Curse");
            var handSize = cursed ? _handSize - 1 : _handSize;
            _drawing = true;
            DrawCards(handSize);
            Core.schedule(handSize * .25f, t => {
                _drawing = false;
            });
        }

        protected override void update()
        {
            if(!_drawing)
            {
                IsFinished = true;
            }
        }

        private void DrawCards(int handSize)
        {
            for (var i = 0; i < handSize; i++)
            {
                var index = i;
                Core.schedule(i * .25f, t => DrawCard(index));
            }
        }

        private void DrawCard(int index)
        {
            if (!_deckBuilderActor.Deck.Any())
            {
                ShuffleDeck();
            }
            _deckBuilderActor.Hand.Add(_deckBuilderActor.Deck.First());
            CreateCardEntity(index, _deckBuilderActor.Deck.First());
            _deckBuilderActor.Deck.Remove(_deckBuilderActor.Deck.First());
        }

        private void CreateCardEntity(int index, Card card)
        {
            var cardPos = new Vector2(Actor.transform.position.X + (125 * (index - 1)), Actor.transform.position.Y - 175);
            var cardEntity = _factory.GetCardEntity(card, cardPos, _deckBuilderActor.DefaultCardTexture, 10);
            _deckBuilderActor.HandEntities.Add(cardEntity);
        }

        private void ShuffleDeck()
        {
            var deckList = _deckBuilderActor.DiscardPile.Concat(_deckBuilderActor.Deck).ToArray();
            deckList.shuffle();
            _deckBuilderActor.Deck = deckList.ToList();
            _deckBuilderActor.DiscardPile = new List<Card>();
        }
    }
}
