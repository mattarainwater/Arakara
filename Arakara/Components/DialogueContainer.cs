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
        private int _entityYPos;
        private int _halfWayPoint;
        private int _spacing;

        public DialogueController Controller { get; set; }

        public DialogueContainer(DialogueActor dialogueActor, Action onComplete, int screenWidth, int screenHeight)
        {
            Controller = new DialogueController(dialogueActor, onComplete);
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            _halfWayPoint = _screenWidth / 2;
            _entityYPos = _screenHeight - (_screenHeight / 3);
            _marginLeftRight = _screenWidth / 5;
            _spacing = _screenWidth / 40;

            dialogueActor.transform.position = new Vector2(_marginLeftRight, _entityYPos);
            dialogueActor.LeftPortaitPosition = new Vector2(_marginLeftRight + 100, _entityYPos - 200);
            dialogueActor.RightPortaitPosition = new Vector2(_screenWidth - _marginLeftRight - 100, _entityYPos - 200);
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
