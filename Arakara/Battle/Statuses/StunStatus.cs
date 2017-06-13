using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Statuses
{
    public class StunStatus : BattleStatus
    {
        public StunStatus(int duration) 
            : base(duration)
        {
        }

        public override void Apply(BattleActor actor, BattleController controller)
        {
            actor.State = BattleState.EndOfTurn;
        }

        public override string GetDescription()
        {
            return $"Stuns the opponent for {Duration} turns.";
        }

        public override string GetCode()
        {
            return "Stun";
        }
    }
}
