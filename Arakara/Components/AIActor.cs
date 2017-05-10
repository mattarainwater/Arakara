using Arakara.Battle;
using Arakara.Battle.AI;
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

        public AIActor(string name, int maxHP, Faction faction, List<BattleAction<TEnum>> actions, AIDecider decider, float dodgeChance, float critChance) :
            base(name, maxHP, faction, dodgeChance, critChance)
        {
            _actions = actions;
            _decider = decider;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            _animator = entity.getComponent<Sprite<TEnum>>();
        }

        protected override void OnStartOfTurn()
        {
            _currentAction = (BattleAction<TEnum>)_decider.ChooseAction(this, _actions.Select(x => (BattleAction)x).ToList(), Controller);
            if (_animator != null)
            {
                _animator.play(_currentAction.Animation);
                _animator.originNormalized = Vector2.Zero;
            }
            State = BattleState.DuringTurn;
        }

        protected override void DuringTurn()
        {
            var enemyActor = Controller.Actors.FirstOrDefault(y => y.Faction.Id != Faction.Id);
            if(enemyActor != null)
            {
                if (_animator != null)
                {
                    if (!_animator.isPlaying)
                    {
                        _currentAction.Effect.Perform(this, enemyActor, Controller);
                        State = BattleState.EndOfTurn;
                    }
                }
                else
                {
                    _currentAction.Effect.Perform(this, enemyActor, Controller);
                    State = BattleState.EndOfTurn;
                }
            }
            else
            {
                State = BattleState.NotTurn;
            }
        }

        protected override void OnEndOfTurn()
        {
            if(_currentAction != null)
            {
                Delay = _currentAction.Speed;
                _currentAction = null;
            }
            Core.schedule(1f, t => {
                State = BattleState.NotTurn;
            });
        }
    }
}
