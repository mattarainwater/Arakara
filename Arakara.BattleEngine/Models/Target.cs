using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Models
{
    public class Target : Aspect
    {
        public bool Required { get; set; }
        public Mark Preferred { get; set; }
        public Mark Allowed { get; set; }
        public Actor Selected { get; set; }
    }
}
