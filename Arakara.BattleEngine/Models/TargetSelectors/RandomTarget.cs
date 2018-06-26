using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Tenswee.Common.Extensions;

namespace Arakara.BattleEngine.Models.TargetSelectors
{
    public class RandomTarget : Aspect, ITargetSelector
    {
        public Mark mark;
        public int count = 1;

        public List<Actor> SelectTargets(IContainer game, int factionId)
        {
            var result = new List<Actor>();
            var system = game.GetAspect<TargetSystem>();
            var move = (Container as Ability).Action;
            var marks = system.GetActors(move, mark, factionId);
            if (marks.Count == 0)
                return result;
            for (int i = 0; i < count; ++i)
            {
                result.Add(marks.PickRandom());
            }
            return result;
        }
    }
}
