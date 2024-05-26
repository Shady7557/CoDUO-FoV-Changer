namespace CoDUO_FoV_Changer.Util
{
    public static class ClampEx
    {
        public static T Clamp<T>(this T value, T minimum, T maximum) where T : System.IComparable<T> { return value.CompareTo(minimum) < 0 ? minimum : value.CompareTo(maximum) > 0 ? maximum : value; }
        public static void Clamp<T>(ref T value, T minimum, T maximum) where T : System.IComparable<T> => value = value.CompareTo(minimum) < 0 ? minimum : value.CompareTo(maximum) > 0 ? maximum : value;
    }
}
