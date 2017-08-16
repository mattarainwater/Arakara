using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;

namespace Arakara.Battle.Effects
{
    public class TrashEffect : ActionEffect
    {
        public int NumberToTrash { get; set; }
        public bool TrashHigh { get; set; }

        public TrashEffect(int numberToTrash, bool trashHigh)
        {
            NumberToTrash = numberToTrash;
            TrashHigh = trashHigh;
        }

        public override void Perform(BattleActor actor, List<BattleActor> targets, BattleController controller)
        {
            foreach(var target in targets)
            {
                if(target.GetType() == typeof(DeckBuilderActor))
                {
                    var deckBuilderActor = (DeckBuilderActor)target;
                    var totalCards = deckBuilderActor.Hand.Concat(deckBuilderActor.DiscardPile).ToList();
                    if(TrashHigh)
                    {
                        totalCards = totalCards.OrderByDescending(x => x.Cost).ToList();
                    }
                    else
                    {
                        totalCards = totalCards.OrderBy(x => x.Cost).ToList();
                    }
                    for(var i = 0; i < NumberToTrash; i++)
                    {
                        if(totalCards.Count() > 6)
                        {
                            totalCards.Remove(totalCards.First());
                        }
                    }
                }
            }
        }

        public override string GetDescription()
        {
            var qualifier = TrashHigh ? "highest cost" : "lowest cost";
            if(NumberToTrash == 1)
            {
                return $"Trash the {qualifier} card from your deck.";
            }
            return $"Trash the {NumberToTrash} {qualifier} cards from your deck.";
        }
    }
}
