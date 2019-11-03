using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Arakara.Components
{
    public class EffectDisplay : TextComponent, IUpdatable
    {
        public int TimeToLive { get; set; }

        private float _elapsedTIme;
        private EffectDisplayContainer _container;

        public EffectDisplay(EffectDisplayContainer container, IFont font, string text, Vector2 position, Color color, int timeToLive) 
            : base(font, text, position, color)
        {
            TimeToLive = timeToLive;
            _container = container;
            RenderLayer = 5;
        }

        public void Update()
        {
            _elapsedTIme += Time.AltDeltaTime;
            if (_elapsedTIme >= TimeToLive)
            {
                Entity.Destroy();
                _container.Remove(this);
            }
        }
    }
}
