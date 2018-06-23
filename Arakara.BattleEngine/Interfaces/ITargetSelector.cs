using Arakara.BattleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Interfaces
{
    public interface ITargetSelector : IAspect
    {
        List<Actor> SelectTargets(IContainer game);
    }
}
