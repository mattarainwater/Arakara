using Arakara.Battle;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class Card : Component
    {
        public BattleAction Action { get; set; }
        public int BuyValue { get; set; }
        public int Cost { get; set; }
    }
}
