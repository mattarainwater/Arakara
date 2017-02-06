using Arakara.Battle;
using Arakara.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
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
            setDesignResolution(512, 256, Scene.SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.setSize(512 * 3, 256 * 3);
            addRenderer(new RenderLayerExcludeRenderer(0));

            clearColor = Color.WhiteSmoke;

            var controllerEntity = createEntity("controller");
            var controllerComponent = new BattleController();
            controllerEntity.addComponent(controllerComponent);

            var mainCharacterEntity = createEntity("mc", new Vector2(110, 150));
            var mainCharacterActor = new DeckBuilderActor("Main Character", 100, new Faction {
                FactionName = "PC",
                Id = 1
            }, new List<Card>()
            {
                new Card
                {
                    Delay =  5,
                    Effect = Battle.Effect.Attack,
                    Name = "Quick Slash",
                    Magnitude = 5,
                    Targeting = Targeting.Enemies,
                },
                new Card
                {
                    Delay =  5,
                    Effect = Battle.Effect.Attack,
                    Name = "Quick Slash",
                    Magnitude = 5,
                    Targeting = Targeting.Enemies,
                },
                new Card
                {
                    Delay =  5,
                    Effect = Battle.Effect.Attack,
                    Name = "Quick Slash",
                    Magnitude = 5,
                    Targeting = Targeting.Enemies,
                },
                new Card
                {
                    Delay =  5,
                    Effect = Battle.Effect.Attack,
                    Name = "Quick Slash",
                    Magnitude = 5,
                    Targeting = Targeting.Enemies,
                },
                new Card
                {
                    Delay =  10,
                    Effect = Battle.Effect.Attack,
                    Name = "Heavy Slash",
                    Magnitude = 10,
                    Targeting = Targeting.Enemies,
                },
                new Card
                {
                    Delay =  10,
                    Effect = Battle.Effect.Attack,
                    Name = "Heavy Slash",
                    Magnitude = 10,
                    Targeting = Targeting.Enemies,
                },
                new Card
                {
                    Delay =  20,
                    Effect = Battle.Effect.Heal,
                    Name = "Pocket Potion",
                    Magnitude = 20,
                    Targeting = Targeting.Self,
                },
                new Card
                {
                    Delay =  20,
                    Effect = Battle.Effect.Heal,
                    Name = "Pocket Potion",
                    Magnitude = 20,
                    Targeting = Targeting.Self
                },
                new Card
                {

                    Delay =  5,
                    Effect = Battle.Effect.Defense,
                    Name = "Evade",
                    Magnitude = 10,
                    Targeting = Targeting.Self,
                },
            });
            var mcVerts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(75, 0),
                new Vector2(75, 40),
                new Vector2(0, 40),
            };
            mainCharacterEntity.addComponent(mainCharacterActor);
            mainCharacterEntity.addComponent(new SimplePolygon(mcVerts, Color.Blue));
            mainCharacterEntity.addComponent(new UpdatableText(Graphics.instance.bitmapFont, new Vector2(0, 50), Color.Red));


            var knight = contentManager.Load<Texture2D>("Knight");
            var subtextures = Subtexture.subtexturesFromAtlas(knight, 98, 40);

            var enemyEntity = createEntity("enemy1", new Vector2(400, 150));
            var aIActor = new AIActor("Guard", 100, new Faction
            {
                FactionName = "Enemies",
                Id = 2
            });
            enemyEntity.addComponent(aIActor);
            enemyEntity.addComponent(new UpdatableText(Graphics.instance.bitmapFont, new Vector2(0, 50), Color.Red));
            var enemySprite = new Sprite(subtextures[0]);
            enemySprite.originNormalized = new Vector2(0, 0);
            enemyEntity.addComponent(enemySprite);


            var enemyEntity2 = createEntity("enemy2", new Vector2(300, 150));
            var aIActor2 = new AIActor("Guard", 100, new Faction
            {
                FactionName = "Enemies",
                Id = 2
            });
            enemyEntity2.addComponent(aIActor2);
            enemyEntity2.addComponent(new UpdatableText(Graphics.instance.bitmapFont, new Vector2(0, 50), Color.Red));
            var enemy2Sprite = new Sprite(subtextures[0]);
            enemy2Sprite.originNormalized = new Vector2(0, 0);
            enemyEntity2.addComponent(enemy2Sprite);

            controllerComponent.Actors.Add(mainCharacterActor);
            controllerComponent.Actors.Add(aIActor);
            controllerComponent.Actors.Add(aIActor2);
        }
    }
}
