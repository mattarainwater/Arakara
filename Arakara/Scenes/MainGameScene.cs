using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Scenes
{
    public class MainGameScene : Scene
    {

        public MainGameScene()
            : base()
        {
        }

        public override void initialize()
        {
            addRenderer(new RenderLayerExcludeRenderer(0));

            clearColor = Color.WhiteSmoke;
        }
    }
}
