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

        public Actor PreviousActor
        {
            get
            {
                var previousIndex = 
                    CurrentActorIndex == 0 ? 
                    Actors.Count() - 1 : 
                    CurrentActorIndex - 1;

                return Actors[previousIndex];
            }
        }
    }
}
