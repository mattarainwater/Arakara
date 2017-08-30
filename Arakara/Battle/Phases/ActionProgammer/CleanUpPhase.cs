using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Nez;

namespace Arakara.Battle.Phases.ActionProgammer
{
    public class CleanUpPhase : Phase
    {
        private ActionProgrammerActor _actionProgrammerActor;

        public CleanUpPhase(ActionProgrammerActor actor) 
            : base(actor)
        {
            _actionProgrammerActor = actor;
        }

        protected override void update()
        {
            _actionProgrammerActor.ActionEntities.ForEach(entity => entity.destroy());
            _actionProgrammerActor.ActionEntities = new List<Entity>();

            _actionProgrammerActor.SelectedTargets = null;
            _actionProgrammerActor.CurrentAction = null;

            IsFinished = true;
        }
    }
}
