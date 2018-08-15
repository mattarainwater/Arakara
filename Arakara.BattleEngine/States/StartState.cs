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
    public class StartState : BaseState
    {
        public const string EnterNotification = "StartState.EnterNotification";
        public const string ExitNotification = "StartState.ExitNotification";

        public override void Enter()
        {
            this.PostNotification(EnterNotification);
            var battle = Container.GetBattle();
            var firstActor = battle.CurrentActor;
            if(firstActor is AIActor)
            {
                Container.ChangeState<EnemyTurnState>();
            }
            else
            {
                Container.ChangeState<WaitingForInputState>();
            }
        }

        public override void Exit()
        {
            this.PostNotification(ExitNotification);
        }
    }
}
