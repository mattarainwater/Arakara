using Arakara.Components;
using System.Collections.Generic;

namespace Arakara.Battle.Effects
{
    public abstract class Condition
    {
        public abstract bool IsMet(BattleActor actor, BattleActor target, BattleController controller);
        public abstract string GetDescription();
    }
}