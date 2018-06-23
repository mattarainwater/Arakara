using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.BattleEngine.Models
{
    public class Battle
    {
        public List<Actor> Actors { get; set; }

        public int CurrentActorIndex { get; set; }

        public Actor CurrentActor
        {
            get
            {
                return Actors[CurrentActorIndex];
            }
        }
    }
}
