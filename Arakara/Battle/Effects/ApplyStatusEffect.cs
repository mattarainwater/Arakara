using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Arakara.Battle.Statuses;

namespace Arakara.Battle.Effects
{
    public class ApplyStatusEffect : ActionEffect
    {
        public BattleStatus Status { get; set; }

        public ApplyStatusEffect(BattleStatus status)
        {
            Status = status;
        }

        public override void Perform(BattleActor actor, List<BattleActor> targets, BattleController controller)
        {
            foreach(var target in targets)
            {
                target.Statuses.Add(Status, Status.Duration);
            }
        }

        public override string GetDescription()
        {
            return $"Apply {Status.GetDescription()}";
        }
    }
}
