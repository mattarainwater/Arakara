using Arakara.Battle;
using Arakara.Battle.AI;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class AIActor : BattleActor
    {
        public List<BattleAction> Actions { get; set; }
        public AIDecider Decider { get; set; }

        public AIActor(
            string name, 
            int maxHP, 
            Faction faction, 
            List<BattleAction> actions, 
            AIDecider decider, 
            float dodgeChance, 
            float critChance, 
            float speed) 
                : base(name, maxHP, faction, dodgeChance, critChance, speed)
        {
            Actions = actions;
            Decider = decider;
        }
    }
}
