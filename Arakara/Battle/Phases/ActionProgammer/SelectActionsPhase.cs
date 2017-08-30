using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Arakara.Common;
using Nez;

namespace Arakara.Battle.Phases.ActionProgammer
{
    public class SelectActionsPhase : Phase
    {
        private ActionProgrammerActor _actionProgrammerActor;
        public Selector ActionSelector { get; set; }

        public SelectActionsPhase(ActionProgrammerActor actor) 
            : base(actor)
        {
            _actionProgrammerActor = actor;
            ActionSelector = actor.entity.addComponent(new Selector(
                VirtualButtons.SelectInput,
                VirtualButtons.LeftInput,
                VirtualButtons.RightInput,
                onFocus: OnActionFocus,
                onBlur: OnActionBlur,
                onSelect: OnActionSelect));
        }

        protected override void initialize()
        {
            ActionSelector.enabled = true;
            ActionSelector.AddEntities(_actionProgrammerActor.ActionEntities);
        }

        protected override void update()
        {
        }

        protected override void finish()
        {
            ActionSelector.enabled = false;
            ActionSelector.Reset();
        }

        private void OnActionFocus(Entity entity)
        {
        }

        private void OnActionBlur(Entity entity)
        {
        }

        private void OnActionSelect(Entity entity)
        {
            var action = entity.getComponent<ProgrammedAction>();
            _actionProgrammerActor.Queue.Enqueue(action.TieredActions[_actionProgrammerActor.Queue.Count()]);
        }
    }
}
