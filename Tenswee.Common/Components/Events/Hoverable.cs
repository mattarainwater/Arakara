using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Notifications;

namespace Tenswee.Common.Components
{
    public class Hoverable : Component, IUpdatable
    {
        public RectangleF Rectangle;

        public const string onHoverNotification = "Hoverable.onHover";
        public const string onBlurNotification = "Hoverable.onBlur";

        private bool _isHovering;

        public Hoverable(RectangleF rectangle)
        {
            Rectangle = rectangle;
        }

        public override void onAddedToEntity()
        {
            Rectangle.location = entity.transform.position;
        }

        public void update()
        {
            if (checkMousePosition())
            {
                this.PostNotification(onHoverNotification, entity);
                _isHovering = true;
            }
            else if(_isHovering)
            {
                _isHovering = false;
                this.PostNotification(onBlurNotification, entity);
            }
        }

        private bool checkMousePosition()
        {
            var mousePos = entity.scene.camera.mouseToWorldPoint();
            return Rectangle.contains(mousePos);
        }
    }
}
