using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Statuses
{
    public class DefenseUpStatus : BattleStatus
    {
        public DefenseUpStatus(int duration) 
            : base(duration)
        {
        }

        public override void Apply(BattleActor actor, BattleController controller)
        {
        }

        public override string GetDescription()
        {
            return $"Reduces damage by 10 for {Duration} turns.";
        }

        public override string GetCode()
        {
            return "DefenseUp";
        }
    }
}
