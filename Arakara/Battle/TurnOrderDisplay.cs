using Arakara.Components;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle
{
    public class TurnOrderDisplay
    {
        public List<BattleActor> Actors { get; set; }

        public TurnOrderDisplay()
        {
            Actors = new List<BattleActor>();
        }

        public void AddActor(BattleActor actor)
        {
            Actors.Add(actor);
        }
    }
}
