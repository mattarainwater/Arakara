using Arakara.Battle;
using Arakara.BattleEngine;
using Arakara.BattleEngine.Actions;
using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Systems;
using Arakara.Common;
using Arakara.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Tenswee.Common.Notifications;
using Tenswee.Common.States;

namespace Arakara.Scenes
{
    public class TestNewBattleScene : BaseScene
    {
        public override void OnStart()
        {
            var gameViewSystem = new GameViewSystem();

            var gameViewSystemEntity = CreateEntity("battle");
            gameViewSystemEntity.AddComponent(gameViewSystem);

            var text = CreateEntity("text");
            text.AddComponent(new UpdatableText(CommonResources.DefaultBitmapFont, new Vector2(300, 100), Color.Black, () => {
                Console.WriteLine(gameViewSystem.Container.GetBattleAsJson());
                return gameViewSystem.Container.GetBattleAsJson();
            }));

            var text2 = CreateEntity("text2");
            text2.AddComponent(new UpdatableText(CommonResources.DefaultBitmapFont, new Vector2(300, 200), Color.Black, () => {
                Console.WriteLine(gameViewSystem.Container.GetAspect<StateMachine>().currentState.GetType().FullName);
                return gameViewSystem.Container.GetAspect<StateMachine>().currentState.GetType().FullName;
            }));

            var guard1 = CreateEntity("guard1");
            var texture = Content.Load<Texture2D>("brigand");
            var subtextures = Sprite.SpritesFromAtlas(texture, 64, 64);
            var sprite = new SpriteAnimator(subtextures[0]);
            guard1.AddComponent(sprite);
            var attackAnimationFrames = new Sprite[]
            {
                subtextures[0],
                subtextures[0],
                subtextures[0],
                subtextures[0],
                subtextures[1],
                subtextures[1],
                subtextures[1],
                subtextures[1],
                subtextures[2],
                subtextures[2],
                subtextures[2],
                subtextures[2],
                subtextures[3],
                subtextures[3],
                subtextures[3],
                subtextures[3],
            };
            var idleAnimationFrames = new Sprite[]
            {
                subtextures[0],
            };
            sprite.AddAnimation(Animations.Attack1, new SpriteAnimation(attackAnimationFrames, 30));
            sprite.AddAnimation(Animations.Idle, new SpriteAnimation(idleAnimationFrames, 30));
            sprite.SetOrigin(Vector2.Zero);
            guard1.Position = new Vector2(100, 100);
            var actor = new TestBattleActor(0, sprite);
            actor.Awake();
            guard1.AddComponent(actor);





            var guard2 = CreateEntity("guard2");
            var texture2 = Content.Load<Texture2D>("brigand");
            var subtextures2 = Sprite.SpritesFromAtlas(texture2, 64, 64);
            var sprite2 = new SpriteAnimator(subtextures2[0]);
            guard2.AddComponent(sprite2);
            var attackAnimationFrames2 = new Sprite[]
            {
                subtextures2[0],
                subtextures2[0],
                subtextures2[0],
                subtextures2[0],
                subtextures2[1],
                subtextures2[1],
                subtextures2[1],
                subtextures2[1],
                subtextures2[2],
                subtextures2[2],
                subtextures2[2],
                subtextures2[2],
                subtextures2[3],
                subtextures2[3],
                subtextures2[3],
                subtextures2[3],
            };
            var idleAnimationFrames2 = new Sprite[]
            {
                subtextures2[0],
            };
            sprite2.AddAnimation(Animations.Attack1, new SpriteAnimation(attackAnimationFrames2, 30));
            sprite2.AddAnimation(Animations.Idle, new SpriteAnimation(idleAnimationFrames2, 30));
            sprite2.SetOrigin(Vector2.Zero);
            sprite2.FlipX = true;
            guard2.Position = new Vector2(200, 100);
            var actor2 = new TestBattleActor(1, sprite2);
            actor2.Awake();
            guard2.AddComponent(actor2);
        }
    }

    public class TestBattleActor : Component, IObservable
    {
        public int Index { get; set; }
        public SpriteAnimator Sprite { get; set; }

        public TestBattleActor(int index, SpriteAnimator sprite)
        {
            Index = index;
            Sprite = sprite;
        }

        public void Awake()
        {
            this.AddObserver(OnPrepareMove, Global.PrepareNotification<BattleMoveAction>());
        }

        public void Destroy()
        {
            this.RemoveObserver(OnPrepareMove, Global.PrepareNotification<BattleMoveAction>());
        }

        private void OnPrepareMove(object gameAsObject, object moveActionAsObject)
        {
            var game = gameAsObject as Container;
            var isCurrentActor = game.GetBattle().CurrentActorIndex == Index;
            if(isCurrentActor)
            {
                var moveAction = moveActionAsObject as BattleMoveAction;
                moveAction.perform.viewer = PlayAttack;
            }
        }

        private IEnumerator PlayAttack(IContainer game, GameAction action)
        {
            Sprite.OnAnimationCompletedEvent += (t) => {
                Sprite.OnAnimationCompletedEvent += null;
                Sprite.Play(Animations.Idle);
            };
            Sprite.Play(Animations.Attack1);
            while (Sprite.IsAnimationActive(Animations.Attack1))
            {
                yield return null;
            }
        }
    }
}
