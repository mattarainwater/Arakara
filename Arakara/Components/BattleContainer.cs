using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Battle;
using Arakara.Common;

namespace Arakara.Components
{
    public class BattleContainer : Component, IUpdatable
    {
        private int _screenWidth;
        private int _screenHeight;
        private int _allyFactionStartingX;
        private int _enemyFactionStartingX;
        private int _marginLeftRight;
        private int _entityYPos;
        private int _halfWayPoint;
        private int _spacing;

        public BattleController Controller { get; set; }

        public BattleContainer(int screenWidth, int screenHeight)
        {
            Controller = new BattleController();
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            _halfWayPoint = _screenWidth / 2;
            _entityYPos = _screenHeight / 3;
            _marginLeftRight = _screenWidth / 20;
            _spacing = _screenWidth / 40;

            _allyFactionStartingX = _halfWayPoint - _marginLeftRight - DimensionConstants.CHARACTER_WIDTH;
            _enemyFactionStartingX = _halfWayPoint + _marginLeftRight;
        }

        public void update()
        {
            Controller.Update();
        }

        public void AddBattleEntity(Entity entity)
        {
            var actor = entity.getComponent<BattleActor>();
            if(actor != null)
            {
                entity.transform.position = GetPositionForEntity(actor);
                //entity.addComponent(new HealthBar(actor));
                entity.addComponent(new UpdatableText(Graphics.instance.bitmapFont, new Vector2(0, DimensionConstants.CHARACTER_HEIGHT + 10), Color.Red, () => "Current HP: " + actor.CurrentHP));
                entity.addComponent(new UpdatableText(Graphics.instance.bitmapFont, new Vector2(0, DimensionConstants.CHARACTER_HEIGHT + 20), Color.Red, () => "Time Until Turn: " + actor.TimeUntilTurn));
                entity.addCollider(new BoxCollider(0, 0, DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT));
                Controller.AddActor(actor);
            }
            else
            {
                throw new ArgumentException("No BattleActor on entity " + entity.name);
            }
        }

        private void InitializeEntitiesPosition()
        {
            foreach (var actor in Controller.Actors)
            {
                var entity = actor.entity;
                entity.transform.position = GetPositionForEntity(actor);
            }
        }

        private Vector2 GetPositionForEntity(BattleActor actor)
        {
            if(actor.Faction.Id == 1)
            {
                var numAllies = Controller.Actors.Count(x => x.Faction.Id == actor.Faction.Id);
                var pos = new Vector2(_allyFactionStartingX, _entityYPos);
                _allyFactionStartingX += DimensionConstants.CHARACTER_WIDTH + _spacing;
                return pos;
            }
            else
            {
                var numEnemies = Controller.Actors.Count(x => x.Faction.Id == actor.Faction.Id);
                var pos = new Vector2(_enemyFactionStartingX, _entityYPos);
                _enemyFactionStartingX += DimensionConstants.CHARACTER_WIDTH + _spacing;
                return pos;
            }
        }

        public void ToggleBattleActive()
        {
            Controller.IsActive = true;
        }
    }
}
