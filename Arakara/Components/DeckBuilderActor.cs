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

            DefaultCardTexture = entity.scene.contentManager.Load<Texture2D>("card");
            HoverCardTexture = entity.scene.contentManager.Load<Texture2D>("card_dark");
        }
    }
}
