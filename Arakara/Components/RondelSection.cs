using Arakara.Battle;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class RondelSection : Component
    {
        public BattleAction Action { get; set; }
        public RondelPiece Piece { get; set; }
        public List<RondelPiece> NextPieces { get; set; }
    }

    public enum RondelPiece
    {
        Ax,
        Dagger,
        Spear,
        Bow,
        Shield,
        Grenados
    }
}
