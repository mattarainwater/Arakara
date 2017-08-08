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
        public int Value { get; set; }

        public DefenseUpStatus(int duration, int value) 
            : base(duration)
        {
            Value = value;
        }

        public override void Apply(BattleActor actor, BattleController controller)
        {
        }

        public override string GetDescription()
        {
            return $"Reduces damage by {Value} for {Duration} turns.";
        }

        public override string GetCode()
        {
            return "DefenseUp";
        }
    }
}
