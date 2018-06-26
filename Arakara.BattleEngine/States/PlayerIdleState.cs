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
    public class PlayerIdleState : BaseState
    {
        public const string EnterNotification = "PlayerIdleState.EnterNotification";
        public const string ExitNotification = "PlayerIdleState.ExitNotification";

        public override void Enter()
        {
            this.PostNotification(EnterNotification);
            var battle = Container.GetBattle();
            if(battle.CurrentActor is AIActor)
            {
                Container.GetAspect<TurnSystem>().ChangeTurn();
            }
        }

        public override void Exit()
        {
            this.PostNotification(ExitNotification);
        }
    }
}
