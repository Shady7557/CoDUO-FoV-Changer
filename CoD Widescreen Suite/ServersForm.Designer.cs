namespace CoD_Widescreen_Suite
{
    partial class ServersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServersForm));
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ServerListView = new CoD_Widescreen_Suite.Controls.ServerListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaxPingNumeric = new System.Windows.Forms.NumericUpDown();
            this.MaxPingLabel = new System.Windows.Forms.Label();
            this.HideNoPingCheckbox = new System.Windows.Forms.CheckBox();
            this.SearchFilterTextbox = new System.Windows.Forms.TextBox();
            this.GameVersionBox = new System.Windows.Forms.ComboBox();
            this.GameVersionLabel = new System.Windows.Forms.Label();
            this.SearchFilterLabel = new System.Windows.Forms.Label();
            this.MapLabel = new System.Windows.Forms.Label();
            this.MapNameLabel = new System.Windows.Forms.Label();
            this.MapImageBox = new System.Windows.Forms.PictureBox();
            this.ServersLabel = new System.Windows.Forms.Label();
            this.CountServersLabel = new System.Windows.Forms.Label();
            this.HideEmptyCheckbox = new System.Windows.Forms.CheckBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.PlayerListView = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlayersTxtLabel = new System.Windows.Forms.Label();
            this.FilterPlayerNamesCheckbox = new System.Windows.Forms.CheckBox();
            this.FilterBotPlayersCheckbox = new System.Windows.Forms.CheckBox();
            this.PlayerCountLabel = new System.Windows.Forms.Label();
            this.RefreshServerButton = new System.Windows.Forms.Button();
            this.FilterBotServersCheckbox = new System.Windows.Forms.CheckBox();
            this.CoDPictureBox = new System.Windows.Forms.PictureBox();
            this.ConnectServerButton = new System.Windows.Forms.Button();
            this.FavoriteServerCheckbox = new System.Windows.Forms.CheckBox();
            this.FavoritesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MaxPingNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoDPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // RefreshButton
            // 
            this.RefreshButton.BackColor = System.Drawing.Color.DarkGray;
            this.RefreshButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Location = new System.Drawing.Point(12, 323);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(446, 35);
            this.RefreshButton.TabIndex = 0;
            this.RefreshButton.Text = "Refresh all servers";
            this.RefreshButton.UseVisualStyleBackColor = false;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ServerListView
            // 
            this.ServerListView.BackColor = System.Drawing.Color.DarkGray;
            this.ServerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.ServerListView.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerListView.FullRowSelect = true;
            this.ServerListView.HideSelection = false;
            this.ServerListView.Location = new System.Drawing.Point(12, 12);
            this.ServerListView.Name = "ServerListView";
            this.ServerListView.Size = new System.Drawing.Size(578, 210);
            this.ServerListView.TabIndex = 1;
            this.ServerListView.UseCompatibleStateImageBehavior = false;
            this.ServerListView.View = System.Windows.Forms.View.Details;
            this.ServerListView.SelectedIndexChanged += new System.EventHandler(this.ServerListView_SelectedIndexChanged);
            this.ServerListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ServerListView_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Hostname";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Map";
            this.columnHeader2.Width = 92;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Players";
            this.columnHeader3.Width = 68;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Game Type";
            this.columnHeader4.Width = 75;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Ping";
            this.columnHeader5.Width = 40;
            // 
            // MaxPingNumeric
            // 
            this.MaxPingNumeric.BackColor = System.Drawing.Color.DarkGray;
            this.MaxPingNumeric.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxPingNumeric.Location = new System.Drawing.Point(12, 297);
            this.MaxPingNumeric.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.MaxPingNumeric.Name = "MaxPingNumeric";
            this.MaxPingNumeric.Size = new System.Drawing.Size(46, 23);
            this.MaxPingNumeric.TabIndex = 2;
            this.MaxPingNumeric.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // MaxPingLabel
            // 
            this.MaxPingLabel.AutoSize = true;
            this.MaxPingLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxPingLabel.Location = new System.Drawing.Point(9, 264);
            this.MaxPingLabel.Name = "MaxPingLabel";
            this.MaxPingLabel.Size = new System.Drawing.Size(42, 30);
            this.MaxPingLabel.TabIndex = 3;
            this.MaxPingLabel.Text = "Max \r\nPing:";
            // 
            // HideNoPingCheckbox
            // 
            this.HideNoPingCheckbox.AutoSize = true;
            this.HideNoPingCheckbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideNoPingCheckbox.Location = new System.Drawing.Point(64, 276);
            this.HideNoPingCheckbox.Name = "HideNoPingCheckbox";
            this.HideNoPingCheckbox.Size = new System.Drawing.Size(320, 19);
            this.HideNoPingCheckbox.TabIndex = 4;
            this.HideNoPingCheckbox.Text = "Hide servers that didn\'t respond to a ping";
            this.HideNoPingCheckbox.UseVisualStyleBackColor = true;
            this.HideNoPingCheckbox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // SearchFilterTextbox
            // 
            this.SearchFilterTextbox.BackColor = System.Drawing.Color.DarkGray;
            this.SearchFilterTextbox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchFilterTextbox.Location = new System.Drawing.Point(123, 223);
            this.SearchFilterTextbox.Multiline = true;
            this.SearchFilterTextbox.Name = "SearchFilterTextbox";
            this.SearchFilterTextbox.Size = new System.Drawing.Size(359, 20);
            this.SearchFilterTextbox.TabIndex = 5;
            this.SearchFilterTextbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // GameVersionBox
            // 
            this.GameVersionBox.BackColor = System.Drawing.Color.DarkGray;
            this.GameVersionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GameVersionBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameVersionBox.FormattingEnabled = true;
            this.GameVersionBox.Items.AddRange(new object[] {
            "CoD:UO (1.51)",
            "CoD:UO (1.41)",
            "CoD (1.5)",
            "CoD (1.4)",
            "CoD (1.3)",
            "CoD (1.2)",
            "CoD (1.1)"});
            this.GameVersionBox.Location = new System.Drawing.Point(469, 336);
            this.GameVersionBox.Name = "GameVersionBox";
            this.GameVersionBox.Size = new System.Drawing.Size(121, 22);
            this.GameVersionBox.TabIndex = 6;
            this.GameVersionBox.SelectedIndexChanged += new System.EventHandler(this.GameVersionBox_SelectedIndexChanged);
            // 
            // GameVersionLabel
            // 
            this.GameVersionLabel.AutoSize = true;
            this.GameVersionLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameVersionLabel.Location = new System.Drawing.Point(466, 318);
            this.GameVersionLabel.Name = "GameVersionLabel";
            this.GameVersionLabel.Size = new System.Drawing.Size(42, 15);
            this.GameVersionLabel.TabIndex = 7;
            this.GameVersionLabel.Text = "Game:";
            // 
            // SearchFilterLabel
            // 
            this.SearchFilterLabel.AutoSize = true;
            this.SearchFilterLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchFilterLabel.Location = new System.Drawing.Point(9, 225);
            this.SearchFilterLabel.Name = "SearchFilterLabel";
            this.SearchFilterLabel.Size = new System.Drawing.Size(105, 15);
            this.SearchFilterLabel.TabIndex = 8;
            this.SearchFilterLabel.Text = "Search/Filter:";
            // 
            // MapLabel
            // 
            this.MapLabel.AutoSize = true;
            this.MapLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MapLabel.Location = new System.Drawing.Point(593, 12);
            this.MapLabel.Name = "MapLabel";
            this.MapLabel.Size = new System.Drawing.Size(35, 15);
            this.MapLabel.TabIndex = 10;
            this.MapLabel.Text = "Map:";
            // 
            // MapNameLabel
            // 
            this.MapNameLabel.AutoSize = true;
            this.MapNameLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MapNameLabel.Location = new System.Drawing.Point(625, 12);
            this.MapNameLabel.Name = "MapNameLabel";
            this.MapNameLabel.Size = new System.Drawing.Size(77, 15);
            this.MapNameLabel.TabIndex = 11;
            this.MapNameLabel.Text = "{MAP_NAME}";
            // 
            // MapImageBox
            // 
            this.MapImageBox.Location = new System.Drawing.Point(596, 30);
            this.MapImageBox.Name = "MapImageBox";
            this.MapImageBox.Size = new System.Drawing.Size(320, 133);
            this.MapImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MapImageBox.TabIndex = 9;
            this.MapImageBox.TabStop = false;
            // 
            // ServersLabel
            // 
            this.ServersLabel.AutoSize = true;
            this.ServersLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServersLabel.Location = new System.Drawing.Point(485, 225);
            this.ServersLabel.Name = "ServersLabel";
            this.ServersLabel.Size = new System.Drawing.Size(63, 15);
            this.ServersLabel.TabIndex = 12;
            this.ServersLabel.Text = "Servers:";
            // 
            // CountServersLabel
            // 
            this.CountServersLabel.AutoSize = true;
            this.CountServersLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountServersLabel.Location = new System.Drawing.Point(545, 225);
            this.CountServersLabel.Name = "CountServersLabel";
            this.CountServersLabel.Size = new System.Drawing.Size(28, 15);
            this.CountServersLabel.TabIndex = 13;
            this.CountServersLabel.Text = "0/0";
            // 
            // HideEmptyCheckbox
            // 
            this.HideEmptyCheckbox.AutoSize = true;
            this.HideEmptyCheckbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideEmptyCheckbox.Location = new System.Drawing.Point(64, 301);
            this.HideEmptyCheckbox.Name = "HideEmptyCheckbox";
            this.HideEmptyCheckbox.Size = new System.Drawing.Size(152, 19);
            this.HideEmptyCheckbox.TabIndex = 14;
            this.HideEmptyCheckbox.Text = "Hide empty servers";
            this.HideEmptyCheckbox.UseVisualStyleBackColor = true;
            this.HideEmptyCheckbox.CheckedChanged += new System.EventHandler(this.HideEmptyCheckbox_CheckedChanged);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.DarkGray;
            this.CloseButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(12, 364);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(446, 35);
            this.CloseButton.TabIndex = 15;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // PlayerListView
            // 
            this.PlayerListView.BackColor = System.Drawing.Color.DarkGray;
            this.PlayerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.PlayerListView.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerListView.FullRowSelect = true;
            this.PlayerListView.HideSelection = false;
            this.PlayerListView.Location = new System.Drawing.Point(599, 219);
            this.PlayerListView.Name = "PlayerListView";
            this.PlayerListView.Size = new System.Drawing.Size(307, 139);
            this.PlayerListView.TabIndex = 16;
            this.PlayerListView.UseCompatibleStateImageBehavior = false;
            this.PlayerListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Player";
            this.columnHeader6.Width = 185;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Score";
            this.columnHeader7.Width = 47;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Ping";
            this.columnHeader8.Width = 40;
            // 
            // PlayersTxtLabel
            // 
            this.PlayersTxtLabel.AutoSize = true;
            this.PlayersTxtLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayersTxtLabel.Location = new System.Drawing.Point(593, 201);
            this.PlayersTxtLabel.Name = "PlayersTxtLabel";
            this.PlayersTxtLabel.Size = new System.Drawing.Size(63, 15);
            this.PlayersTxtLabel.TabIndex = 17;
            this.PlayersTxtLabel.Text = "Players:";
            // 
            // FilterPlayerNamesCheckbox
            // 
            this.FilterPlayerNamesCheckbox.AutoSize = true;
            this.FilterPlayerNamesCheckbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterPlayerNamesCheckbox.Location = new System.Drawing.Point(599, 364);
            this.FilterPlayerNamesCheckbox.Name = "FilterPlayerNamesCheckbox";
            this.FilterPlayerNamesCheckbox.Size = new System.Drawing.Size(222, 19);
            this.FilterPlayerNamesCheckbox.TabIndex = 18;
            this.FilterPlayerNamesCheckbox.Text = "Filter player names (colors)";
            this.FilterPlayerNamesCheckbox.UseVisualStyleBackColor = true;
            this.FilterPlayerNamesCheckbox.CheckedChanged += new System.EventHandler(this.FilterPlayerNamesCheckbox_CheckedChanged);
            // 
            // FilterBotPlayersCheckbox
            // 
            this.FilterBotPlayersCheckbox.AutoSize = true;
            this.FilterBotPlayersCheckbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterBotPlayersCheckbox.Location = new System.Drawing.Point(599, 383);
            this.FilterBotPlayersCheckbox.Name = "FilterBotPlayersCheckbox";
            this.FilterBotPlayersCheckbox.Size = new System.Drawing.Size(103, 19);
            this.FilterBotPlayersCheckbox.TabIndex = 19;
            this.FilterBotPlayersCheckbox.Text = "Filter bots";
            this.FilterBotPlayersCheckbox.UseVisualStyleBackColor = true;
            this.FilterBotPlayersCheckbox.CheckedChanged += new System.EventHandler(this.FilterBotPlayersCheckbox_CheckedChanged);
            // 
            // PlayerCountLabel
            // 
            this.PlayerCountLabel.AutoSize = true;
            this.PlayerCountLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerCountLabel.Location = new System.Drawing.Point(650, 201);
            this.PlayerCountLabel.Name = "PlayerCountLabel";
            this.PlayerCountLabel.Size = new System.Drawing.Size(28, 15);
            this.PlayerCountLabel.TabIndex = 20;
            this.PlayerCountLabel.Text = "0/0";
            // 
            // RefreshServerButton
            // 
            this.RefreshServerButton.BackColor = System.Drawing.Color.DarkGray;
            this.RefreshServerButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshServerButton.Location = new System.Drawing.Point(803, 192);
            this.RefreshServerButton.Name = "RefreshServerButton";
            this.RefreshServerButton.Size = new System.Drawing.Size(113, 22);
            this.RefreshServerButton.TabIndex = 21;
            this.RefreshServerButton.Text = "Refresh Server";
            this.RefreshServerButton.UseVisualStyleBackColor = false;
            this.RefreshServerButton.Click += new System.EventHandler(this.RefreshServerButton_Click);
            // 
            // FilterBotServersCheckbox
            // 
            this.FilterBotServersCheckbox.AutoSize = true;
            this.FilterBotServersCheckbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterBotServersCheckbox.Location = new System.Drawing.Point(222, 301);
            this.FilterBotServersCheckbox.Name = "FilterBotServersCheckbox";
            this.FilterBotServersCheckbox.Size = new System.Drawing.Size(138, 19);
            this.FilterBotServersCheckbox.TabIndex = 22;
            this.FilterBotServersCheckbox.Text = "Hide bot servers";
            this.FilterBotServersCheckbox.UseVisualStyleBackColor = true;
            this.FilterBotServersCheckbox.CheckedChanged += new System.EventHandler(this.FilterBotServersCheckbox_CheckedChanged);
            // 
            // CoDPictureBox
            // 
            this.CoDPictureBox.Image = global::CoD_Widescreen_Suite.Properties.Resources.CoD1_UO_icon;
            this.CoDPictureBox.Location = new System.Drawing.Point(510, 311);
            this.CoDPictureBox.Name = "CoDPictureBox";
            this.CoDPictureBox.Size = new System.Drawing.Size(23, 23);
            this.CoDPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CoDPictureBox.TabIndex = 68;
            this.CoDPictureBox.TabStop = false;
            // 
            // ConnectServerButton
            // 
            this.ConnectServerButton.BackColor = System.Drawing.Color.DarkGray;
            this.ConnectServerButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectServerButton.Location = new System.Drawing.Point(803, 169);
            this.ConnectServerButton.Name = "ConnectServerButton";
            this.ConnectServerButton.Size = new System.Drawing.Size(113, 22);
            this.ConnectServerButton.TabIndex = 69;
            this.ConnectServerButton.Text = "Connect";
            this.ConnectServerButton.UseVisualStyleBackColor = false;
            this.ConnectServerButton.Click += new System.EventHandler(this.ConnectServerButton_Click);
            // 
            // FavoriteServerCheckbox
            // 
            this.FavoriteServerCheckbox.AutoSize = true;
            this.FavoriteServerCheckbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FavoriteServerCheckbox.Location = new System.Drawing.Point(596, 169);
            this.FavoriteServerCheckbox.Name = "FavoriteServerCheckbox";
            this.FavoriteServerCheckbox.Size = new System.Drawing.Size(131, 19);
            this.FavoriteServerCheckbox.TabIndex = 70;
            this.FavoriteServerCheckbox.Text = "Favorite server";
            this.FavoriteServerCheckbox.UseVisualStyleBackColor = true;
            this.FavoriteServerCheckbox.CheckedChanged += new System.EventHandler(this.FavoriteServerCheckbox_CheckedChanged);
            // 
            // FavoritesButton
            // 
            this.FavoritesButton.BackColor = System.Drawing.Color.DarkGray;
            this.FavoritesButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FavoritesButton.Location = new System.Drawing.Point(480, 249);
            this.FavoritesButton.Name = "FavoritesButton";
            this.FavoritesButton.Size = new System.Drawing.Size(113, 22);
            this.FavoritesButton.TabIndex = 71;
            this.FavoritesButton.Text = "Show Favorites";
            this.FavoritesButton.UseVisualStyleBackColor = false;
            this.FavoritesButton.Click += new System.EventHandler(this.FavoritesButton_Click);
            // 
            // ServersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(918, 402);
            this.Controls.Add(this.FavoritesButton);
            this.Controls.Add(this.FavoriteServerCheckbox);
            this.Controls.Add(this.ConnectServerButton);
            this.Controls.Add(this.CoDPictureBox);
            this.Controls.Add(this.FilterBotServersCheckbox);
            this.Controls.Add(this.RefreshServerButton);
            this.Controls.Add(this.PlayerCountLabel);
            this.Controls.Add(this.FilterBotPlayersCheckbox);
            this.Controls.Add(this.FilterPlayerNamesCheckbox);
            this.Controls.Add(this.PlayersTxtLabel);
            this.Controls.Add(this.PlayerListView);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.HideEmptyCheckbox);
            this.Controls.Add(this.CountServersLabel);
            this.Controls.Add(this.ServersLabel);
            this.Controls.Add(this.MapNameLabel);
            this.Controls.Add(this.MapLabel);
            this.Controls.Add(this.MapImageBox);
            this.Controls.Add(this.SearchFilterLabel);
            this.Controls.Add(this.GameVersionLabel);
            this.Controls.Add(this.GameVersionBox);
            this.Controls.Add(this.SearchFilterTextbox);
            this.Controls.Add(this.HideNoPingCheckbox);
            this.Controls.Add(this.MaxPingLabel);
            this.Controls.Add(this.MaxPingNumeric);
            this.Controls.Add(this.ServerListView);
            this.Controls.Add(this.RefreshButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ServersForm";
            this.Text = "Server List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServersForm_FormClosing);
            this.Load += new System.EventHandler(this.ServersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MaxPingNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoDPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RefreshButton;
        private CoD_Widescreen_Suite.Controls.ServerListView ServerListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.NumericUpDown MaxPingNumeric;
        private System.Windows.Forms.Label MaxPingLabel;
        private System.Windows.Forms.CheckBox HideNoPingCheckbox;
        private System.Windows.Forms.TextBox SearchFilterTextbox;
        private System.Windows.Forms.ComboBox GameVersionBox;
        private System.Windows.Forms.Label GameVersionLabel;
        private System.Windows.Forms.Label SearchFilterLabel;
        private System.Windows.Forms.PictureBox MapImageBox;
        private System.Windows.Forms.Label MapLabel;
        private System.Windows.Forms.Label MapNameLabel;
        private System.Windows.Forms.Label ServersLabel;
        private System.Windows.Forms.Label CountServersLabel;
        private System.Windows.Forms.CheckBox HideEmptyCheckbox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ListView PlayerListView;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label PlayersTxtLabel;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.CheckBox FilterPlayerNamesCheckbox;
        private System.Windows.Forms.CheckBox FilterBotPlayersCheckbox;
        private System.Windows.Forms.Label PlayerCountLabel;
        private System.Windows.Forms.Button RefreshServerButton;
        private System.Windows.Forms.CheckBox FilterBotServersCheckbox;
        internal System.Windows.Forms.PictureBox CoDPictureBox;
        private System.Windows.Forms.Button ConnectServerButton;
        private System.Windows.Forms.CheckBox FavoriteServerCheckbox;
        private System.Windows.Forms.Button FavoritesButton;
    }
}