using Arakara.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using System.Collections.Generic;

namespace Arakara
{
    public class GameMain : Core
    {
        private const int WIDTH = 1920;
        private const int HEIGHT = 1200;

        public GameMain()
            : base(width: WIDTH, height: HEIGHT, isFullScreen: true, windowTitle: "Arakara")
        {
            Core.defaultSamplerState = SamplerState.PointClamp;
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            scene = new MainGameScene();
        }
    }
}
