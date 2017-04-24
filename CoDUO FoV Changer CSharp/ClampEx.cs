namespace ClampExt
{
    public static class ClampEx
    {
        public static int Clamp(int value, int min, int max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static uint Clamp(uint value, uint min, uint max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static decimal Clamp(decimal value, decimal min, decimal max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static double Clamp(double value, double min, double max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static long Clamp(long value, long min, long max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static ulong Clamp(ulong value, ulong min, ulong max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static short Clamp(short value, short min, short max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static ushort Clamp(ushort value, ushort min, ushort max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static sbyte Clamp(sbyte value, sbyte min, sbyte max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static byte Clamp(byte value, byte min, byte max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static char Clamp(char value, char min, char max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
    }
}
