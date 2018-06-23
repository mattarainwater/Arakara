using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.BattleEngine.Models
{
    public class Energy
    {
        public const int MAX_SLOTS = 10;

        public int Spent { get; set; }
        public int Permanent { get; set; }
        public int Overloaded { get; set; }
        public int PendingOverloaded { get; set; }
        public int Temporary { get; set; }

        public int Unlocked
        {
            get
            {
                return Math.Min(Permanent + Temporary, MAX_SLOTS);
            }
        }

        public int Available
        {
            get
            {
                return Math.Min(Permanent + Temporary - Spent, MAX_SLOTS) - Overloaded;
            }
        }
    }
}
