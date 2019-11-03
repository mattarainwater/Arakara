using Arakara.Battle;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class ActionProgrammerActor : BattleActor
    {
        public List<ProgrammedAction> Actions { get; set; }
        public Queue<BattleAction> Queue { get; set; }
        public List<Entity> ActionEntities { get; set; }

        public ActionProgrammerActor(
            string name,
            int maxHP,
            Faction faction,
            float dodgeChance,
            float critChance,
            float speed,
            List<ProgrammedAction> actions) 
                : base(name, maxHP, faction, dodgeChance, critChance, speed)
        {
            Actions = actions;
            Queue = new Queue<BattleAction>();
        }
    }
}
