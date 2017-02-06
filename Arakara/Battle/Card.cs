using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle
{
    public class Card
    {
        public string Name { get; set; }
        public Effect Effect { get; set; }
        public Targeting Targeting { get; set; }
        public int Magnitude { get; set; }
        public int Delay { get; set; }
        public Grade Grade { get; set; }
        public string Text {
            get
            {
                switch(Effect)
                {
                    case Effect.Attack:
                        return $"Deal {Magnitude} damage";
                    case Effect.Defense:
                        return "Evade the next attack";
                    case Effect.Heal:
                        return $"Heal {Magnitude} HP";
                    default:
                        return "";
                }
            }
        }
    }
}
