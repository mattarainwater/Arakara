using System;

namespace Arakara.Battle.Events
{
    public abstract class BattleEventEffect : IComparable<BattleEventEffect>
    {
        public int Sequence { get; set; }
        public BattleEventEffectState State { get; set; }
        public BattleController Controller { get; set; }

        public BattleEventEffect(int sequence)
        {
            Sequence = sequence;
        }

        public int CompareTo(BattleEventEffect obj)
        {
            return Sequence.CompareTo(obj.Sequence);
        }

        public void Perform()
        {
            switch (State)
            {
                case BattleEventEffectState.StartOfEventEffect:
                    OnStartOfEvent();
                    break;
                case BattleEventEffectState.DuringEventEffect:
                    DuringEvent();
                    break;
                case BattleEventEffectState.EndOfEventEffect:
                    OnEndOfEvent();
                    break;
            }
        }

        protected abstract void OnStartOfEvent();

        protected abstract void DuringEvent();

        protected abstract void OnEndOfEvent();
    }
}