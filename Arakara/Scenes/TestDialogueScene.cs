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
    public class TestDialogueScene : Scene
    {
        private DialogueContainer _dialogue;

        public TestDialogueScene()
            : base()
        {
        }

        public override void initialize()
        {
            addRenderer(new RenderLayerExcludeRenderer(0));
            setDesignResolution(DimensionConstants.SCREEN_WIDTH, DimensionConstants.SCREEN_HEIGHT, SceneResolutionPolicy.NoBorderPixelPerfect);
            clearColor = Color.WhiteSmoke;
        }

        public override void onStart()
        {
            var dialogueActorEntity = createEntity("dialogueActor");
            var dialogueActor = dialogueActorEntity.addComponent(new DialogueActor());

            var dialogueEntity = createEntity("dialogue");
            _dialogue = new DialogueContainer(dialogueActor, OnComplete, DimensionConstants.SCREEN_WIDTH, DimensionConstants.SCREEN_HEIGHT);
            dialogueEntity.addComponent(_dialogue);

            var texture = contentManager.Load<Texture2D>("prisca-portrait");
            var leftPortrait = new DialoguePortrait(texture);
            leftPortrait.IsActive = true;
            var rightPortrait = new DialoguePortrait(texture);

            _dialogue.Controller.AddDialogueEntry(new DialogueEntry("This is a test!This is a test!This is a test!This is a test!This is a test!This is a test!", "Prisca", leftPortrait, rightPortrait));
            _dialogue.Controller.AddDialogueEntry(new DialogueEntry("This is a test 22!", "Prisca", rightPortrait, leftPortrait));
            _dialogue.Controller.AddDialogueEntry(new DialogueEntry("This is a test, a very good one!", "Prisca", leftPortrait, rightPortrait));

            _dialogue.ToggleDialogueActive();
        }

        private void OnComplete()
        {
            Core.startSceneTransition(new FadeTransition(LoadStartScene) { fadeInDuration = 0f, fadeOutDuration = 0f, delayBeforeFadeInDuration = 0f, fadeToColor = Color.WhiteSmoke });
        }

        private Scene LoadStartScene()
        {
            return new TestGameScene();
        }
    }
}
