using Arakara.BattleEngine.Actions;
using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Models;
using Arakara.BattleEngine.Models.AI;
using Arakara.BattleEngine.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Tenswee.Common.Extensions;
using Tenswee.Common.Notifications;
using Tenswee.Common.States;

namespace Arakara.BattleEngine.Systems.AI
{
    public class AITurnSystem : Aspect, IObservable
    {
        public void Awake()
        {
            this.AddObserver(StartTurn, typeof(AIActor).FullName + ".startTurn");
        }

        public void Destroy()
        {
            this.RemoveObserver(StartTurn, typeof(AIActor).FullName + ".startTurn");
        }

        private void StartTurn(object sender, object args)
        {
            Container.ChangeState<EnemyTurnState>();
            var battle = Container.GetBattle();
            var currentActor = battle.CurrentActor;
            performAction(currentActor as AIActor);
        }

        private void performAction(AIActor actor)
        {
            var randomAction = actor.Moves.PickRandom();
            var target = randomAction.GetAspect<Target>();
            var targetSystem = Container.GetAspect<TargetSystem>();
            targetSystem.AutoTarget(randomAction, Control.Computer, actor.FactionId);
            Container.Perform(new BattleMoveAction(randomAction, actor.FactionId));
        }
    }
}
