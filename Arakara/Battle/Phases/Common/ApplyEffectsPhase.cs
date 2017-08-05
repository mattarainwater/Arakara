using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle.Phases.Common
{
    public class ApplyEffectsPhase : Phase
    {
        public ApplyEffectsPhase(BattleActor actor) 
            : base(actor)
        {
        }

        protected override void update()
        {
            Actor.CurrentAction.Effect.Perform(Actor, Actor.SelectedTargets, Actor.Controller);
            IsFinished = true;
        }
    }
}
