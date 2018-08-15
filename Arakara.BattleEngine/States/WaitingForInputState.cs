using Arakara.BattleEngine.Models.AI;
using Arakara.BattleEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Notifications;
using Tenswee.Common.States;

namespace Arakara.BattleEngine.States
{
    public class WaitingForInputState : BaseState
    {
        public const string EnterNotification = "WaitingForInputState.EnterNotification";
        public const string ExitNotification = "WaitingForInputState.ExitNotification";

        public override void Enter()
        {
            this.PostNotification(EnterNotification);
        }

        public override void Exit()
        {
            this.PostNotification(ExitNotification);
        }
    }
}
