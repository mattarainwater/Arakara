using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Dialogue
{
    public class DialogueEntry
    {
        public string RawText { get; set; }
        public string SpeakerName { get; set; }
        public DialoguePortrait LeftPortait { get; set; }
        public DialoguePortrait RightPortait { get; set; }

        public DialogueEntry(string rawText, string speakerName, DialoguePortrait leftPortrait = null, DialoguePortrait rightPortrait = null)
        {
            RawText = rawText;
            SpeakerName = speakerName;
            LeftPortait = leftPortrait;
            RightPortait = rightPortrait;
        }
    }
}
