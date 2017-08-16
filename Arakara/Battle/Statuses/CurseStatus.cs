using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle.Statuses
{
    public class CurseStatus : BattleStatus
    {
        public CurseStatus(int duration) 
            : base(duration)
        {
        }

        public override void Apply(BattleActor actor, BattleController controller)
        {
        }

        public override string GetCode()
        {
            return "Curse";
        }

        public override string GetDescription()
        {
            return $"Curses for {Duration} turns.";
        }
    }
}
