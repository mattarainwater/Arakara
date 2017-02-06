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
        private BattleContainer _battle;

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

            var battleEntity = createEntity("battle");
            _battle = new BattleContainer();
            battleEntity.addComponent(_battle);
        }

        public override void onStart()
        {
            var knight = contentManager.Load<Texture2D>("Knight");

            _battle.AddBattleEntity(MakeMC());
            _battle.AddBattleEntity(MakeEnemy(knight));
            _battle.AddBattleEntity(MakeEnemy(knight));

            _battle.ToggleBattleActive();
        }

        private Entity MakeMC()
        {
            var mainCharacterEntity = createEntity("mc");
            var mainCharacterActor = new DeckBuilderActor("Main Character", 100, new Faction
            {
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

            return mainCharacterEntity;
        }

        private Entity MakeEnemy(Texture2D image)
        {
            var subtextures = Subtexture.subtexturesFromAtlas(image, 98, 40);
            var enemyEntity = createEntity("enemy");
            var aIActor = new AIActor("Guard", 100, new Faction
            {
                FactionName = "Enemies",
                Id = 2
            });
            enemyEntity.addComponent(aIActor);
            var enemySprite = new Sprite(subtextures[0]);
            enemySprite.originNormalized = new Vector2(0, 0);
            enemyEntity.addComponent(enemySprite);

            return enemyEntity;
        }
    }
}
