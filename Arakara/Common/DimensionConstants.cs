using Arakara.Common.Models;
using Nez;
using System.Collections.Generic;

namespace Arakara.Common
{
    public static class DimensionConstants
    {
        public const int DESIGN_WIDTH = 640;
        public const int DESIGN_HEIGHT = 360;
        public static ResolutionSetting GetCurrentResolution()
        {
            var res = GetResolutions()[_resolutionIndex];
            res.IsFullScreen = _isFullScreen;
            return res;
        }

        public static void SetCurrentResolution(int index, bool isFullScreen = false)
        {
            _resolutionIndex = index;
            var currentResolution = GetCurrentResolution();
            Screen.SetSize(currentResolution.ScreenWidth, currentResolution.ScreenHeight);
            Screen.IsFullscreen = isFullScreen;
        }

        public static List<ResolutionSetting> GetResolutions()
        {
            // asset size for full screen res is
            // 9600 x 5400
            return new List<ResolutionSetting>
            {
                new ResolutionSetting
                {
                    ScreenWidth = 1600,
                    ScreenHeight = 900,
                    Vector = 1 / 6
                },
                new ResolutionSetting
                {
                    ScreenWidth = 1920,
                    ScreenHeight = 1080,
                    Vector = 1 / 5
                },
            };
        }

        private static int _resolutionIndex { get; set; }
        private static bool _isFullScreen { get; set; }
    }
}
