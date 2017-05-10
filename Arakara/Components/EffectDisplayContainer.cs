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
            var scene = entity.scene;
            var display = scene.addEntity(new Entity());
            var xPos = entity.transform.position.X + (DimensionConstants.CHARACTER_WIDTH / 2);
            var yPos = entity.transform.position.Y - 13 - (EffectDisplays.Count * 15);
            var effectDisplay = new EffectDisplay(this, Graphics.instance.bitmapFont, text, new Vector2(xPos, yPos), color, 3);
            display.addComponent(effectDisplay);
            EffectDisplays.Add(effectDisplay);
        }

        public void Remove(EffectDisplay display)
        {
            EffectDisplays.Remove(display);
            foreach(var effectDisplay in EffectDisplays)
            {
                effectDisplay.transform.position = new Vector2(effectDisplay.transform.position.X, effectDisplay.transform.position.Y + 15);
            }
        }
    }
}
