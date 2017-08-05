using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle.Phases.DeckBuilder
{
    public class SelectCardPhase : Phase
    {
        private DeckBuilderActor _deckBuilderActor;

        public SelectCardPhase(DeckBuilderActor actor) 
            : base(actor)
        {
            _deckBuilderActor = actor;
        }

        public override void Update()
        {
            _deckBuilderActor.CardSelector.enabled = true;
            if(_deckBuilderActor.SelectedCard != null)
            {
                _deckBuilderActor.CardSelector.enabled = false;
                IsFinished = true;
            }
        }
    }
}
