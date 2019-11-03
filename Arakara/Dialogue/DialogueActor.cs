using Arakara.Common;
using Arakara.Dialogue.Models;
using Arakara.Scenes;
using Microsoft.Xna.Framework;
using Nez;
using Nez.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using Tenswee.Common.Extensions;

namespace Arakara.Dialogue
{
    public class DialogueActor : Component
    {
        private const int MAX_CHARS_PER_LINE = 75;

        public bool IsFinished { get; set; }
        public string RawText { get; set; }

        public BitmapFont FontForText { get; set; }

        public Entity TextEntity { get; set; }
        public TextComponent Text { get; set; }
        public List<TextComponent> Choices { get; set; }

        public int SelectedChoiceIndex = 0;

        private string _displayedText;
        private string _textRemaining;
        private float _dt;
        private float _letterTimeStep = .025f;

        public DialogueActor()
        {
            _displayedText = "";
            _textRemaining = "";
            RawText = "";
            FontForText = CommonResources.DefaultBitmapFont.Copy();
            FontForText.LineHeight = 15;
            Choices = new List<TextComponent>();
        }

        public override void OnAddedToEntity()
        {
            float vec = .277f;

            TextEntity = Entity.Scene.CreateEntity("text");
            TextEntity.Transform.Position = new Vector2(153 * vec, 425 * vec);
            Text = TextEntity.AddComponent(new TextComponent(FontForText, "", Vector2.Zero, Color.OldLace));
            Text.RenderLayer = BaseScene.SCREEN_SPACE_RENDER_LAYER;
        }

        public void ResetDialogue(DialogueEntry dialogue)
        {
            _displayedText = "";
            RawText = dialogue.RawText.FormatRawText(MAX_CHARS_PER_LINE, new char[1] { ' ' });
            _textRemaining = RawText;
            IsFinished = false;
            _dt = 0f;
            SelectedChoiceIndex = 0;

            // choices
            Choices.ForEach(x => Entity.RemoveComponent(x));
            Choices.Clear();
            foreach (var choice in dialogue.Choices)
            {
                var textComponent = TextEntity.AddComponent(new TextComponent(FontForText, choice.Text, Vector2.Zero, Color.Gray));
                textComponent.RenderLayer = BaseScene.SCREEN_SPACE_RENDER_LAYER;
                textComponent.LocalOffset = Vector2.UnitY * 25 * (dialogue.Choices.IndexOf(choice) + 1);
                textComponent.SetEnabled(false);
                Choices.Add(textComponent);
            }
            SelectChoice(SelectedChoiceIndex);
        }

        public void Update()
        {
            if (_displayedText == RawText)
            {
                IsFinished = true;
                Choices.ForEach(x => x.SetEnabled(true));
            }
            else if (!IsFinished)
            {
                _dt += Time.DeltaTime;
                if (_dt >= _letterTimeStep)
                {
                    _dt = 0f;
                    _displayedText += _textRemaining.First();
                    _textRemaining = _textRemaining.Remove(0, 1);
                    Text.SetText(_displayedText);
                }
            }
        }

        public void MoveChoice(bool down)
        {
            if (down)
            {
                SelectedChoiceIndex = Math.Min(Choices.Count() - 1, SelectedChoiceIndex + 1);
            }
            else
            {
                SelectedChoiceIndex = Math.Max(0, SelectedChoiceIndex - 1);
            }
            SelectChoice(SelectedChoiceIndex);
        }

        private void SelectChoice(int index)
        {
            if (index >= Choices.Count())
            {
                return;
            }
            Choices.ForEach(x => x.SetColor(Color.Gray));
            var selectedChoice = Choices[index];
            selectedChoice.SetColor(Color.Goldenrod);
        }

        public void SkipToEnd()
        {
            _displayedText = RawText;
            Text.SetText(_displayedText);
            IsFinished = true;
            Choices.ForEach(x => x.SetEnabled(true));
        }
    }
}
