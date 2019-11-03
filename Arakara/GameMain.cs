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
            DefaultSamplerState = SamplerState.PointClamp;
            DimensionConstants.SetCurrentResolution(0);
        }

        protected override void Initialize()
        {
            base.Initialize();
            CommonResources.DefaultBitmapFont = Graphics.Instance.BitmapFont;
            CommonResources.Icons = Sprite.SpritesFromAtlas(Content.Load<Texture2D>("icons"), 32, 32);
            Scene = new StartMenuScene();
            VirtualButtons.SetupInput();
        }
    }
}
