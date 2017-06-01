using Arakara.Components;
using System.Collections.Generic;

namespace Arakara.Battle
{
    public abstract class BattleEvent
    {
        public BattleController Controller { get; set; }
        public EventState State { get; set; }

        public BattleEvent(BattleController controller)
        {
            Controller = controller;
        }

        public void Perform()
        {
            switch (State)
            {
                case EventState.StartOfEvent:
                    OnStartOfEvent();
                    break;
                case EventState.DuringEvent:
                    DuringEvent();
                    break;
                case EventState.EndOfEvent:
                    OnEndOfEvent();
                    break;
            }
        }

        protected abstract void OnStartOfEvent();

        protected abstract void DuringEvent();

        protected abstract void OnEndOfEvent();
    }
}