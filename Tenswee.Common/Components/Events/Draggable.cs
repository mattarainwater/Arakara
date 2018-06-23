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
            if(Input.leftMouseButtonReleased)
            {
                _isDragging = false;
            }
            if(_isDragging)
            {
                var newPos = entity.scene.camera.mouseToWorldPoint() - new Vector2(Rectangle.width / 2, Rectangle.height / 2);
                var tween = entity.transform.tweenPositionTo(newPos, .02f);
                tween.start();
                //entity.transform.position = newPos;
                Rectangle.location = entity.transform.position;
            }
        }

        private void drag(object sender, object entity)
        {
            _isDragging = true;
        }
    }
}
