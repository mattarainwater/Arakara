using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Phases
{
    public abstract class Phase
    {
        public bool IsFinished { get; set; }
        public BattleActor Actor { get; set; }

        private bool _logged;

        public Phase(BattleActor actor)
        {
            Actor = actor;
        }

        public void Perform()
        {
            if(!_logged)
            {
                Console.Out.WriteLine($"Actor: {Actor.Name} Phase: {GetType().ToString()}");
                _logged = true;
            }
            Update();
            if(IsFinished)
            {
                _logged = false;
            }
        }

        public abstract void Update();
    }
}
