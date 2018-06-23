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
using Tenswee.Common.Extensions;
using Tenswee.Common.Notifications;

namespace Arakara.BattleEngine.Systems.Deckbuilder
{
    public class DrawCardsSystem : Aspect, IObservable
    {
        public void Awake()
        {
            this.AddObserver(OnPerformDrawCards, Global.PerformNotification<DrawCardsAction>(), Container);
            this.AddObserver(OnPerformOverDraw, Global.PerformNotification<OverdrawAction>(), Container);
        }

        public void Destroy()
        {
            this.RemoveObserver(OnPerformDrawCards, Global.PerformNotification<DrawCardsAction>(), Container);
            this.RemoveObserver(OnPerformOverDraw, Global.PerformNotification<OverdrawAction>(), Container);
        }

        void OnPerformDrawCards(object sender, object args)
        {
            var action = args as DrawCardsAction;

            int deckCount = action.Actor[Zones.Deck].Count;
            if(action.Amount > deckCount)
            {
                var firstDrawAction = new DrawCardsAction(action.Actor, deckCount);
                var shuffleAction = new ShuffleAction();
                var secondDrawAction = new DrawCardsAction(action.Actor, action.Amount - deckCount);

                Container.AddReaction(firstDrawAction);
                Container.AddReaction(shuffleAction);
                Container.AddReaction(secondDrawAction);
            }
            else
            {
                int handCount = action.Actor[Zones.Hand].Count;

                int roomInHand = DeckbuilderActor.MAX_HAND - handCount;
                int overDraw = Math.Max(action.Amount - roomInHand, 0);
                if (overDraw > 0)
                {
                    var overDrawAction = new OverdrawAction(action.Actor, overDraw);
                    Container.AddReaction(overDrawAction);
                }

                int drawCount = action.Amount - overDraw;
                action.cards = action.Actor[Zones.Deck].Draw(drawCount);
                foreach (Card card in action.cards)
                {
                    ChangeZone(card, Zones.Hand);
                }
            }
        }

        void OnPerformOverDraw(object sender, object args)
        {
            var action = args as OverdrawAction;
            action.cards = action.Actor[Zones.Deck].Draw(action.Amount);
            foreach (Card card in action.cards)
            {
                ChangeZone(card, Zones.DiscardPile);
            }
        }

        void ChangeZone(Card card, Zones zone, DeckbuilderActor toActor = null)
        {
            var cardSystem = Container.GetAspect<CardSystem>();
            cardSystem.ChangeZone(card, zone, toActor);
        }
    }
}
