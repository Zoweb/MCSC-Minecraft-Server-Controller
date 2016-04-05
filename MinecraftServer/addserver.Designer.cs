namespace MinecraftServer
{
    partial class addserver
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
            this.snamelabel = new System.Windows.Forms.Label();
            this.serverName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // snamelabel
            // 
            this.snamelabel.AutoSize = true;
            this.snamelabel.Location = new System.Drawing.Point(13, 13);
            this.snamelabel.Name = "snamelabel";
            this.snamelabel.Size = new System.Drawing.Size(69, 13);
            this.snamelabel.TabIndex = 0;
            this.snamelabel.Text = "Server Name";
            // 
            // serverName
            // 
            this.serverName.Location = new System.Drawing.Point(16, 30);
            this.serverName.Name = "serverName";
            this.serverName.Size = new System.Drawing.Size(256, 20);
            this.serverName.TabIndex = 1;
            this.serverName.TextChanged += new System.EventHandler(this.serverName_TextChanged);
            // 
            // addserver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.serverName);
            this.Controls.Add(this.snamelabel);
            this.Name = "addserver";
            this.Text = "Add Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label snamelabel;
        private System.Windows.Forms.TextBox serverName;
    }
}