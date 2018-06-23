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
    public class BattleController : Component, IUpdatable
    {
        public List<BattleActor> Actors { get; set; }
        public BattleActor CurrentActor { get; set; }
        public List<BattleEvent> Events { get; set; }
        public BattleEvent CurrentEvent { get; set; }
        public int CurrentActorIndex { get; set; }

        public bool IsActive { get; set; }
        private bool IsInitialized { get; set; }

        public BattleController()
        {
            Actors = new List<BattleActor>();
            Events = new List<BattleEvent>();
        }

        public void update()
        {
            if(IsActive)
            {
                if(!IsInitialized)
                {
                    IsInitialized = true;
                    CurrentActor = Actors.First();
                }
                var triggeredEvent = GetTriggeredEvent();
                if (CurrentEvent != null)
                {
                    CurrentEvent.Perform();
                }
                else if (triggeredEvent != null)
                {
                    CurrentEvent = triggeredEvent;
                }
                else if (CurrentActor != null)
                {
                    CurrentActor.ProcessTurn();
                    if (!CurrentActor.IsActive)
                    {
                        var indexOfNextActor = Actors.IndexOf(CurrentActor) + 1 == Actors.Count() ? 0 : Actors.IndexOf(CurrentActor) + 1;
                        CurrentActorIndex = indexOfNextActor;
                        CurrentActor = Actors[CurrentActorIndex];
                        Actors.ForEach(x => x.Targetable = false);
                    }
                }
                else
                {
                    CurrentActorIndex++;
                    CurrentActorIndex = CurrentActorIndex == Actors.Count() ? 0 : CurrentActorIndex;
                    CurrentActor = Actors[CurrentActorIndex];
                }
            }
        }

        public IEnumerable<Entity> MakeTargetables(BattleActor targerter, Targeting targeting)
        {
            RemoveTargetables();
            var targetable = GetTargetableActors(targerter, targeting);
            targetable.ForEach(x => x.Targetable = true);
            return targetable.Select(x => x.entity);
        }

        public void RemoveTargetables()
        {
            Actors.ForEach(x => x.Targetable = false);
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
            AddEvent(battleEvent);
            battleEvent.AddEffect(new KillEffect(0, actor));
        }

        private BattleEvent GetTriggeredEvent()
        {
            return Events.FirstOrDefault(x => x.Trigger.IsTriggered(this));
        }

        private List<BattleActor> GetTargetableActors(BattleActor targerter, Targeting targeting)
        {
            switch (targeting)
            {
                case Targeting.Allies:
                    return Actors.Where(x => x.Faction == targerter.Faction).ToList();
                case Targeting.Enemies:
                    return Actors.Where(x => x.Faction != targerter.Faction).ToList();
                case Targeting.Self:
                    return Actors.Where(x => x == targerter).ToList();
                default:
                    return null;
            }
        }
    }
}
