using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Models;
using Arakara.BattleEngine.Models.AI;
using Arakara.BattleEngine.Systems;
using Tenswee.Common.Containers;
using Tenswee.Common.Notifications;
using Tenswee.Common.States;

namespace Arakara.BattleEngine.States
{
    public class GlobalGameState : Aspect, IObservable
    {
        private Actor _actorAtLastSequenceEnd;

        public void Awake()
        {
            this.AddObserver(OnBeginSequence, ActionSystem.BeginSequenceNotification);
            this.AddObserver(OnCompleteAllActions, ActionSystem.CompleteNotification);
        }

        public void Destroy()
        {
            this.RemoveObserver(OnBeginSequence, ActionSystem.BeginSequenceNotification);
            this.RemoveObserver(OnCompleteAllActions, ActionSystem.CompleteNotification);
        }

        void OnBeginSequence(object sender, object args)
        {
            Container.ChangeState<SequenceState>();
        }

        void OnCompleteAllActions(object sender, object args)
        {
            var battle = Container.GetBattle();
            var firstActor = battle.CurrentActor;
            if(firstActor == _actorAtLastSequenceEnd)
            {
                if(firstActor is AIActor)
                {
                    Container.GetAspect<TurnSystem>().ChangeTurn();
                }
                else
                {
                    Container.ChangeState<WaitingForInputState>();
                }
            }
            else
            {
                this.PostNotification(firstActor.GetType().FullName + ".startTurn", firstActor);
            }
            _actorAtLastSequenceEnd = firstActor;
        }
    }
}
