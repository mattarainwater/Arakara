using Arakara.BattleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.BattleEngine.Actions
{
    public class AbilityAction : GameAction
    {
        public Ability Ability { get; set; }

        public AbilityAction(Ability ability)
        {
            Ability = ability;
        }
    }
}
