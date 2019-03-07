namespace EventsAndInvitesUI
{
    partial class EventTypeForm
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
            this.existingEventTypesLabel = new System.Windows.Forms.Label();
            this.newEventTypeLabel = new System.Windows.Forms.Label();
            this.newEventTypeTextBox = new System.Windows.Forms.TextBox();
            this.existingEventTypesListBox = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.showHideEventGrouperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createEventTypeButton = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.listBox5 = new System.Windows.Forms.ListBox();
            this.listBox6 = new System.Windows.Forms.ListBox();
            this.eventType1TextBox = new System.Windows.Forms.TextBox();
            this.eventType2TextBox = new System.Windows.Forms.TextBox();
            this.eventType3TextBox = new System.Windows.Forms.TextBox();
            this.eventType6TextBox = new System.Windows.Forms.TextBox();
            this.eventType5TextBox = new System.Windows.Forms.TextBox();
            this.eventType4TextBox = new System.Windows.Forms.TextBox();
            this.listBox7 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // existingEventTypesLabel
            // 
            this.existingEventTypesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.existingEventTypesLabel.AutoSize = true;
            this.existingEventTypesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.existingEventTypesLabel.Location = new System.Drawing.Point(-3, 101);
            this.existingEventTypesLabel.Name = "existingEventTypesLabel";
            this.existingEventTypesLabel.Size = new System.Drawing.Size(195, 25);
            this.existingEventTypesLabel.TabIndex = 1;
            this.existingEventTypesLabel.Text = "Existing Event Types";
            // 
            // newEventTypeLabel
            // 
            this.newEventTypeLabel.AutoSize = true;
            this.newEventTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.newEventTypeLabel.Location = new System.Drawing.Point(7, 28);
            this.newEventTypeLabel.Name = "newEventTypeLabel";
            this.newEventTypeLabel.Size = new System.Drawing.Size(166, 25);
            this.newEventTypeLabel.TabIndex = 2;
            this.newEventTypeLabel.Text = "New Event Types";
            // 
            // newEventTypeTextBox
            // 
            this.newEventTypeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.newEventTypeTextBox.Location = new System.Drawing.Point(12, 56);
            this.newEventTypeTextBox.Name = "newEventTypeTextBox";
            this.newEventTypeTextBox.Size = new System.Drawing.Size(249, 30);
            this.newEventTypeTextBox.TabIndex = 3;
            // 
            // existingEventTypesListBox
            // 
            this.existingEventTypesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.existingEventTypesListBox.FormattingEnabled = true;
            this.existingEventTypesListBox.Location = new System.Drawing.Point(2, 129);
            this.existingEventTypesListBox.Name = "existingEventTypesListBox";
            this.existingEventTypesListBox.Size = new System.Drawing.Size(364, 433);
            this.existingEventTypesListBox.TabIndex = 10;
            this.existingEventTypesListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.existingEventTypesListBox_MouseDoubleClick);
            this.existingEventTypesListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.existingEventTypesListBox_MouseDown);
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(639, 134);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(142, 173);
            this.listBox1.TabIndex = 11;
            this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragOver += new System.Windows.Forms.DragEventHandler(this.listBox1_DragOver);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideEventGrouperToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1140, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // showHideEventGrouperToolStripMenuItem
            // 
            this.showHideEventGrouperToolStripMenuItem.Name = "showHideEventGrouperToolStripMenuItem";
            this.showHideEventGrouperToolStripMenuItem.Size = new System.Drawing.Size(156, 20);
            this.showHideEventGrouperToolStripMenuItem.Text = "Show/Hide Event Grouper";
            this.showHideEventGrouperToolStripMenuItem.Click += new System.EventHandler(this.showHideEventGrouperToolStripMenuItem_Click);
            // 
            // createEventTypeButton
            // 
            this.createEventTypeButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.createEventTypeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.createEventTypeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.createEventTypeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createEventTypeButton.Location = new System.Drawing.Point(273, 45);
            this.createEventTypeButton.Name = "createEventTypeButton";
            this.createEventTypeButton.Size = new System.Drawing.Size(93, 58);
            this.createEventTypeButton.TabIndex = 55;
            this.createEventTypeButton.Text = "Add New Event Type";
            this.createEventTypeButton.UseVisualStyleBackColor = true;
            this.createEventTypeButton.Click += new System.EventHandler(this.createEventTypeButton_Click);
            // 
            // listBox2
            // 
            this.listBox2.AllowDrop = true;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(812, 134);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(142, 173);
            this.listBox2.TabIndex = 56;
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(985, 134);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(142, 173);
            this.listBox3.TabIndex = 57;
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.Location = new System.Drawing.Point(641, 389);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(142, 173);
            this.listBox4.TabIndex = 58;
            // 
            // listBox5
            // 
            this.listBox5.FormattingEnabled = true;
            this.listBox5.Location = new System.Drawing.Point(812, 389);
            this.listBox5.Name = "listBox5";
            this.listBox5.Size = new System.Drawing.Size(142, 173);
            this.listBox5.TabIndex = 59;
            // 
            // listBox6
            // 
            this.listBox6.FormattingEnabled = true;
            this.listBox6.Location = new System.Drawing.Point(983, 389);
            this.listBox6.Name = "listBox6";
            this.listBox6.Size = new System.Drawing.Size(142, 173);
            this.listBox6.TabIndex = 60;
            // 
            // eventType1TextBox
            // 
            this.eventType1TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.eventType1TextBox.Location = new System.Drawing.Point(639, 96);
            this.eventType1TextBox.Name = "eventType1TextBox";
            this.eventType1TextBox.Size = new System.Drawing.Size(142, 26);
            this.eventType1TextBox.TabIndex = 61;
            this.eventType1TextBox.Text = "<<EVENT TYPE 1>>";
            // 
            // eventType2TextBox
            // 
            this.eventType2TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.eventType2TextBox.Location = new System.Drawing.Point(812, 96);
            this.eventType2TextBox.Name = "eventType2TextBox";
            this.eventType2TextBox.Size = new System.Drawing.Size(142, 26);
            this.eventType2TextBox.TabIndex = 62;
            this.eventType2TextBox.Text = "<<EVENT TYPE 2>>";
            // 
            // eventType3TextBox
            // 
            this.eventType3TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.eventType3TextBox.Location = new System.Drawing.Point(986, 98);
            this.eventType3TextBox.Name = "eventType3TextBox";
            this.eventType3TextBox.Size = new System.Drawing.Size(142, 26);
            this.eventType3TextBox.TabIndex = 63;
            this.eventType3TextBox.Text = "<<EVENT TYPE 3>>";
            // 
            // eventType6TextBox
            // 
            this.eventType6TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.eventType6TextBox.Location = new System.Drawing.Point(983, 348);
            this.eventType6TextBox.Name = "eventType6TextBox";
            this.eventType6TextBox.Size = new System.Drawing.Size(142, 26);
            this.eventType6TextBox.TabIndex = 66;
            this.eventType6TextBox.Text = "<<EVENT TYPE 6>>";
            // 
            // eventType5TextBox
            // 
            this.eventType5TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.eventType5TextBox.Location = new System.Drawing.Point(812, 348);
            this.eventType5TextBox.Name = "eventType5TextBox";
            this.eventType5TextBox.Size = new System.Drawing.Size(142, 26);
            this.eventType5TextBox.TabIndex = 65;
            this.eventType5TextBox.Text = "<<EVENT TYPE 5>>";
            // 
            // eventType4TextBox
            // 
            this.eventType4TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.eventType4TextBox.Location = new System.Drawing.Point(641, 348);
            this.eventType4TextBox.Name = "eventType4TextBox";
            this.eventType4TextBox.Size = new System.Drawing.Size(142, 26);
            this.eventType4TextBox.TabIndex = 64;
            this.eventType4TextBox.Text = "<<EVENT TYPE 4>>";
            // 
            // listBox7
            // 
            this.listBox7.AllowDrop = true;
            this.listBox7.FormattingEnabled = true;
            this.listBox7.Location = new System.Drawing.Point(413, 129);
            this.listBox7.Name = "listBox7";
            this.listBox7.Size = new System.Drawing.Size(198, 433);
            this.listBox7.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(402, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 25);
            this.label1.TabIndex = 68;
            this.label1.Text = "Unassigned Event Types";
            // 
            // EventTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1140, 567);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox7);
            this.Controls.Add(this.eventType6TextBox);
            this.Controls.Add(this.eventType5TextBox);
            this.Controls.Add(this.eventType4TextBox);
            this.Controls.Add(this.eventType3TextBox);
            this.Controls.Add(this.eventType2TextBox);
            this.Controls.Add(this.eventType1TextBox);
            this.Controls.Add(this.listBox6);
            this.Controls.Add(this.listBox5);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.createEventTypeButton);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.existingEventTypesListBox);
            this.Controls.Add(this.newEventTypeTextBox);
            this.Controls.Add(this.newEventTypeLabel);
            this.Controls.Add(this.existingEventTypesLabel);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EventTypeForm";
            this.Text = "Add New Event Type";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label existingEventTypesLabel;
        private System.Windows.Forms.Label newEventTypeLabel;
        private System.Windows.Forms.TextBox newEventTypeTextBox;
        private System.Windows.Forms.ListBox existingEventTypesListBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showHideEventGrouperToolStripMenuItem;
        private System.Windows.Forms.Button createEventTypeButton;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.ListBox listBox5;
        private System.Windows.Forms.ListBox listBox6;
        private System.Windows.Forms.TextBox eventType1TextBox;
        private System.Windows.Forms.TextBox eventType2TextBox;
        private System.Windows.Forms.TextBox eventType3TextBox;
        private System.Windows.Forms.TextBox eventType6TextBox;
        private System.Windows.Forms.TextBox eventType5TextBox;
        private System.Windows.Forms.TextBox eventType4TextBox;
        private System.Windows.Forms.ListBox listBox7;
        private System.Windows.Forms.Label label1;
    }
}