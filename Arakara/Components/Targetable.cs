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
        public BattleActor Target { get; set; }
        public bool Selected { get; set; }
        public bool Visible { get; set; }

        private SimplePolygon _simplePolygon;

        public Targetable(BattleActor target, SimplePolygon polygon)
        {
            Target = target;
            _simplePolygon = polygon;
        }

        public void update()
        {
            var mainColor = Color.LightPink;
            var hoverColor = Color.Red;
            if (!Selected)
            {
                _simplePolygon.setColor(mainColor);
            }
            else
            {
                _simplePolygon.setColor(hoverColor);
            }
        }
    }
}
