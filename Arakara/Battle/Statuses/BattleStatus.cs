using Arakara.Components;

namespace Arakara.Battle.Statuses
{
    public abstract class BattleStatus
    {
        public int Duration { get; set; }
        public int CurrentDuration { get; set; }

        public BattleStatus(int duration)
        {
            Duration = duration;
            CurrentDuration = 0;
        }

        public abstract void Apply(BattleActor actor, BattleController controller);
        public abstract string GetDescription();
        public abstract string GetCode();
    }
}