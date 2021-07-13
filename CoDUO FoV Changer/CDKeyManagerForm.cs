using Microsoft.Win32;
using ShadyPool;
using System;
using System.Text;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    public partial class CDKeyManagerForm : Form
    {
        public static CDKeyManagerForm Instance = null;

        private readonly string RegistryPath = PathScanner.RegistryPath;
        private readonly string RegistryPathVirtualStore = PathScanner.RegistryPathVirtualStore;

        private readonly string RegistryPathCoD = PathScanner.RegistryPathCoD;
        private readonly string RegistryPathCoDVirtualStore = PathScanner.RegistryPathCoDVirtualStore;

        private CoDCDKey CoDKey = null;
        private CoDCDKey CoDKeyVS = null;

        private CoDCDKey UOKey = null;
        private CoDCDKey UOKeyVS = null;


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        } //makes the loading look less shitty

        public CDKeyManagerForm()
        {
            Instance = this;
            InitializeComponent();
        }

        private string GetFormattedCDKey(string key)
        {
            if (string.IsNullOrEmpty(key) || key.Length < 5) return key;
            var length = key.Length;

            var sb = Pool.Get<StringBuilder>();
            try
            {
                sb.Clear();

                var count4 = 0;

                for (int i = 0; i < length; i++)
                {
                    var chr = key[i];
                    sb.Append(chr);
                    count4++;
                    if (count4 == 4)
                    {
                        sb.Append("-");
                        count4 = 0;
                    }
                }

                if (sb.Length > 1 && sb[sb.Length - 1] == '-') sb.Length -= 1;
                return sb.ToString();
            }
            finally { Pool.Free(ref sb); }


        }

        private string GetCleanCDKey(string key)
        {
            if (string.IsNullOrEmpty(key)) return key;

            var sb = Pool.Get<StringBuilder>();
            try { return sb.Clear().Append(key).Replace("-", string.Empty).ToString(); }
            finally { Pool.Free(ref sb); }

        }

        private string GetCDKey(bool unitedOffensive, bool virtualStore = false)
        {
            var regPath = unitedOffensive ? (virtualStore ? RegistryPathVirtualStore : RegistryPath) : (virtualStore ? RegistryPathCoDVirtualStore : RegistryPathCoD);

            var keyGet = Registry.GetValue(regPath, unitedOffensive ? "key" : "codkey", string.Empty)?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(keyGet)) keyGet = Registry.GetValue(regPath, !unitedOffensive ? "key" : "codkey", string.Empty)?.ToString() ?? string.Empty;

            return keyGet;

        }

        private void CDKeyManagerForm_Load(object sender, EventArgs e)
        {
            RefreshAllCDKeyCaches();
            RefreshAllCDKeyTextBoxes();
        }

        private void RefreshAllCDKeyCaches()
        {
            if (UOKey != null) UOKey.CDKey = GetCDKey(true);
            else UOKey = new CoDCDKey(GetCDKey(true), CoDCDKey.KeyType.UO);

            if (UOKeyVS != null) UOKeyVS.CDKey = GetCDKey(true);
            else UOKeyVS = new CoDCDKey(GetCDKey(true, true), CoDCDKey.KeyType.UO);

            if (CoDKey != null) CoDKey.CDKey = GetCDKey(false);
            else CoDKey = new CoDCDKey(GetCDKey(false), CoDCDKey.KeyType.CoD);

            if (CoDKeyVS != null) CoDKeyVS.CDKey = GetCDKey(false, true);
            else CoDKeyVS = new CoDCDKey(GetCDKey(false, true), CoDCDKey.KeyType.CoD);

        }

        private void RefreshAllCDKeyTextBoxes()
        {
            uoKeyTextBox.Text = GetFormattedCDKey(GetCDKey(true));

            uoKeyVirtualTextBox.Text = GetFormattedCDKey(GetCDKey(true, true));

            codKeyTextBox.Text = GetFormattedCDKey(GetCDKey(false));

            codKeyVirtualTextBox.Text = GetFormattedCDKey(GetCDKey(false, true));
        }

        private void SaveAllCDKeysToRegistry()
        {
            var uoKey = GetCleanCDKey(uoKeyTextBox.Text);
            var uoKeyVS = GetCleanCDKey(uoKeyVirtualTextBox.Text);

            var codKey = GetCleanCDKey(codKeyTextBox.Text);
            var codKeyVS = GetCleanCDKey(codKeyVirtualTextBox.Text);


            if ((string.IsNullOrEmpty(uoKey) || uoKey.Length == 20) && uoKey != UOKey?.CDKey) Registry.SetValue(RegistryPath, "key", uoKey);
            if ((string.IsNullOrEmpty(uoKeyVS) || uoKeyVS.Length == 20) && uoKeyVS != UOKeyVS?.CDKey) Registry.SetValue(RegistryPathVirtualStore, "key", uoKeyVS);

            if ((string.IsNullOrEmpty(codKey) || codKey.Length == 20) && codKey != CoDKey?.CDKey) Registry.SetValue(RegistryPathCoD, "codkey", codKey);
            if ((string.IsNullOrEmpty(codKeyVS) || codKeyVS.Length == 20) && codKeyVS != CoDKeyVS?.CDKey) Registry.SetValue(RegistryPathCoDVirtualStore, "codkey", codKeyVS);

        }

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var usePwChar = !showPasswordCheckBox.Checked;

            uoKeyTextBox.UseSystemPasswordChar = usePwChar;
            uoKeyVirtualTextBox.UseSystemPasswordChar = usePwChar;
            codKeyTextBox.UseSystemPasswordChar = usePwChar;
            codKeyVirtualTextBox.UseSystemPasswordChar = usePwChar;
        }

        private void refreshKeysButton_Click(object sender, EventArgs e)
        {
            RefreshAllCDKeyTextBoxes();
            RefreshAllCDKeyCaches();
        }

        private void saveExitButton_Click(object sender, EventArgs e)
        {
            SaveAllCDKeysToRegistry();
            Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
