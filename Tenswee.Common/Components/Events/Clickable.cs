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

        public const string OnClickNotifcation = "Clickable.OnClick";

        public Clickable(RectangleF rectangle)
        {
            Rectangle = rectangle;
        }

        public override void OnAddedToEntity()
        {
            Rectangle.Location = Entity.Transform.Position;
        }

        public virtual void Update()
        {
            if(Input.LeftMouseButtonPressed 
                && CheckMousePosition())
            {
                this.PostNotification(OnClickNotifcation, Input.MousePosition);
            }
        }

        private bool CheckMousePosition()
        {
            var mousePos = Entity.Scene.Camera.MouseToWorldPoint();
            return Rectangle.Contains(mousePos);
        }
    }
}
