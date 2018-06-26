using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Systems;
using Tenswee.Common.Containers;
using Tenswee.Common.Notifications;
using Tenswee.Common.States;

namespace Arakara.BattleEngine.States
{
    public class GlobalGameState : Aspect, IObservable
    {
        public void Awake()
        {
            this.AddObserver(OnBeginSequence, ActionSystem.beginSequenceNotification);
            this.AddObserver(OnCompleteAllActions, ActionSystem.completeNotification);
        }

        public void Destroy()
        {
            this.RemoveObserver(OnBeginSequence, ActionSystem.beginSequenceNotification);
            this.RemoveObserver(OnCompleteAllActions, ActionSystem.completeNotification);
        }

        void OnBeginSequence(object sender, object args)
        {
            Container.ChangeState<SequenceState>();
        }

        void OnCompleteAllActions(object sender, object args)
        {
            Container.ChangeState<PlayerIdleState>();
        }
    }
}
