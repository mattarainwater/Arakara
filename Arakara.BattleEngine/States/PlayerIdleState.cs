using Arakara.BattleEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers.States;
using Tenswee.Common.Notifications;

namespace Arakara.BattleEngine.States
{
    public class PlayerIdleState : BaseState
    {
        public const string EnterNotification = "PlayerIdleState.EnterNotification";
        public const string ExitNotification = "PlayerIdleState.ExitNotification";

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
