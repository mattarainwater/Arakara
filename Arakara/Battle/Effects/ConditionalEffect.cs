using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle.Effects
{
    public class ConditionalEffect : ActionEffect
    {
        public ActionEffect DefaultEffect { get; set; }
        public ActionEffect SecondaryEffect { get; set; }
        public Condition Condition { get; set; }

        public ConditionalEffect(ActionEffect defaultEffect, ActionEffect secondaryEffect, Condition condition)
        {
            DefaultEffect = defaultEffect;
            SecondaryEffect = secondaryEffect;
            Condition = condition;
        }

        public override void Perform(BattleActor actor, List<BattleActor> targets, BattleController controller)
        {
            foreach(var target in targets)
            {
                if (Condition.IsMet(actor, target, controller))
                {
                    SecondaryEffect.Perform(actor, target, controller);
                }
                else
                {
                    DefaultEffect.Perform(actor, target, controller);
                }
            }
        }

        public override string GetDescription()
        {
            return DefaultEffect.GetDescription() + ", if " + Condition.GetDescription() + " then " + SecondaryEffect.GetDescription() + " instead.";
        }
    }
}
