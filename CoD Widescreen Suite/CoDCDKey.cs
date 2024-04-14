namespace CoD_Widescreen_Suite
{
    public class CoDCDKey
    {
        public enum KeyType : ushort
        {
            CoD = 0,
            UO = 1
        }

        public KeyType TypeOfKey { get; set; } = KeyType.CoD;
        public string CDKey { get; set; } = string.Empty;

        public CoDCDKey() { }

        public CoDCDKey(string key, KeyType type)
        {
            CDKey = key;
            TypeOfKey = type;
        }
    }
}
