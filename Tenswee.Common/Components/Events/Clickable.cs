using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Notifications;

namespace Tenswee.Common.Components
{
    public class Clickable : Component, IUpdatable
    {
        public RectangleF Rectangle;

        public const string onClickNotification = "Clickable.onClick";

        public Clickable(RectangleF rectangle)
        {
            Rectangle = rectangle;
        }

        public override void onAddedToEntity()
        {
            Rectangle.location = entity.transform.position;
        }

        public virtual void update()
        {
            if(Input.leftMouseButtonPressed 
                && checkMousePosition())
            {
                this.PostNotification(onClickNotification, Input.mousePosition);
            }
        }

        private bool checkMousePosition()
        {
            var mousePos = entity.scene.camera.mouseToWorldPoint();
            return Rectangle.contains(mousePos);
        }
    }
}
