using Arakara.Common;
using Arakara.Components;
using Microsoft.Xna.Framework;
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

        private Vector2 _centerPos;
        private Vector2 _leftPos;
        private Vector2 _rightPos;

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
            var backdrop = Actor.entity.scene.findEntity("background");
            _leftPos = new Vector2(backdrop.transform.position.X + (175 * 0), backdrop.transform.position.Y + 50);
            _centerPos = new Vector2(backdrop.transform.position.X + (175 * 1), backdrop.transform.position.Y + 50);
            _rightPos = new Vector2(backdrop.transform.position.X + (175 * 2), backdrop.transform.position.Y + 50);

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
            if (_deckBuilderActor.SelectedBuyableCard != null || VirtualButtons.BackInput.isPressed)
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
            var index = _deckBuilderActor.BuyableHandEntities.IndexOf(entity);
            if (_deckBuilderActor.BuyPoints >= card.Cost)
            {
                sprite.subtexture = new Subtexture(_deckBuilderActor.HoverCardTexture);
            }

            _deckBuilderActor.BuyableHandEntities.ForEach(x => x.enabled = false);

            Entity left = null;
            Entity right = null;
            if (index == 0)
            {
                left = _deckBuilderActor.BuyableHandEntities.Last();
                right = _deckBuilderActor.BuyableHandEntities[index + 1];
            }
            else if(index == _deckBuilderActor.BuyableHandEntities.Count() - 1)
            {
                left = _deckBuilderActor.BuyableHandEntities[index - 1];
                right = _deckBuilderActor.BuyableHandEntities.First();
            }
            else
            {
                left = _deckBuilderActor.BuyableHandEntities[index - 1];
                right = _deckBuilderActor.BuyableHandEntities[index + 1];
            }

            left.enabled = true;
            left.transform.position = _leftPos;

            right.enabled = true;
            right.transform.position = _rightPos;

            entity.enabled = true;
            entity.transform.position = _centerPos;
        }

        private void OnCardBlur(Entity entity)
        {
            var card = entity.getComponent<Card>();
            var sprite = entity.getComponent<Sprite>();
            if (_deckBuilderActor.BuyPoints >= card.Cost)
            {
                sprite.subtexture = new Subtexture(_deckBuilderActor.DefaultCardTexture);
            }
        }

        private void OnCardSelect(Entity entity)
        {
            var card = entity.getComponent<Card>();
            if (_deckBuilderActor.BuyPoints >= card.Cost)
            {
                BuyCard(card);
            }
        }

        private void BuyCard(Card card)
        {
            _deckBuilderActor.DiscardPile.Add(card);
            _deckBuilderActor.SelectedBuyableCard = card;
        }
    }
}
