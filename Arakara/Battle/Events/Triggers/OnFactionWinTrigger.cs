using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Events.Triggers
{
    public class OnFactionWinTrigger : BattleEventTrigger
    {
        public Faction Faction { get; set; }

        public OnFactionWinTrigger(Faction faction)
        {
            Faction = faction;
        }

        public override bool IsTriggered(BattleController controller)
        {
            return controller.Actors.All(x => x.Faction == Faction);
        }
    }
}
