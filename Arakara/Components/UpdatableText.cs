using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Arakara.Components
{
    public class UpdatableText : Text, IUpdatable
    {
        public UpdatableText(IFont font, Vector2 position, Color color) 
            : base(font, "", position, color)
        {
        }

        public void update()
        {
            var actor = entity.getComponent<BattleActor>();
            setText("Current HP: " + actor.CurrentHP);
        }
    }
}
