using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Models;
using Arakara.BattleEngine.Models.Deckbuilder;
using Arakara.BattleEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Actions.Deckbuilder
{
    public class DrawCardsAction : GameAction, IAbilityLoader
    {
        public int Amount { get; set; }
        public List<Card> cards;
        public DeckbuilderActor Actor { get; set; }

        #region Constructors
        public DrawCardsAction()
        {

        }

        public DrawCardsAction(DeckbuilderActor actor, int amount)
        {
            Actor = actor;
            Amount = amount;
        }
        #endregion

        #region IAbility
        public void Load(IContainer game, Ability ability)
        {
            Actor = game.GetBattle().Actors.First(x => x is DeckbuilderActor) as DeckbuilderActor;
            Amount = Convert.ToInt32(ability.UserInfo);
        }

        public override string GetLog()
        {
            return $"{Actor.Name} drew {Amount} cards";
        }
        #endregion
    }
}
