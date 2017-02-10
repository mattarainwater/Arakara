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

            clearColor = Color.WhiteSmoke;

            var battleEntity = createEntity("battle");
            _battle = new BattleContainer(Screen.width, Screen.height);
            battleEntity.addComponent(_battle);
        }

        public override void onStart()
        {
            var knight = contentManager.Load<Texture2D>("Knight");

            _battle.AddBattleEntity(MakeMC());

            _battle.AddBattleEntity(MakeKnight());
            _battle.AddBattleEntity(MakeKnight());
            _battle.AddBattleEntity(MakeNecromancer());

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
            }, .5f, .5f);
            var mcVerts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, 0),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT),
                new Vector2(0, DimensionConstants.CHARACTER_HEIGHT),
            };
            mainCharacterEntity.addComponent(mainCharacterActor);
            mainCharacterEntity.addComponent(new SimplePolygon(mcVerts, Color.LightSkyBlue));

            return mainCharacterEntity;
        }

        private Entity MakeKnight()
        {
            var enemyEntity = createEntity("enemy");

            var mcVerts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, 0),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT),
                new Vector2(0, DimensionConstants.CHARACTER_HEIGHT),
            };
            enemyEntity.addComponent(new SimplePolygon(mcVerts, Color.LightGreen));
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

            var mcVerts = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, 0),
                new Vector2(DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT),
                new Vector2(0, DimensionConstants.CHARACTER_HEIGHT),
            };
            enemyEntity.addComponent(new SimplePolygon(mcVerts, Color.DarkGray));
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
