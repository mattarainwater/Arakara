using Arakara.BattleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Systems
{
    public class DataSystem : Aspect
    {
        public Battle battle = new Battle();
    }

    public static class DataSystemExtensions
    {
        public static Battle GetBattle(this IContainer game)
        {
            var dataSystem = game.GetAspect<DataSystem>();
            return dataSystem.battle;
        }
    }
}
