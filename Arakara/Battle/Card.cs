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
        public string Text { get; set; }
        public Effect Effect { get; set; }
        public Targeting Targeting { get; set; }
        public int Magnitude { get; set; }
        public int Delay { get; set; }
        public Grade Grade { get; set; }
    }
}
