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
            var mainCharacterActor = new DeckBuilderActor("Prisca", 100, new Faction
            {
                FactionName = "PC",
                Id = 1
            }, new List<Card>()
            {
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Poison",
                        Effect = new ApplyStatusEffect(new PoisonStatus(5, 10, 20)),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5, 10),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5, 10),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5, 10),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5, 10),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5, 10),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Quick Slash",
                        Effect = new DamageEffect(5, 10),
                        Speed = 5,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Heavy Slash",
                        Effect = new DamageEffect(10, 20),
                        Speed = 10,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Heavy Slash",
                        Effect = new DamageEffect(10, 20),
                        Speed = 10,
                        Targeting = Targeting.Enemies
                    }
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Potion",
                        Effect = new HealEffect(40),
                        Speed = 10,
                        Targeting = Targeting.Self
                    }
                },
            }, .25f, .25f);

            mainCharacterEntity.addComponent(mainCharacterActor);
            var subtexture = new Subtexture(contentManager.Load<Texture2D>("prisca_big"), new Rectangle(0, 0, 64, 64));
            var sprite = new Sprite(subtexture);
            sprite.flipX = true;
            sprite.setOrigin(Vector2.Zero);
            mainCharacterEntity.addComponent(sprite);

            return mainCharacterEntity;
        }

        private Entity MakeKnight()
        {
            var enemyEntity = createEntity("enemy");

            var sprite = new Sprite(contentManager.Load<Texture2D>("guard_big"));
            sprite.setOrigin(Vector2.Zero);
            enemyEntity.addComponent(sprite);

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
                        Effect = new DamageEffect(10,  15),
                        Name = "Stab",
                        Speed = 10,
                        Targeting = Targeting.Enemies
                    },
                    new BattleAction<Animations>
                    {
                        Animation = Animations.Attack,
                        Effect = new DamageEffect(20, 30),
                        Name = "Super Stab",
                        Speed = 70,
                        Targeting = Targeting.Enemies
                    }
                },
                new RandomAIDecider(), 0f, 0f
            );
            enemyEntity.addComponent(aIActor);
            return enemyEntity;
        }

        private Entity MakeNecromancer()
        {
            var enemyEntity = createEntity("enemy");
            
            var sprite = new Sprite(contentManager.Load<Texture2D>("necromancer_big"));
            sprite.setOrigin(Vector2.Zero);
            enemyEntity.addComponent(sprite);

            var aIActor = new AIActor<Animations>("Necromancer", 200, new Faction
            {
                FactionName = "Enemies",
                Id = 2
            },
                new List<BattleAction<Animations>>
                {
                    new BattleAction<Animations>
                    {
                        Animation = Animations.Attack,
                        Effect = new LifeDrainEffect(10, 20),
                        Name = "Necro Bolt",
                        Speed = 10,
                        Targeting = Targeting.Enemies
                    },
                },
                new RandomAIDecider(), 0f, 0f
            );
            enemyEntity.addComponent(aIActor);

            return enemyEntity;
        }
    }
}
