namespace Arakara.Battle.Events
{
    public abstract class BattleEventTrigger
    {
        public abstract bool IsTriggered(BattleController controller);
    }
}