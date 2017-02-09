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
    public class Targetable : Component, IUpdatable
    {
        private SimplePolygon _simplePolygon;
        private BattleActor _target;

        public bool Selected { get; set; }

        public Targetable(BattleActor target, SimplePolygon polygon)
        {
            _target = target;
            _simplePolygon = polygon;
        }

        public void update()
        {
            var mainColor = Color.LightPink;
            var hoverColor = Color.Red;

            var mousePosition = entity.scene.camera.screenToWorldPoint(Input.mousePosition);
            if (!Selected)
            {
                if (entity.colliders.mainCollider.bounds.contains(mousePosition))
                {
                    if (Input.leftMouseButtonReleased)
                    {
                        var targetables = entity.scene.findEntitiesWithTag(EntityTags.TARGETABLE_TAG);
                        foreach (var targetableEntity in targetables)
                        {
                            var targetable = targetableEntity.getComponent<Targetable>();
                            targetable.Selected = false;
                            targetable._simplePolygon.changeColor(mainColor);
                        }

                        Selected = true;
                        _target.Controller.CurrentActor.SelectTarget(_target);
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
    }
}
