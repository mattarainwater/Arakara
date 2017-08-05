using Arakara.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using Nez;
using Microsoft.Xna.Framework;
using Arakara.Common;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez.Textures;
using Arakara.Battle.Phases;

namespace Arakara.Components
{
    public class DeckBuilderActor : BattleActor
    {
        public List<Card<Animations>> Deck { get; set; }
        public List<Card<Animations>> Hand { get; set; }
        public List<Card<Animations>> DiscardPile { get; set; }
        public List<Entity> HandEntities { get; set; }

        public Card<Animations> SelectedCard { get; set; }

        public Texture2D DefaultCardTexture { get; set; }
        public Texture2D HoverCardTexture { get; set; }

        public Selector CardSelector { get; set; }
        public Selector TargetSelector { get; set; }
        
        private VirtualButton _selectInput;
        private VirtualButton _backInput;
        private VirtualButton _leftInput;
        private VirtualButton _rightInput;

        public DeckBuilderActor(string name, 
            int maxHP, 
            Faction faction, 
            List<Card<Animations>> cards, 
            float dodgeChance, 
            float critChance, 
            float speed,
            Animations idleAnimation) 
                : base(name, maxHP, faction, dodgeChance, critChance, speed)
        {
            Deck = cards;
            Deck.shuffle();
            Hand = new List<Card<Animations>>();
            DiscardPile = new List<Card<Animations>>();
            HandEntities = new List<Entity>();
            IdleAnimation = idleAnimation;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            SetupInput();

            DefaultCardTexture = entity.scene.contentManager.Load<Texture2D>("card");
            HoverCardTexture = entity.scene.contentManager.Load<Texture2D>("card_dark");

            var cardSelectorEntity = entity.scene.createEntity("cardSelector");
            CardSelector = cardSelectorEntity.addComponent(new Selector(
                _selectInput,
                _leftInput,
                _rightInput,
                onFocus: OnCardFocus,
                onBlur: OnCardBlur,
                onSelect: OnCardSelect));
            CardSelector.enabled = false;

            var targetSelectorEntity = entity.scene.createEntity("targetSelector");
            TargetSelector = targetSelectorEntity.addComponent(new Selector(
                _selectInput,
                _leftInput,
                _rightInput,
                onFocus: OnTargetFocus,
                onBlur: OnTargetBlur,
                onSelect: OnTargetSelect));
            TargetSelector.enabled = false;
        }

        public override void onRemovedFromEntity()
        {
            _selectInput.deregister();
            _backInput.deregister();
            _leftInput.deregister();
            _rightInput.deregister();
        }

        private void SetupInput()
        {
            _selectInput = new VirtualButton();
            _selectInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.J));
            _selectInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.A));

            _backInput = new VirtualButton();
            _backInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.L));
            _backInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.B));

            _leftInput = new VirtualButton();
            _leftInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.A));
            _leftInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.Left));
            _leftInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.DPadLeft));
            _leftInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.LeftThumbstickLeft));

            _rightInput = new VirtualButton();
            _rightInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.D));
            _rightInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.Right));
            _rightInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.DPadRight));
            _rightInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.LeftThumbstickRight));
        }

        public void PlayCard(Card<Animations> card)
        {
            SelectedCard = card;
            CurrentAction = card.Action;
            var targets = Controller.MakeTargetables(this, SelectedCard.Action.Targeting);
            foreach(var target in targets)
            {
                TargetSelector.AddEntity(target);
            }
        }

        private void OnCardFocus(Entity entity)
        {
            entity.getComponent<Sprite>().subtexture = new Subtexture(HoverCardTexture);
        }

        private void OnCardBlur(Entity entity)
        {
            entity.getComponent<Sprite>().subtexture = new Subtexture(DefaultCardTexture);
        }

        private void OnCardSelect(Entity entity)
        {
            var card = entity.getComponent<Card<Animations>>();
            PlayCard(card);
        }

        private void OnTargetFocus(Entity entity)
        {
            var simplePolygon = entity.getComponent<TargetPolygon>();
            if (simplePolygon != null)
            {
                simplePolygon.setColor(Color.Red);
            }
        }

        private void OnTargetBlur(Entity entity)
        {
            var simplePolygon = entity.getComponent<TargetPolygon>();
            if(simplePolygon != null)
            {
                simplePolygon.setColor(Color.LightPink);
            }
        }

        private void OnTargetSelect(Entity entity)
        {
            var battleActor = entity.getComponent<BattleActor>();
            Controller.CurrentActor.SelectTarget(battleActor);
        }
    }
}
