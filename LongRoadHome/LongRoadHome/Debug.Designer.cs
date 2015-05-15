namespace uk.ac.dundee.arpond.longRoadHome
{
    partial class Debug
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
            this.healthLabel = new System.Windows.Forms.Label();
            this.hungerLabel = new System.Windows.Forms.Label();
            this.thirstLabel = new System.Windows.Forms.Label();
            this.Sanity = new System.Windows.Forms.Label();
            this.pcGroupBox = new System.Windows.Forms.GroupBox();
            this.inventoryListBox = new System.Windows.Forms.ListBox();
            this.locationGroupBox = new System.Windows.Forms.GroupBox();
            this.currentLocationLabel = new System.Windows.Forms.Label();
            this.currentSublocationLabel = new System.Windows.Forms.Label();
            this.visitedListBox = new System.Windows.Forms.ListBox();
            this.unvisitedListBox = new System.Windows.Forms.ListBox();
            this.eventGroupBox = new System.Windows.Forms.GroupBox();
            this.currentEventLabel = new System.Windows.Forms.Label();
            this.usedEventsLabel = new System.Windows.Forms.Label();
            this.eventCatalogue = new System.Windows.Forms.ListBox();
            this.itemCatalogue = new System.Windows.Forms.ListBox();
            this.pcGroupBox.SuspendLayout();
            this.locationGroupBox.SuspendLayout();
            this.eventGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // healthLabel
            // 
            this.healthLabel.AutoSize = true;
            this.healthLabel.Location = new System.Drawing.Point(6, 27);
            this.healthLabel.Name = "healthLabel";
            this.healthLabel.Size = new System.Drawing.Size(38, 13);
            this.healthLabel.TabIndex = 0;
            this.healthLabel.Text = "Health";
            // 
            // hungerLabel
            // 
            this.hungerLabel.AutoSize = true;
            this.hungerLabel.Location = new System.Drawing.Point(141, 27);
            this.hungerLabel.Name = "hungerLabel";
            this.hungerLabel.Size = new System.Drawing.Size(42, 13);
            this.hungerLabel.TabIndex = 1;
            this.hungerLabel.Text = "Hunger";
            // 
            // thirstLabel
            // 
            this.thirstLabel.AutoSize = true;
            this.thirstLabel.Location = new System.Drawing.Point(257, 27);
            this.thirstLabel.Name = "thirstLabel";
            this.thirstLabel.Size = new System.Drawing.Size(33, 13);
            this.thirstLabel.TabIndex = 2;
            this.thirstLabel.Text = "Thirst";
            // 
            // Sanity
            // 
            this.Sanity.AutoSize = true;
            this.Sanity.Location = new System.Drawing.Point(370, 27);
            this.Sanity.Name = "Sanity";
            this.Sanity.Size = new System.Drawing.Size(36, 13);
            this.Sanity.TabIndex = 3;
            this.Sanity.Text = "Sanity";
            // 
            // pcGroupBox
            // 
            this.pcGroupBox.Controls.Add(this.itemCatalogue);
            this.pcGroupBox.Controls.Add(this.healthLabel);
            this.pcGroupBox.Controls.Add(this.Sanity);
            this.pcGroupBox.Controls.Add(this.inventoryListBox);
            this.pcGroupBox.Controls.Add(this.hungerLabel);
            this.pcGroupBox.Controls.Add(this.thirstLabel);
            this.pcGroupBox.Location = new System.Drawing.Point(430, 12);
            this.pcGroupBox.Name = "pcGroupBox";
            this.pcGroupBox.Size = new System.Drawing.Size(577, 394);
            this.pcGroupBox.TabIndex = 4;
            this.pcGroupBox.TabStop = false;
            this.pcGroupBox.Text = "Player Character";
            // 
            // inventoryListBox
            // 
            this.inventoryListBox.FormattingEnabled = true;
            this.inventoryListBox.Location = new System.Drawing.Point(11, 79);
            this.inventoryListBox.Name = "inventoryListBox";
            this.inventoryListBox.Size = new System.Drawing.Size(552, 134);
            this.inventoryListBox.TabIndex = 5;
            // 
            // locationGroupBox
            // 
            this.locationGroupBox.Controls.Add(this.unvisitedListBox);
            this.locationGroupBox.Controls.Add(this.visitedListBox);
            this.locationGroupBox.Controls.Add(this.currentSublocationLabel);
            this.locationGroupBox.Controls.Add(this.currentLocationLabel);
            this.locationGroupBox.Location = new System.Drawing.Point(12, 12);
            this.locationGroupBox.Name = "locationGroupBox";
            this.locationGroupBox.Size = new System.Drawing.Size(399, 624);
            this.locationGroupBox.TabIndex = 6;
            this.locationGroupBox.TabStop = false;
            this.locationGroupBox.Text = "Location";
            // 
            // currentLocationLabel
            // 
            this.currentLocationLabel.AutoSize = true;
            this.currentLocationLabel.Location = new System.Drawing.Point(12, 25);
            this.currentLocationLabel.Name = "currentLocationLabel";
            this.currentLocationLabel.Size = new System.Drawing.Size(85, 13);
            this.currentLocationLabel.TabIndex = 0;
            this.currentLocationLabel.Text = "Current Location";
            // 
            // currentSublocationLabel
            // 
            this.currentSublocationLabel.AutoSize = true;
            this.currentSublocationLabel.Location = new System.Drawing.Point(12, 48);
            this.currentSublocationLabel.Name = "currentSublocationLabel";
            this.currentSublocationLabel.Size = new System.Drawing.Size(100, 13);
            this.currentSublocationLabel.TabIndex = 7;
            this.currentSublocationLabel.Text = "Current Sublocation";
            // 
            // visitedListBox
            // 
            this.visitedListBox.FormattingEnabled = true;
            this.visitedListBox.Location = new System.Drawing.Point(15, 79);
            this.visitedListBox.Name = "visitedListBox";
            this.visitedListBox.Size = new System.Drawing.Size(365, 238);
            this.visitedListBox.TabIndex = 8;
            // 
            // unvisitedListBox
            // 
            this.unvisitedListBox.FormattingEnabled = true;
            this.unvisitedListBox.Location = new System.Drawing.Point(15, 323);
            this.unvisitedListBox.Name = "unvisitedListBox";
            this.unvisitedListBox.Size = new System.Drawing.Size(365, 251);
            this.unvisitedListBox.TabIndex = 9;
            // 
            // eventGroupBox
            // 
            this.eventGroupBox.Controls.Add(this.eventCatalogue);
            this.eventGroupBox.Controls.Add(this.usedEventsLabel);
            this.eventGroupBox.Controls.Add(this.currentEventLabel);
            this.eventGroupBox.Location = new System.Drawing.Point(430, 412);
            this.eventGroupBox.Name = "eventGroupBox";
            this.eventGroupBox.Size = new System.Drawing.Size(577, 224);
            this.eventGroupBox.TabIndex = 7;
            this.eventGroupBox.TabStop = false;
            this.eventGroupBox.Text = "Event";
            // 
            // currentEventLabel
            // 
            this.currentEventLabel.AutoSize = true;
            this.currentEventLabel.Location = new System.Drawing.Point(6, 19);
            this.currentEventLabel.Name = "currentEventLabel";
            this.currentEventLabel.Size = new System.Drawing.Size(72, 13);
            this.currentEventLabel.TabIndex = 0;
            this.currentEventLabel.Text = "Current Event";
            // 
            // usedEventsLabel
            // 
            this.usedEventsLabel.AutoSize = true;
            this.usedEventsLabel.Location = new System.Drawing.Point(6, 41);
            this.usedEventsLabel.Name = "usedEventsLabel";
            this.usedEventsLabel.Size = new System.Drawing.Size(68, 13);
            this.usedEventsLabel.TabIndex = 1;
            this.usedEventsLabel.Text = "Used Events";
            // 
            // eventCatalogue
            // 
            this.eventCatalogue.FormattingEnabled = true;
            this.eventCatalogue.Location = new System.Drawing.Point(11, 67);
            this.eventCatalogue.Name = "eventCatalogue";
            this.eventCatalogue.Size = new System.Drawing.Size(550, 108);
            this.eventCatalogue.TabIndex = 2;
            // 
            // itemCatalogue
            // 
            this.itemCatalogue.FormattingEnabled = true;
            this.itemCatalogue.Location = new System.Drawing.Point(11, 219);
            this.itemCatalogue.Name = "itemCatalogue";
            this.itemCatalogue.Size = new System.Drawing.Size(552, 134);
            this.itemCatalogue.TabIndex = 6;
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 648);
            this.Controls.Add(this.eventGroupBox);
            this.Controls.Add(this.locationGroupBox);
            this.Controls.Add(this.pcGroupBox);
            this.Name = "Debug";
            this.Text = "Debug";
            this.pcGroupBox.ResumeLayout(false);
            this.pcGroupBox.PerformLayout();
            this.locationGroupBox.ResumeLayout(false);
            this.locationGroupBox.PerformLayout();
            this.eventGroupBox.ResumeLayout(false);
            this.eventGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label healthLabel;
        private System.Windows.Forms.Label hungerLabel;
        private System.Windows.Forms.Label thirstLabel;
        private System.Windows.Forms.Label Sanity;
        private System.Windows.Forms.GroupBox pcGroupBox;
        private System.Windows.Forms.ListBox inventoryListBox;
        private System.Windows.Forms.GroupBox locationGroupBox;
        private System.Windows.Forms.ListBox unvisitedListBox;
        private System.Windows.Forms.ListBox visitedListBox;
        private System.Windows.Forms.Label currentSublocationLabel;
        private System.Windows.Forms.Label currentLocationLabel;
        private System.Windows.Forms.ListBox itemCatalogue;
        private System.Windows.Forms.GroupBox eventGroupBox;
        private System.Windows.Forms.ListBox eventCatalogue;
        private System.Windows.Forms.Label usedEventsLabel;
        private System.Windows.Forms.Label currentEventLabel;
    }
}