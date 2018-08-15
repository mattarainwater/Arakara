using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Notifications;
using Tenswee.Common.States;

namespace Arakara.BattleEngine.States
{
    public class BattleOverState : BaseState
    {
        public const string EnterNotification = "BattleOverState.EnterNotification";
        public const string ExitNotification = "BattleOverState.ExitNotification";

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
