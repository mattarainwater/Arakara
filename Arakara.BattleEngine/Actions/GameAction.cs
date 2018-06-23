using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;
using Tenswee.Common.Notifications;

namespace Arakara.BattleEngine.Actions
{
    public class GameAction
    {
        #region Fields & Properties
        public readonly int id;
        public int priority { get; set; }
        public int orderOfPlay { get; set; }
        public bool isCanceled { get; protected set; }
        public Phase prepare { get; protected set; }
        public Phase perform { get; protected set; }
        public Phase cancel { get; protected set; }
        #endregion

        #region Constructor
        public GameAction()
        {
            id = Global.GenerateID(this.GetType());
            prepare = new Phase(this, OnPrepareKeyFrame);
            perform = new Phase(this, OnPerformKeyFrame);
            cancel = new Phase(this, OnCancelKeyFrame);
        }
        #endregion

        #region Public
        public virtual void Cancel()
        {
            isCanceled = true;
        }
        #endregion

        #region Protected
        protected virtual void OnPrepareKeyFrame(IContainer game)
        {
            var notificationName = Global.PrepareNotification(this.GetType());
            game.PostNotification(notificationName, this);
        }

        protected virtual void OnPerformKeyFrame(IContainer game)
        {
            var notificationName = Global.PerformNotification(this.GetType());
            game.PostNotification(notificationName, this);
        }

        protected virtual void OnCancelKeyFrame(IContainer game)
        {
            var notificationName = Global.CancelNotification(this.GetType());
            game.PostNotification(notificationName, this);
        }
        #endregion
    }
}
