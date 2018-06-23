using System.Collections.Generic;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Models.AI
{
    public class AIActor : Actor
    {
        public AIActor(int index)
            : base(index)
        {

        }

        public List<BattleAction> Actions { get; set; }
    }
}
