using Arakara.Battle.Statuses;
using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle
{
    public class BattleStatusCollection
    {
        private List<BattleStatus> _statuses;

        public BattleStatusCollection()
        {
            _statuses = new List<BattleStatus>();
        }

        public void Add(BattleStatus status, int duration)
        {
            _statuses.Add(status);
        }

        public BattleStatus Get(string statusCode)
        {
            return _statuses.FirstOrDefault(x => x.GetCode() == statusCode);
        }

        public bool Contains(string statusCode)
        {
            return _statuses.Any(x => x.GetCode() == statusCode);
        }

        public void ApplyStatuses(BattleActor actor, BattleController controller)
        {
            foreach(var status in _statuses)
            {
                status.Apply(actor, controller);
                status.CurrentDuration++;
            }

            _statuses = _statuses.Where(x => x.CurrentDuration != x.Duration).ToList();
        }
    }
}
