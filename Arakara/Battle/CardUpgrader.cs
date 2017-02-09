using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle
{
    public class CardUpgrader
    {
        public void UpgradeCards(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                UpgradeCard(card);
            }
        }

        private void UpgradeCard(Card card)
        {
            if(card.Action.Effect.GetType() == typeof(DamageEffect))
            {
                var newDamage = (int)(((DamageEffect)card.Action.Effect).Damage * 1.5);
                if (card.Grade == Grade.Bronze)
                {
                    card.Grade = Grade.Silver;
                    ((DamageEffect)card.Action.Effect).Damage = newDamage;
                }
                else if (card.Grade == Grade.Silver)
                {
                    card.Grade = Grade.Gold;
                    ((DamageEffect)card.Action.Effect).Damage = newDamage;
                }
            }
            else if (card.Action.Effect.GetType() == typeof(HealEffect))
            {
                var newHealing = (int)(((HealEffect)card.Action.Effect).Healing * 1.5);
                if (card.Grade == Grade.Bronze)
                {
                    card.Grade = Grade.Silver;
                    ((HealEffect)card.Action.Effect).Healing = newHealing;
                }
                else if (card.Grade == Grade.Silver)
                {
                    card.Grade = Grade.Gold;
                    ((HealEffect)card.Action.Effect).Healing = newHealing;
                }
            }
        }
    }
}
