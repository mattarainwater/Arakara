using Arakara.BattleEngine.Interfaces;
using Arakara.BattleEngine.Models;
using Arakara.BattleEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Actions
{
    public class DamageAction : GameAction, IAbilityLoader
    {
        public List<Actor> Targets { get; set; }
        public int Amount { get; set; }

        public DamageAction()
        {

        }

        public DamageAction(Actor target, int amount)
        {
            Targets = new List<Actor>(1);
            Targets.Add(target);
            Amount = amount;
        }

        public DamageAction(List<Actor> targets, int amount)
        {
            Targets = targets;
            Amount = amount;
        }

        public void Load(IContainer game, Ability ability)
        {
            var targetSelector = ability.GetAspect<ITargetSelector>();
            var actors = targetSelector.SelectTargets(game, game.GetBattle().CurrentActor.FactionId);
            Targets = new List<Actor>();
            foreach (Actor actor in actors)
            {
                if (actor != null)
                    Targets.Add(actor);
            }
            Amount = Convert.ToInt32(ability.UserInfo);
        }
    }
}
