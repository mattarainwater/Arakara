using Arakara.Common;
using Arakara.Dialogue.Models;
using Arakara.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.BitmapFonts;
using System;
using System.Linq;
using Tenswee.Common.Extensions;

namespace Arakara.Dialogue
{
    public class DialogueBox : Component, IUpdatable
    {
        private const int MAX_CHARS_PER_LINE = 52;

        private string _displayedText;
        private string _textRemaining;
        private float _dt;
        private float _letterTimeStep = .025f;

        public bool IsFinished { get; set; }
        public string RawText { get; set; }
        public Text Text { get; set; }

        public override void onAddedToEntity()
        {
            Text = entity.addComponent(new Text(CommonResources.DefaultBitmapFont, "", Vector2.Zero, new Color(101, 67, 33)));
            Text.renderLayer = BaseScene.SCREEN_SPACE_RENDER_LAYER;
        }

        public void update()
        {
            if (_displayedText == RawText)
            {
                IsFinished = true;
            }
            else if (!IsFinished)
            {
                _dt += Time.deltaTime;
                if (_dt >= _letterTimeStep)
                {
                    _dt = 0f;
                    _displayedText += _textRemaining.First();
                    _textRemaining = _textRemaining.Remove(0, 1);
                    Text.setText(_displayedText);
                }
            }
        }

        public void ResetDialogue(DialogueEntry dialogue)
        {
            _displayedText = "";
            RawText = dialogue.RawText.FormatRawText(MAX_CHARS_PER_LINE, new char[] { ' ' });
            _textRemaining = RawText;
            IsFinished = false;
            _dt = 0f;
        }

        public void SkipToEnd()
        {
            _displayedText = RawText;
            Text.setText(_displayedText);
            IsFinished = true;
        }
    }
}
