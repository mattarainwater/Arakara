using Arakara.Battle;
using Arakara.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Scenes
{
    public class MainGameScene : Scene
    {
        public MainGameScene()
            : base()
        {
        }

        public override void initialize()
        {
            addRenderer(new RenderLayerExcludeRenderer(0));

            clearColor = Color.WhiteSmoke;

            var controllerEntity = createEntity("controller", new Vector2(0, 0));
            var controllerComponent = new BattleController();
            controllerEntity.addComponent(controllerComponent);

            var mainCharacterEntity = createEntity("mc", new Vector2(300, 300));
            var mainCharacterActor = new DeckBuilderActor(100, new Faction {
                FactionName = "PC",
                Id = 1
            });
            var mcVerts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(150, 0),
                new Vector2(150, 200),
                new Vector2(0, 200),
            };
            mainCharacterEntity.addComponent(mainCharacterActor);
            mainCharacterEntity.addComponent(new SimplePolygon(mcVerts, Color.Aquamarine));
            mainCharacterEntity.addComponent(new UpdatableText(Graphics.instance.bitmapFont, new Vector2(0, 210), Color.Red));
            //var mainCharacterTexture = content.Load<Texture2D>(Content.Shared.moon);
            //mainCharacterEntity.addComponent(new Sprite(mainCharacterTexture));

            var enemyEntity = createEntity("enemy", new Vector2(1000, 200));
            var aIActor = new AIActor(100, new Faction
            {
                FactionName = "Enemies",
                Id = 2
            });
            enemyEntity.addComponent(aIActor);
            //var moonTexture = content.Load<Texture2D>(Content.Shared.moon);
            //enemyEntity.addComponent(new Sprite(moonTexture));

            controllerComponent.Actors.Add(mainCharacterActor);
            controllerComponent.Actors.Add(aIActor);
        }
    }
}
