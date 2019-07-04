using Arakara.Common;
using Arakara.Components;
using Arakara.Dialogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Dialogue
{
    public class DialogueController
    {
        public List<DialogueEntry> DialogueEntries { get; set; }
        public DialogueEntry CurrentDialogueEntry { get; set; }
        public int CurrentDialogueIndex { get; set; }

        public DialogueActor DialogueActor { get; set; }

        public bool IsActive { get; set; }
        public bool IsInitialized { get; set; }

        public bool IsDone { get; set; }

        public Action OnComplete { get; set; }

        public int resindex { get; set; }

        public DialogueController(DialogueActor actor, Action onComplete)
        {
            DialogueEntries = new List<DialogueEntry>();
            DialogueActor = actor;
            OnComplete = onComplete;
        }

        public void AddDialogueEntry(DialogueEntry dialogue)
        {
            DialogueEntries.Add(dialogue);
        }

        public void Update()
        {
            if(VirtualButtons.Dummyinput.isPressed)
            {
                resindex++;
                if(resindex == 3)
                {
                    resindex = 0;
                }
                DimensionConstants.SetCurrentResolution(resindex);
            }

            if (IsActive && !IsDone)
            {
                if (!IsInitialized)
                {
                    IsInitialized = true;
                    CurrentDialogueIndex = 0;
                    CurrentDialogueEntry = DialogueEntries[CurrentDialogueIndex];
                    DialogueActor.ResetDialogue(DialogueEntries.First());
                }

                if(!DialogueActor.IsFinished)
                {
                    DialogueActor.Update();
                }

                if (VirtualButtons.SelectInput.isPressed)
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

                if(CurrentDialogueEntry == null)
                {
                    IsDone = true;
                }
            }

            if(IsDone)
            {
                if (VirtualButtons.SelectInput.isPressed)
                {
                    OnComplete();
                }
            }
        }

        private void GetNextDialogue()
        {
            CurrentDialogueIndex++;
            if (CurrentDialogueIndex == DialogueEntries.Count())
            {
                CurrentDialogueEntry = null;
                return;
            }
            CurrentDialogueEntry = DialogueEntries[CurrentDialogueIndex];
            DialogueActor.ResetDialogue(CurrentDialogueEntry);
        }
    }
}
