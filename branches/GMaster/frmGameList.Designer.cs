namespace GMaster
{
    partial class frmGameList
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
            this.ListGames = new System.Windows.Forms.ListBox();
            this.txtPrivateSearch = new System.Windows.Forms.TextBox();
            this.BSPrivate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListGames
            // 
            this.ListGames.FormattingEnabled = true;
            this.ListGames.Location = new System.Drawing.Point(3, 28);
            this.ListGames.Name = "ListGames";
            this.ListGames.Size = new System.Drawing.Size(266, 264);
            this.ListGames.TabIndex = 0;
            // 
            // txtPrivateSearch
            // 
            this.txtPrivateSearch.Location = new System.Drawing.Point(3, 298);
            this.txtPrivateSearch.Name = "txtPrivateSearch";
            this.txtPrivateSearch.Size = new System.Drawing.Size(163, 20);
            this.txtPrivateSearch.TabIndex = 1;
            // 
            // BSPrivate
            // 
            this.BSPrivate.Location = new System.Drawing.Point(172, 298);
            this.BSPrivate.Name = "BSPrivate";
            this.BSPrivate.Size = new System.Drawing.Size(97, 20);
            this.BSPrivate.TabIndex = 2;
            this.BSPrivate.Text = "Search private";
            this.BSPrivate.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(266, 21);
            this.button1.TabIndex = 3;
            this.button1.Text = "Refresh gamelist";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmGameList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 325);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BSPrivate);
            this.Controls.Add(this.txtPrivateSearch);
            this.Controls.Add(this.ListGames);
            this.Name = "frmGameList";
            this.Text = "frmGameList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListGames;
        private System.Windows.Forms.TextBox txtPrivateSearch;
        private System.Windows.Forms.Button BSPrivate;
        private System.Windows.Forms.Button button1;
    }
}