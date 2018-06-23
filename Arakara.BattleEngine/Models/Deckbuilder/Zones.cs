namespace Arakara.BattleEngine.Models.Deckbuilder
{
    public enum Zones
    {
        None = 0,
        Deck = 1 << 1,
        Hand = 1 << 2,
        Active = 1 << 3,
        DiscardPile = 1 << 4,
        Trash = 1 << 5
    }
}