using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle.Phases.Common
{
    public class AnimationPhase : Phase
    {
        public AnimationPhase(BattleActor actor) 
            : base(actor)
        {
        }

        public override void Update()
        {
            if (!EqualityComparer<Animations>.Default.Equals(Actor.Animator.currentAnimation, Actor.CurrentAction.Animation)
                && !IsFinished)
            {
                Actor.Animator.play(Actor.CurrentAction.Animation);
                Actor.Animator.onAnimationCompletedEvent = (t) => {
                    Actor.Animator.onAnimationCompletedEvent = null;
                    IsFinished = true;
                };
            }
        }
    }
}
