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
    public class HealthBar : Component, IUpdatable
    {
        public SimplePolygon CurrentHealthBar { get; set; }
        public SimplePolygon MaxHealthBar { get; set; }

        public BattleActor Actor { get; set; }
        public int LastCurrentHP { get; set; }

        public HealthBar(BattleActor actor)
        {
            Actor = actor;
            LastCurrentHP = -1;
        }

        public void update()
        {
            if(Actor.CurrentHP != LastCurrentHP)
            {
                var currentHealthBarEndPoint = (float)Actor.CurrentHP / (float)Actor.MaxHP * (float)DimensionConstants.CHARACTER_WIDTH;
                if(currentHealthBarEndPoint < 0)
                {
                    currentHealthBarEndPoint = 0;
                }

                if (CurrentHealthBar != null)
                {
                    entity.removeComponent(CurrentHealthBar);
                }
                var currentHealthBarVerts = new Vector2[4]
                {
                new Vector2(0, -15),
                new Vector2(currentHealthBarEndPoint, -15),
                new Vector2(currentHealthBarEndPoint, -5),
                new Vector2(0, -5),
                };
                CurrentHealthBar = new SimplePolygon(currentHealthBarVerts, Color.Green);
                entity.addComponent(CurrentHealthBar);


                var maxHealthBarVerts = new Vector2[4]
                {
                new Vector2(currentHealthBarEndPoint, -15),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, -15),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, -5),
                new Vector2(currentHealthBarEndPoint, -5),
                };
                MaxHealthBar = new SimplePolygon(maxHealthBarVerts, Color.Red);
                entity.addComponent(MaxHealthBar);
            }
            LastCurrentHP = Actor.CurrentHP;
        }
    }
}
