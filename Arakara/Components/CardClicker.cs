using Arakara.Battle;
using Arakara.Battle.Card;
using Arakara.Common;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class CardClicker : Component, IUpdatable
    {
        private SimplePolygon _simplePolygon;
        private Card _card;
        private DeckBuilderActor _actor;

        public bool Selected { get; set; }

        public CardClicker(Card card, DeckBuilderActor actor)
        {
            _card = card;
            _actor = actor;
        }

        public override void onAddedToEntity()
        {
            _simplePolygon = entity.getComponent<SimplePolygon>();
        }

        public void update()
        {
            var mainColor = GetMainColor(_card.Grade);
            var hoverColor = GetSmartShadeColorByBase(mainColor, .25f);
            var selectedColor = GetSmartShadeColorByBase(mainColor, -.25f);

            var mousePosition = entity.scene.camera.screenToWorldPoint(Input.mousePosition);
            if(!Selected)
            {
                if (entity.colliders.mainCollider.bounds.contains(mousePosition))
                {
                    if (Input.leftMouseButtonReleased)
                    {
                        var cards = entity.scene.findEntitiesWithTag(EntityTags.CARDCLICKER_TAG);
                        foreach(var card in cards)
                        {
                            var clicker = card.getComponent<CardClicker>();
                            clicker.Selected = false;
                            var mainColorForRest = GetMainColor(clicker._card.Grade);
                            clicker._simplePolygon.changeColor(mainColorForRest);
                        }

                        Selected = true;
                        _simplePolygon.changeColor(selectedColor);
                        _actor.PlayCard(_card);
                    }
                    else
                    {
                        _simplePolygon.changeColor(hoverColor);
                    }
                }
                else
                {
                    _simplePolygon.changeColor(mainColor);
                }
            }
        }

        private Color GetMainColor(Grade grade)
        {
            switch(grade)
            {
                case Grade.Bronze:
                    return new Color(144, 89, 35);
                case Grade.Silver:
                    return new Color(128, 128, 128);
                case Grade.Gold:
                    return new Color(212, 175, 55);
                default:
                    return Color.Black;
            }
        }

        private Color GetSmartShadeColorByBase(Color color, float percent)
        {
            var t = percent < 0 ? 0 : 255;
            var p = percent < 0 ? percent * -1 : percent;

            var newR = Convert.ToInt32(Math.Round((t - color.R) * p) + color.R);
            var newG = Convert.ToInt32(Math.Round((t - color.G) * p) + color.G);
            var newB = Convert.ToInt32(Math.Round((t - color.B) * p) + color.B);

            return new Color(newR, newG, newB);
        }

    }
}
