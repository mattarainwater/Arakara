using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Card
{
    public class Card<TEnum> where TEnum : struct, IComparable, IFormattable
    {
        public BattleAction<TEnum> Action { get; set; }
        public int BuyValue { get; set; }
        public int Cost { get; set; }
    }
}
