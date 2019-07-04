using Ink.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.DialogueEngine
{
    public class LoadTest
    {
        private Story _story;

        public void Load(string json)
        {
            _story = new Story(json);
        }
    }
}
