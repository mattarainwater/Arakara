using Arakara.BattleEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Models.TargetSelectors
{
    public class ManualTarget : Aspect, ITargetSelector
    {
        public List<Actor> SelectTargets(IContainer game, int factionId)
        {
            var move = (Container as Ability).Action;
            var target = move.GetAspect<Target>();
            var result = new List<Actor>();
            result.Add(target.Selected);
            return result;
        }

        public void Load(Dictionary<string, object> data)
        {

        }
    }

}
