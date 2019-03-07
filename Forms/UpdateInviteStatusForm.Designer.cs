namespace EventsAndInvitesUI
{
    partial class UpdateInviteStatusForm
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.shortlistButton = new System.Windows.Forms.Button();
            this.removeInviteButton = new System.Windows.Forms.Button();
            this.rejectedInviteButton = new System.Windows.Forms.Button();
            this.sendInviteButton = new System.Windows.Forms.Button();
            this.acceptedInviteButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulkUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invitationsDataGridView = new System.Windows.Forms.DataGridView();
            this.searchClientsTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invitationsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // shortlistButton
            // 
            this.shortlistButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.shortlistButton.BackgroundImage = global::EventsAndInvitesUI.Properties.Resources.GreenPlus;
            this.shortlistButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shortlistButton.Enabled = false;
            this.shortlistButton.Location = new System.Drawing.Point(526, 30);
            this.shortlistButton.Name = "shortlistButton";
            this.shortlistButton.Size = new System.Drawing.Size(45, 50);
            this.shortlistButton.TabIndex = 3;
            this.toolTip1.SetToolTip(this.shortlistButton, "Add Client to ShortList");
            this.shortlistButton.UseVisualStyleBackColor = true;
            this.shortlistButton.Click += new System.EventHandler(this.shortlistButton_Click);
            // 
            // removeInviteButton
            // 
            this.removeInviteButton.BackgroundImage = global::EventsAndInvitesUI.Properties.Resources.RedMinus;
            this.removeInviteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.removeInviteButton.Enabled = false;
            this.removeInviteButton.Location = new System.Drawing.Point(80, 30);
            this.removeInviteButton.Name = "removeInviteButton";
            this.removeInviteButton.Size = new System.Drawing.Size(45, 50);
            this.removeInviteButton.TabIndex = 2;
            this.toolTip1.SetToolTip(this.removeInviteButton, "Remove Client from Shortlist");
            this.removeInviteButton.UseVisualStyleBackColor = true;
            this.removeInviteButton.Click += new System.EventHandler(this.removeInviteButton_Click);
            // 
            // rejectedInviteButton
            // 
            this.rejectedInviteButton.BackgroundImage = global::EventsAndInvitesUI.Properties.Resources.RedCross;
            this.rejectedInviteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rejectedInviteButton.Enabled = false;
            this.rejectedInviteButton.Location = new System.Drawing.Point(13, 30);
            this.rejectedInviteButton.Name = "rejectedInviteButton";
            this.rejectedInviteButton.Size = new System.Drawing.Size(45, 50);
            this.rejectedInviteButton.TabIndex = 1;
            this.toolTip1.SetToolTip(this.rejectedInviteButton, "Client rejected invitation");
            this.rejectedInviteButton.UseVisualStyleBackColor = true;
            this.rejectedInviteButton.Click += new System.EventHandler(this.rejectionButton_Click);
            // 
            // sendInviteButton
            // 
            this.sendInviteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendInviteButton.BackgroundImage = global::EventsAndInvitesUI.Properties.Resources.GreenEmail;
            this.sendInviteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sendInviteButton.Enabled = false;
            this.sendInviteButton.Location = new System.Drawing.Point(583, 30);
            this.sendInviteButton.Name = "sendInviteButton";
            this.sendInviteButton.Size = new System.Drawing.Size(45, 50);
            this.sendInviteButton.TabIndex = 4;
            this.toolTip1.SetToolTip(this.sendInviteButton, "Send Client an Invite Email");
            this.sendInviteButton.UseVisualStyleBackColor = true;
            this.sendInviteButton.Click += new System.EventHandler(this.sendEmailToClientButton_Click);
            // 
            // acceptedInviteButton
            // 
            this.acceptedInviteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptedInviteButton.BackgroundImage = global::EventsAndInvitesUI.Properties.Resources.GreenTick;
            this.acceptedInviteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.acceptedInviteButton.Enabled = false;
            this.acceptedInviteButton.Location = new System.Drawing.Point(640, 30);
            this.acceptedInviteButton.Name = "acceptedInviteButton";
            this.acceptedInviteButton.Size = new System.Drawing.Size(45, 50);
            this.acceptedInviteButton.TabIndex = 5;
            this.toolTip1.SetToolTip(this.acceptedInviteButton, "Client is coming");
            this.acceptedInviteButton.UseVisualStyleBackColor = true;
            this.acceptedInviteButton.Click += new System.EventHandler(this.eventAcceptedClientButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcelToolStripMenuItem,
            this.bulkUpdateToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(698, 24);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exportToExcelToolStripMenuItem
            // 
            this.exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
            this.exportToExcelToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.exportToExcelToolStripMenuItem.Text = "Export to Excel";
            this.exportToExcelToolStripMenuItem.Click += new System.EventHandler(this.exportToExcelToolStripMenuItem_Click);
            // 
            // bulkUpdateToolStripMenuItem
            // 
            this.bulkUpdateToolStripMenuItem.Enabled = false;
            this.bulkUpdateToolStripMenuItem.Name = "bulkUpdateToolStripMenuItem";
            this.bulkUpdateToolStripMenuItem.Size = new System.Drawing.Size(142, 20);
            this.bulkUpdateToolStripMenuItem.Text = "Bulk Update No. Invites";
            this.bulkUpdateToolStripMenuItem.Click += new System.EventHandler(this.bulkUpdateNoPlacesToolStripMenuItem_Click);
            // 
            // invitationsDataGridView
            // 
            this.invitationsDataGridView.AllowUserToAddRows = false;
            this.invitationsDataGridView.AllowUserToDeleteRows = false;
            this.invitationsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.invitationsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.invitationsDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.invitationsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.invitationsDataGridView.Location = new System.Drawing.Point(0, 86);
            this.invitationsDataGridView.Name = "invitationsDataGridView";
            this.invitationsDataGridView.RowHeadersVisible = false;
            this.invitationsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.invitationsDataGridView.Size = new System.Drawing.Size(698, 571);
            this.invitationsDataGridView.TabIndex = 26;
            this.invitationsDataGridView.SelectionChanged += new System.EventHandler(this.invitationsDataGridView_SelectionChanged);
            // 
            // searchClientsTextBox
            // 
            this.searchClientsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchClientsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchClientsTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.searchClientsTextBox.Location = new System.Drawing.Point(131, 37);
            this.searchClientsTextBox.Name = "searchClientsTextBox";
            this.searchClientsTextBox.Size = new System.Drawing.Size(389, 31);
            this.searchClientsTextBox.TabIndex = 27;
            this.searchClientsTextBox.Text = "Search for Client...";
            this.searchClientsTextBox.Enter += new System.EventHandler(this.searchClientsTextBox_Enter);
            this.searchClientsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchClientsTextBox_KeyDown);
            this.searchClientsTextBox.Leave += new System.EventHandler(this.searchClientsTextBox_Leave);
            // 
            // UpdateInviteStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(698, 657);
            this.Controls.Add(this.searchClientsTextBox);
            this.Controls.Add(this.invitationsDataGridView);
            this.Controls.Add(this.shortlistButton);
            this.Controls.Add(this.removeInviteButton);
            this.Controls.Add(this.rejectedInviteButton);
            this.Controls.Add(this.sendInviteButton);
            this.Controls.Add(this.acceptedInviteButton);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "UpdateInviteStatusForm";
            this.Text = "<>";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invitationsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button acceptedInviteButton;
        private System.Windows.Forms.Button sendInviteButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button rejectedInviteButton;
        private System.Windows.Forms.Button removeInviteButton;
        private System.Windows.Forms.Button shortlistButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;
        private System.Windows.Forms.DataGridView invitationsDataGridView;
        private System.Windows.Forms.ToolStripMenuItem bulkUpdateToolStripMenuItem;
        private System.Windows.Forms.TextBox searchClientsTextBox;
    }
}