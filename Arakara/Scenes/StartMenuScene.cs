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
            _canvas = CreateEntity("ui").AddComponent(new UICanvas());
            _canvas.IsFullScreen = true;
            _canvas.RenderLayer = SCREEN_SPACE_RENDER_LAYER;

            _table = _canvas.Stage.AddElement(new Table());
            _table.SetFillParent(true).Center();

            AddStartMenuOptions();
        }

        private void AddStartMenuOptions()
        {
            var topButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f), new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
            {
                DownFontColor = Color.Black,
            };
            _table.Add(new TextButton("Start Game", topButtonStyle)).SetFillX().SetAlign(Align.Center).SetMinHeight(60).SetMinWidth(200).GetElement<Button>().OnClicked += OnStartButtonClicked;
        }

        private void OnStartButtonClicked(Button button)
        {
            Core.StartSceneTransition(new FadeTransition(LoadStartScene));
        }

        private Scene LoadStartScene()
        {
            return new TestDialogueScene();
        }
    }
}
