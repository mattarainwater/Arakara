﻿using Arakara.Battle;
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
using Arakara.Factories.Entities;

namespace Arakara.Components
{
    public class DeckBuilderActor : BattleActor
    {
        public List<Card> Deck { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> DiscardPile { get; set; }
        public List<Entity> HandEntities { get; set; }

        public Card SelectedCard { get; set; }

        public List<Card> BuyableDeck { get; set; }
        public List<Entity> BuyableHandEntities { get; set; }

        public int BuyPoints { get; set; }

        public Card SelectedBuyableCard { get; set; }

        public Texture2D DefaultCardTexture { get; set; }
        public Texture2D HoverCardTexture { get; set; }
        public Texture2D DisabledCardTexture { get; set; }

        public Texture2D BackdropTexture { get; set; }

        public CardEntityFactory Factory { get; set; }

        public DeckBuilderActor(string name, 
            int maxHP, 
            Faction faction, 
            List<Card> cards,
            List<Card> buyableCards,
            float dodgeChance, 
            float critChance, 
            float speed) 
                : base(name, maxHP, faction, dodgeChance, critChance, speed)
        {
            Deck = cards;
            Deck.Shuffle();
            Hand = new List<Card>();
            DiscardPile = new List<Card>();
            HandEntities = new List<Entity>();
            
            BuyableDeck = buyableCards;
            BuyableHandEntities = new List<Entity>();
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();

            DefaultCardTexture = Entity.Scene.Content.Load<Texture2D>("card");
            DisabledCardTexture = Entity.Scene.Content.Load<Texture2D>("card_dark");
            HoverCardTexture = Entity.Scene.Content.Load<Texture2D>("card_light");
            BackdropTexture = Entity.Scene.Content.Load<Texture2D>("backdrop");

            Factory = new CardEntityFactory(DefaultCardTexture);
        }
    }
}
