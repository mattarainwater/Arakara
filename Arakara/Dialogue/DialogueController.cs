using Arakara.Common;
using Arakara.Components;
using Arakara.Dialogue.Models;
using Arakara.DialogueEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Dialogue
{
    public class DialogueController
    {
        public DialogueActor DialogueActor { get; set; }
        public bool IsActive { get; set; }
        public bool IsInitialized { get; set; }
        public Action OnComplete { get; set; }

        public int resindex { get; set; }

        private DialogueManager _manager;
        private bool _isDone;

        public DialogueController(DialogueActor actor, Action onComplete, string jsonDialogue)
        {
            DialogueActor = actor;
            OnComplete = onComplete;
            _manager = new DialogueManager();
            _manager.Load(jsonDialogue);
        }

        public void Update()
        {
            _isDone = !_manager.CanContinue() && !_manager.GetCurrentChoices().Any();

            if (VirtualButtons.Dummyinput.IsPressed)
            {
                resindex++;
                if(resindex == 3)
                {
                    resindex = 0;
                }
                DimensionConstants.SetCurrentResolution(resindex);
            }

            if (IsActive && !_isDone)
            {
                if (!IsInitialized)
                {
                    IsInitialized = true;
                    GetNextDialogue();
                }

                if(!DialogueActor.IsFinished)
                {
                    DialogueActor.Update();
                }

                if (VirtualButtons.SelectInput.IsPressed)
                {
                    if (DialogueActor.IsFinished)
                    {
                        GetNextDialogue();
                    }
                    else
                    {
                        DialogueActor.SkipToEnd();
                    }
                }
            }

            if(_isDone)
            {
                if (VirtualButtons.SelectInput.IsPressed)
                {
                    OnComplete();
                }
            }
        }

        private void GetNextDialogue()
        {
            _manager.Continue();
            DialogueActor.ResetDialogue(ToDialogue());
        }

        private DialogueEntry ToDialogue()
        {
            var text = _manager.GetCurrentText();
            var tags = _manager.GetTags().Select(x => x.Split(':'));
            var nameTag = tags.FirstOrDefault(x => x[0] == "name");
            var name = "";
            if(nameTag != null)
            {
                name = nameTag[1];
            }
            return new DialogueEntry(text, null)
            {
            };
        }
    }
}
