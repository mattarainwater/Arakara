using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Models
{
    public class Ability : Container, IAspect
    {
        public IContainer Container { get; set; }
        public Move Action { get { return Container as Move; } }
        public string ActionName { get; set; }
        public object UserInfo { get; set; }
    }
}
