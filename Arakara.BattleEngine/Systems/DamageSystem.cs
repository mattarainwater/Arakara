using Arakara.BattleEngine.Actions;
using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Models;
using Tenswee.Common.Containers;
using Tenswee.Common.Notifications;

namespace Arakara.BattleEngine.Systems
{
    public class DamageSystem : Aspect, IObservable
    {
        public void Awake()
        {
            this.AddObserver(OnPerformDamageAction, Global.PerformNotification<DamageAction>(), Container);
        }

        public void Destroy()
        {
            this.RemoveObserver(OnPerformDamageAction, Global.PerformNotification<DamageAction>(), Container);
        }

        void OnPerformDamageAction(object sender, object args)
        {
            var action = args as DamageAction;
            foreach (Actor target in action.Targets)
            {
                target.CurrentHP -= action.Amount;
            }
        }
    }
}
