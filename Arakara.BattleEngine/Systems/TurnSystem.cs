using Arakara.BattleEngine.Actions;
using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Tenswee.Common.Notifications;

namespace Arakara.BattleEngine.Systems
{
    public class TurnSystem : Aspect, IObservable
    {
        public void ChangeTurn()
        {
            var nextIndex = GetNextIndex();
            ChangeTurn(nextIndex);
        }

        public void ChangeTurn(int index)
        {
            var action = new ChangeTurnAction(index);
            Container.Perform(action);
        }

        public void Awake()
        {
            this.AddObserver(OnPerformChangeTurn, Global.PerformNotification<ChangeTurnAction>(), Container);
        }

        public void Destroy()
        {
            this.RemoveObserver(OnPerformChangeTurn, Global.PerformNotification<ChangeTurnAction>(), Container);
        }

        void OnPerformChangeTurn(object sender, object args)
        {
            var action = args as ChangeTurnAction;
            var battle = Container.GetBattle();
            battle.CurrentActorIndex = action.TargetActorIndex;
        }

        public int GetNextIndex()
        {
            var battle = Container.GetBattle();
            var currentIndex = battle.CurrentActorIndex;
            if(currentIndex + 1 == battle.Actors.Count())
            {
                return 0;
            }
            return currentIndex + 1;
        }
    }
}
