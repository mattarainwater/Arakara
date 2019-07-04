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
            _screenSpaceRenderer.shouldDebugRender = false;
            finalRenderDelegate = this;

            addRenderer(new RenderLayerExcludeRenderer(0, SCREEN_SPACE_RENDER_LAYER));
        }

        public override void initialize()
        {
            setDesignResolution(DimensionConstants.DESIGN_WIDTH, DimensionConstants.DESIGN_HEIGHT, SceneResolutionPolicy.BestFit);
            clearColor = Color.WhiteSmoke;
        }

        public void onAddedToScene()
        { }

        public void onSceneBackBufferSizeChanged(int newWidth, int newHeight)
        {
            _screenSpaceRenderer.onSceneBackBufferSizeChanged(newWidth, newHeight);
        }

        public Scene scene { get; set; }

        public void handleFinalRender(Color letterboxColor, RenderTarget2D source, Rectangle finalRenderDestinationRect, SamplerState samplerState)
        {
            Core.graphicsDevice.SetRenderTarget(null);
            Core.graphicsDevice.Clear(letterboxColor);
            Graphics.instance.batcher.begin(BlendState.Opaque, samplerState, DepthStencilState.None, RasterizerState.CullNone, null);
            Graphics.instance.batcher.draw(source, finalRenderDestinationRect, Color.White);
            Graphics.instance.batcher.end();

            _screenSpaceRenderer.render(scene);
        }
    }
}
