using Arakara.BattleEngine.States;
using Arakara.BattleEngine.Systems;
using Arakara.BattleEngine.Systems.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Tenswee.Common.States;

namespace Arakara.BattleEngine.Factories
{
    public static class GameFactory
    {
        public static Container Create()
        {
            Container game = new Container();

            // Add Systems
            game.AddAspect<ActionSystem>();
            game.AddAspect<DataSystem>();
            game.AddAspect<TurnSystem>();
            game.AddAspect<TargetSystem>();
            game.AddAspect<DamageSystem>();
            game.AddAspect<AbilitySystem>();
            game.AddAspect<AITurnSystem>();
            game.AddAspect<BattleMoveSystem>();

            // Add Other
            game.AddAspect<StateMachine>();
            game.AddAspect<GlobalGameState>();

            return game;
        }
    }
}
