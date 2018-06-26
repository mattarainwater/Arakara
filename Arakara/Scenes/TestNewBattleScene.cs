using Arakara.BattleEngine;
using Arakara.BattleEngine.Systems;
using Arakara.Common;
using Arakara.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            text.addComponent(new UpdatableText(CommonResources.DefaultBitmapFont, new Vector2(100, 100), Color.Black, () => {
                Console.WriteLine(gameViewSystem.Container.GetBattleAsJson());
                return gameViewSystem.Container.GetBattleAsJson();
            }));
        }
    }
}
