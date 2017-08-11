using Arakara.Common;
using Arakara.Components;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Phases.DeckBuilder
{
    public class SelectBuyableCardPhase : Phase
    {
        private DeckBuilderActor _deckBuilderActor;
        public Selector CardSelector { get; set; }

        public SelectBuyableCardPhase(DeckBuilderActor actor) 
            : base(actor)
        {
            _deckBuilderActor = actor;
            CardSelector = actor.entity.addComponent(new Selector(
                VirtualButtons.SelectInput,
                VirtualButtons.LeftInput,
                VirtualButtons.RightInput,
                onFocus: OnCardFocus,
                onBlur: OnCardBlur,
                onSelect: OnCardSelect));
            CardSelector.enabled = false;
        }

        protected override void initialize()
        {
            CardSelector.enabled = true;
            _deckBuilderActor.SelectedBuyableCard = null;
            CardSelector.AddEntities(_deckBuilderActor.BuyableHandEntities);
            foreach(var entity in _deckBuilderActor.BuyableHandEntities)
            {
                var card = entity.getComponent<Card>();
                var sprite = entity.getComponent<Sprite>();
                if(_deckBuilderActor.BuyPoints < card.Cost)
                {
                    sprite.subtexture = new Subtexture(_deckBuilderActor.DisabledCardTexture);
                }
            }
        }

        protected override void update()
        {
            if (_deckBuilderActor.SelectedBuyableCard != null)
            {
                IsFinished = true;
            }
        }

        protected override void finish()
        {
            CardSelector.enabled = false;
            CardSelector.Reset();
            Actor.entity.scene.findEntitiesWithTag(DrawBuyableCardsPhase.BUYABLE_TEMP_TAG).ForEach(x => x.destroy());
        }

        private void OnCardFocus(Entity entity)
        {
            var card = entity.getComponent<Card>();
            var sprite = entity.getComponent<Sprite>();
            if (_deckBuilderActor.BuyPoints < card.Cost)
            {
                CardSelector.MoveNext();
            }
            else
            {
                sprite.subtexture = new Subtexture(_deckBuilderActor.HoverCardTexture);
            }
        }

        private void OnCardBlur(Entity entity)
        {
            entity.getComponent<Sprite>().subtexture = new Subtexture(_deckBuilderActor.DefaultCardTexture);
        }

        private void OnCardSelect(Entity entity)
        {
            var card = entity.getComponent<Card>();
            BuyCard(card);
        }

        private void BuyCard(Card card)
        {
            _deckBuilderActor.BuyableHand.Remove(card);
            _deckBuilderActor.DiscardPile.Add(card);
            _deckBuilderActor.SelectedBuyableCard = card;
        }
    }
}
