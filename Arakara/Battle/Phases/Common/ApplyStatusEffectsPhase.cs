using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle.Phases.Common
{
    public class ApplyStatusEffectsPhase : Phase
    {
        public ApplyStatusEffectsPhase(BattleActor actor) 
            : base(actor)
        {
        }

        protected override void update()
        {
            Actor.Statuses.ApplyStatuses(Actor, Actor.Controller);
            IsFinished = true;
        }
    }
}
