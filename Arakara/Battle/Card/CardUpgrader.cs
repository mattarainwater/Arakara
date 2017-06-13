using Arakara.Battle.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Card
{
    public class CardUpgrader<TEnum> where TEnum : struct, IComparable, IFormattable
    {
        public void UpgradeCards(IEnumerable<Card<TEnum>> cards)
        {
            foreach (var card in cards)
            {
                UpgradeCard(card);
            }
        }

        private void UpgradeCard(Card<TEnum> card)
        {
            if(card.Action.Effect.GetType() == typeof(DamageEffect))
            {
                var newMinDamage = (int)(((DamageEffect)card.Action.Effect).MinDamage * 1.5);
                var newMaxDamage = (int)(((DamageEffect)card.Action.Effect).MaxDamage * 1.5);
                if (card.Grade == Grade.Bronze)
                {
                    card.Grade = Grade.Silver;
                    ((DamageEffect)card.Action.Effect).MinDamage = newMinDamage;
                    ((DamageEffect)card.Action.Effect).MaxDamage = newMaxDamage;
                }
                else if (card.Grade == Grade.Silver)
                {
                    card.Grade = Grade.Gold;
                    ((DamageEffect)card.Action.Effect).MinDamage = newMinDamage;
                    ((DamageEffect)card.Action.Effect).MaxDamage = newMaxDamage;
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
