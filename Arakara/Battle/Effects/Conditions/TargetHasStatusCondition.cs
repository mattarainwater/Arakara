using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle.Effects.Conditions
{
    public class TargetHasStatusCondition : Condition
    {
        public string Status { get; set; }

        public TargetHasStatusCondition(string status)
        {
            Status = status;
        }

        public override bool IsMet(BattleActor actor, BattleActor target, BattleController controller)
        {
            return target.Statuses.Contains(Status);
        }

        public override string GetDescription()
        {
            return "target has " + Status;
        }
    }
}
