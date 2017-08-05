﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Nez;

namespace Arakara.Battle.Phases.DeckBuilder
{
    public class CleanUpPhase : Phase
    {
        private DeckBuilderActor _deckBuilderActor;

        public CleanUpPhase(DeckBuilderActor actor) 
            : base(actor)
        {
            _deckBuilderActor = actor;
        }

        protected override void update()
        {
            _deckBuilderActor.DiscardPile.AddRange(_deckBuilderActor.Hand);
            _deckBuilderActor.HandEntities.ForEach(entity => entity.destroy());
            _deckBuilderActor.HandEntities = new List<Entity>();
            _deckBuilderActor.Hand = new List<Card<Animations>>();
            _deckBuilderActor.SelectedCard = null;
            _deckBuilderActor.SelectedTargets = null;
            _deckBuilderActor.CurrentAction = null;
            IsFinished = true;
        }
    }
}
