using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arakara.Components;
using Arakara.Common;
using Nez;
using Nez.Textures;
using Nez.Sprites;

namespace Arakara.Battle.Phases.DeckBuilder
{
    public class SelectCardPhase : Phase
    {
        private DeckBuilderActor _deckBuilderActor;
        public Selector CardSelector { get; set; }

        public SelectCardPhase(DeckBuilderActor actor) 
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
            _deckBuilderActor.SelectedCard = null;
            CardSelector.AddEntities(_deckBuilderActor.HandEntities);
        }

        protected override void update()
        {
            if(_deckBuilderActor.SelectedCard != null)
            {
                IsFinished = true;
            }
        }

        protected override void finish()
        {
            CardSelector.enabled = false;
            CardSelector.Reset();
        }

        private void OnCardFocus(Entity entity)
        {
            entity.getComponent<Sprite>().subtexture = new Subtexture(_deckBuilderActor.HoverCardTexture);
        }

        private void OnCardBlur(Entity entity)
        {
            entity.getComponent<Sprite>().subtexture = new Subtexture(_deckBuilderActor.DefaultCardTexture);
        }

        private void OnCardSelect(Entity entity)
        {
            var card = entity.getComponent<Card<Animations>>();
            PlayCard(card);
        }

        private void PlayCard(Card<Animations> card)
        {
            _deckBuilderActor.SelectedCard = card;
            Actor.CurrentAction = card.Action;
        }
    }
}
