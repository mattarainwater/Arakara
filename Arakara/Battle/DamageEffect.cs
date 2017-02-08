using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle
{
    public class DamageEffect : ActionEffect
    {
        public int Damage { get; set; }

        public DamageEffect(int damage)
        {
            Damage = damage;
        }

        public override void Perform(BattleActor actor, List<BattleActor> targets, BattleController controller)
        {
            foreach(var target in targets)
            {
                target.CurrentHP -= Damage;
            }
        }

        public override string GetDescription()
        {
            return $"Deal {Damage} Damage";
        }
    }
}
