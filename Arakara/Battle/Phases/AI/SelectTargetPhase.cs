using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle.Phases.AI
{
    public class SelectTargetPhase : Phase
    {
        private AIActor _aiActor;

        public SelectTargetPhase(AIActor actor) 
            : base(actor)
        {
            _aiActor = actor;
        }

        protected override void update()
        {
            var enemyActor = Actor.Controller.Actors.FirstOrDefault(y => y.Faction != Actor.Faction);
            Actor.SelectTarget(enemyActor);
            IsFinished = true;
        }
    }
}
