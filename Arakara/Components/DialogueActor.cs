using Arakara.Common;
using Arakara.Dialogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.BitmapFonts;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class DialogueActor : Component
    {
        private const int MAX_CHARS_PER_LINE = 52;

        public bool IsFinished { get; set; }
        public string RawText { get; set; }

        public BitmapFont FontForText { get; set; }

        public Entity TextEntity { get; set; }
        public Text Text { get; set; }

        public Entity TextBoxEntity { get; set; }
        public Texture2D TextBox { get; set; }

        public Entity NameEntity { get; set; }
        public Text Name { get; set; }

        public Entity NameBoxEntity { get; set; }
        public Texture2D NameBox { get; set; }

        public Entity RightPortraitEntity { get; set; }
        public Vector2 RightPortaitPosition { get; set; }

        public Entity LeftPortraitEntity { get; set; }
        public Vector2 LeftPortaitPosition { get; set; }

        public Entity NextDialogueMarker { get; set; }
        public Texture2D Marker { get; set; }

        private string _displayedText;
        private string _textRemaining;
        private float _dt;
        private float _letterTimeStep = .025f;

        public DialogueActor(Texture2D textBox, Texture2D nameBox, Texture2D marker)
        {
            _displayedText = "";
            _textRemaining = "";
            RawText = "";
            TextBox = textBox;
            NameBox = nameBox;
            FontForText = CommonResources.DefaultBitmapFont.Copy();
            FontForText.lineHeight = 15;
            Marker = marker;
        }

        public override void onAddedToEntity()
        {
            TextEntity = entity.scene.createEntity("text");
            TextEntity.transform.position = new Vector2(entity.transform.position.X + 35, entity.transform.position.Y + 20);
            Text = TextEntity.addComponent(new Text(FontForText, "", Vector2.Zero, new Color(101, 67, 33)));
            Text.renderLayer = 70;
            Text.transform.scale = new Vector2(2f, 2f);

            TextBoxEntity = entity.scene.createEntity("textBox");
            TextBoxEntity.transform.position = new Vector2(entity.transform.position.X, entity.transform.position.Y - 15);
            var textSprite = TextBoxEntity.addComponent(new Sprite(TextBox));
            textSprite.renderLayer = 80;
            textSprite.setOrigin(Vector2.Zero);

            NameEntity = entity.scene.createEntity("name");
            Name = NameEntity.addComponent(new Text(CommonResources.DefaultBitmapFont, "", new Vector2(entity.transform.position.X + 90, entity.transform.position.Y - 20f), Color.WhiteSmoke));
            Name.renderLayer = 70;
            Name.transform.scale = new Vector2(2f, 2f);

            NameBoxEntity = entity.scene.createEntity("nameBox");
            NameBoxEntity.transform.position = new Vector2(entity.transform.position.X + 20, entity.transform.position.Y - 30f);
            var nameSprite = NameBoxEntity.addComponent(new Sprite(NameBox));
            nameSprite.renderLayer = 80;
            nameSprite.setOrigin(Vector2.Zero);

            NextDialogueMarker = entity.scene.createEntity("marker");
            NextDialogueMarker.transform.position = new Vector2(570f, 320f);
            var markerSprite = NextDialogueMarker.addComponent(new Sprite(Marker));
            markerSprite.renderLayer = 60;
            markerSprite.setColor(new Color(101, 67, 33));
            NextDialogueMarker.enabled = false;
        }

        public void ResetDialogue(DialogueEntry dialogue)
        {
            if (LeftPortraitEntity != null)
            {
                LeftPortraitEntity.destroy();
            }
            if (RightPortraitEntity != null)
            {
                RightPortraitEntity.destroy();
            }

            _displayedText = "";
            RawText = FormatRawText(dialogue.RawText);
            _textRemaining = RawText;
            IsFinished = false;
            _dt = 0f;

            Name.setText(dialogue.SpeakerName);

            if(dialogue.LeftPortait != null)
            {
                LeftPortraitEntity = entity.scene.createEntity("portait");
                var leftSprite = LeftPortraitEntity.addComponent(new Sprite(dialogue.LeftPortait.PortraitTexture));
                leftSprite.setOrigin(Vector2.Zero);
                leftSprite.renderLayer = 100;
                LeftPortraitEntity.transform.position = LeftPortaitPosition;
                if (!dialogue.LeftPortait.IsActive)
                {
                    leftSprite.setColor(new Color(125, 125, 125));
                }
                leftSprite.flipX = true;
            }

            if (dialogue.RightPortait != null)
            {
                RightPortraitEntity = entity.scene.createEntity("portait");
                var rightSprite = RightPortraitEntity.addComponent(new Sprite(dialogue.RightPortait.PortraitTexture));
                rightSprite.setOrigin(Vector2.Zero);
                rightSprite.renderLayer = 100;
                RightPortraitEntity.transform.position = RightPortaitPosition;
                if(!dialogue.RightPortait.IsActive)
                {
                    rightSprite.setColor(new Color(125, 125, 125));
                }
            }

            NextDialogueMarker.enabled = false;
        }

        private string FormatRawText(string rawText)
        {
            string[] words = Explode(rawText);

            int curLineLength = 0;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < words.Length; i += 1)
            {
                string word = words[i];
                // If adding the new word to the current line would be too long,
                // then put it on a new line (and split it up if it's too long).
                if (curLineLength + word.Length > MAX_CHARS_PER_LINE)
                {
                    // Only move down to a new line if we have text on the current line.
                    // Avoids situation where wrapped whitespace causes emptylines in text.
                    if (curLineLength > 0)
                    {
                        strBuilder.Append(Environment.NewLine);
                        curLineLength = 0;
                    }

                    // If the current word is too long to fit on a line even on it's own then
                    // split the word up.
                    while (word.Length > MAX_CHARS_PER_LINE)
                    {
                        strBuilder.Append(word.Substring(0, MAX_CHARS_PER_LINE - 1) + "-");
                        word = word.Substring(MAX_CHARS_PER_LINE - 1);

                        strBuilder.Append(Environment.NewLine);
                    }

                    // Remove leading whitespace from the word so the new line starts flush to the left.
                    word = word.TrimStart();
                }
                strBuilder.Append(word);
                curLineLength += word.Length;
            }

            return strBuilder.ToString();
        }

        private static string[] Explode(string str)
        {
            List<string> parts = new List<string>();
            int startIndex = 0;
            while (true)
            {
                int index = str.IndexOfAny(new char[1] { ' ' }, startIndex);

                if (index == -1)
                {
                    parts.Add(str.Substring(startIndex));
                    return parts.ToArray();
                }

                string word = str.Substring(startIndex, index - startIndex);
                char nextChar = str.Substring(index, 1)[0];
                // Dashes and the likes should stick to the word occuring before it. Whitespace doesn't have to.
                if (char.IsWhiteSpace(nextChar))
                {
                    parts.Add(word);
                    parts.Add(nextChar.ToString());
                }
                else
                {
                    parts.Add(word + nextChar);
                }

                startIndex = index + 1;
            }
        }

        public void Update()
        {
            if (_displayedText == RawText)
            {
                IsFinished = true;
                NextDialogueMarker.enabled = true;
            }
            else if(!IsFinished)
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

        public void SkipToEnd()
        {
            _displayedText = RawText;
            Text.setText(_displayedText);
            IsFinished = true;
            NextDialogueMarker.enabled = true;
        }
    }
}
