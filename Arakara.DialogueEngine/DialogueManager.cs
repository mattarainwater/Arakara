using Arakara.DialogueEngine.Models;
using Ink.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.DialogueEngine
{
    public class DialogueManager
    {
        private Story _story;

        public void Load(string json)
        {
            _story = new Story(json);
        }

        public string GetCurrentText()
        {
            return _story.currentText;
        }

        public bool CanContinue()
        {
            return _story.canContinue;
        }

        public void Continue()
        {
            if(_story.canContinue)
            {
                _story.Continue();
            }
        }

        public void SetChoice(int choiceIndex)
        {
            if(_story.currentChoices.Any(x => x.index == choiceIndex))
            {
                _story.ChooseChoiceIndex(choiceIndex);
            }
        }

        public IEnumerable<DialogueChoice> GetCurrentChoices()
        {
            return _story.currentChoices.Select(x => new DialogueChoice(x));
        }

        public IEnumerable<string> GetTags()
        {
            return _story.currentTags;
        }
    }
}
