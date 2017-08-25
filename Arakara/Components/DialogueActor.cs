using Arakara.Common;
using Arakara.Dialogue;
using Microsoft.Xna.Framework;
using Nez;
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
        public bool IsFinished { get; set; }
        public string RawText { get; set; }

        public Entity TextEntity { get; set; }
        public Text Text { get; set; }

        public Entity NameEntity { get; set; }
        public Text Name { get; set; }

        public Entity RightPortraitEntity { get; set; }
        public Vector2 RightPortaitPosition { get; set; }

        public Entity LeftPortraitEntity { get; set; }
        public Vector2 LeftPortaitPosition { get; set; }

        private string _displayedText;
        private string _textRemaining;
        private float _dt;
        private float _letterTimeStep = .025f;

        public DialogueActor()
        {
            _displayedText = "";
            _textRemaining = "";
            RawText = "";
        }

        public override void onAddedToEntity()
        {
            TextEntity = entity.scene.createEntity("text");
            Text = TextEntity.addComponent(new Text(CommonResources.DefaultBitmapFont, "", entity.transform.position, Color.Black));
            Text.renderLayer = 70;

            NameEntity = entity.scene.createEntity("name");
            Name = NameEntity.addComponent(new Text(CommonResources.DefaultBitmapFont, "", new Vector2(entity.transform.position.X, entity.transform.position.Y - 30f), Color.Black));
            Name.renderLayer = 70;
            NameEntity.transform.scale = new Vector2(2f, 2f);
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
            _textRemaining = dialogue.RawText;
            RawText = dialogue.RawText;
            IsFinished = false;
            _dt = 0f;

            Name.setText(dialogue.SpeakerName);

            if(dialogue.LeftPortait != null)
            {
                LeftPortraitEntity = entity.scene.createEntity("portait");
                var leftSprite = LeftPortraitEntity.addComponent(new Sprite(dialogue.LeftPortait.PortraitTexture));
                leftSprite.transform.position = LeftPortaitPosition;
                leftSprite.transform.scale = new Vector2(3f, 3f);
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
                rightSprite.transform.position = RightPortaitPosition;
                rightSprite.transform.scale = new Vector2(3f, 3f);
                if(!dialogue.RightPortait.IsActive)
                {
                    rightSprite.setColor(new Color(125, 125, 125));
                }
            }
        }

        public void Update()
        {
            if (_displayedText == RawText)
            {
                IsFinished = true;
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
        }
    }
}
