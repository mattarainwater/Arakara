using Arakara.BattleEngine.Actions;
using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Models;
using Arakara.BattleEngine.Models.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Tenswee.Common.Extensions;
using Tenswee.Common.Notifications;

namespace Arakara.BattleEngine.Systems.AI
{
    public class AITurnSystem : Aspect, IObservable
    {
        public void Awake()
        {
            this.AddObserver(PerformAction, Global.PerformNotification<ChangeTurnAction>(), Container);
        }

        public void Destroy()
        {
            this.RemoveObserver(PerformAction, Global.PerformNotification<ChangeTurnAction>(), Container);
        }

        void PerformAction(object sender, object args)
        {
            var action = args as ChangeTurnAction;
            var battle = Container.GetBattle();
            var currentActor = battle.Actors[action.TargetActorIndex];
            if(currentActor is AIActor)
            {
                performAction(currentActor as AIActor);
            }
        }

        private void performAction(AIActor actor)
        {
            var randomAction = actor.Moves.PickRandom();
            var target = randomAction.GetAspect<Target>();
            var targetSystem = Container.GetAspect<TargetSystem>();
            targetSystem.AutoTarget(randomAction, Control.Computer, actor.FactionId);
            var ability = randomAction.GetAspect<Ability>();
            Container.AddReaction(new BattleMoveAction(randomAction, actor.FactionId));
        }
    }
}
