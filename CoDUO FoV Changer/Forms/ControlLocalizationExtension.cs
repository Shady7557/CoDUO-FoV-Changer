using Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlExtensions
{
    public static class ControlLocalizationExtensions
    {
        public static void ApplyLocalization(this Control control, int index = 0)
        {
            if (LocalizationManager.Instance is null)
                return;

            LocalizationManager.Instance.ApplyLocalization(control, index);
        }
    }
}
