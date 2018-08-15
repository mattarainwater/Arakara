using Arakara.BattleEngine.Systems.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Notifications;
using Tenswee.Common.States;

namespace Arakara.BattleEngine.States
{
    public class EnemyTurnState : BaseState
    {
        public const string EnterNotification = "EnemyTurnState.EnterNotification";
        public const string ExitNotification = "EnemyTurnState.ExitNotification";

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
