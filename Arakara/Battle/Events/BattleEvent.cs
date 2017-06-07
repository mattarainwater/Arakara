using Arakara.Components;
using System.Collections.Generic;
using System.Linq;

namespace Arakara.Battle.Events
{
    public class BattleEvent
    {
        public BattleController Controller { get; set; }
        public BattleEventTrigger Trigger { get; set; }
        public List<BattleEventEffect> Effects { get; set; }
        public bool Repeatable { get; set; }

        private BattleEventEffect CurrentEffect { get; set; }

        public BattleEvent(BattleEventTrigger trigger)
        {
            Trigger = trigger;
            Effects = new List<BattleEventEffect>();
        }

        public void Perform()
        {
            if(CurrentEffect == null)
            {
                CurrentEffect = Effects.First();
            }
            if(CurrentEffect.State == BattleEventEffectState.EndOfEventEffect)
            {
                var indexOfNextEffect = Effects.IndexOf(CurrentEffect) + 1 == Effects.Count() ? 0 : Effects.IndexOf(CurrentEffect) + 1;
                if(indexOfNextEffect == 0)
                {
                    Controller.CurrentEvent = null;
                    if(!Repeatable)
                    {
                        Controller.Events.Remove(this);
                    }
                }
                else
                {
                    CurrentEffect = Effects.ElementAt(indexOfNextEffect);
                    CurrentEffect.State = BattleEventEffectState.StartOfEventEffect;
                }
            }
            CurrentEffect.Perform();
        }

        public void AddEffect(BattleEventEffect effect)
        {
            effect.Controller = effect.Controller;
            Effects.Add(effect);
            Effects.Sort();
        }
    }
}