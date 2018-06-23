using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Systems.Enemy
{
    public class EnemyTurnSystem : Aspect
    {
        public void TakeTurn()
        {
            if (performAction())
                return;
            Container.GetAspect<TurnSystem>().ChangeTurn();
        }

        private bool performAction()
        {
            var battle = Container.GetBattle();
            var currentEnemy = battle.CurrentActor;
            Container.Perform(action);
            return true;
        }
    }
}
