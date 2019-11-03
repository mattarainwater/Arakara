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

        public const string OnHoverNotification = "Hoverable.OnHover";
        public const string OnBlurNotification = "Hoverable.OnBlur";

        private bool _isHovering;

        public Hoverable(RectangleF rectangle)
        {
            Rectangle = rectangle;
        }

        public override void OnAddedToEntity()
        {
            Rectangle.Location = Entity.Transform.Position;
        }

        public void Update()
        {
            if (CheckMousePosition())
            {
                this.PostNotification(OnHoverNotification, Entity);
                _isHovering = true;
            }
            else if(_isHovering)
            {
                _isHovering = false;
                this.PostNotification(OnBlurNotification, Entity);
            }
        }

        private bool CheckMousePosition()
        {
            var mousePos = Entity.Scene.Camera.MouseToWorldPoint();
            return Rectangle.Contains(mousePos);
        }
    }
}
