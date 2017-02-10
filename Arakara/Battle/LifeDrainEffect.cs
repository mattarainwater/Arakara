using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle
{
    public class LifeDrainEffect : ActionEffect
    {
        public DamageEffect DamageEffect { get; set; }
        public HealEffect HealEffect { get; set; }
        public int Damage { get; set; }

        public LifeDrainEffect(int damage) 
        {
            Damage = damage;
            HealEffect = new HealEffect(damage);
            DamageEffect = new DamageEffect(damage);
        }

        public override void Perform(BattleActor actor, List<BattleActor> targets, BattleController controller)
        {
            DamageEffect.Perform(actor, targets, controller);
            HealEffect.Perform(actor, actor, controller);
        }

        public override string GetDescription()
        {
            return $"Deal {Damage} Damage and heal that much life";
        }
    }
}
