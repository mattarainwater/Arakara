using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Arakara.Common;
using Tenswee.Common.Extensions;

namespace Arakara.Battle.Effects
{
    public abstract class ActionEffect
    {
        public abstract void Perform(BattleActor actor, List<BattleActor> targets, BattleController controller);

        public abstract string GetDescription();

        public string FormatDescription()
        {
            return GetDescription().WrapText(15);
        }

        public void Perform(BattleActor actor, BattleActor target, BattleController controller)
        {
            Perform(actor, new List<BattleActor> { target }, controller);
        }
    }
}
