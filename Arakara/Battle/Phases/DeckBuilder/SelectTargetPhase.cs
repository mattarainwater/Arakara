using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle.Phases.DeckBuilder
{
    public class SelectTargetPhase : Phase
    {
        private DeckBuilderActor _deckBuilderActor;

        public SelectTargetPhase(DeckBuilderActor actor) 
            : base(actor)
        {
            _deckBuilderActor = actor;
        }

        public override void Update()
        {
            _deckBuilderActor.TargetSelector.enabled = true;
            if (_deckBuilderActor.SelectedTargets != null)
            {
                _deckBuilderActor.TargetSelector.enabled = false;
                IsFinished = true;
            }
        }
    }
}
