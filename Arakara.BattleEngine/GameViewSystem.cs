using Arakara.BattleEngine.Interfaces;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Arakara.BattleEngine.Systems;
using Arakara.BattleEngine.Factories;
using Arakara.BattleEngine.States;
using Tenswee.Common.States;
using Arakara.BattleEngine.Models;
using Arakara.BattleEngine.Models.AI;
using Arakara.BattleEngine.Actions;
using Arakara.BattleEngine.Models.TargetSelectors;

namespace Arakara.BattleEngine
{
    public class GameViewSystem : Component, IAspect, IUpdatable
    {
        public IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = GameFactory.Create();
                    _container.AddAspect(this);
                }
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        IContainer _container;

        ActionSystem actionSystem;

        public override void initialize()
        {
            Container.Awake();
            actionSystem = Container.GetAspect<ActionSystem>();
            Temp_SetupDefaultBattle();
            Container.GetAspect<TurnSystem>().ChangeTurn(0);
        }

        public override void onAddedToEntity()
        {
            Container.ChangeState<PlayerIdleState>();
        }

        public void update()
        {
            actionSystem.Update();
        }

        void Temp_SetupDefaultBattle()
        {
            var match = Container.GetBattle();
            match.Actors = new List<Actor>();
            match.Actors.Add(Temp_MakeAIActor(0));
            match.Actors.Add(Temp_MakeAIActor(1));
            // todo make actors
        }

        AIActor Temp_MakeAIActor(int index)
        {
            var move = new Move
            {
                Name = "Attack",
                Text = "He attacks!"
            };
            move.AddAspect(new Target() {
                Allowed = new Mark(Side.Enemy),
                Preferred = new Mark(Side.Enemy),
                Required = true,
            });
            move.AddAspect(new ManualTarget() as ITargetSelector);
            move.AddAspect(new Ability()
            {
                ActionName = typeof(DamageAction).AssemblyQualifiedName,
                UserInfo = 3
            });

            return new AIActor(index)
            {
                CurrentHP = 10,
                MaxHP = 10,
                Name = "Guy " + index,
                FactionId = index,
                Moves = new List<Move>
                {
                    move
                }
            };
        }
    }
}
