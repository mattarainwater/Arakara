using Arakara.Battle;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class ProgrammedAction : Component
    {
        public List<BattleAction> TieredActions { get; set; }

        public ProgrammedAction(List<BattleAction> tieredActions)
        {
            TieredActions = tieredActions;
        }
    }
}
