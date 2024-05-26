namespace CoDUO_FoV_Changer
{
    public class MemoryAddresses
    {
        // United Offensive memory addresses

        public const int UO_R_MODE_ADDRESS = 0x4899D50;
        public const int UO_R_HEIGHT_ADDRESS = 0x4899FCC;
        public const int UO_R_WIDTH_ADDRESS = 0x4899D30;

        public const int UO_FOV_OFFSET = 0x52F7C8;


        // Multiplayer:
        public const string UO_CGAME_MP_DLL = "uo_cgame_mp_x86.dll";
        public const string UO_UI_MP_DLL = "uo_ui_mp_x86.dll";

        // Generic UO UI:
        public const string UO_UI_DLL = "uo_uix86.dll";

        // Call of Duty memory addresses

        public const int COD_FOV_ADDRESS = 0x3029CA28;

        // Multiplayer:
        public const string COD_UI_MP_DLL = "ui_mp_x86.dll";
        public const string COD_CGAME_MP_DLL = "cgame_mp_x86.dll";

        // Generic CoD UI:
        public const string COD_UI_DLL = "uix86.dll";
    }
}
