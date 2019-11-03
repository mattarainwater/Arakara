using Arakara.Battle;
using Arakara.Common;
using Arakara.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
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
            var cardEntity = Core.Scene.CreateEntity("card", position);

            var sprite = new SpriteRenderer(_cardSprite);
            sprite.Origin = Vector2.Zero;
            sprite.SetRenderLayer(layer);
            cardEntity.AddComponent(sprite);

            var nameText = new TextComponent(CommonResources.DefaultBitmapFont, card.Action.Name, new Vector2(20, 7), Color.White);
            nameText.SetRenderLayer(layer - 1);
            cardEntity.AddComponent(nameText);

            var costText = new TextComponent(CommonResources.DefaultBitmapFont, card.Cost.ToString(), new Vector2(97, 7), Color.White);
            costText.SetRenderLayer(layer - 1);
            cardEntity.AddComponent(costText);

            var cardText = new TextComponent(CommonResources.DefaultBitmapFont, card.Action.Effect.FormatDescription(), new Vector2(20, 40), Color.White);
            cardText.SetRenderLayer(layer - 1);
            cardEntity.AddComponent(cardText);

            var buyValueText = new TextComponent(CommonResources.DefaultBitmapFont, card.BuyValue.ToString(), new Vector2(97, 107), Color.White);
            buyValueText.SetRenderLayer(layer - 1);
            cardEntity.AddComponent(buyValueText);

            cardEntity.AddComponent(card);

            return cardEntity;
        }
    }
}
