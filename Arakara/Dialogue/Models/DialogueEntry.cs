using Arakara.DialogueEngine.Models;
using System.Collections.Generic;

namespace Arakara.Dialogue.Models
{
    public class DialogueEntry
    {
        public string RawText { get; set; }
        public List<DialogueChoice> Choices { get; set; }

        public DialogueEntry(string rawText, List<DialogueChoice> choices)
        {
            RawText = rawText;
            Choices = choices;
        }
    }
}
