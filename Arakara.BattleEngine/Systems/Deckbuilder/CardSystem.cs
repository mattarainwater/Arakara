using Arakara.BattleEngine.Models.Deckbuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Systems.Deckbuilder
{
    public class CardSystem : Aspect
    {
        //public List<PlayCardAction> playable = new List<PlayCardAction>();

        public void Refresh()
        {
        }

        public void ChangeZone(Card card, Zones zone, DeckbuilderActor toActor = null)
        {
            var fromPlayer = Container.GetBattle()
                .Actors.First(x => x is DeckbuilderActor) as DeckbuilderActor;
            toActor = toActor ?? fromPlayer;
            fromPlayer[card.Zone].Remove(card);
            toActor[zone].Add(card);
            card.Zone = zone;
        }
    }
}
