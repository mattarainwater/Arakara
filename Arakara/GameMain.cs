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
        public GameMain()
            : base(width: DimensionConstants.SCREEN_WIDTH, height: DimensionConstants.SCREEN_HEIGHT, isFullScreen: DimensionConstants.IS_FULL_SCREEN, windowTitle: "Arakara")
        {
            Core.defaultSamplerState = SamplerState.PointClamp;
        }

        protected override void Initialize()
        {
            base.Initialize();
            CommonResources.DefaultBitmapFont = Graphics.instance.bitmapFont;
            scene = new TestDialogueScene();
            VirtualButtons.SetupInput();
        }
    }
}
