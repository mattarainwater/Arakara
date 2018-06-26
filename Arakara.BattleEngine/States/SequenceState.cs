using Arakara.BattleEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Notifications;
using Tenswee.Common.States;

namespace Arakara.BattleEngine.States
{
    public class SequenceState : BaseState
    {
        public const string EnterNotification = "SequenceState.EnterNotification";
        public const string ExitNotification = "SequenceState.ExitNotification";
    }
}
