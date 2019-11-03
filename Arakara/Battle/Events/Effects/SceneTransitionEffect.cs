using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Events.Effects
{
    public class SceneTransitionEffect : BattleEventEffect
    {
        public Func<Scene> SceneTransition { get; set; }
        private bool _transitionStarted;

        public SceneTransitionEffect(int sequence, Func<Scene> sceneTransition) 
            : base(sequence)
        {
            SceneTransition = sceneTransition;
        }

        protected override void OnStartOfEvent()
        {
            State = BattleEventEffectState.DuringEventEffect;
        }

        protected override void DuringEvent()
        {
            if(!_transitionStarted)
            {
                _transitionStarted = true;
                Core.StartSceneTransition(new FadeTransition(SceneTransition)
                {
                    FadeInDuration = 0f,
                    FadeOutDuration = 0f,
                    DelayBeforeFadeInDuration = 0f,
                    FadeToColor = Color.WhiteSmoke
                });
            }
        }

        protected override void OnEndOfEvent()
        {
            State = BattleEventEffectState.Done;
        }
    }
}
