using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Arakara.Components
{
    public class UpdatableText : TextComponent, IUpdatable
    {
        public Func<string> TextFunc { get; set; }

        public UpdatableText(IFont font, Vector2 position, Color color, Func<string> func) 
            : base(font, "", position, color)
        {
            TextFunc = func;
            RenderLayer = 5;
        }

        public void Update()
        {
            SetText(TextFunc());
        }
    }
}
