using Arakara.Common;
using Arakara.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.BitmapFonts;
using System.Collections.Generic;

namespace Arakara
{
    public class GameMain : Core
    {
        //private const int WIDTH = 1920;
        //private const int HEIGHT = 1200;

        private const int WIDTH = 1600;
        private const int HEIGHT = 900;

        public GameMain()
            : base(width: WIDTH, height: HEIGHT, isFullScreen: false, windowTitle: "Arakara")
        {
            Core.defaultSamplerState = SamplerState.PointClamp;
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            //CommonResources.DefaultBitmapFont = contentManager.Load<BitmapFont>("default");
            CommonResources.DefaultBitmapFont = Graphics.instance.bitmapFont;
            scene = new MainGameScene();
            VirtualButtons.SetupInput();
        }
    }
}
