using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Events.Triggers
{
    public class OnFactionWinTrigger : BattleEventTrigger
    {
        public int FactionId { get; set; }

        public OnFactionWinTrigger(int factionId)
        {
            FactionId = factionId;
        }

        public override bool IsTriggered(BattleController controller)
        {
            return controller.Actors.All(x => x.Faction.Id == FactionId);
        }
    }
}
