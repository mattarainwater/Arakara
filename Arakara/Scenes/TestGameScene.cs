using Arakara.Battle;
using Arakara.Battle.AI;
using Arakara.Battle.Effects;
using Arakara.Battle.Events;
using Arakara.Battle.Events.Effects;
using Arakara.Battle.Events.Triggers;
using Arakara.Battle.Statuses;
using Arakara.Common;
using Arakara.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Scenes
{
    public class TestGameScene : BaseScene
    {
        public override void OnStart()
        {
            var battleController = new BattleController();

            var battleEntity = CreateEntity("battle");
            battleEntity.AddComponent(battleController);

            battleController.AddActor(MakeMC());

            battleController.AddActor(MakeKnight());
            battleController.AddActor(MakeNecromancer());

            var pcWin = new BattleEvent(new OnFactionWinTrigger(Faction.Ally));
            pcWin.AddEffect(new SceneTransitionEffect(1, LoadWinScreen));
            battleController.AddEvent(pcWin);

            var enemiesWin = new BattleEvent(new OnFactionWinTrigger(Faction.Enemy));
            enemiesWin.AddEffect(new SceneTransitionEffect(1, LoadLoseScreen));
            battleController.AddEvent(enemiesWin);

            battleController.IsActive = true;
        }

        private Scene LoadWinScreen()
        {
            return new MessageScene("You Win!");
        }

        private Scene LoadLoseScreen()
        {
            return new MessageScene("You Lose!");
        }

        private BattleActor MakeMC()
        {
            var mainCharacterEntity = CreateEntity("mc");
            var mainCharacterActor = new DeckBuilderActor("Prisca", 20, Faction.Ally, 
            new List<Card>()
            {
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Slash",
                        Effect = new DamageEffect(1, 3),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1,
                    },
                    BuyValue = 2,
                    Cost = 0
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Slash",
                        Effect = new DamageEffect(1, 3),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1,
                    },
                    BuyValue = 2,
                    Cost = 0
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Slash",
                        Effect = new DamageEffect(1, 3),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1,
                    },
                    BuyValue = 2,
                    Cost = 0
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Slash",
                        Effect = new DamageEffect(1, 3),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1,
                    },
                    BuyValue = 2,
                    Cost = 0
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Guard",
                        Effect = new ApplyStatusEffect(new DefenseUpStatus(3, 2)),
                        Targeting = Targeting.Self,
                        Animation = Animations.Hit,
                    },
                    BuyValue = 1,
                    Cost = 0
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Guard",
                        Effect = new ApplyStatusEffect(new DefenseUpStatus(3, 2)),
                        Targeting = Targeting.Self,
                        Animation = Animations.Hit,
                    },
                    BuyValue = 1,
                    Cost = 0
                },
            },

            new List<Card>()
            {
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Heavy Slash",
                        Effect = new DamageEffect(3, 5),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1,
                    },
                    BuyValue = 2,
                    Cost = 3
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Poison",
                        Effect = new ApplyStatusEffect(new PoisonStatus(3, 2, 4)),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1,
                    },
                    BuyValue = 3,
                    Cost = 4
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Potion",
                        Effect = new HealEffect(5),
                        Targeting = Targeting.Self,
                        Animation = Animations.Support1,
                    },
                    BuyValue = 3,
                    Cost = 4
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Assassinate",
                        Effect = new DamageEffect(5, 7),
                        Targeting = Targeting.Enemies,
                        Animation = Animations.Attack1,
                    },
                    BuyValue = 2,
                    Cost = 6
                },
                new Card
                {
                    Action = new BattleAction
                    {
                        Name = "Prepare",
                        Effect = new TrashEffect(2, false),
                        Targeting = Targeting.Self,
                        Animation = Animations.Hit,
                    },
                    BuyValue = 4,
                    Cost = 3
                },
            }
            ,0, 0, 200f);

            mainCharacterEntity.AddComponent(mainCharacterActor);

            var texture = Content.Load<Texture2D>("prisca_5");
            var subtextures = Sprite.SpritesFromAtlas(texture, 128, 128);
            var sprite = new SpriteAnimator(subtextures[5]);
            sprite.Transform.Scale = Vector2.One * 2;
            mainCharacterEntity.AddComponent(sprite);
            var attackAnimationFrames = new Sprite[]
            {
                subtextures[5],
            };
            var idleAnimationFrames = new Sprite[]
            {
                subtextures[5],
            };
            var potionAnimationFrames = new Sprite[]
            {
                subtextures[5],
            };
            var woundedAnimationFrames = new Sprite[]
            {
                subtextures[5],
            };
            sprite.AddAnimation(Animations.Attack1, new SpriteAnimation(attackAnimationFrames, 30));
            sprite.AddAnimation(Animations.Idle, new SpriteAnimation(idleAnimationFrames, 30));
            sprite.AddAnimation(Animations.Support1, new SpriteAnimation(potionAnimationFrames, 30));
            sprite.AddAnimation(Animations.Hit, new SpriteAnimation(woundedAnimationFrames, 30));
            sprite.FlipX = false;
            sprite.SetOrigin(Vector2.Zero);
            sprite.RenderLayer = 1000;

            return mainCharacterActor;
        }

        private BattleActor MakeKnight()
        {
            var enemyEntity = CreateEntity("enemy");

            var texture = Content.Load<Texture2D>("guard_big");
            var subtextures = Sprite.SpritesFromAtlas(texture, 64, 64);
            var sprite = new SpriteAnimator(subtextures[0]);
            enemyEntity.AddComponent(sprite);
            var animationFrames = new Sprite[]
            {
                subtextures[0],
                subtextures[1],
                subtextures[1],
                subtextures[1],
                subtextures[0]
            };
            var idleAnimationFrames = new Sprite[]
            {
                subtextures[0]
            };
            sprite.AddAnimation(Animations.Attack1, new SpriteAnimation(animationFrames, 30));
            sprite.AddAnimation(Animations.Idle, new SpriteAnimation(idleAnimationFrames, 30));
            sprite.SetOrigin(Vector2.Zero);
            sprite.RenderLayer = 1000;

            var aIActor = new AIActor("Guard", 15, Faction.Enemy,
                new List<BattleAction>
                {
                    new BattleAction
                    {
                        Animation = Animations.Attack1,
                        Effect = new DamageEffect(2, 4),
                        Name = "Stab",
                        Targeting = Targeting.Enemies
                    },
                },
                new RandomAIDecider(), 0f, 0f, 100f);

            enemyEntity.AddComponent(aIActor);

            return aIActor;
        }

        private BattleActor MakeNecromancer()
        {
            var enemyEntity = CreateEntity("enemy");

            var texture = Content.Load<Texture2D>("necromancer_big");
            var subtextures = Sprite.SpritesFromAtlas(texture, 64, 64);
            var sprite = new SpriteAnimator(subtextures[0]);
            enemyEntity.AddComponent(sprite);
            var animationFrames = new Sprite[]
            {
                subtextures[0],
                subtextures[1],
                subtextures[2],
                subtextures[2],
                subtextures[1],
                subtextures[0]
            };
            var idleAnimationFrames = new Sprite[]
            {
                subtextures[0],
                subtextures[3],
                subtextures[3],
                subtextures[4],
                subtextures[4],
                subtextures[3],
                subtextures[3],
                subtextures[0]
            };
            sprite.AddAnimation(Animations.Attack1, new SpriteAnimation(animationFrames, 30));
            sprite.AddAnimation(Animations.Idle, new SpriteAnimation(idleAnimationFrames, 30));
            sprite.SetOrigin(Vector2.Zero);
            sprite.RenderLayer = 1000;

            var aIActor = new AIActor("Warlock", 10, Faction.Enemy,
                new List<BattleAction>
                {
                    new BattleAction
                    {
                        Animation = Animations.Attack1,
                        Effect = new DamageEffect(1, 3),
                        Name = "Necro Bolt",
                        Targeting = Targeting.Enemies
                    },
                    new BattleAction
                    {
                        Animation = Animations.Attack1,
                        Effect = new ApplyStatusEffect(new CurseStatus(1)),
                        Name = "Curse",
                        Targeting = Targeting.Enemies
                    },
                },
                new RandomAIDecider(), 0f, 0f, 50f);

            enemyEntity.AddComponent(aIActor);

            return aIActor;
        }
    }
}
