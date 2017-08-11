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
using Arakara.Battle.Phases;

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
        public List<Card> BuyableHand { get; set; }
        public List<Card> BuyableDiscardPile { get; set; }
        public List<Entity> BuyableHandEntities { get; set; }

        public int BuyPoints { get; set; }

        public Card SelectedBuyableCard { get; set; }

        public Texture2D DefaultCardTexture { get; set; }
        public Texture2D HoverCardTexture { get; set; }
        public Texture2D DisabledCardTexture { get; set; }

        public Texture2D BackdropTexture { get; set; }

        public DeckBuilderActor()
        {
            Hand = new List<Card>();
            DiscardPile = new List<Card>();
            HandEntities = new List<Entity>();

            BuyableHand = new List<Card>();
            BuyableDiscardPile = new List<Card>();
            BuyableHandEntities = new List<Entity>();
        }

        public DeckBuilderActor(string name, 
            int maxHP, 
            Faction faction, 
            List<Card> cards,
            List<Card> buyableCards,
            float dodgeChance, 
            float critChance, 
            float speed,
            Animations idleAnimation) 
                : base(name, maxHP, faction, dodgeChance, critChance, speed, idleAnimation)
        {
            Deck = cards;
            Deck.shuffle();
            Hand = new List<Card>();
            DiscardPile = new List<Card>();
            HandEntities = new List<Entity>();
            
            BuyableDeck = buyableCards;
            BuyableDeck.shuffle();
            BuyableHand = new List<Card>();
            BuyableDiscardPile = new List<Card>();
            BuyableHandEntities = new List<Entity>();
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            DefaultCardTexture = entity.scene.contentManager.Load<Texture2D>("card");
            DisabledCardTexture = entity.scene.contentManager.Load<Texture2D>("card_dark");
            HoverCardTexture = entity.scene.contentManager.Load<Texture2D>("card_light");
            BackdropTexture = entity.scene.contentManager.Load<Texture2D>("backdrop");
        }
    }
}
