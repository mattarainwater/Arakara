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
    public class TurnMarkerPolygon : SimplePolygon
    {
        private static readonly Vector2[] TURN_MARKER_POLYGON_VERTS = new Vector2[4]
        {
            new Vector2(0, DimensionConstants.CHARACTER_HEIGHT + 7),
            new Vector2(DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT + 7),
            new Vector2(DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT + 9),
            new Vector2(0, DimensionConstants.CHARACTER_HEIGHT + 9),
        };
        private static readonly Color TURN_MARKER_POLYGON_COLOR = Color.Gold;

        public TurnMarkerPolygon() 
            : base(TURN_MARKER_POLYGON_VERTS, TURN_MARKER_POLYGON_COLOR)
        {
        }
    }
}
