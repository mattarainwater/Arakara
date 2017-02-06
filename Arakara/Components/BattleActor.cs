using Arakara.Battle;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public abstract class BattleActor : Component
    {
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int TimeUntilTurn { get; set; }
        public int Delay { get; set; }
        public Faction Faction { get; set; }
        public BattleState State { get; set; }
        public bool Immune { get; set; }

        protected BattleActor _selectedTarget;

        public BattleActor(string name, int maxHp, Faction faction)
        {
            Name = name;
            MaxHP = maxHp;
            CurrentHP = maxHp;
            TimeUntilTurn = 0;
            Delay = 0;
            Faction = faction;
            State = BattleState.NotTurn;
        }

        public virtual void ProcessTurn(BattleController controller)
        {
            switch (State)
            {
                case BattleState.StartOfTurn:
                    OnStartOfTurn(controller);
                    break;
                case BattleState.AwaitingDecision:
                    OnAwaitingDecision(controller);
                    break;
                case BattleState.Targeting:
                    OnTargeting(controller);
                    break;
                case BattleState.EndOfTurn:
                    OnEndOfTurn(controller);
                    break;
            }
        }

        public void SelectTarget(BattleActor target)
        {
            _selectedTarget = target;
        }

        protected abstract void OnStartOfTurn(BattleController controller);

        protected abstract void OnAwaitingDecision(BattleController controller);

        protected abstract void OnTargeting(BattleController controller);

        protected abstract void OnEndOfTurn(BattleController controller);
    }
}
