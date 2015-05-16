namespace uk.ac.dundee.arpond.longRoadHome.View
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
            this.sanityLabel = new System.Windows.Forms.Label();
            this.pcGroupBox = new System.Windows.Forms.GroupBox();
            this.itemCatalogue = new System.Windows.Forms.ListBox();
            this.inventoryListBox = new System.Windows.Forms.ListBox();
            this.locationGroupBox = new System.Windows.Forms.GroupBox();
            this.sublocTextBox = new System.Windows.Forms.TextBox();
            this.changeSubBtn = new System.Windows.Forms.Button();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.changeLocationBtn = new System.Windows.Forms.Button();
            this.sublocationsAvailLabel = new System.Windows.Forms.Label();
            this.currentConnectionsLabel = new System.Windows.Forms.Label();
            this.unvisitedListBox = new System.Windows.Forms.ListBox();
            this.visitedListBox = new System.Windows.Forms.ListBox();
            this.currentSublocationLabel = new System.Windows.Forms.Label();
            this.currentLocationLabel = new System.Windows.Forms.Label();
            this.eventGroupBox = new System.Windows.Forms.GroupBox();
            this.drawEventBtn = new System.Windows.Forms.Button();
            this.eventCatalogue = new System.Windows.Forms.ListBox();
            this.usedEventsLabel = new System.Windows.Forms.Label();
            this.currentEventLabel = new System.Windows.Forms.Label();
            this.startNewGameBtn = new System.Windows.Forms.Button();
            this.loadGameBtn = new System.Windows.Forms.Button();
            this.dcGroupBox = new System.Windows.Forms.GroupBox();
            this.endLocLabel = new System.Windows.Forms.Label();
            this.eventModifierLabel = new System.Windows.Forms.Label();
            this.playerStatusLabel = new System.Windows.Forms.Label();
            this.eventChanceLabel = new System.Windows.Forms.Label();
            this.trackerListBox = new System.Windows.Forms.ListBox();
            this.bestFitLine = new System.Windows.Forms.ListBox();
            this.pcGroupBox.SuspendLayout();
            this.locationGroupBox.SuspendLayout();
            this.eventGroupBox.SuspendLayout();
            this.dcGroupBox.SuspendLayout();
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
            // sanityLabel
            // 
            this.sanityLabel.AutoSize = true;
            this.sanityLabel.Location = new System.Drawing.Point(370, 27);
            this.sanityLabel.Name = "sanityLabel";
            this.sanityLabel.Size = new System.Drawing.Size(36, 13);
            this.sanityLabel.TabIndex = 3;
            this.sanityLabel.Text = "Sanity";
            // 
            // pcGroupBox
            // 
            this.pcGroupBox.Controls.Add(this.itemCatalogue);
            this.pcGroupBox.Controls.Add(this.healthLabel);
            this.pcGroupBox.Controls.Add(this.sanityLabel);
            this.pcGroupBox.Controls.Add(this.inventoryListBox);
            this.pcGroupBox.Controls.Add(this.hungerLabel);
            this.pcGroupBox.Controls.Add(this.thirstLabel);
            this.pcGroupBox.Location = new System.Drawing.Point(430, 74);
            this.pcGroupBox.Name = "pcGroupBox";
            this.pcGroupBox.Size = new System.Drawing.Size(577, 394);
            this.pcGroupBox.TabIndex = 4;
            this.pcGroupBox.TabStop = false;
            this.pcGroupBox.Text = "Player Character";
            // 
            // itemCatalogue
            // 
            this.itemCatalogue.FormattingEnabled = true;
            this.itemCatalogue.Location = new System.Drawing.Point(11, 219);
            this.itemCatalogue.Name = "itemCatalogue";
            this.itemCatalogue.Size = new System.Drawing.Size(552, 134);
            this.itemCatalogue.TabIndex = 6;
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
            this.locationGroupBox.Controls.Add(this.sublocTextBox);
            this.locationGroupBox.Controls.Add(this.changeSubBtn);
            this.locationGroupBox.Controls.Add(this.locationTextBox);
            this.locationGroupBox.Controls.Add(this.changeLocationBtn);
            this.locationGroupBox.Controls.Add(this.sublocationsAvailLabel);
            this.locationGroupBox.Controls.Add(this.currentConnectionsLabel);
            this.locationGroupBox.Controls.Add(this.unvisitedListBox);
            this.locationGroupBox.Controls.Add(this.visitedListBox);
            this.locationGroupBox.Controls.Add(this.currentSublocationLabel);
            this.locationGroupBox.Controls.Add(this.currentLocationLabel);
            this.locationGroupBox.Location = new System.Drawing.Point(12, 74);
            this.locationGroupBox.Name = "locationGroupBox";
            this.locationGroupBox.Size = new System.Drawing.Size(399, 728);
            this.locationGroupBox.TabIndex = 6;
            this.locationGroupBox.TabStop = false;
            this.locationGroupBox.Text = "Location";
            // 
            // sublocTextBox
            // 
            this.sublocTextBox.Location = new System.Drawing.Point(15, 675);
            this.sublocTextBox.Name = "sublocTextBox";
            this.sublocTextBox.Size = new System.Drawing.Size(60, 20);
            this.sublocTextBox.TabIndex = 14;
            // 
            // changeSubBtn
            // 
            this.changeSubBtn.Location = new System.Drawing.Point(81, 671);
            this.changeSubBtn.Name = "changeSubBtn";
            this.changeSubBtn.Size = new System.Drawing.Size(123, 27);
            this.changeSubBtn.TabIndex = 13;
            this.changeSubBtn.Text = "Change Sublocation";
            this.changeSubBtn.UseVisualStyleBackColor = true;
            this.changeSubBtn.Click += new System.EventHandler(this.changeSubBtn_Click);
            // 
            // locationTextBox
            // 
            this.locationTextBox.Location = new System.Drawing.Point(15, 642);
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.Size = new System.Drawing.Size(60, 20);
            this.locationTextBox.TabIndex = 12;
            // 
            // changeLocationBtn
            // 
            this.changeLocationBtn.Location = new System.Drawing.Point(81, 638);
            this.changeLocationBtn.Name = "changeLocationBtn";
            this.changeLocationBtn.Size = new System.Drawing.Size(123, 27);
            this.changeLocationBtn.TabIndex = 10;
            this.changeLocationBtn.Text = "Change Location";
            this.changeLocationBtn.UseVisualStyleBackColor = true;
            this.changeLocationBtn.Click += new System.EventHandler(this.changeLocationBtn_Click);
            // 
            // sublocationsAvailLabel
            // 
            this.sublocationsAvailLabel.AutoSize = true;
            this.sublocationsAvailLabel.Location = new System.Drawing.Point(12, 91);
            this.sublocationsAvailLabel.Name = "sublocationsAvailLabel";
            this.sublocationsAvailLabel.Size = new System.Drawing.Size(114, 13);
            this.sublocationsAvailLabel.TabIndex = 11;
            this.sublocationsAvailLabel.Text = "Sublocations Available";
            // 
            // currentConnectionsLabel
            // 
            this.currentConnectionsLabel.AutoSize = true;
            this.currentConnectionsLabel.Location = new System.Drawing.Point(12, 47);
            this.currentConnectionsLabel.Name = "currentConnectionsLabel";
            this.currentConnectionsLabel.Size = new System.Drawing.Size(103, 13);
            this.currentConnectionsLabel.TabIndex = 10;
            this.currentConnectionsLabel.Text = "Current Connections";
            // 
            // unvisitedListBox
            // 
            this.unvisitedListBox.FormattingEnabled = true;
            this.unvisitedListBox.Location = new System.Drawing.Point(15, 373);
            this.unvisitedListBox.Name = "unvisitedListBox";
            this.unvisitedListBox.Size = new System.Drawing.Size(365, 251);
            this.unvisitedListBox.TabIndex = 9;
            // 
            // visitedListBox
            // 
            this.visitedListBox.FormattingEnabled = true;
            this.visitedListBox.Location = new System.Drawing.Point(15, 129);
            this.visitedListBox.Name = "visitedListBox";
            this.visitedListBox.Size = new System.Drawing.Size(365, 238);
            this.visitedListBox.TabIndex = 8;
            // 
            // currentSublocationLabel
            // 
            this.currentSublocationLabel.AutoSize = true;
            this.currentSublocationLabel.Location = new System.Drawing.Point(12, 69);
            this.currentSublocationLabel.Name = "currentSublocationLabel";
            this.currentSublocationLabel.Size = new System.Drawing.Size(100, 13);
            this.currentSublocationLabel.TabIndex = 7;
            this.currentSublocationLabel.Text = "Current Sublocation";
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
            // eventGroupBox
            // 
            this.eventGroupBox.Controls.Add(this.drawEventBtn);
            this.eventGroupBox.Controls.Add(this.eventCatalogue);
            this.eventGroupBox.Controls.Add(this.usedEventsLabel);
            this.eventGroupBox.Controls.Add(this.currentEventLabel);
            this.eventGroupBox.Location = new System.Drawing.Point(430, 474);
            this.eventGroupBox.Name = "eventGroupBox";
            this.eventGroupBox.Size = new System.Drawing.Size(577, 224);
            this.eventGroupBox.TabIndex = 7;
            this.eventGroupBox.TabStop = false;
            this.eventGroupBox.Text = "Event";
            // 
            // drawEventBtn
            // 
            this.drawEventBtn.Location = new System.Drawing.Point(11, 195);
            this.drawEventBtn.Name = "drawEventBtn";
            this.drawEventBtn.Size = new System.Drawing.Size(123, 23);
            this.drawEventBtn.TabIndex = 10;
            this.drawEventBtn.Text = "Draw Event";
            this.drawEventBtn.UseVisualStyleBackColor = true;
            this.drawEventBtn.Click += new System.EventHandler(this.drawEventBtn_Click);
            // 
            // eventCatalogue
            // 
            this.eventCatalogue.FormattingEnabled = true;
            this.eventCatalogue.Location = new System.Drawing.Point(11, 67);
            this.eventCatalogue.Name = "eventCatalogue";
            this.eventCatalogue.Size = new System.Drawing.Size(550, 108);
            this.eventCatalogue.TabIndex = 2;
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
            // currentEventLabel
            // 
            this.currentEventLabel.AutoSize = true;
            this.currentEventLabel.Location = new System.Drawing.Point(6, 19);
            this.currentEventLabel.Name = "currentEventLabel";
            this.currentEventLabel.Size = new System.Drawing.Size(72, 13);
            this.currentEventLabel.TabIndex = 0;
            this.currentEventLabel.Text = "Current Event";
            // 
            // startNewGameBtn
            // 
            this.startNewGameBtn.Location = new System.Drawing.Point(12, 27);
            this.startNewGameBtn.Name = "startNewGameBtn";
            this.startNewGameBtn.Size = new System.Drawing.Size(123, 23);
            this.startNewGameBtn.TabIndex = 8;
            this.startNewGameBtn.Text = "Start New Game";
            this.startNewGameBtn.UseVisualStyleBackColor = true;
            this.startNewGameBtn.Click += new System.EventHandler(this.startNewGameBtn_Click);
            // 
            // loadGameBtn
            // 
            this.loadGameBtn.Location = new System.Drawing.Point(171, 27);
            this.loadGameBtn.Name = "loadGameBtn";
            this.loadGameBtn.Size = new System.Drawing.Size(123, 23);
            this.loadGameBtn.TabIndex = 9;
            this.loadGameBtn.Text = "Load Game";
            this.loadGameBtn.UseVisualStyleBackColor = true;
            this.loadGameBtn.Click += new System.EventHandler(this.loadGameBtn_Click);
            // 
            // dcGroupBox
            // 
            this.dcGroupBox.Controls.Add(this.bestFitLine);
            this.dcGroupBox.Controls.Add(this.trackerListBox);
            this.dcGroupBox.Controls.Add(this.eventChanceLabel);
            this.dcGroupBox.Controls.Add(this.playerStatusLabel);
            this.dcGroupBox.Controls.Add(this.eventModifierLabel);
            this.dcGroupBox.Controls.Add(this.endLocLabel);
            this.dcGroupBox.Location = new System.Drawing.Point(1013, 74);
            this.dcGroupBox.Name = "dcGroupBox";
            this.dcGroupBox.Size = new System.Drawing.Size(169, 394);
            this.dcGroupBox.TabIndex = 10;
            this.dcGroupBox.TabStop = false;
            this.dcGroupBox.Text = "Difficulty Controller";
            // 
            // endLocLabel
            // 
            this.endLocLabel.AutoSize = true;
            this.endLocLabel.Location = new System.Drawing.Point(7, 79);
            this.endLocLabel.Name = "endLocLabel";
            this.endLocLabel.Size = new System.Drawing.Size(110, 13);
            this.endLocLabel.TabIndex = 0;
            this.endLocLabel.Text = "End Location Chance";
            // 
            // eventModifierLabel
            // 
            this.eventModifierLabel.AutoSize = true;
            this.eventModifierLabel.Location = new System.Drawing.Point(6, 56);
            this.eventModifierLabel.Name = "eventModifierLabel";
            this.eventModifierLabel.Size = new System.Drawing.Size(75, 13);
            this.eventModifierLabel.TabIndex = 1;
            this.eventModifierLabel.Text = "Event Modifier";
            // 
            // playerStatusLabel
            // 
            this.playerStatusLabel.AutoSize = true;
            this.playerStatusLabel.Location = new System.Drawing.Point(6, 27);
            this.playerStatusLabel.Name = "playerStatusLabel";
            this.playerStatusLabel.Size = new System.Drawing.Size(69, 13);
            this.playerStatusLabel.TabIndex = 2;
            this.playerStatusLabel.Text = "Player Status";
            // 
            // eventChanceLabel
            // 
            this.eventChanceLabel.AutoSize = true;
            this.eventChanceLabel.Location = new System.Drawing.Point(7, 104);
            this.eventChanceLabel.Name = "eventChanceLabel";
            this.eventChanceLabel.Size = new System.Drawing.Size(75, 13);
            this.eventChanceLabel.TabIndex = 4;
            this.eventChanceLabel.Text = "Event Chance";
            // 
            // trackerListBox
            // 
            this.trackerListBox.FormattingEnabled = true;
            this.trackerListBox.Items.AddRange(new object[] {
            "This should be replaced"});
            this.trackerListBox.Location = new System.Drawing.Point(12, 129);
            this.trackerListBox.Name = "trackerListBox";
            this.trackerListBox.Size = new System.Drawing.Size(144, 121);
            this.trackerListBox.TabIndex = 5;
            // 
            // bestFitLine
            // 
            this.bestFitLine.FormattingEnabled = true;
            this.bestFitLine.Location = new System.Drawing.Point(12, 267);
            this.bestFitLine.Name = "bestFitLine";
            this.bestFitLine.Size = new System.Drawing.Size(144, 121);
            this.bestFitLine.TabIndex = 6;
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 837);
            this.Controls.Add(this.dcGroupBox);
            this.Controls.Add(this.loadGameBtn);
            this.Controls.Add(this.startNewGameBtn);
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
            this.dcGroupBox.ResumeLayout(false);
            this.dcGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label healthLabel;
        private System.Windows.Forms.Label hungerLabel;
        private System.Windows.Forms.Label thirstLabel;
        private System.Windows.Forms.Label sanityLabel;
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
        private System.Windows.Forms.Button startNewGameBtn;
        private System.Windows.Forms.Button loadGameBtn;
        private System.Windows.Forms.Label sublocationsAvailLabel;
        private System.Windows.Forms.Label currentConnectionsLabel;
        private System.Windows.Forms.TextBox sublocTextBox;
        private System.Windows.Forms.Button changeSubBtn;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.Button changeLocationBtn;
        private System.Windows.Forms.Button drawEventBtn;
        private System.Windows.Forms.GroupBox dcGroupBox;
        private System.Windows.Forms.Label endLocLabel;
        private System.Windows.Forms.Label playerStatusLabel;
        private System.Windows.Forms.Label eventModifierLabel;
        private System.Windows.Forms.ListBox bestFitLine;
        private System.Windows.Forms.ListBox trackerListBox;
        private System.Windows.Forms.Label eventChanceLabel;
    }
}