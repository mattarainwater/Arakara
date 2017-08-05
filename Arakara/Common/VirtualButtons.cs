using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Common
{
    public static class VirtualButtons
    {
        public static VirtualButton SelectInput;
        public static VirtualButton BackInput;
        public static VirtualButton LeftInput;
        public static VirtualButton RightInput;

        public static void SetupInput()
        {
            SelectInput = new VirtualButton();
            SelectInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.J));
            SelectInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.A));

            BackInput = new VirtualButton();
            BackInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.L));
            BackInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.B));

            LeftInput = new VirtualButton();
            LeftInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.A));
            LeftInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.Left));
            LeftInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.DPadLeft));
            LeftInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.LeftThumbstickLeft));

            RightInput = new VirtualButton();
            RightInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.D));
            RightInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.Right));
            RightInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.DPadRight));
            RightInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.LeftThumbstickRight));
        }
    }
}
