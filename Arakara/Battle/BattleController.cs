using Arakara.Battle;
using Arakara.Battle.Events;
using Arakara.Battle.Events.Effects;
using Arakara.Battle.Events.Triggers;
using Arakara.Common;
using Arakara.Components;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle
{
    public class BattleController
    {
        public List<BattleActor> Actors { get; set; }
        public BattleActor CurrentActor { get; set; }
        public List<BattleEvent> Events { get; set; }
        public BattleEvent CurrentEvent { get; set; }

        public bool IsActive { get; set; }

        public BattleController()
        {
            Actors = new List<BattleActor>();
            Events = new List<BattleEvent>();
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
                    if (CurrentActor.State == BattleState.NotTurn)
                    {
                        var triggeredEvent = GetTriggeredEvent();
                        if(triggeredEvent != null)
                        {
                            CurrentEvent = triggeredEvent;
                        }
                        else
                        {
                            var indexOfNextActor = Actors.IndexOf(CurrentActor) + 1 == Actors.Count() ? 0 : Actors.IndexOf(CurrentActor) + 1;
                            CurrentActor = Actors.ElementAt(indexOfNextActor);
                            Actors.ForEach(x => x.Targetable = false);
                            CurrentActor.State = BattleState.StartOfTurn;
                        }
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

        public void AddEvent(BattleEvent battleEvent)
        {
            battleEvent.Controller = this;
            Events.Add(battleEvent);
        }

        public void AddActor(BattleActor actor)
        {
            actor.Controller = this;
            Actors.Add(actor);
            Actors.Sort();
            var battleEvent = new BattleEvent(new OnDeathTrigger(actor));
            battleEvent.AddEffect(new KillEffect(0, actor));
        }

        private BattleEvent GetTriggeredEvent()
        {
            return Events.FirstOrDefault(x => x.Trigger.IsTriggered());
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
    }
}
