using Arakara.Common;
using Arakara.Scenes;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;

namespace Arakara
{
    public class GameMain : Core
    {
        public GameMain()
            : base(windowTitle: "Arakara", isFullScreen: false)
        {
            defaultSamplerState = SamplerState.PointClamp;
            DimensionConstants.SetCurrentResolution(0);
        }

        protected override void Initialize()
        {
            base.Initialize();
            CommonResources.DefaultBitmapFont = Graphics.instance.bitmapFont;
            CommonResources.Icons = Subtexture.subtexturesFromAtlas(content.Load<Texture2D>("icons"), 32, 32);
            scene = new StartMenuScene();
            VirtualButtons.SetupInput();
        }
    }
}
