namespace GMaster
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installTheWc3LoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.gamelistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapCfgGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bNETLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TabBnet = new System.Windows.Forms.TabPage();
            this.BExitBnet = new System.Windows.Forms.Button();
            this.BChannel = new System.Windows.Forms.Button();
            this.ListUsers = new System.Windows.Forms.ListBox();
            this.txtChatter = new System.Windows.Forms.TextBox();
            this.rtbBnetChat = new System.Windows.Forms.RichTextBox();
            this.TxtChannel = new System.Windows.Forms.TextBox();
            this.TabConsole = new System.Windows.Forms.TabPage();
            this.BStop = new System.Windows.Forms.Button();
            this.BStart = new System.Windows.Forms.Button();
            this.txtIndicator = new System.Windows.Forms.TextBox();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NumLPort = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.MainMenu.SuspendLayout();
            this.TabBnet.SuspendLayout();
            this.TabConsole.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumLPort)).BeginInit();
            this.Tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.logsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(600, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem1,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem1.Text = "Settings";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installTheWc3LoaderToolStripMenuItem,
            this.addTheToolStripMenuItem,
            this.toolStripSeparator1,
            this.gamelistToolStripMenuItem,
            this.mapCfgGeneratorToolStripMenuItem,
            this.toolStripSeparator3});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.settingsToolStripMenuItem.Text = "Tools";
            // 
            // installTheWc3LoaderToolStripMenuItem
            // 
            this.installTheWc3LoaderToolStripMenuItem.Name = "installTheWc3LoaderToolStripMenuItem";
            this.installTheWc3LoaderToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.installTheWc3LoaderToolStripMenuItem.Text = "Install the WC3  loader";
            this.installTheWc3LoaderToolStripMenuItem.Click += new System.EventHandler(this.installTheWc3LoaderToolStripMenuItem_Click);
            // 
            // addTheToolStripMenuItem
            // 
            this.addTheToolStripMenuItem.Name = "addTheToolStripMenuItem";
            this.addTheToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.addTheToolStripMenuItem.Text = "Add the gateway";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(189, 6);
            // 
            // gamelistToolStripMenuItem
            // 
            this.gamelistToolStripMenuItem.Name = "gamelistToolStripMenuItem";
            this.gamelistToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.gamelistToolStripMenuItem.Text = "Gamelist";
            // 
            // mapCfgGeneratorToolStripMenuItem
            // 
            this.mapCfgGeneratorToolStripMenuItem.Name = "mapCfgGeneratorToolStripMenuItem";
            this.mapCfgGeneratorToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.mapCfgGeneratorToolStripMenuItem.Text = "Map cfg generator";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(189, 6);
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameLogToolStripMenuItem,
            this.bNETLogToolStripMenuItem});
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.logsToolStripMenuItem.Text = "Logs";
            // 
            // gameLogToolStripMenuItem
            // 
            this.gameLogToolStripMenuItem.Name = "gameLogToolStripMenuItem";
            this.gameLogToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.gameLogToolStripMenuItem.Text = "Game Log";
            // 
            // bNETLogToolStripMenuItem
            // 
            this.bNETLogToolStripMenuItem.Name = "bNETLogToolStripMenuItem";
            this.bNETLogToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.bNETLogToolStripMenuItem.Text = "BNET Log";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator4,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem1.Text = "Site";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(104, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // TrayIcon
            // 
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Game Master";
            this.TrayIcon.Visible = true;
            // 
            // TabBnet
            // 
            this.TabBnet.Controls.Add(this.BExitBnet);
            this.TabBnet.Controls.Add(this.BChannel);
            this.TabBnet.Controls.Add(this.ListUsers);
            this.TabBnet.Controls.Add(this.txtChatter);
            this.TabBnet.Controls.Add(this.rtbBnetChat);
            this.TabBnet.Controls.Add(this.TxtChannel);
            this.TabBnet.Location = new System.Drawing.Point(4, 22);
            this.TabBnet.Name = "TabBnet";
            this.TabBnet.Padding = new System.Windows.Forms.Padding(3);
            this.TabBnet.Size = new System.Drawing.Size(592, 264);
            this.TabBnet.TabIndex = 0;
            this.TabBnet.Text = "Bnet";
            this.TabBnet.UseVisualStyleBackColor = true;
            // 
            // BExitBnet
            // 
            this.BExitBnet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BExitBnet.Location = new System.Drawing.Point(523, 233);
            this.BExitBnet.Name = "BExitBnet";
            this.BExitBnet.Size = new System.Drawing.Size(62, 25);
            this.BExitBnet.TabIndex = 5;
            this.BExitBnet.Text = "Exit bnet";
            this.BExitBnet.UseVisualStyleBackColor = true;
            this.BExitBnet.Click += new System.EventHandler(this.BExitBnet_Click);
            // 
            // BChannel
            // 
            this.BChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BChannel.Location = new System.Drawing.Point(452, 233);
            this.BChannel.Name = "BChannel";
            this.BChannel.Size = new System.Drawing.Size(62, 25);
            this.BChannel.TabIndex = 4;
            this.BChannel.Text = "Channel";
            this.BChannel.UseVisualStyleBackColor = true;
            // 
            // ListUsers
            // 
            this.ListUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListUsers.BackColor = System.Drawing.Color.Black;
            this.ListUsers.ForeColor = System.Drawing.Color.Gold;
            this.ListUsers.FormattingEnabled = true;
            this.ListUsers.Location = new System.Drawing.Point(452, 31);
            this.ListUsers.Name = "ListUsers";
            this.ListUsers.Size = new System.Drawing.Size(137, 199);
            this.ListUsers.TabIndex = 3;
            // 
            // txtChatter
            // 
            this.txtChatter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChatter.Location = new System.Drawing.Point(3, 238);
            this.txtChatter.Name = "txtChatter";
            this.txtChatter.Size = new System.Drawing.Size(443, 20);
            this.txtChatter.TabIndex = 2;
            this.txtChatter.TextChanged += new System.EventHandler(this.txtChatter_TextChanged);
            this.txtChatter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChatter_KeyDown);
            // 
            // rtbBnetChat
            // 
            this.rtbBnetChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbBnetChat.BackColor = System.Drawing.Color.Black;
            this.rtbBnetChat.ForeColor = System.Drawing.Color.White;
            this.rtbBnetChat.Location = new System.Drawing.Point(3, 31);
            this.rtbBnetChat.Name = "rtbBnetChat";
            this.rtbBnetChat.ReadOnly = true;
            this.rtbBnetChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbBnetChat.Size = new System.Drawing.Size(443, 204);
            this.rtbBnetChat.TabIndex = 1;
            this.rtbBnetChat.Text = "";
            // 
            // TxtChannel
            // 
            this.TxtChannel.BackColor = System.Drawing.Color.LightGray;
            this.TxtChannel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtChannel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.TxtChannel.Location = new System.Drawing.Point(3, 3);
            this.TxtChannel.Name = "TxtChannel";
            this.TxtChannel.ReadOnly = true;
            this.TxtChannel.Size = new System.Drawing.Size(586, 22);
            this.TxtChannel.TabIndex = 0;
            this.TxtChannel.Text = "Channel";
            this.TxtChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TabConsole
            // 
            this.TabConsole.Controls.Add(this.BStop);
            this.TabConsole.Controls.Add(this.BStart);
            this.TabConsole.Controls.Add(this.txtIndicator);
            this.TabConsole.Controls.Add(this.txtHostname);
            this.TabConsole.Controls.Add(this.rtbConsole);
            this.TabConsole.Controls.Add(this.label3);
            this.TabConsole.Controls.Add(this.NumLPort);
            this.TabConsole.Controls.Add(this.label2);
            this.TabConsole.Controls.Add(this.label1);
            this.TabConsole.Location = new System.Drawing.Point(4, 22);
            this.TabConsole.Name = "TabConsole";
            this.TabConsole.Padding = new System.Windows.Forms.Padding(3);
            this.TabConsole.Size = new System.Drawing.Size(592, 264);
            this.TabConsole.TabIndex = 1;
            this.TabConsole.Text = "Console";
            this.TabConsole.UseVisualStyleBackColor = true;
            // 
            // BStop
            // 
            this.BStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BStop.Location = new System.Drawing.Point(484, 215);
            this.BStop.Name = "BStop";
            this.BStop.Size = new System.Drawing.Size(98, 31);
            this.BStop.TabIndex = 19;
            this.BStop.Text = "&Stop";
            this.BStop.UseVisualStyleBackColor = true;
            this.BStop.Click += new System.EventHandler(this.BStop_Click);
            // 
            // BStart
            // 
            this.BStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BStart.Location = new System.Drawing.Point(485, 167);
            this.BStart.Name = "BStart";
            this.BStart.Size = new System.Drawing.Size(98, 36);
            this.BStart.TabIndex = 18;
            this.BStart.Text = "&Start";
            this.BStart.UseVisualStyleBackColor = true;
            this.BStart.Click += new System.EventHandler(this.BStart_Click);
            // 
            // txtIndicator
            // 
            this.txtIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIndicator.Location = new System.Drawing.Point(484, 131);
            this.txtIndicator.Name = "txtIndicator";
            this.txtIndicator.Size = new System.Drawing.Size(99, 20);
            this.txtIndicator.TabIndex = 17;
            this.txtIndicator.Text = "[G]";
            // 
            // txtHostname
            // 
            this.txtHostname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHostname.Location = new System.Drawing.Point(484, 32);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(99, 20);
            this.txtHostname.TabIndex = 13;
            this.txtHostname.Text = "europe.battle.net";
            // 
            // rtbConsole
            // 
            this.rtbConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbConsole.BackColor = System.Drawing.Color.Black;
            this.rtbConsole.ForeColor = System.Drawing.Color.White;
            this.rtbConsole.Location = new System.Drawing.Point(3, 0);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.ReadOnly = true;
            this.rtbConsole.Size = new System.Drawing.Size(475, 256);
            this.rtbConsole.TabIndex = 2;
            this.rtbConsole.Text = "";
            this.rtbConsole.TextChanged += new System.EventHandler(this.rtbConsole_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Game indicator";
            // 
            // NumLPort
            // 
            this.NumLPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NumLPort.Location = new System.Drawing.Point(484, 80);
            this.NumLPort.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.NumLPort.Name = "NumLPort";
            this.NumLPort.Size = new System.Drawing.Size(99, 20);
            this.NumLPort.TabIndex = 15;
            this.NumLPort.Value = new decimal(new int[] {
            6113,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(484, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Game listening port";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(484, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "BNET Hostname";
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.TabConsole);
            this.Tabs.Controls.Add(this.TabBnet);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 24);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(600, 290);
            this.Tabs.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 314);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Main";
            this.Text = "GMaster";
            this.Load += new System.EventHandler(this.Main_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.TabBnet.ResumeLayout(false);
            this.TabBnet.PerformLayout();
            this.TabConsole.ResumeLayout(false);
            this.TabConsole.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumLPort)).EndInit();
            this.Tabs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installTheWc3LoaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTheToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem gamelistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapCfgGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem gameLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bNETLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TabPage TabBnet;
        private System.Windows.Forms.Button BExitBnet;
        private System.Windows.Forms.Button BChannel;
        private System.Windows.Forms.ListBox ListUsers;
        private System.Windows.Forms.TextBox txtChatter;
        private System.Windows.Forms.RichTextBox rtbBnetChat;
        private System.Windows.Forms.TextBox TxtChannel;
        private System.Windows.Forms.TabPage TabConsole;
        private System.Windows.Forms.Button BStop;
        private System.Windows.Forms.Button BStart;
        private System.Windows.Forms.TextBox txtIndicator;
        private System.Windows.Forms.TextBox txtHostname;
        private System.Windows.Forms.RichTextBox rtbConsole;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NumLPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl Tabs;
    }
}

