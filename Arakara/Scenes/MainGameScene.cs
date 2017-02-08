using Arakara.Battle;
using Arakara.Common;
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
            addRenderer(new RenderLayerExcludeRenderer(0));

            clearColor = Color.WhiteSmoke;

            var battleEntity = createEntity("battle");
            _battle = new BattleContainer(Screen.width, Screen.height);
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
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Heavy Slash",
                        Effect = new DamageEffect(10),
                        Speed = 10,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Heavy Slash",
                        Effect = new DamageEffect(10),
                        Speed = 10,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Heavy Slash",
                        Effect = new DamageEffect(10),
                        Speed = 10,
                        Targeting = Targeting.Enemies
                    }
                },
            });
            var mcVerts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, 0),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT),
                new Vector2(0, DimensionConstants.CHARACTER_HEIGHT),
            };
            mainCharacterEntity.addComponent(mainCharacterActor);
            mainCharacterEntity.addComponent(new SimplePolygon(mcVerts, Color.Blue));

            return mainCharacterEntity;
        }

        private Entity MakeEnemy(Texture2D image)
        {
        //    var subtextures = Subtexture.subtexturesFromAtlas(image, 98, 40);
        //    subtextures.ForEach(x => x.center = Vector2.Zero);
            var enemyEntity = createEntity("enemy");

            //var animation = enemyEntity.addComponent(new Sprite<Animations>(subtextures[0]));
            //animation.addAnimation(Animations.Idle, new SpriteAnimation(new List<Subtexture>()
            //{
            //    subtextures[0],
            //    subtextures[20],
            //}));
            //var attackAnimation = new SpriteAnimation(subtextures.Take(21).ToList());
            //attackAnimation.loop = false;
            //animation.originNormalized = Vector2.Zero;
            //animation.addAnimation(Animations.Attack, attackAnimation);

            var mcVerts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, 0),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT),
                new Vector2(0, DimensionConstants.CHARACTER_HEIGHT),
            };
            enemyEntity.addComponent(new SimplePolygon(mcVerts, Color.Red));
            var aIActor = new AIActor<Animations>("Guard", 100, new Faction
                {
                    FactionName = "Enemies",
                    Id = 2
                },
                new List<BattleAction<Animations>>
                {
                    new BattleAction<Animations>
                    {
                        Animation = Animations.Attack,
                        Effect = new DamageEffect(10),
                        Name = "Stab",
                        Speed = 10,
                        Targeting = Targeting.Enemies
                    },
                    new BattleAction<Animations>
                    {
                        Animation = Animations.Attack,
                        Effect = new DamageEffect(50),
                        Name = "Super Stab",
                        Speed = 70,
                        Targeting = Targeting.Enemies
                    }
                },
                new RandomAIDecider()
            );
            enemyEntity.addComponent(aIActor);

            return enemyEntity;
        }
    }
}
