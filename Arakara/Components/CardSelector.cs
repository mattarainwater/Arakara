using Arakara.Battle;
using Arakara.Battle.Card;
using Arakara.Common;
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

namespace Arakara.Components
{
    public class CardSelector<TEnum> : Component, IUpdatable where TEnum : struct, IComparable, IFormattable
    {
        private Texture2D _defaultCard;
        private Texture2D _hoverCard;
        private Sprite _sprite;

        public bool Selected { get; set; }
        public Card<TEnum> Card { get; set; }

        public CardSelector(Card<TEnum> card, Texture2D defaultCard, Texture2D hoverCard)
        {
            Card = card;
            _defaultCard = defaultCard;
            _hoverCard = hoverCard;
        }

        public override void onAddedToEntity()
        {
            _sprite = entity.getComponent<Sprite>();
        }

        public void update()
        {
            if (!Selected)
            {
                _sprite.subtexture = new Subtexture(_defaultCard);
            }
            else
            {
                _sprite.subtexture = new Subtexture(_hoverCard);
            }
        }
    }
}
