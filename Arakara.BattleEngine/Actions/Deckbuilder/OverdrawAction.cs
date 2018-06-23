using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.BattleEngine.Models.Deckbuilder;

namespace Arakara.BattleEngine.Actions.Deckbuilder
{
    public class OverdrawAction : DrawCardsAction
    {
        public OverdrawAction(DeckbuilderActor actor, int amount) : base(actor, amount)
        {
        }
    }
}
