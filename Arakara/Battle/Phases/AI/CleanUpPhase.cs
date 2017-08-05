using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Phases.AI
{
    public class CleanUpPhase : Phase
    {
        private AIActor _aiActor;

        public CleanUpPhase(AIActor actor) 
            : base(actor)
        {
            _aiActor = actor;
        }

        public override void Update()
        {
            Actor.Animator.play(Actor.IdleAnimation);
            _aiActor.SelectedTargets = null;
            _aiActor.CurrentAction = null;
            IsFinished = true;
        }
    }
}
