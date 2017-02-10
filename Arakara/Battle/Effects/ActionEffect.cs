using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Effects
{
    public abstract class ActionEffect
    {
        public abstract void Perform(BattleActor actor, List<BattleActor> targets, BattleController controller);

        public abstract string GetDescription();

        public void Perform(BattleActor actor, BattleActor target, BattleController controller)
        {
            Perform(actor, new List<BattleActor> { target }, controller);
        }
    }
}
