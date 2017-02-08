using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle
{
    public class BattleAction
    {
        public ActionEffect Effect { get; set; }
        public Targeting Targeting { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }
    }

    public class BattleAction<TEnum> : BattleAction where TEnum : struct, IComparable, IFormattable
    {
        public TEnum Animation { get; set; }
    }
}
