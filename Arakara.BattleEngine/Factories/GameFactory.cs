using Arakara.BattleEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Tenswee.Common.Containers.States;

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

            // Add Other
            game.AddAspect<StateMachine>();
            //game.AddAspect<GlobalGameState>();

            return game;
        }
    }
}
