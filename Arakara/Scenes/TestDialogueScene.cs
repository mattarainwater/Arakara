using Arakara.Common;
using Arakara.Components;
using Arakara.Dialogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Scenes
{
    public class TestDialogueScene : BaseScene
    {
        private DialogueContainer _dialogue;

        public override void OnStart()
        {
            var dialogueActorEntity = CreateEntity("dialogueActor");
            var textBoxTexture = Content.Load<Texture2D>("textbox-2");
            var nameBoxTexture = Content.Load<Texture2D>("namebox");
            var markerTexture = Content.Load<Texture2D>("marker");
            var dialogueActor = dialogueActorEntity.AddComponent(new DialogueActor());

            var dialogueEntity = CreateEntity("dialogue");

            var json = File.ReadAllText(@"Content\test.ink.json");

            _dialogue = new DialogueContainer(dialogueActor, OnComplete, DimensionConstants.GetCurrentResolution().ScreenWidth, DimensionConstants.GetCurrentResolution().ScreenHeight, json);
            dialogueEntity.AddComponent(_dialogue);

            var texture1 = Content.Load<Texture2D>("fuuka");
            var texture2 = Content.Load<Texture2D>("akihiko");
            //var leftPortrait = new DialoguePortrait(texture1);
            //var rightPortrait = new DialoguePortrait(texture2);

            //_dialogue.Controller.AddDialogueEntry(new DialogueEntry("This is a test! This is a test! This is a test! This isss a test! This is a test! This is a test!", "Prisca", leftPortrait, rightPortrait, true));
            //_dialogue.Controller.AddDialogueEntry(new DialogueEntry("This is a test 22!", "Prisca", leftPortrait, rightPortrait, false));
            //_dialogue.Controller.AddDialogueEntry(new DialogueEntry("This is a test, a very good one!", "Prisca", leftPortrait, rightPortrait, true));

            _dialogue.ToggleDialogueActive();
        }

        private void OnComplete()
        {
            Core.Scene = new TestDialogueScene();
        }
    }
}
