using Arakara.Battle;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public abstract class BattleActor : Component, IUpdatable
    {
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int TimeUntilTurn { get; set; }
        public int Delay { get; set; }
        public Faction Faction { get; set; }
        public BattleState State { get; set; }

        public BattleActor(int maxHp, Faction faction)
        {
            MaxHP = maxHp;
            CurrentHP = maxHp;
            TimeUntilTurn = 0;
            Delay = 0;
            Faction = faction;
            State = BattleState.NotTurn;
        }

        public abstract void update();
    }
}
