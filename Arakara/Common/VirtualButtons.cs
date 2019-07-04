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
        public static VirtualButton Dummyinput;

        public static void SetupInput()
        {
            SelectInput = new VirtualButton();
            SelectInput.nodes.Add(new VirtualButton.KeyboardKey(Keys.J));
            SelectInput.nodes.Add(new VirtualButton.GamePadButton(0, Buttons.A));

            BackInput = new VirtualButton();
            BackInput.nodes.Add(new VirtualButton.KeyboardKey(Keys.L));
            BackInput.nodes.Add(new VirtualButton.GamePadButton(0, Buttons.B));

            Dummyinput = new VirtualButton();
            Dummyinput.nodes.Add(new VirtualButton.KeyboardKey(Keys.Q));
            Dummyinput.nodes.Add(new VirtualButton.GamePadButton(0, Buttons.X));

            LeftInput = new VirtualButton();
            LeftInput.nodes.Add(new VirtualButton.KeyboardKey(Keys.A));
            LeftInput.nodes.Add(new VirtualButton.KeyboardKey(Keys.Left));
            LeftInput.nodes.Add(new VirtualButton.GamePadButton(0, Buttons.DPadLeft));
            LeftInput.nodes.Add(new VirtualButton.GamePadButton(0, Buttons.LeftThumbstickLeft));

            RightInput = new VirtualButton();
            RightInput.nodes.Add(new VirtualButton.KeyboardKey(Keys.D));
            RightInput.nodes.Add(new VirtualButton.KeyboardKey(Keys.Right));
            RightInput.nodes.Add(new VirtualButton.GamePadButton(0, Buttons.DPadRight));
            RightInput.nodes.Add(new VirtualButton.GamePadButton(0, Buttons.LeftThumbstickRight));
        }
    }
}
