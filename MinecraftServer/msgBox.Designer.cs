namespace MinecraftServer
{
    partial class msgBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(msgBox));
            this.Title = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(13, 13);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(64, 28);
            this.Title.TabIndex = 0;
            this.Title.Text = "Error";
            // 
            // close
            // 
            this.close.BackColor = System.Drawing.SystemColors.Control;
            this.close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.close.FlatAppearance.BorderSize = 0;
            this.close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.close.Location = new System.Drawing.Point(396, -1);
            this.close.Margin = new System.Windows.Forms.Padding(0);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(26, 26);
            this.close.TabIndex = 2;
            this.close.Text = "X";
            this.close.UseVisualStyleBackColor = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // message
            // 
            this.message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.message.Cursor = System.Windows.Forms.Cursors.Default;
            this.message.Font = new System.Drawing.Font("Open Sans", 12F);
            this.message.Location = new System.Drawing.Point(18, 44);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.message.Size = new System.Drawing.Size(415, 92);
            this.message.TabIndex = 3;
            this.message.Text = "Error";
            this.message.TextChanged += new System.EventHandler(this.message_TextChanged);
            this.message.KeyDown += new System.Windows.Forms.KeyEventHandler(this.message_KeyDown);
            // 
            // msgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(445, 148);
            this.ControlBox = false;
            this.Controls.Add(this.message);
            this.Controls.Add(this.close);
            this.Controls.Add(this.Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "msgBox";
            this.ShowInTaskbar = false;
            this.Text = " ";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.msgBox_close);
            this.Load += new System.EventHandler(this.msgBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.TextBox message;
    }
}