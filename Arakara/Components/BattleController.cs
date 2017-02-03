using Arakara.Battle;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class BattleController : Component, IUpdatable
    {
        public List<BattleActor> Actors { get; set; }

        private BattleActor _currentActor;

        public BattleController()
        {
            Actors = new List<BattleActor>();
        }

        public void update()
        {
            if(Actors.All(x => x.State == BattleState.NotTurn) || _currentActor == null)
            {
                var nextActor = Actors.OrderBy(x => x.TimeUntilTurn).First();
                nextActor.State = BattleState.StartOfTurn;
                _currentActor = nextActor;
                foreach(var actor in Actors)
                {
                    actor.TimeUntilTurn -= _currentActor.TimeUntilTurn;
                }
            }
            if(_currentActor.State == BattleState.EndOfTurn)
            {
                while(Actors.Any(x => x.TimeUntilTurn == _currentActor.Delay))
                {
                    _currentActor.Delay++;
                }
                _currentActor.TimeUntilTurn = _currentActor.Delay;
                _currentActor.State = BattleState.NotTurn;
            }
        }
    }
}
