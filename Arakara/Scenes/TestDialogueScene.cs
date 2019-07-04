using Arakara.Common;
using Arakara.Components;
using Arakara.Dialogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Scenes
{
    public class TestDialogueScene : BaseScene
    {
        private DialogueContainer _dialogue;

        public override void onStart()
        {
            var dialogueActorEntity = createEntity("dialogueActor");
            var textBoxTexture = content.Load<Texture2D>("textbox-2");
            var nameBoxTexture = content.Load<Texture2D>("namebox");
            var markerTexture = content.Load<Texture2D>("marker");
            var dialogueActor = dialogueActorEntity.addComponent(new DialogueActor(textBoxTexture, nameBoxTexture, markerTexture));

            var dialogueEntity = createEntity("dialogue");
            _dialogue = new DialogueContainer(dialogueActor, OnComplete, DimensionConstants.GetCurrentResolution().ScreenWidth, DimensionConstants.GetCurrentResolution().ScreenHeight);
            dialogueEntity.addComponent(_dialogue);

            var texture1 = content.Load<Texture2D>("fuuka");
            var texture2 = content.Load<Texture2D>("akihiko");
            //var leftPortrait = new DialoguePortrait(texture1);
            //var rightPortrait = new DialoguePortrait(texture2);

            //_dialogue.Controller.AddDialogueEntry(new DialogueEntry("This is a test! This is a test! This is a test! This isss a test! This is a test! This is a test!", "Prisca", leftPortrait, rightPortrait, true));
            //_dialogue.Controller.AddDialogueEntry(new DialogueEntry("This is a test 22!", "Prisca", leftPortrait, rightPortrait, false));
            //_dialogue.Controller.AddDialogueEntry(new DialogueEntry("This is a test, a very good one!", "Prisca", leftPortrait, rightPortrait, true));

            _dialogue.ToggleDialogueActive();
        }

        private void OnComplete()
        {
            Core.scene = new TestDialogueScene();
        }
    }
}
