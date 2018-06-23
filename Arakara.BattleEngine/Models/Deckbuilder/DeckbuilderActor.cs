using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.BattleEngine.Models.Deckbuilder
{
    public class DeckbuilderActor : Actor
    {
        public const int MAX_HAND = 10;

        public DeckbuilderActor(int index)
            : base(index)
        {

        }

        public Energy Energy { get; set; }

        public List<Card> Deck { get; set; }
        public List<Card> DiscardPile { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> Trash { get; set; }

        public List<Card> BuyableCards { get; set; }

        public List<Card> this[Zones z]
        {
            get
            {
                switch (z)
                {
                    case Zones.Deck:
                        return Deck;
                    case Zones.Hand:
                        return Hand;
                    case Zones.DiscardPile:
                        return DiscardPile;
                    case Zones.Trash:
                        return Trash;
                    default:
                        return null;
                }
            }
        }
    }
}
