using Arakara.Battle;
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

        public bool Selected { get; set; }

        public CardClicker(Card card)
        {
            _card = card;
        }

        public override void onAddedToEntity()
        {
            _simplePolygon = entity.getComponent<SimplePolygon>();
        }

        public void update()
        {
            var mousePosition = Input.mousePosition;
            if(!Selected)
            {
                if (entity.colliders.mainCollider.bounds.contains(mousePosition))
                {
                    if (Input.leftMouseButtonReleased)
                    {
                        var cards = entity.scene.findEntitiesWithTag(5);
                        foreach(var card in cards)
                        {
                            var clicker = card.getComponent<CardClicker>();
                            clicker.Selected = false;
                            clicker._simplePolygon.changeColor(Color.Black);
                        }

                        Selected = true;
                        _simplePolygon.changeColor(Color.Blue);
                        var mc = entity.scene.entities.findEntity("mc");
                        var mcActor = mc.getComponent<DeckBuilderActor>();
                        mcActor.PlayCard(_card);
                    }
                    else if (Input.leftMouseButtonDown)
                    {
                        _simplePolygon.changeColor(Color.Green);
                    }
                    else
                    {
                        _simplePolygon.changeColor(Color.Red);
                    }
                }
                else
                {
                    _simplePolygon.changeColor(Color.Black);
                }
            }
        }
    }
}
