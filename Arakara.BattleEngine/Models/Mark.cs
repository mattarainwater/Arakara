using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.BattleEngine.Models
{
    public class Mark
    {
        public Side Side { get; set; }

        public Mark(Side side)
        {
            Side = side;
        }
    }
}
