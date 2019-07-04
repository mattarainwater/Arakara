using Microsoft.Xna.Framework.Graphics;

namespace Arakara.Dialogue.Models
{
    public class DialoguePortrait
    {
        public Texture2D PortraitTexture { get; set; }
        public bool IsActive { get; set; }

        public DialoguePortrait(Texture2D portraitTexture)
        {
            PortraitTexture = portraitTexture;
        }
    }
}