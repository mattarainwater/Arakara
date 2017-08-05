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

namespace Arakara.Battle.Phases.DeckBuilder
{
    public class DrawCardsPhase : Phase
    {
        private bool _drawing;
        private int _handSize;
        private DeckBuilderActor _deckBuilderActor;

        public DrawCardsPhase(DeckBuilderActor actor)
            : base(actor)
        {
            _handSize = 3;
            _deckBuilderActor = actor;
            _drawing = false;
        }

        public override void Update()
        {
            if (!_drawing && !IsFinished)
            {
                _drawing = true;
                DrawCards();
                Core.schedule(_handSize * .25f, t => {
                    _drawing = false;
                    IsFinished = true;
                });
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
            if (!_deckBuilderActor.Deck.Any())
            {
                ShuffleDeck();
            }
            _deckBuilderActor.Hand.Add(_deckBuilderActor.Deck.First());
            CreateCardEntity(index, _deckBuilderActor.Deck.First());
            _deckBuilderActor.Deck.Remove(_deckBuilderActor.Deck.First());
        }

        private void CreateCardEntity(int index, Card<Animations> card)
        {
            var cardEntity = Actor.entity.scene.createEntity("card " + index, new Vector2(Actor.transform.position.X + (125 * (index - 1)), Actor.transform.position.Y - 175));
            cardEntity.tag = EntityTags.CARDCLICKER_TAG;

            var sprite = new Sprite(_deckBuilderActor.DefaultCardTexture);
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

            _deckBuilderActor.HandEntities.Add(cardEntity);
            _deckBuilderActor.CardSelector.AddEntity(cardEntity);
        }

        private void ShuffleDeck()
        {
            var deckList = _deckBuilderActor.DiscardPile.Concat(_deckBuilderActor.Deck).ToArray();
            deckList.shuffle();
            _deckBuilderActor.Deck = deckList.ToList();
            _deckBuilderActor.DiscardPile = new List<Card<Animations>>();
        }
    }
}
