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
    public class BattleMoveSystem : Aspect, IObservable
    {
        public void Awake()
        {
            this.AddObserver(OnPerformAbilityAction, Global.PerformNotification<BattleMoveAction>(), Container);
        }

        public void Destroy()
        {
            this.RemoveObserver(OnPerformAbilityAction, Global.PerformNotification<BattleMoveAction>(), Container);
        }

        void OnPerformAbilityAction(object sender, object args)
        {
            var action = args as BattleMoveAction;
            var ability = action.Move.GetAspect<Ability>();
            var target = action.Move.GetAspect<Target>();
            if(target != null)
            {
                ability.AddOrUpdate(target);
            }
            var targetSelector = action.Move.GetAspect<ITargetSelector>();
            ability.AddOrUpdate(targetSelector);
            Container.AddReaction(new AbilityAction(ability));
        }
    }
}
