using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Models
{
    public class Move : Container
    {
        public Move()
        {
        }

        public string Name { get; set; }
        public string Text { get; set; }
    }
}
