using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Models.Deckbuilder
{
    public class Card : Container
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Cost { get; set; }
        public Zones Zone { get; set; }
    }
}