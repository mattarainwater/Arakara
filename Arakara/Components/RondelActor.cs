using Arakara.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class RondelActor : BattleActor
    {
        public List<RondelSection> Rondel { get; set; }

        public RondelActor(List<RondelSection> rondel,
            string name,
            int maxHP,
            Faction faction,
            float dodgeChance,
            float critChance,
            float speed)
                : base(name, maxHP, faction, dodgeChance, critChance, speed)
        {
            Rondel = rondel;
        }
    }
}
