using System.Drawing;

namespace CoDUO_FoV_Changer.Util
{
    public class RModeToResolutionMapping
    {
        public static Size GetResolutionFromMode(int mode)
        {
            switch (mode)
            {
                case 3:
                    return new Size(640, 480);
                case 4:
                    return new Size(800, 600);
                case 6:
                    return new Size(1024, 768);
                case 7:
                    return new Size(1152, 864);
                case 8:
                    return new Size(1280, 1024);
                    case 9:
                    return new Size(1600, 1200);
                case 10:
                    return new Size(2048, 1536);
                default:
                    return new Size(0, 0);
            }
        }

    }
}
