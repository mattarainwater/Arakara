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
    public class Draggable : Clickable
    {
        private bool _isDragging;

        public Draggable(RectangleF rectangle)
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
            if(Input.LeftMouseButtonReleased)
            {
                _isDragging = false;
            }
            if(_isDragging)
            {
                var newPos = Entity.Scene.Camera.MouseToWorldPoint() - new Vector2(Rectangle.Width / 2, Rectangle.Height / 2);
                var tween = Entity.Transform.TweenPositionTo(newPos, .02f);
                tween.Start();
                //entity.transform.position = newPos;
                Rectangle.Location = Entity.Transform.Position;
            }
        }

        private void Drag(object sender, object entity)
        {
            _isDragging = true;
        }
    }
}
