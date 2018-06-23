using Arakara.BattleEngine.Actions;
using Arakara.BattleEngine.Actions.Deckbuilder;
using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Models.Deckbuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Tenswee.Common.Notifications;

namespace Arakara.BattleEngine.Systems.Deckbuilder
{
    public class DeckbuilderTurnSystem : Aspect, IObservable
    {
        public void Awake()
        {
            this.AddObserver(OnPerformChangeTurn, Global.PerformNotification<ChangeTurnAction>(), Container);
        }

        public void Destroy()
        {
            this.RemoveObserver(OnPerformChangeTurn, Global.PerformNotification<ChangeTurnAction>(), Container);
        }

        void OnPerformChangeTurn(object sender, object args)
        {
            var action = args as ChangeTurnAction;
            var battle = Container.GetBattle();
            if(battle.CurrentActor is DeckbuilderActor)
            {
                DrawCards(battle.CurrentActor as DeckbuilderActor, 5);
            }
        }

        void DrawCards(DeckbuilderActor actor, int amount)
        {
            var action = new DrawCardsAction(actor, amount);
            Container.AddReaction(action);
        }
    }
}
