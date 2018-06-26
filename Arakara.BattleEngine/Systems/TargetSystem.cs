using Arakara.BattleEngine.Actions;
using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Models;
using System.Collections.Generic;
using System.Linq;
using Tenswee.Common.Containers;
using Tenswee.Common.Extensions;
using Tenswee.Common.Notifications;

namespace Arakara.BattleEngine.Systems
{
    public class TargetSystem : Aspect, IObservable
    {
        public void Awake()
        {
            this.AddObserver(OnValidatePlayCard, Global.ValidateNotification<BattleMoveAction>());
        }

        public void Destroy()
        {
            this.RemoveObserver(OnValidatePlayCard, Global.ValidateNotification<BattleMoveAction>());
        }

        public void AutoTarget(Move move, Control control, int factionId)
        {
            var target = move.GetAspect<Target>();
            if (target == null)
                return;
            var mark = control == Control.Computer ? target.Preferred : target.Allowed;
            var candidates = GetActors(move, mark, factionId);
            target.Selected = candidates.Count > 0 ? candidates.PickRandom() : null;
        }

        public List<Actor> GetActors(Move source, Mark mark, int factionId)
        {
            var dataSystem = Container.GetAspect<DataSystem>();
            var actors = new List<Actor>();
            return dataSystem.Battle.Actors.Where(x => mark.Side == Side.Ally ? x.FactionId == factionId : x.FactionId != factionId).ToList();
        }

        void OnValidatePlayCard(object sender, object args)
        {
            var battleMoveAction = sender as BattleMoveAction;
            var move = battleMoveAction.Move;
            var target = move.GetAspect<Target>();
            if (target == null || (target.Required == false && target.Selected == null))
                return;
            var validator = args as Validator;
            var candidates = GetActors(move, target.Allowed, battleMoveAction.FactionId);
            if (!candidates.Contains(target.Selected))
            {
                validator.Invalidate();
            }
        }
    }
}
