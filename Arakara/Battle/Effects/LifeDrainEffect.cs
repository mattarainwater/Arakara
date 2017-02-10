using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Effects
{
    public class LifeDrainEffect : ActionEffect
    {
        public DamageEffect DamageEffect { get; set; }
        public HealEffect HealEffect { get; set; }

        public LifeDrainEffect(int minDamage, int maxDamage)
        {
            HealEffect = new HealEffect(0);
            DamageEffect = new DamageEffect(minDamage, maxDamage);
        }

        public override void Perform(BattleActor actor, List<BattleActor> targets, BattleController controller)
        {
            DamageEffect.Perform(actor, targets, controller);
            HealEffect.Healing = DamageEffect.DamageDealt;
            HealEffect.Perform(actor, actor, controller);
        }

        public override string GetDescription()
        {
            return DamageEffect.GetDescription() + " and Heal that much";
        }
    }
}
