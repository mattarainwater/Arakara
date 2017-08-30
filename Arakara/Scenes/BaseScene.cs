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
    public class BaseScene : Scene
    {
        public override void initialize()
        {
            addRenderer(new DefaultRenderer());
            setDesignResolution(DimensionConstants.DESIGN_WIDTH, DimensionConstants.DESIGN_HEIGHT, SceneResolutionPolicy.ShowAllPixelPerfect);
            clearColor = Color.WhiteSmoke;
        }
    }
}
