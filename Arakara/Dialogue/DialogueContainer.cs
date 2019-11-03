using Arakara.Common;
using Arakara.Dialogue;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Dialogue
{
    public class DialogueContainer : Component, IUpdatable
    {
        private int _screenWidth;
        private int _screenHeight;
        private int _marginLeftRight;
        private int _marginUpDown;

        public DialogueController Controller { get; set; }

        public DialogueContainer(DialogueActor dialogueActor, Action onComplete, int screenWidth, int screenHeight, string dialogueJson)
        {
            Controller = new DialogueController(dialogueActor, onComplete, dialogueJson);
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _marginLeftRight = _screenWidth / 20;
            _marginUpDown = screenHeight / 20;
        }

        public void Update()
        {
            Controller.Update();
        }

        public void ToggleDialogueActive()
        {
            Controller.IsActive = true;
        }
    }
}
