using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Effects
{
    public class HealEffect : ActionEffect
    {
        public int Healing { get; set; }

        public HealEffect(int healing)
        {
            Healing = healing;
        }

        public override void Perform(BattleActor actor, List<BattleActor> targets, BattleController controller)
        {
            foreach (var target in targets)
            {
                target.CurrentHP += Healing;
                if(target.CurrentHP > target.MaxHP)
                {
                    target.CurrentHP = target.MaxHP;
                }
            }
        }

        public override string GetDescription()
        {
            return $"Heal {Healing} Damage";
        }
    }
}
