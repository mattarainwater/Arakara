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
        private bool _isAnimating;

        public AnimationPhase(BattleActor actor) 
            : base(actor)
        {
        }

        protected override void initialize()
        {
            _isAnimating = true;
            Actor.Animator.play(Actor.CurrentAction.Animation);
            Actor.Animator.onAnimationCompletedEvent += (t) => {
                Actor.Animator.onAnimationCompletedEvent += null;
                _isAnimating = false;
            };
        }

        protected override void update()
        {
            if(!_isAnimating)
            {
                IsFinished = true;
            }
        }

        protected override void finish()
        {
            Actor.Animator.play(Actor.IdleAnimation);
        }
    }
}
