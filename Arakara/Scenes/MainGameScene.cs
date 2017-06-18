using Arakara.Battle;
using Arakara.Battle.AI;
using Arakara.Battle.Card;
using Arakara.Battle.Effects;
using Arakara.Battle.Statuses;
using Arakara.Common;
using Arakara.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
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
            setDesignResolution(DimensionConstants.SCREEN_WIDTH, DimensionConstants.SCREEN_HEIGHT, SceneResolutionPolicy.NoBorderPixelPerfect);
            clearColor = Color.WhiteSmoke;

            var battleEntity = createEntity("battle");
            _battle = new BattleContainer(DimensionConstants.SCREEN_WIDTH, DimensionConstants.SCREEN_HEIGHT);
            battleEntity.addComponent(_battle);

            //var tiledEntity = createEntity("tiled-map-entity");
            //var tiledmap = contentManager.Load<TiledMap>("background");
            //var tiledMapComponent = tiledEntity.addComponent(new TiledMapComponent(tiledmap));
            //tiledMapComponent.renderLayer = 10;
            //tiledEntity.transform.scale = new Vector2(.5f, .5f);
        }

        public override void onStart()
        {
            _battle.AddBattleEntity(MakeMC(), this);

            _battle.AddBattleEntity(MakeKnight(), this);
            _battle.AddBattleEntity(MakeKnight(), this);
            _battle.AddBattleEntity(MakeNecromancer(), this);

            _battle.ToggleBattleActive();
        }

        private Entity MakeMC()
        {
            var mainCharacterEntity = createEntity("mc");
            var mainCharacterActor = new DeckBuilderActor<Animations>("Prisca", 10000, new Faction
            {
                FactionName = "PC",
                Id = 1
            }, new List<Card<Animations>>()
            {
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Poison",
                        Effect = new ApplyStatusEffect(new PoisonStatus(5, 10, 20)),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1
                    }
                },
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Poison",
                        Effect = new ApplyStatusEffect(new PoisonStatus(5, 10, 20)),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1
                    }
                },
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Poison",
                        Effect = new ApplyStatusEffect(new PoisonStatus(5, 10, 20)),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1
                    }
                },
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Poison",
                        Effect = new ApplyStatusEffect(new PoisonStatus(5, 10, 20)),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1
                    }
                },
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Poison",
                        Effect = new ApplyStatusEffect(new PoisonStatus(5, 10, 20)),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1
                    }
                },
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5, 10),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1
                    }
                },
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5, 10),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1
                    }
                },
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5, 10),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1
                    }
                },
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Heavy Slash",
                        Effect = new DamageEffect(10, 20),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1
                    }
                },
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Heavy Slash",
                        Effect = new DamageEffect(10, 20),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1
                    }
                },
                new Card<Animations>
                {
                    Action = new BattleAction<Animations>
                    {
                        Name = "Potion",
                        Effect = new HealEffect(40),
                        Targeting = Targeting.Self,
                        Animation = Animations.Support1
                    }
                },
            }, .25f, .25f, 200f, Animations.Idle);

            mainCharacterEntity.addComponent(mainCharacterActor);

            var texture = contentManager.Load<Texture2D>("prisca_big");
            var subtextures = Subtexture.subtexturesFromAtlas(texture, 64, 64);
            var sprite = new Sprite<Animations>(subtextures[0]);
            mainCharacterEntity.addComponent(sprite);
            var attackAnimationFrames = new List<Subtexture>
            {
                subtextures[0],
                subtextures[1],
                subtextures[2],
                subtextures[2],
                subtextures[2],
                subtextures[3],
                subtextures[3],
                subtextures[3],
                subtextures[4],
            };
            var idleAnimationFrames = new List<Subtexture>
            {
                subtextures[0],
                subtextures[1],
                subtextures[2],
                subtextures[1],
                subtextures[0]
            };
            var potionAnimationFrames = new List<Subtexture>
            {
                subtextures[0],
                subtextures[5],
                subtextures[6],
                subtextures[6],
                subtextures[6],
                subtextures[7],
                subtextures[0]
            };
            sprite.addAnimation(Animations.Attack1, new SpriteAnimation(attackAnimationFrames) { loop = false, fps = 30 }, Vector2.Zero);
            sprite.addAnimation(Animations.Idle, new SpriteAnimation(idleAnimationFrames) { loop = true }, Vector2.Zero);
            sprite.addAnimation(Animations.Support1, new SpriteAnimation(potionAnimationFrames) { loop = false }, Vector2.Zero);
            sprite.flipX = true;
            sprite.setOrigin(Vector2.Zero);

            return mainCharacterEntity;
        }

        private Entity MakeKnight()
        {
            var enemyEntity = createEntity("enemy");

            var texture = contentManager.Load<Texture2D>("guard_big");
            var subtextures = Subtexture.subtexturesFromAtlas(texture, 64, 64);
            var sprite = new Sprite<Animations>(subtextures[0]);
            enemyEntity.addComponent(sprite);
            var animationFrames = new List<Subtexture>
            {
                subtextures[0],
                subtextures[1],
                subtextures[1],
                subtextures[1],
                subtextures[0]
            };
            sprite.addAnimation(Animations.Attack1, new SpriteAnimation(animationFrames) { loop = false }, Vector2.Zero);
            sprite.setOrigin(Vector2.Zero);

            var aIActor = new AIActor<Animations>("Guard", 1, new Faction
                {
                    FactionName = "Enemies",
                    Id = 2
                },
                new List<BattleAction<Animations>>
                {
                    new BattleAction<Animations>
                    {
                        Animation = Animations.Attack1,
                        Effect = new DamageEffect(10,  15),
                        Name = "Stab",
                        Targeting = Targeting.Enemies
                    },
                    new BattleAction<Animations>
                    {
                        Animation = Animations.Attack1,
                        Effect = new DamageEffect(20, 30),
                        Name = "Super Stab",
                        Targeting = Targeting.Enemies
                    }
                },
                new RandomAIDecider(), 0f, 0f, 100f
            );
            enemyEntity.addComponent(aIActor);
            return enemyEntity;
        }

        private Entity MakeNecromancer()
        {
            var enemyEntity = createEntity("enemy");

            var texture = contentManager.Load<Texture2D>("necromancer_big");
            var subtextures = Subtexture.subtexturesFromAtlas(texture, 64, 64);
            var sprite = new Sprite<Animations>(subtextures[0]);
            enemyEntity.addComponent(sprite);
            var animationFrames = new List<Subtexture>
            {
                subtextures[0],
                subtextures[1],
                subtextures[2],
                subtextures[2],
                subtextures[1],
                subtextures[0]
            };
            sprite.addAnimation(Animations.Attack1, new SpriteAnimation(animationFrames) { loop = false }, Vector2.Zero);
            sprite.setOrigin(Vector2.Zero);

            var aIActor = new AIActor<Animations>("Necromancer", 10000, new Faction
            {
                FactionName = "Enemies",
                Id = 2
            },
                new List<BattleAction<Animations>>
                {
                    new BattleAction<Animations>
                    {
                        Animation = Animations.Attack1,
                        Effect = new DamageEffect(10, 20),
                        Name = "Necro Bolt",
                        Targeting = Targeting.Enemies
                    },
                },
                new RandomAIDecider(), 0f, 0f, 50f
            );
            enemyEntity.addComponent(aIActor);

            return enemyEntity;
        }
    }
}
