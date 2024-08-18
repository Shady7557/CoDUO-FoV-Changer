using Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    public class ExtendedForm : Form
    {

        private static readonly Dictionary<Type, ExtendedForm> _instances = new Dictionary<Type, ExtendedForm>();
        
        protected ExtendedForm()
        {
            if (DesignMode)
                return;

            _instances[GetType()] = this;

            Load += OnExtendedForm_Load;
            FormClosed += OnExtendedForm_Close;

            // Arguably (?) makes the loading look better:
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            UpdateStyles(); // Unsure if UpdateStyles() is necessary
        }


        private void OnExtendedForm_Load(object sender, EventArgs e)
        {
            if (AttachToOwner)
                SetOwnerOffsetPosition();

            LoadAllLocalization();

        }

        public void LoadAllLocalization()
        {
            foreach (var control in Controls)
            {
                if (!(control is Control ctrl))
                    continue;

                // 'pre-load' any that don't have indexes (multiple text options)
                if (LocalizationManager.Instance.GetControlIndexCount(ctrl) <= 1)
                    LocalizationManager.Instance.ApplyLocalization(ctrl, 0);
            }
        }

        private void OnExtendedForm_Close(object sender, EventArgs e)
        {
            if (Owner == null || Owner.IsDisposed || Owner.Disposing)
                return;

            Owner.LocationChanged -= ParentForm_LocationChanged;
        }

        private bool _attachToOwner = false;
        /// <summary>
        /// If set to true, this form will be moved when its owner form is moved.
        /// </summary>
        public bool AttachToOwner
        {
            get { return _attachToOwner; }
            set
            {
                if (Owner != null)
                {
                    if (value)
                        Owner.LocationChanged += ParentForm_LocationChanged;
                    else Owner.LocationChanged -= ParentForm_LocationChanged;
                }

                _attachToOwner = value;
            }
        }

        // Makes the loading look smoother        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                if (DesignMode)
                    return cp;

                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED

                return cp;
            }
        } 

        private void ParentForm_LocationChanged(object sender, EventArgs e) { SetOwnerOffsetPosition(); }

        public static T GetInstance<T>() where T : ExtendedForm, new()
        {
            if (_instances.TryGetValue(typeof(T), out var instance))
            {
                if (instance == null || instance.IsDisposed || instance.Disposing)
                {
                    _instances.Remove(typeof(T));
                    return null;
                }

                return (T)instance;
            }

            return null;
        }

        public void SetOwnerOffsetPosition()
        {
            if (Owner == null)
                return;

            var newX = Owner.Left - Width;
            var newY = Owner.Top;

            // Ensure the new position is within screen bounds
            newX = Math.Max(newX, Screen.FromControl(Owner).WorkingArea.Left);
            newY = Math.Max(newY, Screen.FromControl(Owner).WorkingArea.Top);
            newY = Math.Min(newY, Screen.FromControl(Owner).WorkingArea.Bottom - Height);

            Location = new Point(newX, newY);
        }

        public void UnminimizeAndSelect(FormWindowState windowState = FormWindowState.Normal)
        {
            WindowState = windowState;
            Show();
            BringToFront();
            Select();
        }
    }
}
