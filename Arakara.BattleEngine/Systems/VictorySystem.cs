using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Systems
{
    public class VictorySystem : Aspect, IObservable
    {
        public void Awake()
        {
        }

        public void Destroy()
        {
        }

        public bool IsGameOver()
        {
            var match = Container.GetBattle();
            return CheckForVictory(match.Actors);
        }

        private bool CheckForVictory(List<Actor> actors)
        {
            var firstFactionId = actors.First().FactionId;
            return actors.All(x => x.FactionId == firstFactionId);
        }
    }
}
