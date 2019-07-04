using Arakara.DialogueEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;
using System.IO;

namespace Arakara.Scenes
{
    public class StartMenuScene : BaseScene
    {
        private UICanvas _canvas;
        private Table _table;

        public StartMenuScene()
        {
            _canvas = createEntity("ui").addComponent(new UICanvas());
            _canvas.isFullScreen = true;
            _canvas.renderLayer = SCREEN_SPACE_RENDER_LAYER;

            _table = _canvas.stage.addElement(new Table());
            _table.setFillParent(true).center();

            AddStartMenuOptions();

            var json = File.ReadAllText(@"Content\test.ink.json");
            var loader = new LoadTest();
            loader.Load(json);
        }

        private void AddStartMenuOptions()
        {
            var topButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f), new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
            {
                downFontColor = Color.Black,
            };
            _table.add(new TextButton("Start Game", topButtonStyle)).setFillX().setAlign(Align.center).setMinHeight(60).setMinWidth(200).getElement<Button>().onClicked += OnStartButtonClicked;
        }

        private void OnStartButtonClicked(Button button)
        {
            Core.startSceneTransition(new FadeTransition(LoadStartScene));
        }

        private Scene LoadStartScene()
        {
            return new TestDialogueScene();
        }
    }
}
