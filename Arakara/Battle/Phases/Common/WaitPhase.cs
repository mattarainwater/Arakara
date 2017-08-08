using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Nez;

namespace Arakara.Battle.Phases.Common
{
    public class WaitPhase : Phase
    {
        public WaitPhase(BattleActor actor) 
            : base(actor)
        {
        }

        protected override void update()
        {
            Core.schedule(1f, t => {
                IsFinished = true;
            });
        }
    }
}
