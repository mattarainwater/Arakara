﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Nez;

namespace Arakara.Battle.Effects
{
    public class DamageEffect : ActionEffect
    {
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int DamageDealt { get; set; }

        public DamageEffect(int minDamage, int maxDamage)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            DamageDealt = 0;
        }

        public override void Perform(BattleActor actor, List<BattleActor> targets, BattleController controller)
        {
            var crit = Nez.Random.nextFloat() <= actor.CriticalHitChance ? 2 : 1;
            DamageDealt = Nez.Random.range(MinDamage, MaxDamage + 1) * crit;
            foreach(var target in targets)
            {
                var dodge = Nez.Random.nextFloat() <= target.DodgeChance ? 0 : 1;
                DamageDealt *= dodge;
                target.CurrentHP -= DamageDealt;
            }
        }

        public override string GetDescription()
        {
            return $"Deal {MinDamage}-{MaxDamage} Damage";
        }
    }
}
