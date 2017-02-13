using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Arakara.Components
{
    public class EffectDisplay : Text, IUpdatable
    {
        public int TimeToLive { get; set; }

        private float _elapsedTIme;

        public EffectDisplay(IFont font, string text, Vector2 position, Color color, int timeToLive) 
            : base(font, text, position, color)
        {
            TimeToLive = timeToLive;
        }

        public void update()
        {
            _elapsedTIme += Time.altDeltaTime;
            if (_elapsedTIme >= TimeToLive)
            {
                entity.destroy();
            }
        }
    }
}
