using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Phases.AI
{
    public class SelectActionPhase : Phase
    {
        private AIActor _aiActor;

        public SelectActionPhase(AIActor actor) 
            : base(actor)
        {
            _aiActor = actor;
        }

        protected override void update()
        {
            Actor.CurrentAction = (BattleAction<Animations>)_aiActor.Decider.ChooseAction(Actor, _aiActor.Actions.Select(x => (BattleAction)x).ToList(), Actor.Controller);
            IsFinished = true;
        }
    }
}
