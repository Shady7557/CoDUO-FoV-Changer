namespace CoDUO_FoV_Changer
{
    public class CoDCDKey
    {
        public enum KeyType : ushort
        {
            CoD = 0,
            UO = 1,
            None = 2
        }

        public KeyType TypeOfKey { get; set; } = KeyType.None;
        public string CDKey { get; set; } = string.Empty;

        public CoDCDKey() { }

        public CoDCDKey(string key, KeyType type)
        {
            CDKey = key;
            TypeOfKey = type;
        }
    }
}
