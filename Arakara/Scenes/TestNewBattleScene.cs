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
        public override void onStart()
        {
            var gameViewSystem = new GameViewSystem();

            var gameViewSystemEntity = createEntity("battle");
            gameViewSystemEntity.addComponent(gameViewSystem);

            var text = createEntity("text");
            text.addComponent(new UpdatableText(CommonResources.DefaultBitmapFont, new Vector2(300, 100), Color.Black, () => {
                Console.WriteLine(gameViewSystem.Container.GetBattleAsJson());
                return gameViewSystem.Container.GetBattleAsJson();
            }));

            var text2 = createEntity("text2");
            text2.addComponent(new UpdatableText(CommonResources.DefaultBitmapFont, new Vector2(300, 200), Color.Black, () => {
                Console.WriteLine(gameViewSystem.Container.GetAspect<StateMachine>().currentState.GetType().FullName);
                return gameViewSystem.Container.GetAspect<StateMachine>().currentState.GetType().FullName;
            }));

            var guard1 = createEntity("guard1");
            var texture = content.Load<Texture2D>("brigand");
            var subtextures = Subtexture.subtexturesFromAtlas(texture, 64, 64);
            var sprite = new Sprite<Animations>(subtextures[0]);
            guard1.addComponent(sprite);
            var attackAnimationFrames = new List<Subtexture>
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
            var idleAnimationFrames = new List<Subtexture>
            {
                subtextures[0],
            };
            sprite.addAnimation(Animations.Attack1, new SpriteAnimation(attackAnimationFrames) { loop = false, fps = 30});
            sprite.addAnimation(Animations.Idle, new SpriteAnimation(idleAnimationFrames) { loop = true });
            sprite.setOrigin(Vector2.Zero);
            guard1.position = new Vector2(100, 100);
            var actor = new TestBattleActor(0, sprite);
            actor.Awake();
            guard1.addComponent(actor);





            var guard2 = createEntity("guard2");
            var texture2 = content.Load<Texture2D>("brigand");
            var subtextures2 = Subtexture.subtexturesFromAtlas(texture2, 64, 64);
            var sprite2 = new Sprite<Animations>(subtextures2[0]);
            guard2.addComponent(sprite2);
            var attackAnimationFrames2 = new List<Subtexture>
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
            var idleAnimationFrames2 = new List<Subtexture>
            {
                subtextures2[0],
            };
            sprite2.addAnimation(Animations.Attack1, new SpriteAnimation(attackAnimationFrames2) { loop = false, fps = 30 });
            sprite2.addAnimation(Animations.Idle, new SpriteAnimation(idleAnimationFrames2) { loop = true });
            sprite2.setOrigin(Vector2.Zero);
            sprite2.flipX = true;
            guard2.position = new Vector2(200, 100);
            var actor2 = new TestBattleActor(1, sprite2);
            actor2.Awake();
            guard2.addComponent(actor2);
        }
    }

    public class TestBattleActor : Component, IObservable
    {
        public int Index { get; set; }
        public Sprite<Animations> Sprite { get; set; }

        public TestBattleActor(int index, Sprite<Animations> sprite)
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
            Sprite.onAnimationCompletedEvent += (t) => {
                Sprite.onAnimationCompletedEvent += null;
                Sprite.play(Animations.Idle);
            };
            Sprite.play(Animations.Attack1);
            while (Sprite.isAnimationPlaying(Animations.Attack1))
            {
                yield return null;
            }
        }
    }
}
