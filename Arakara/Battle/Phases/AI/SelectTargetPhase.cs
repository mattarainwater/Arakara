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

        public override void Update()
        {
            var enemyActor = Actor.Controller.Actors.FirstOrDefault(y => y.Faction.Id != Actor.Faction.Id);
            Actor.SelectTarget(enemyActor);
            IsFinished = true;
        }
    }
}
