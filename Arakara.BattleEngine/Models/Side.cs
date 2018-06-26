using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.BattleEngine.Models
{
    public enum Side
    {
        None = 0,
        Ally = 1 << 0,
        Enemy = 1 << 1,
        Any = Ally | Enemy
    }

    public static class SideExtensions
    {
        public static bool Contains(this Side source, Side target)
        {
            return (source & target) == target;
        }
    }
}
