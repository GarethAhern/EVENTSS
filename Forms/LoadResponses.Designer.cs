namespace EventsAndInvitesUI
{
    partial class LoadResponses
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
            this.customColumnHeadersDGV = new System.Windows.Forms.DataGridView();
            this.sendInviteButton = new System.Windows.Forms.Button();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.moveQuestionUpButton = new System.Windows.Forms.Button();
            this.columnHeaderGroupBox = new System.Windows.Forms.GroupBox();
            this.columnHeaderSplitContainer = new System.Windows.Forms.SplitContainer();
            this.defaultColumnHeadersDGV = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCurrentResponsesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearInviteAcceptRejectStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.customColumnHeadersDGV)).BeginInit();
            this.columnHeaderGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.columnHeaderSplitContainer)).BeginInit();
            this.columnHeaderSplitContainer.Panel1.SuspendLayout();
            this.columnHeaderSplitContainer.Panel2.SuspendLayout();
            this.columnHeaderSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultColumnHeadersDGV)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // customColumnHeadersDGV
            // 
            this.customColumnHeadersDGV.AllowUserToResizeRows = false;
            this.customColumnHeadersDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.customColumnHeadersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customColumnHeadersDGV.ColumnHeadersVisible = false;
            this.customColumnHeadersDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customColumnHeadersDGV.Location = new System.Drawing.Point(0, 0);
            this.customColumnHeadersDGV.Name = "customColumnHeadersDGV";
            this.customColumnHeadersDGV.RowHeadersVisible = false;
            this.customColumnHeadersDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.customColumnHeadersDGV.Size = new System.Drawing.Size(299, 206);
            this.customColumnHeadersDGV.TabIndex = 0;
            // 
            // sendInviteButton
            // 
            this.sendInviteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendInviteButton.BackgroundImage = global::EventsAndInvitesUI.Properties.Resources.arrow_outline_green_down;
            this.sendInviteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sendInviteButton.Location = new System.Drawing.Point(371, 159);
            this.sendInviteButton.Name = "sendInviteButton";
            this.sendInviteButton.Size = new System.Drawing.Size(81, 73);
            this.sendInviteButton.TabIndex = 8;
            this.sendInviteButton.UseVisualStyleBackColor = true;
            this.sendInviteButton.Click += new System.EventHandler(this.sendInviteButton_Click);
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.saveChangesButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.saveChangesButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.saveChangesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveChangesButton.Location = new System.Drawing.Point(323, 238);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(133, 104);
            this.saveChangesButton.TabIndex = 9;
            this.saveChangesButton.Text = "Save Changes";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            this.saveChangesButton.Click += new System.EventHandler(this.saveChangesButton_Click);
            // 
            // moveQuestionUpButton
            // 
            this.moveQuestionUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveQuestionUpButton.BackgroundImage = global::EventsAndInvitesUI.Properties.Resources.arrow_outline_green_up;
            this.moveQuestionUpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveQuestionUpButton.Location = new System.Drawing.Point(371, 39);
            this.moveQuestionUpButton.Name = "moveQuestionUpButton";
            this.moveQuestionUpButton.Size = new System.Drawing.Size(81, 73);
            this.moveQuestionUpButton.TabIndex = 11;
            this.moveQuestionUpButton.UseVisualStyleBackColor = true;
            this.moveQuestionUpButton.Click += new System.EventHandler(this.moveQuestionUpButton_Click);
            // 
            // columnHeaderGroupBox
            // 
            this.columnHeaderGroupBox.Controls.Add(this.columnHeaderSplitContainer);
            this.columnHeaderGroupBox.Location = new System.Drawing.Point(12, 23);
            this.columnHeaderGroupBox.Name = "columnHeaderGroupBox";
            this.columnHeaderGroupBox.Size = new System.Drawing.Size(305, 319);
            this.columnHeaderGroupBox.TabIndex = 12;
            this.columnHeaderGroupBox.TabStop = false;
            this.columnHeaderGroupBox.Text = "ColumnHeaders";
            // 
            // columnHeaderSplitContainer
            // 
            this.columnHeaderSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnHeaderSplitContainer.Location = new System.Drawing.Point(3, 16);
            this.columnHeaderSplitContainer.Name = "columnHeaderSplitContainer";
            this.columnHeaderSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // columnHeaderSplitContainer.Panel1
            // 
            this.columnHeaderSplitContainer.Panel1.Controls.Add(this.defaultColumnHeadersDGV);
            // 
            // columnHeaderSplitContainer.Panel2
            // 
            this.columnHeaderSplitContainer.Panel2.Controls.Add(this.customColumnHeadersDGV);
            this.columnHeaderSplitContainer.Size = new System.Drawing.Size(299, 300);
            this.columnHeaderSplitContainer.SplitterDistance = 90;
            this.columnHeaderSplitContainer.TabIndex = 15;
            // 
            // defaultColumnHeadersDGV
            // 
            this.defaultColumnHeadersDGV.AllowUserToAddRows = false;
            this.defaultColumnHeadersDGV.AllowUserToDeleteRows = false;
            this.defaultColumnHeadersDGV.AllowUserToResizeRows = false;
            this.defaultColumnHeadersDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.defaultColumnHeadersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.defaultColumnHeadersDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultColumnHeadersDGV.Location = new System.Drawing.Point(0, 0);
            this.defaultColumnHeadersDGV.Name = "defaultColumnHeadersDGV";
            this.defaultColumnHeadersDGV.ReadOnly = true;
            this.defaultColumnHeadersDGV.RowHeadersVisible = false;
            this.defaultColumnHeadersDGV.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.defaultColumnHeadersDGV.Size = new System.Drawing.Size(299, 90);
            this.defaultColumnHeadersDGV.TabIndex = 14;
            this.defaultColumnHeadersDGV.SelectionChanged += new System.EventHandler(this.defaultColumnHeadersDGV_SelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataToolStripMenuItem,
            this.clearCurrentResponsesToolStripMenuItem,
            this.clearInviteAcceptRejectStatusToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(489, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadDataToolStripMenuItem
            // 
            this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            this.loadDataToolStripMenuItem.Text = "Load New Responses";
            this.loadDataToolStripMenuItem.Click += new System.EventHandler(this.loadDataToolStripMenuItem_Click);
            // 
            // clearCurrentResponsesToolStripMenuItem
            // 
            this.clearCurrentResponsesToolStripMenuItem.Name = "clearCurrentResponsesToolStripMenuItem";
            this.clearCurrentResponsesToolStripMenuItem.Size = new System.Drawing.Size(155, 20);
            this.clearCurrentResponsesToolStripMenuItem.Text = "Clear Question Responses";
            this.clearCurrentResponsesToolStripMenuItem.Click += new System.EventHandler(this.clearCurrentResponsesToolStripMenuItem_Click);
            // 
            // clearInviteAcceptRejectStatusToolStripMenuItem
            // 
            this.clearInviteAcceptRejectStatusToolStripMenuItem.Name = "clearInviteAcceptRejectStatusToolStripMenuItem";
            this.clearInviteAcceptRejectStatusToolStripMenuItem.Size = new System.Drawing.Size(190, 20);
            this.clearInviteAcceptRejectStatusToolStripMenuItem.Text = "Clear Invite Accept/Reject Status";
            this.clearInviteAcceptRejectStatusToolStripMenuItem.Click += new System.EventHandler(this.clearInviteAcceptRejectStatusToolStripMenuItem_Click);
            // 
            // LoadResponses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(489, 347);
            this.Controls.Add(this.columnHeaderGroupBox);
            this.Controls.Add(this.moveQuestionUpButton);
            this.Controls.Add(this.saveChangesButton);
            this.Controls.Add(this.sendInviteButton);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LoadResponses";
            this.Text = "LoadResponses";
            ((System.ComponentModel.ISupportInitialize)(this.customColumnHeadersDGV)).EndInit();
            this.columnHeaderGroupBox.ResumeLayout(false);
            this.columnHeaderSplitContainer.Panel1.ResumeLayout(false);
            this.columnHeaderSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.columnHeaderSplitContainer)).EndInit();
            this.columnHeaderSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultColumnHeadersDGV)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView customColumnHeadersDGV;
        private System.Windows.Forms.Button sendInviteButton;
        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.Button moveQuestionUpButton;
        private System.Windows.Forms.GroupBox columnHeaderGroupBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCurrentResponsesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearInviteAcceptRejectStatusToolStripMenuItem;
        private System.Windows.Forms.DataGridView defaultColumnHeadersDGV;
        private System.Windows.Forms.SplitContainer columnHeaderSplitContainer;
    }
}