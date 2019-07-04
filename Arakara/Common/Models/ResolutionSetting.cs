using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Common.Models
{
    public class ResolutionSetting
    {
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public float Vector { get; set; }
        public bool IsFullScreen { get; set; }

        public AspectRatios MyProperty
        {
            get
            {
                if(ScreenWidth % 16 == 0 && ScreenHeight % 9 == 0)
                {
                    return AspectRatios.SixteenByNine;
                }
                else
                {
                    return AspectRatios.FourByThree;
                }
            }
        }
    }
}
