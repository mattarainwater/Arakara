using Arakara.Components;
using System;

namespace Arakara.Battle.Events.Triggers
{
    public class OnDeathTrigger : BattleEventTrigger
    {
        public BattleActor Target { get; set; }

        public OnDeathTrigger(BattleActor target)
        {
            Target = target;
        }

        public override bool IsTriggered(BattleController controller)
        {
            return Target.CurrentHP <= 0;
        }
    }
}
