using Ink.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.DialogueEngine.Models
{
    public class DialogueChoice
    {
        public DialogueChoice(Choice choice)
        {
            Text = choice.text;
            Index = choice.index;
        }

        public string Text { get; set; }
        public int Index { get; set; }
    }
}
