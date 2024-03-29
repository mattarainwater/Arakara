﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Nez;
using Microsoft.Xna.Framework;
using Arakara.Battle.Statuses;
using Nez.Sprites;

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
            var crit = Nez.Random.NextFloat() <= actor.CriticalHitChance ? 2 : 1;
            DamageDealt = Nez.Random.Range(MinDamage, MaxDamage + 1) * crit;
            foreach(var target in targets)
            {
                var dodge = Nez.Random.NextFloat() <= target.DodgeChance ? 0 : 1;
                DamageDealt *= dodge;
                var defense = 0;
                var defenseUp = target.Statuses.Get("DefenseUp");
                if(defenseUp != null)
                {
                    defense = ((DefenseUpStatus)defenseUp).Value;
                }
                DamageDealt = DamageDealt - defense < 0 ? 0 : DamageDealt - defense;
                target.CurrentHP -= DamageDealt;
                var displayText = dodge == 0 ? "Miss" : crit == 2 ? "Crit! " + DamageDealt.ToString() : DamageDealt == 0 ? "Blocked!" : DamageDealt.ToString();
                var displayColor = DamageDealt == 0 ? Color.DarkGray : crit == 2 ? Color.DarkViolet : Color.Red;
                var effectDisplayContainer = target.GetComponent<EffectDisplayContainer>();
                effectDisplayContainer.MakeEffectDisplay(displayText, displayColor);

                var animator = target.GetComponent<SpriteAnimator>();
                if(animator.HasAnimation(Animations.Hit) && dodge != 0)
                {
                    animator.Play(Animations.Hit);
                    Core.Schedule(1f, (t) => {
                        animator.Play(Animations.Idle);
                    });
                }
            }
        }

        public override string GetDescription()
        {
            return $"Deal {MinDamage}-{MaxDamage} Damage";
        }
    }
}
