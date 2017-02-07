using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Nez;

namespace Arakara.Battle
{
    public class RandomAIDecider : AIDecider
    {
        public override BattleAction ChooseAction(BattleActor self, List<BattleAction> actions, BattleController controller)
        {
            return actions.RandomElement();
        }
    }
}
