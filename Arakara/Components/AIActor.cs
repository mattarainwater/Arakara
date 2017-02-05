﻿using Arakara.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class AIActor : BattleActor
    {
        public AIActor(int maxHP, Faction faction) :
            base(maxHP, faction)
        {

        }

        protected override void OnAwaitingDecision(BattleController controller)
        {
        }

        protected override void OnEndOfTurn(BattleController controller)
        {
        }

        protected override void OnStartOfTurn(BattleController controller)
        {
            var enemyActor = controller.Actors.First(x => x.Faction.Id != Faction.Id);
            if (!enemyActor.Immune)
            {
                enemyActor.CurrentHP -= 5;
            }
            Delay = 10;
            State = BattleState.NotTurn;
        }

        protected override void OnTargeting(BattleController controller)
        {
        }
    }
}