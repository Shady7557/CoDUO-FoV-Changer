namespace CoDUO_FoV_Changer
{
    public class MemoryAddresses
    {
        //United Offensive memory addresses

        public const int UO_R_MODE_ADDRESS = 0x4899D50;
        public const int UO_R_HEIGHT_ADDRESS = 0x4899FCC;
        public const int UO_R_WIDTH_ADDRESS = 0x4899D30;

        public const int UO_FOV_OFFSET = 0x52F7C8;

        public const int UO_DVAR_ADDRESS_1 = 0x43DD86;
        public const int UO_DVAR_ADDRESS_2 = 0x43DDA3;
        public const int UO_DVAR_ADDRESS_3 = 0x43DDC1;

        public const int UO_FOG_POINTER_ADDRESS = 0x489A0D4;

        public const string UO_CGAME_MP_DLL = "uo_cgame_mp_x86.dll";
        public const string UO_UI_MP_DLL = "uo_ui_mp_x86.dll";

        //Call of Duty memory addresses

        public const int COD_FOV_ADDRESS = 0x3029CA28;
    }
}
