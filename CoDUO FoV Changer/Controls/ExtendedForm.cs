using Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
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

        /// <summary>
        /// Loads localization for all controls, menus, and tooltips in the form.
        /// </summary>
        public void LoadAllLocalization()
        {
            if (LocalizationManager.Instance == null)
                return;

            // Localize all controls recursively
            LoadControlLocalization(this);

            // Localize menu strips
            LocalizeMenuStrips();

            // Localize tooltips
            LoadTooltipLocalization();
        }

        /// <summary>
        /// Recursively localizes all controls within a parent control.
        /// </summary>
        /// <param name="parent">The parent control to start from.</param>
        private void LoadControlLocalization(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (string.IsNullOrWhiteSpace(control.Name))
                    continue;

                // Apply localization to this control if it doesn't have multiple text variants
                // (controls with multiple variants are handled manually in code)
                if (LocalizationManager.Instance.GetControlIndexCount(control) <= 1)
                    LocalizationManager.Instance.ApplyLocalization(control, 0);

                // Recursively process nested controls (GroupBox, Panel, TabControl, SplitContainer, etc.)
                if (control.HasChildren)
                    LoadControlLocalization(control);
            }
        }

        /// <summary>
        /// Localizes all MenuStrip and ContextMenuStrip components in the form.
        /// </summary>
        private void LocalizeMenuStrips()
        {
            // Get all fields in the form class (including private)
            var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var value = field.GetValue(this);

                if (value is MenuStrip menuStrip)
                {
                    LocalizeToolStripItems(menuStrip.Items);
                }
                else if (value is ContextMenuStrip contextMenu)
                {
                    LocalizeToolStripItems(contextMenu.Items);
                }
            }
        }

        /// <summary>
        /// Recursively localizes ToolStripItem collections (menus, context menus).
        /// </summary>
        /// <param name="items">The collection of ToolStripItems to localize.</param>
        private void LocalizeToolStripItems(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                if (string.IsNullOrWhiteSpace(item.Name) || string.IsNullOrWhiteSpace(item.Text))
                    continue;

                // Try to get localized string for this menu item
                var key = $"{Name}.{item.Name}";
                var localized = LocalizationManager.Instance.GetString(key);

                // Only apply if we found a translation (not just the key back)
                if (localized != key)
                {
                    item.Text = localized;
                }
                else
                {
                    // Register this menu item for localization
                    LocalizationManager.Instance.RegisterMissingString(key, item.Text);
                }

                // Recursively localize dropdown items
                if (item is ToolStripDropDownItem dropDown && dropDown.HasDropDownItems)
                {
                    LocalizeToolStripItems(dropDown.DropDownItems);
                }
            }
        }

        /// <summary>
        /// Localizes tooltip strings for controls that have tooltips assigned.
        /// </summary>
        private void LoadTooltipLocalization()
        {
            // Find all ToolTip components in the form
            var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.GetValue(this) is ToolTip tooltip)
                {
                    LocalizeTooltipsForControls(tooltip, this);
                }
            }
        }

        /// <summary>
        /// Recursively localizes tooltips for all controls.
        /// </summary>
        /// <param name="tooltip">The ToolTip component.</param>
        /// <param name="parent">The parent control to process.</param>
        private void LocalizeTooltipsForControls(ToolTip tooltip, Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (string.IsNullOrWhiteSpace(control.Name))
                    continue;

                var currentTip = tooltip.GetToolTip(control);
                if (!string.IsNullOrWhiteSpace(currentTip))
                {
                    var key = $"Tooltip.{Name}.{control.Name}";
                    var localized = LocalizationManager.Instance.GetString(key);

                    if (localized != key)
                    {
                        // Apply localized tooltip
                        tooltip.SetToolTip(control, localized);
                    }
                    else
                    {
                        // Register this tooltip for localization
                        LocalizationManager.Instance.RegisterMissingString(key, currentTip);
                    }
                }

                // Recursively process nested controls
                if (control.HasChildren)
                    LocalizeTooltipsForControls(tooltip, control);
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
