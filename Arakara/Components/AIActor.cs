using Arakara.Battle;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class AIActor<TEnum> : BattleActor where TEnum : struct, IComparable, IFormattable
    {
        private Sprite<TEnum> _animator;
        private List<BattleAction<TEnum>> _actions;
        private BattleAction<TEnum> _currentAction;
        private AIDecider _decider;

        public AIActor(string name, int maxHP, Faction faction, List<BattleAction<TEnum>> actions, AIDecider decider) :
            base(name, maxHP, faction)
        {
            _actions = actions;
            _decider = decider;
        }

        public override void onAddedToEntity()
        {
            _animator = entity.getComponent<Sprite<TEnum>>();
        }

        protected override void OnAwaitingDecision(BattleController controller)
        {
            if (!_animator.isPlaying)
            {
                var enemyActor = controller.Actors.First(y => y.Faction.Id != Faction.Id);
                _currentAction.Effect.Perform(this, enemyActor, controller);
                State = BattleState.EndOfTurn;
            }
        }

        protected override void OnEndOfTurn(BattleController controller)
        {
            Delay = _currentAction.Speed;
            State = BattleState.NotTurn;
            _currentAction = null;
        }

        protected override void OnStartOfTurn(BattleController controller)
        {
            _currentAction = (BattleAction<TEnum>)_decider.ChooseAction(this, _actions.Select(x => (BattleAction)x).ToList(), controller);
            _animator.play(_currentAction.Animation);
            _animator.originNormalized = Vector2.Zero;
            State = BattleState.AwaitingDecision;
        }

        protected override void OnTargeting(BattleController controller)
        {
        }
    }
}
