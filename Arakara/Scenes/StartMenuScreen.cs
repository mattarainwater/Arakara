using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;

namespace Arakara.Scenes
{
    public class StartMenuScreen : Scene
    {
        private UICanvas _canvas;
        private Table _table;

        public StartMenuScreen()
            : base()
        {
        }

        public override void initialize()
        {
            Screen.setSize(512 * 3, 256 * 3);
            addRenderer(new RenderLayerExcludeRenderer(0));

            _canvas = createEntity("ui").addComponent(new UICanvas());
            _canvas.isFullScreen = true;

            _table = _canvas.stage.addElement(new Table());
            _table.setFillParent(true);

            AddStartMenuOptions();
        }

        private void AddStartMenuOptions()
        {
            var topButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f), new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
            {
                downFontColor = Color.Black,
            };
            _table.add(new TextButton("Start Game", topButtonStyle)).setFillX().setMinHeight(60).setMinWidth(200).getElement<Button>().onClicked += OnStartButtonClicked;
        }

        private void OnStartButtonClicked(Button button)
        {
            Core.startSceneTransition(new FadeTransition(LoadStartScene));
        }

        private Scene LoadStartScene()
        {
            return new MainGameScene();
        }
    }
}
