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
    public class MessageScene : BaseScene
    {
        public string Message { get; set; }

        public MessageScene(string message)
        {
            Message = message;
        }

        public override void OnStart()
        {
            var messageEntity = CreateEntity("message");
            var textComponent = new TextComponent(CommonResources.DefaultBitmapFont, Message, Vector2.Zero, Color.Black);
            messageEntity.AddComponent(textComponent);
            messageEntity.Transform.Position = new Vector2(150, 150);
        }
    }
}
