using Arakara.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Scenes
{
    public class BaseScene : Scene, IFinalRenderDelegate
    {
        public const int SCREEN_SPACE_RENDER_LAYER = 999;
        private ScreenSpaceRenderer _screenSpaceRenderer;

        public BaseScene()
        {
            _screenSpaceRenderer = new ScreenSpaceRenderer(100, SCREEN_SPACE_RENDER_LAYER);
            _screenSpaceRenderer.ShouldDebugRender = false;
            FinalRenderDelegate = this;

            AddRenderer(new RenderLayerExcludeRenderer(0, SCREEN_SPACE_RENDER_LAYER));
        }

        public override void Initialize()
        {
            SetDesignResolution(DimensionConstants.DESIGN_WIDTH, DimensionConstants.DESIGN_HEIGHT, SceneResolutionPolicy.BestFit);
            ClearColor = Color.WhiteSmoke;
        }

        public void OnSceneBackBufferSizeChanged(int newWidth, int newHeight)
        {
            _screenSpaceRenderer.OnSceneBackBufferSizeChanged(newWidth, newHeight);
        }

        public Scene scene { get; set; }

        public void HandleFinalRender(RenderTarget2D finalRenderTarget, Color letterboxColor, RenderTarget2D source, Rectangle finalRenderDestinationRect, SamplerState samplerState)
        {
            Core.GraphicsDevice.SetRenderTarget(null);
            Core.GraphicsDevice.Clear(letterboxColor);
            Graphics.Instance.Batcher.Begin(BlendState.Opaque, samplerState, DepthStencilState.None, RasterizerState.CullNone, null);
            Graphics.Instance.Batcher.Draw(source, finalRenderDestinationRect, Color.White);
            Graphics.Instance.Batcher.End();

            _screenSpaceRenderer.Render(scene);
        }

        public void OnAddedToScene(Scene scene)
        {
        }
    }
}
