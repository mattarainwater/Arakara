using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Arakara.Common;

namespace Arakara.Components
{
    public class TargetPolygon : SimplePolygon
    {
        private static readonly Vector2[] TARGET_POLYGON_VERTS = new Vector2[4]
        {
            new Vector2(0, DimensionConstants.CHARACTER_HEIGHT + 3),
            new Vector2(DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT + 3),
            new Vector2(DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT + 5),
            new Vector2(0, DimensionConstants.CHARACTER_HEIGHT + 5),
        };
        private static readonly Color TARGET_POLYGON_COLOR = Color.LightPink;

        public TargetPolygon() 
            : base(TARGET_POLYGON_VERTS, TARGET_POLYGON_COLOR)
        {
        }
    }
}
