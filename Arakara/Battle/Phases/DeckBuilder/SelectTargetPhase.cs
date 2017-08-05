using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Nez;
using Microsoft.Xna.Framework;
using Arakara.Common;

namespace Arakara.Battle.Phases.DeckBuilder
{
    public class SelectTargetPhase : Phase
    {
        private DeckBuilderActor _deckBuilderActor;
        public Selector TargetSelector { get; set; }

        public SelectTargetPhase(DeckBuilderActor actor) 
            : base(actor)
        {
            _deckBuilderActor = actor;
            TargetSelector = actor.entity.addComponent(new Selector(
                VirtualButtons.SelectInput,
                VirtualButtons.LeftInput,
                VirtualButtons.RightInput,
                onFocus: OnTargetFocus,
                onBlur: OnTargetBlur,
                onSelect: OnTargetSelect));
            TargetSelector.enabled = false;
        }

        protected override void initialize()
        {
            TargetSelector.enabled = true;
            var targets = Actor.Controller.MakeTargetables(Actor, _deckBuilderActor.SelectedCard.Action.Targeting);
            foreach (var target in targets)
            {
                TargetSelector.AddEntity(target);
            }
        }

        protected override void update()
        {
            if (_deckBuilderActor.SelectedTargets != null)
            {
                IsFinished = true;
            }
            else if(VirtualButtons.BackInput.isPressed)
            {
                GoBack = true;
                TargetSelector.Reset();
                Actor.Controller.RemoveTargetables();
            }
        }

        protected override void finish()
        {
            TargetSelector.enabled = false;
            TargetSelector.Reset();
            Actor.Controller.RemoveTargetables();
        }

        private void OnTargetFocus(Entity entity)
        {
            var simplePolygon = entity.getComponent<TargetPolygon>();
            if (simplePolygon != null)
            {
                simplePolygon.setColor(Color.Red);
            }
        }

        private void OnTargetBlur(Entity entity)
        {
            var simplePolygon = entity.getComponent<TargetPolygon>();
            if (simplePolygon != null)
            {
                simplePolygon.setColor(Color.LightPink);
            }
        }

        private void OnTargetSelect(Entity entity)
        {
            var battleActor = entity.getComponent<BattleActor>();
            Actor.Controller.CurrentActor.SelectTarget(battleActor);
        }
    }
}
