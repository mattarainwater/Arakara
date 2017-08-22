using Arakara.Common;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Scenes
{
    public class MessageScene : Scene
    {
        public string Message { get; set; }

        public MessageScene(string message)
        {
            Message = message;
        }

        public override void initialize()
        {
            addRenderer(new RenderLayerExcludeRenderer(0));
            setDesignResolution(DimensionConstants.SCREEN_WIDTH, DimensionConstants.SCREEN_HEIGHT, SceneResolutionPolicy.NoBorderPixelPerfect);
            clearColor = Color.WhiteSmoke;
        }

        public override void onStart()
        {
            var messageEntity = createEntity("message");
            var textComponent = new Text(CommonResources.DefaultBitmapFont, Message, Vector2.Zero, Color.Black);
            messageEntity.addComponent(textComponent);
            messageEntity.transform.position = new Vector2(150, 150);
            messageEntity.transform.scale = new Vector2(2, 2);
        }
    }
}
