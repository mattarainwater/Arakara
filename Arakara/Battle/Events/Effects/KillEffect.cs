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
                Controller.CurrentActorIndex--;
            }
            Target.Entity.Destroy();
            State = BattleEventEffectState.EndOfEventEffect;
        }

        protected override void OnEndOfEvent()
        {
            State = BattleEventEffectState.Done;
        }

        protected override void OnStartOfEvent()
        {
            State = BattleEventEffectState.DuringEventEffect;
        }
    }
}
