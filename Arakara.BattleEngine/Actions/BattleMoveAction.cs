using Arakara.BattleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.BattleEngine.Actions
{
    public class BattleMoveAction : GameAction
    {
        public Move Move { get; set; }
        public int FactionId { get; set; }

        public BattleMoveAction(Move move, int factionId)
        {
            Move = move;
            FactionId = factionId;
        }

        public override string GetLog()
        {
            return $"Move {Move.Name} executed";
        }
    }
}
