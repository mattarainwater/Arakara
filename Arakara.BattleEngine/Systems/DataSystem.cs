using Arakara.BattleEngine.Models;
using Newtonsoft.Json;
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
        public Battle Battle = new Battle();
    }

    public static class DataSystemExtensions
    {
        public static Battle GetBattle(this IContainer game)
        {
            var dataSystem = game.GetAspect<DataSystem>();
            return dataSystem.Battle;
        }

        public static string GetBattleAsJson(this IContainer game)
        {
            var dataSystem = game.GetAspect<DataSystem>();
            return string.Join("\r\n", dataSystem.Battle.Actors.Select(x => x.Name + " " + x.CurrentHP + " / " + x.MaxHP));
        }
    }
}
