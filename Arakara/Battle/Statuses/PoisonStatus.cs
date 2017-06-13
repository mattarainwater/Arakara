using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Arakara.Battle.Effects;

namespace Arakara.Battle.Statuses
{
    public class PoisonStatus : BattleStatus
    {
        public DamageEffect DamageEffect { get; set; }

        public PoisonStatus(int duration, int minDamage, int maxDamage)
            : base (duration)
        {
            DamageEffect = new DamageEffect(minDamage, maxDamage);
        }

        public override void Apply(BattleActor actor, BattleController controller)
        {
            DamageEffect.Perform(actor, actor, controller);
        }

        public override string GetDescription()
        {
            return $"Deal {DamageEffect.MinDamage}-{DamageEffect.MaxDamage} every turn for {Duration} turns.";
        }

        public override string GetCode()
        {
            return "Poison";
        }
    }
}
