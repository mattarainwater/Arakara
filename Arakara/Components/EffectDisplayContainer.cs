using Arakara.Common;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class EffectDisplayContainer : Component
    {
        public List<EffectDisplay> EffectDisplays { get; set; }

        public EffectDisplayContainer()
        {
            EffectDisplays = new List<EffectDisplay>();
        }

        public void MakeEffectDisplay(string text, Color color)
        {
            var scene = Entity.Scene;
            var display = scene.AddEntity(new Entity());
            var xPos = Entity.Transform.Position.X + (32 / 2);
            var yPos = Entity.Transform.Position.Y - 13 - (EffectDisplays.Count * 15);
            var effectDisplay = new EffectDisplay(this, CommonResources.DefaultBitmapFont, text, new Vector2(xPos, yPos), color, 3);
            display.AddComponent(effectDisplay);
            EffectDisplays.Add(effectDisplay);
        }

        public void Remove(EffectDisplay display)
        {
            EffectDisplays.Remove(display);
            foreach(var effectDisplay in EffectDisplays)
            {
                effectDisplay.Transform.Position = new Vector2(effectDisplay.Transform.Position.X, effectDisplay.Transform.Position.Y + 15);
            }
        }
    }
}
