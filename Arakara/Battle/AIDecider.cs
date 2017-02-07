using Arakara.Battle;
using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle
{
    public abstract class AIDecider
    {
        public abstract BattleAction ChooseAction(BattleActor self, List<BattleAction> actions, BattleController controller);
    }
}
