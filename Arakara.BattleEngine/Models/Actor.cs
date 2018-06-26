namespace Arakara.BattleEngine.Models
{
    public abstract class Actor
    {
        public Actor(int index)
        {
            Index = index;
        }

        public readonly int Index;
        public string Name { get; set; }

        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int FactionId { get; set; }
    }
}