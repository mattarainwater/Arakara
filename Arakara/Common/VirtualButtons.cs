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
            SelectInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.J));
            SelectInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.A));

            BackInput = new VirtualButton();
            BackInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.L));
            BackInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.B));

            Dummyinput = new VirtualButton();
            Dummyinput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.Q));
            Dummyinput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.X));

            LeftInput = new VirtualButton();
            LeftInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.A));
            LeftInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.Left));
            LeftInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.DPadLeft));
            LeftInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.LeftThumbstickLeft));

            RightInput = new VirtualButton();
            RightInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.D));
            RightInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.Right));
            RightInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.DPadRight));
            RightInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.LeftThumbstickRight));
        }
    }
}
