namespace EventsAndInvitesUI
{
    partial class MainDashBoards
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.searchEventsTextBox = new System.Windows.Forms.TextBox();
            this.includeEventDescriptionCheckBox = new System.Windows.Forms.CheckBox();
            this.yearsFilterLabel = new System.Windows.Forms.Label();
            this.yearsFilterListBox = new System.Windows.Forms.ListBox();
            this.monthsFilterLabel = new System.Windows.Forms.Label();
            this.monthsFilterListBox = new System.Windows.Forms.ListBox();
            this.showOnlyFutureEventsCheckBox = new System.Windows.Forms.CheckBox();
            this.eventsLabel = new System.Windows.Forms.Label();
            this.mainDashBoardMenuStrip = new System.Windows.Forms.MenuStrip();
            this.createNewEventToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.existingEventsListBox = new System.Windows.Forms.ListBox();
            this.createNewEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewRegistrationSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.mainDashBoardMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.searchEventsTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.includeEventDescriptionCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.yearsFilterLabel);
            this.splitContainer1.Panel1.Controls.Add(this.yearsFilterListBox);
            this.splitContainer1.Panel1.Controls.Add(this.monthsFilterLabel);
            this.splitContainer1.Panel1.Controls.Add(this.monthsFilterListBox);
            this.splitContainer1.Panel1.Controls.Add(this.showOnlyFutureEventsCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.eventsLabel);
            this.splitContainer1.Panel1.Controls.Add(this.mainDashBoardMenuStrip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.existingEventsListBox);
            this.splitContainer1.Size = new System.Drawing.Size(857, 627);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 1;
            // 
            // searchEventsTextBox
            // 
            this.searchEventsTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.searchEventsTextBox.Location = new System.Drawing.Point(20, 95);
            this.searchEventsTextBox.Name = "searchEventsTextBox";
            this.searchEventsTextBox.Size = new System.Drawing.Size(491, 30);
            this.searchEventsTextBox.TabIndex = 12;
            this.searchEventsTextBox.Text = "Search for Event...";
            this.searchEventsTextBox.Enter += new System.EventHandler(this.searchEventsTextBox_Enter);
            this.searchEventsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchEventsTextBox_KeyDown);
            this.searchEventsTextBox.Leave += new System.EventHandler(this.searchEventsTextBox_Leave);
            // 
            // includeEventDescriptionCheckBox
            // 
            this.includeEventDescriptionCheckBox.AutoSize = true;
            this.includeEventDescriptionCheckBox.Location = new System.Drawing.Point(20, 60);
            this.includeEventDescriptionCheckBox.Name = "includeEventDescriptionCheckBox";
            this.includeEventDescriptionCheckBox.Size = new System.Drawing.Size(339, 29);
            this.includeEventDescriptionCheckBox.TabIndex = 11;
            this.includeEventDescriptionCheckBox.Text = "Search Includes Event Descriptions";
            this.includeEventDescriptionCheckBox.UseVisualStyleBackColor = true;
            this.includeEventDescriptionCheckBox.CheckedChanged += new System.EventHandler(this.includeEventDescriptionCheckBox_CheckedChanged);
            // 
            // yearsFilterLabel
            // 
            this.yearsFilterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yearsFilterLabel.AutoSize = true;
            this.yearsFilterLabel.Location = new System.Drawing.Point(501, 18);
            this.yearsFilterLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.yearsFilterLabel.Name = "yearsFilterLabel";
            this.yearsFilterLabel.Size = new System.Drawing.Size(138, 25);
            this.yearsFilterLabel.TabIndex = 10;
            this.yearsFilterLabel.Text = "Filter By Years";
            // 
            // yearsFilterListBox
            // 
            this.yearsFilterListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.yearsFilterListBox.ColumnWidth = 45;
            this.yearsFilterListBox.FormattingEnabled = true;
            this.yearsFilterListBox.ItemHeight = 25;
            this.yearsFilterListBox.Location = new System.Drawing.Point(506, 46);
            this.yearsFilterListBox.MultiColumn = true;
            this.yearsFilterListBox.Name = "yearsFilterListBox";
            this.yearsFilterListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.yearsFilterListBox.Size = new System.Drawing.Size(89, 100);
            this.yearsFilterListBox.TabIndex = 9;
            this.yearsFilterListBox.SelectedIndexChanged += new System.EventHandler(this.yearsFilterListBox_SelectedIndexChanged);
            // 
            // monthsFilterLabel
            // 
            this.monthsFilterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monthsFilterLabel.AutoSize = true;
            this.monthsFilterLabel.Location = new System.Drawing.Point(663, 18);
            this.monthsFilterLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.monthsFilterLabel.Name = "monthsFilterLabel";
            this.monthsFilterLabel.Size = new System.Drawing.Size(152, 25);
            this.monthsFilterLabel.TabIndex = 8;
            this.monthsFilterLabel.Text = "Filter By Months";
            // 
            // monthsFilterListBox
            // 
            this.monthsFilterListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.monthsFilterListBox.ColumnWidth = 45;
            this.monthsFilterListBox.FormattingEnabled = true;
            this.monthsFilterListBox.ItemHeight = 25;
            this.monthsFilterListBox.Location = new System.Drawing.Point(668, 46);
            this.monthsFilterListBox.MultiColumn = true;
            this.monthsFilterListBox.Name = "monthsFilterListBox";
            this.monthsFilterListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.monthsFilterListBox.Size = new System.Drawing.Size(177, 100);
            this.monthsFilterListBox.TabIndex = 5;
            this.monthsFilterListBox.SelectedIndexChanged += new System.EventHandler(this.monthsListBox_SelectedIndexChanged);
            // 
            // showOnlyFutureEventsCheckBox
            // 
            this.showOnlyFutureEventsCheckBox.AutoSize = true;
            this.showOnlyFutureEventsCheckBox.Checked = true;
            this.showOnlyFutureEventsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOnlyFutureEventsCheckBox.Location = new System.Drawing.Point(20, 31);
            this.showOnlyFutureEventsCheckBox.Name = "showOnlyFutureEventsCheckBox";
            this.showOnlyFutureEventsCheckBox.Size = new System.Drawing.Size(253, 29);
            this.showOnlyFutureEventsCheckBox.TabIndex = 2;
            this.showOnlyFutureEventsCheckBox.Text = "Show Only Future Events";
            this.showOnlyFutureEventsCheckBox.UseVisualStyleBackColor = true;
            this.showOnlyFutureEventsCheckBox.CheckedChanged += new System.EventHandler(this.showOnlyFutureEventsCheckBox_CheckedChanged);
            // 
            // eventsLabel
            // 
            this.eventsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.eventsLabel.AutoSize = true;
            this.eventsLabel.Location = new System.Drawing.Point(15, 128);
            this.eventsLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.eventsLabel.Name = "eventsLabel";
            this.eventsLabel.Size = new System.Drawing.Size(72, 25);
            this.eventsLabel.TabIndex = 1;
            this.eventsLabel.Text = "Events";
            // 
            // mainDashBoardMenuStrip
            // 
            this.mainDashBoardMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewEventToolStripMenuItem1});
            this.mainDashBoardMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainDashBoardMenuStrip.Name = "mainDashBoardMenuStrip";
            this.mainDashBoardMenuStrip.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.mainDashBoardMenuStrip.Size = new System.Drawing.Size(857, 27);
            this.mainDashBoardMenuStrip.TabIndex = 0;
            this.mainDashBoardMenuStrip.Text = "menuStrip1";
            // 
            // createNewEventToolStripMenuItem1
            // 
            this.createNewEventToolStripMenuItem1.Name = "createNewEventToolStripMenuItem1";
            this.createNewEventToolStripMenuItem1.Size = new System.Drawing.Size(112, 19);
            this.createNewEventToolStripMenuItem1.Text = "Create New Event";
            this.createNewEventToolStripMenuItem1.Click += new System.EventHandler(this.createNewEventToolStripMenuItem1_Click);
            // 
            // existingEventsListBox
            // 
            this.existingEventsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.existingEventsListBox.FormattingEnabled = true;
            this.existingEventsListBox.ItemHeight = 25;
            this.existingEventsListBox.Location = new System.Drawing.Point(0, 0);
            this.existingEventsListBox.Name = "existingEventsListBox";
            this.existingEventsListBox.Size = new System.Drawing.Size(857, 466);
            this.existingEventsListBox.TabIndex = 1;
            this.existingEventsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.existingEventsListBox_MouseDoubleClick);
            // 
            // createNewEventToolStripMenuItem
            // 
            this.createNewEventToolStripMenuItem.Name = "createNewEventToolStripMenuItem";
            this.createNewEventToolStripMenuItem.Size = new System.Drawing.Size(112, 19);
            this.createNewEventToolStripMenuItem.Text = "Create New Event";
            this.createNewEventToolStripMenuItem.Click += new System.EventHandler(this.createNewEventToolStripMenuItem_Click);
            // 
            // viewRegistrationSetupToolStripMenuItem
            // 
            this.viewRegistrationSetupToolStripMenuItem.Name = "viewRegistrationSetupToolStripMenuItem";
            this.viewRegistrationSetupToolStripMenuItem.Size = new System.Drawing.Size(143, 19);
            this.viewRegistrationSetupToolStripMenuItem.Text = "View Registration Setup";
            this.viewRegistrationSetupToolStripMenuItem.Click += new System.EventHandler(this.viewRegistrationSetupToolStripMenuItem_Click);
            // 
            // MainDashBoards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(857, 582);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainDashBoards";
            this.Text = "Events & Invites";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.mainDashBoardMenuStrip.ResumeLayout(false);
            this.mainDashBoardMenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip mainDashBoardMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createNewEventToolStripMenuItem;
        private System.Windows.Forms.Label eventsLabel;
        private System.Windows.Forms.ToolStripMenuItem viewRegistrationSetupToolStripMenuItem;
        private System.Windows.Forms.CheckBox showOnlyFutureEventsCheckBox;
        private System.Windows.Forms.ListBox monthsFilterListBox;
        private System.Windows.Forms.ListBox existingEventsListBox;
        private System.Windows.Forms.Label monthsFilterLabel;
        private System.Windows.Forms.ToolStripMenuItem createNewEventToolStripMenuItem1;
        private System.Windows.Forms.Label yearsFilterLabel;
        private System.Windows.Forms.ListBox yearsFilterListBox;
        private System.Windows.Forms.CheckBox includeEventDescriptionCheckBox;
        private System.Windows.Forms.TextBox searchEventsTextBox;
    }
}

