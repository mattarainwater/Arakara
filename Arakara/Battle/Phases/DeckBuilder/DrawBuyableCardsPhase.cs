using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Nez;
using Microsoft.Xna.Framework;
using Arakara.Common;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Arakara.Factories.Entities;

namespace Arakara.Battle.Phases.DeckBuilder
{
    public class DrawBuyableCardsPhase : Phase
    {
        private bool _drawing;
        private int _handSize;
        private DeckBuilderActor _deckBuilderActor;
        private Entity _backdrop;
        private CardEntityFactory _factory;

        public DrawBuyableCardsPhase(DeckBuilderActor actor) 
            : base(actor)
        {
            _handSize = 3;
            _deckBuilderActor = actor;
            _drawing = false;
            _factory = new CardEntityFactory();
        }

        protected override void initialize()
        {
            _backdrop = Actor.entity.scene.createEntity("backdrop", new Vector2(DimensionConstants.SCREEN_WIDTH / 4, DimensionConstants.SCREEN_HEIGHT / 4));
            var sprite = _backdrop.addComponent(new Sprite(_deckBuilderActor.BackdropTexture));
            sprite.renderLayer = 4;
            sprite.setOrigin(Vector2.Zero);
            _drawing = true;
            DrawCards();
            Core.schedule(_handSize * .25f, t => {
                _drawing = false;
            });
        }

        protected override void update()
        {
            if (!_drawing)
            {
                IsFinished = true;
            }
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
            if (!_deckBuilderActor.BuyableDeck.Any())
            {
                ShuffleDeck();
            }
            _deckBuilderActor.BuyableHand.Add(_deckBuilderActor.BuyableDeck.First());
            CreateCardEntity(index, _deckBuilderActor.BuyableDeck.First());
            _deckBuilderActor.BuyableDeck.Remove(_deckBuilderActor.BuyableDeck.First());
        }

        private void CreateCardEntity(int index, Card card)
        {
            var cardPos = new Vector2(_backdrop.transform.position.X + (175 * index), _backdrop.transform.position.Y + 50);
            var cardEntity = _factory.GetCardEntity(card, cardPos, _deckBuilderActor.DefaultCardTexture);
            _deckBuilderActor.BuyableHandEntities.Add(cardEntity);
        }

        private void ShuffleDeck()
        {
            var deckList = _deckBuilderActor.BuyableDiscardPile.Concat(_deckBuilderActor.BuyableDeck).ToArray();
            deckList.shuffle();
            _deckBuilderActor.BuyableDeck = deckList.ToList();
            _deckBuilderActor.BuyableDiscardPile = new List<Card>();
        }
    }
}
