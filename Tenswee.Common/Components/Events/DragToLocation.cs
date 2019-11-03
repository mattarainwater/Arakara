using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Notifications;

namespace Tenswee.Common.Components
{
    public class DragToLocation : Clickable
    {
        private bool _isDragging;

        public const string OnDropNotification = "DragToLocation.OnDrop";

        public DragToLocation(RectangleF rectangle)
            : base(rectangle)
        {
        }

        public override void OnEnabled()
        {
            this.AddObserver(Drag, OnClickNotifcation);
        }

        public override void OnDisabled()
        {
            this.RemoveObserver(Drag, OnClickNotifcation);
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
        }

        public override void Update()
        {
            base.Update();
            if (Input.LeftMouseButtonReleased && _isDragging)
            {
                _isDragging = false;
                this.PostNotification(OnDropNotification, Input.MousePosition);
            }
        }

        private void Drag(object sender, object position)
        {
            _isDragging = true;
        }
    }
}
