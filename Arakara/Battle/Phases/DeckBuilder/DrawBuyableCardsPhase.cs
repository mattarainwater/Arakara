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
        public const int BUYABLE_TEMP_TAG = 1225;

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
            _backdrop = Actor.entity.scene.createEntity("background", new Vector2(DimensionConstants.SCREEN_WIDTH / 4, DimensionConstants.SCREEN_HEIGHT / 4));
            _backdrop.setTag(BUYABLE_TEMP_TAG);
            var sprite = _backdrop.addComponent(new Sprite(_deckBuilderActor.BackdropTexture));
            DrawBuyPoints();
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

        private void DrawBuyPoints()
        {
            var buyableText = Actor.entity.scene.createEntity("buyableText", new Vector2(DimensionConstants.SCREEN_WIDTH / 4, DimensionConstants.SCREEN_HEIGHT / 4));
            buyableText.setTag(BUYABLE_TEMP_TAG);
            var buyableTextComponent = new Text(CommonResources.DefaultBitmapFont, GetBuyablePoints(), new Vector2(10, 10), Color.White);
            buyableText.addComponent(buyableTextComponent);
        }

        private string GetBuyablePoints()
        {
            var template = "Buy Points: {0}";
            var cardsInHand = _deckBuilderActor.Hand.Where(x => x != _deckBuilderActor.SelectedCard);
            _deckBuilderActor.BuyPoints = cardsInHand.Sum(x => x.BuyValue);
            return string.Format(template, _deckBuilderActor.BuyPoints);
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
            var drawnCard = _deckBuilderActor.BuyableDeck.First();
            _deckBuilderActor.BuyableHand.Add(drawnCard);
            CreateCardEntity(index, drawnCard);
            _deckBuilderActor.BuyableDeck.Remove(drawnCard);
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
