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

        public const string onDropNotification = "DragToLocation.onDrop";

        public DragToLocation(RectangleF rectangle)
            : base(rectangle)
        {
        }

        public override void onEnabled()
        {
            this.AddObserver(drag, onClickNotification);
        }

        public override void onDisabled()
        {
            this.RemoveObserver(drag, onClickNotification);
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
        }

        public override void update()
        {
            base.update();
            if (Input.leftMouseButtonReleased && _isDragging)
            {
                _isDragging = false;
                this.PostNotification(onDropNotification, Input.mousePosition);
            }
        }

        private void drag(object sender, object position)
        {
            _isDragging = true;
        }
    }
}
