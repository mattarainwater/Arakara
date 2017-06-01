using Arakara.Battle;
using Arakara.Common;
using Microsoft.Xna.Framework;
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
        public BattleEvent CurrentEvent { get; set; }

        public bool IsActive { get; set; }

        public BattleController()
        {
            Actors = new List<BattleActor>();
        }

        public void Update()
        {
            if(IsActive)
            {
                if(CurrentEvent != null)
                {
                    CurrentEvent.Perform();
                }
                else if (CurrentActor != null)
                {
                    CurrentActor.ProcessTurn();
                    if(CurrentActor == null)
                    {
                        CurrentActor = Actors.OrderBy(x => x.TimeUntilTurn).First();
                    }
                    else if (CurrentActor.State == BattleState.NotTurn)
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
                        Actors.ForEach(x => x.Targetable = false);
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

        public void Kill(BattleActor target)
        {
            Actors.Remove(target);
            if(CurrentActor == target)
            {
                CurrentActor = null;
            }
            target.entity.destroy();
        }

        public void MakeTargetables(BattleActor targerter, Targeting targeting)
        {
            Actors.ForEach(x => x.Targetable = false);
            var targetable = GetTargetableActors(targerter, targeting);
            targetable.ForEach(x => x.Targetable = true);
        }

        private List<BattleActor> GetTargetableActors(BattleActor targerter, Targeting targeting)
        {
            switch (targeting)
            {
                case Targeting.Allies:
                    return Actors.Where(x => x.Faction.Id == targerter.Faction.Id).ToList();
                case Targeting.Enemies:
                    return Actors.Where(x => x.Faction.Id != targerter.Faction.Id).ToList();
                case Targeting.Self:
                    return Actors.Where(x => x == targerter).ToList();
                default:
                    return null;
            }
        }

        public void AddActor(BattleActor actor)
        {
            actor.Controller = this;
            Actors.Add(actor);
        }
    }
}
