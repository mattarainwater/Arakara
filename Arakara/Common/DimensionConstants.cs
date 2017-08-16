using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Common
{
    public static class DimensionConstants
    {
        public const int SCREEN_WIDTH = 1024;
        public const int SCREEN_HEIGHT = 600;
        public const int SCREEN_WIDTH_ONE_AND_A_HALF = (int)(SCREEN_WIDTH * 1.5);
        public const int SCREEN_HEIGHT_ONE_AND_A_HALF = (int)(SCREEN_HEIGHT * 1.5);
        public const int SCREEN_WIDTH_HALFWAY = SCREEN_WIDTH / 2;
        public const int SCREEN_HEIGHT_HALFWAY = SCREEN_HEIGHT / 2;
        public const int CHARACTER_WIDTH = 64;
        public const int CHARACTER_WIDTH_HALVED = CHARACTER_WIDTH / 2;
        public const int CHARACTER_HEIGHT = 64;
        public const int CHARACTER_HEIGHT_HALVED = CHARACTER_HEIGHT / 2;
    }
}
