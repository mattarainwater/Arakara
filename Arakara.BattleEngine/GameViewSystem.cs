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
using Tenswee.Common.Containers.States;
using Arakara.BattleEngine.States;

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
            // todo make actors
        }
    }
}
