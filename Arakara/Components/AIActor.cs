using Arakara.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class AIActor : BattleActor
    {
        public AIActor(int maxHP, Faction faction) :
            base(maxHP, faction)
        {

        }

        public override void update()
        {
        }
    }
}
