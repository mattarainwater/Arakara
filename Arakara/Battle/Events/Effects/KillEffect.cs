using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Events.Effects
{
    public class KillEffect : BattleEventEffect
    {
        public BattleActor Target { get; set; }

        public KillEffect(int sequence, BattleActor target) 
            : base(sequence)
        {
            Target = target;
        }

        protected override void DuringEvent()
        {
            Controller.Actors.Remove(Target);
            if (Controller.CurrentActor == Target)
            {
                Controller.CurrentActor = null;
            }
            Target.entity.destroy();
        }

        protected override void OnEndOfEvent()
        {
        }

        protected override void OnStartOfEvent()
        {
        }
    }
}
