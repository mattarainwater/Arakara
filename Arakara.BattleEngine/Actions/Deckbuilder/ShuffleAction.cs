using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.BattleEngine.Actions.Deckbuilder
{
    public class ShuffleAction : GameAction
    {
        public override string GetLog()
        {
            return $"Shuffle deck";
        }
    }
}
