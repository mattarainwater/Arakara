using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Battle;

namespace Arakara.Components
{
    public class BattleContainer : Component, IUpdatable
    {
        private int _screenWidth;
        private int _screenHeight;
        private int _allyFactionStartingX;
        private int _enemyFactionStartingX;
        private int _entityYPos;

        private bool _firstInitialized;

        public BattleController Controller { get; set; }
        public List<Entity> BattleEntities { get; set; }

        public BattleContainer()
        {
            Controller = new BattleController();
            BattleEntities = new List<Entity>();
        }

        public override void onAddedToEntity()
        {
            _screenWidth = entity.scene.sceneRenderTargetSize.X;
            _screenHeight = entity.scene.sceneRenderTargetSize.Y;

            _allyFactionStartingX = 125;
            _enemyFactionStartingX = (_screenWidth / 2) + 75;

            _entityYPos = _screenHeight / 2;
        }

        public void update()
        {
            if(!_firstInitialized)
            {
                InitializeEntitiesPosition();
            }
            Controller.Update();
            _firstInitialized = true;
            //entity.scene.camera.entity.removeComponent<FollowCamera>();
            //entity.scene.camera.entity.addComponent(new FollowCamera(Controller.CurrentActor.entity));
        }

        public void AddBattleEntity(Entity entity)
        {
            var actor = entity.getComponent<BattleActor>();
            if(actor != null)
            {
                if(_firstInitialized)
                {
                    entity.transform.position = GetPositionForEntity(actor.Faction);
                }
                entity.addComponent(new UpdatableText(Graphics.instance.bitmapFont, new Vector2(0, 50), Color.Red));
                BattleEntities.Add(entity);
                Controller.AddActor(actor);
            }
            else
            {
                throw new ArgumentException("No BattleActor on entity " + entity.name);
            }
        }

        private void InitializeEntitiesPosition()
        {
            foreach (var entity in BattleEntities)
            {
                var actor = entity.getComponent<BattleActor>();
                entity.transform.position = GetPositionForEntity(actor.Faction);
            }
        }

        private Vector2 GetPositionForEntity(Faction faction)
        {
            if(faction.Id == 1)
            {
                return new Vector2(_allyFactionStartingX, _entityYPos);
            }
            else
            {
                var pos = new Vector2(_enemyFactionStartingX, _entityYPos);
                _enemyFactionStartingX += 100;
                return pos;
            }
        }

        public void ToggleBattleActive()
        {
            Controller.IsActive = true;
        }
    }
}
