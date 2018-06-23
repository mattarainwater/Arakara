using Arakara.Battle;
using Arakara.Common;
using Arakara.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Factories.Entities
{
    public class CardEntityFactory
    {
        private Texture2D _cardSprite;

        public CardEntityFactory(Texture2D cardSprite)
        {
            _cardSprite = cardSprite;
        }

        public Entity GetCardEntity(Card card, Vector2 position, int layer)
        {
            var cardEntity = Core.scene.createEntity("card", position);

            var sprite = new Sprite(_cardSprite);
            sprite.origin = Vector2.Zero;
            sprite.setRenderLayer(layer);
            cardEntity.addComponent(sprite);

            var nameText = new Text(CommonResources.DefaultBitmapFont, card.Action.Name, new Vector2(20, 7), Color.White);
            nameText.setRenderLayer(layer - 1);
            cardEntity.addComponent(nameText);

            var costText = new Text(CommonResources.DefaultBitmapFont, card.Cost.ToString(), new Vector2(97, 7), Color.White);
            costText.setRenderLayer(layer - 1);
            cardEntity.addComponent(costText);

            var cardText = new Text(CommonResources.DefaultBitmapFont, card.Action.Effect.FormatDescription(), new Vector2(20, 40), Color.White);
            cardText.setRenderLayer(layer - 1);
            cardEntity.addComponent(cardText);

            var buyValueText = new Text(CommonResources.DefaultBitmapFont, card.BuyValue.ToString(), new Vector2(97, 107), Color.White);
            buyValueText.setRenderLayer(layer - 1);
            cardEntity.addComponent(buyValueText);

            cardEntity.addComponent(card);

            return cardEntity;
        }
    }
}
