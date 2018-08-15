using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.BattleEngine.Actions
{
    public class ChangeTurnAction : GameAction
    {
        public int TargetActorIndex { get; set; }

        public ChangeTurnAction(int targetActorIndex)
        {
            TargetActorIndex = targetActorIndex;
        }

        public override string GetLog()
        {
            return $"Change turn, now {TargetActorIndex} turn";
        }
    }
}
