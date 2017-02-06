using Arakara.Battle;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class BattleController
    {
        public List<BattleActor> Actors { get; set; }
        public BattleActor CurrentActor { get; set; }

        public bool IsActive { get; set; }

        public BattleController()
        {
            Actors = new List<BattleActor>();
        }

        public void Update()
        {
            if(IsActive)
            {
                if (CurrentActor != null)
                {
                    CurrentActor.ProcessTurn(this);
                    if (CurrentActor.State == BattleState.NotTurn)
                    {
                        CurrentActor.TimeUntilTurn = CurrentActor.Delay;
                        while (Actors.Any(x => x.TimeUntilTurn == CurrentActor.TimeUntilTurn && x != CurrentActor))
                        {
                            CurrentActor.TimeUntilTurn++;
                        }
                        CurrentActor = Actors.OrderBy(x => x.TimeUntilTurn).First();
                        var timeUntilTurn = CurrentActor.TimeUntilTurn;
                        foreach (var actor in Actors)
                        {
                            actor.TimeUntilTurn -= timeUntilTurn;
                        }
                        CurrentActor.State = BattleState.StartOfTurn;
                    }
                }
                else
                {
                    CurrentActor = Actors.First();
                    CurrentActor.State = BattleState.StartOfTurn;
                }
            }
        }

        public void AddActor(BattleActor actor)
        {
            Actors.Add(actor);
        }
    }
}
