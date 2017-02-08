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
        private const int WIDTH = 1900;
        private const int HEIGHT = 1080;

        public GameMain()
            : base(width: WIDTH, height: HEIGHT)
        {
            Core.defaultSamplerState = SamplerState.PointClamp;
        }

        protected override void Initialize()
        {
            base.Initialize();
            scene = new StartMenuScreen();
        }
    }
}
