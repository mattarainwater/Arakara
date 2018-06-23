using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Models
{
    public class BattleAction : Container
    {
        public BattleAction()
        {
            OrderOfPlay = int.MaxValue;
        }

        public int OrderOfPlay { get; set; }
    }
}
