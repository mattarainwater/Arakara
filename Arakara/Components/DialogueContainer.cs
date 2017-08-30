using Arakara.Common;
using Arakara.Dialogue;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class DialogueContainer : Component, IUpdatable
    {
        private int _screenWidth;
        private int _screenHeight;
        private int _marginLeftRight;
        private int _marginUpDown;
        private int _entityYPos;
        private int _halfWayPoint;
        private int _spacing;

        public DialogueController Controller { get; set; }

        public DialogueContainer(DialogueActor dialogueActor, Action onComplete, int screenWidth, int screenHeight)
        {
            Controller = new DialogueController(dialogueActor, onComplete);
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _marginLeftRight = _screenWidth / 20;
            _marginUpDown = screenHeight / 20;

            _halfWayPoint = _screenWidth / 2;
            _entityYPos = 230;
            _spacing = _screenWidth / 40;

            dialogueActor.transform.position = new Vector2(20, _entityYPos);
            dialogueActor.LeftPortaitPosition = new Vector2(0, _entityYPos - DimensionConstants.PORTRAIT_HEIGHT - 10);
            dialogueActor.RightPortaitPosition = new Vector2(_screenWidth - DimensionConstants.PORTRAIT_WIDTH, _entityYPos - DimensionConstants.PORTRAIT_HEIGHT - 10);
        }

        public void update()
        {
            Controller.Update();
        }

        public void ToggleDialogueActive()
        {
            Controller.IsActive = true;
        }
    }
}
