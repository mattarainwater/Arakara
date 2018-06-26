using Arakara.BattleEngine.Actions;
using Arakara.BattleEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Tenswee.Common.Notifications;

namespace Arakara.BattleEngine.Systems
{
    public class AbilitySystem : Aspect, IObservable
    {
        public void Awake()
        {
            this.AddObserver(OnPerformAbilityAction, Global.PerformNotification<AbilityAction>(), Container);
        }

        public void Destroy()
        {
            this.RemoveObserver(OnPerformAbilityAction, Global.PerformNotification<AbilityAction>(), Container);
        }

        void OnPerformAbilityAction(object sender, object args)
        {
            var action = args as AbilityAction;
            var type = Type.GetType(action.Ability.ActionName);
            var instance = Activator.CreateInstance(type) as GameAction;
            var loader = instance as IAbilityLoader;
            if (loader != null)
                loader.Load(Container, action.Ability);
            Container.AddReaction(instance);
        }
    }
}
