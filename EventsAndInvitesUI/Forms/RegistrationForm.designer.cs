namespace EventsAndInvitesUI
{
    partial class RegistrationForm
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
            this.unregisteredMemberList = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.registerMemberListLabel = new System.Windows.Forms.Label();
            this.previewRegistrationEmailButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.unregisteredMemberList)).BeginInit();
            this.SuspendLayout();
            // 
            // unregisteredMemberList
            // 
            this.unregisteredMemberList.BackgroundColor = System.Drawing.Color.White;
            this.unregisteredMemberList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.unregisteredMemberList.Location = new System.Drawing.Point(13, 60);
            this.unregisteredMemberList.Name = "unregisteredMemberList";
            this.unregisteredMemberList.Size = new System.Drawing.Size(304, 543);
            this.unregisteredMemberList.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(340, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(517, 360);
            this.textBox1.TabIndex = 1;
            // 
            // registerMemberListLabel
            // 
            this.registerMemberListLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.registerMemberListLabel.AutoSize = true;
            this.registerMemberListLabel.Location = new System.Drawing.Point(8, 15);
            this.registerMemberListLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.registerMemberListLabel.Name = "registerMemberListLabel";
            this.registerMemberListLabel.Size = new System.Drawing.Size(249, 25);
            this.registerMemberListLabel.TabIndex = 2;
            this.registerMemberListLabel.Text = "Select Member To Register";
            // 
            // previewRegistrationEmailButton
            // 
            this.previewRegistrationEmailButton.Location = new System.Drawing.Point(647, 537);
            this.previewRegistrationEmailButton.Name = "previewRegistrationEmailButton";
            this.previewRegistrationEmailButton.Size = new System.Drawing.Size(209, 66);
            this.previewRegistrationEmailButton.TabIndex = 3;
            this.previewRegistrationEmailButton.Text = "Send Registration Email";
            this.previewRegistrationEmailButton.UseVisualStyleBackColor = true;
            this.previewRegistrationEmailButton.Click += new System.EventHandler(this.previewRegistrationEmailButton_Click);
            // 
            // Registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(869, 615);
            this.Controls.Add(this.previewRegistrationEmailButton);
            this.Controls.Add(this.registerMemberListLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.unregisteredMemberList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Registration";
            this.Text = "Registration";
            ((System.ComponentModel.ISupportInitialize)(this.unregisteredMemberList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView unregisteredMemberList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label registerMemberListLabel;
        private System.Windows.Forms.Button previewRegistrationEmailButton;
    }
}