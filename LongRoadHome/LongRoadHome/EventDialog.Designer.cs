namespace uk.ac.dundee.arpond.longRoadHome
{
    partial class EventDialog
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
            this.eventText = new System.Windows.Forms.Label();
            this.okayBtn = new System.Windows.Forms.Button();
            this.optionSelectionBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // eventText
            // 
            this.eventText.AutoSize = true;
            this.eventText.Location = new System.Drawing.Point(12, 9);
            this.eventText.Name = "eventText";
            this.eventText.Size = new System.Drawing.Size(59, 13);
            this.eventText.TabIndex = 0;
            this.eventText.Text = "Event Text";
            // 
            // okayBtn
            // 
            this.okayBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okayBtn.Location = new System.Drawing.Point(178, 226);
            this.okayBtn.Name = "okayBtn";
            this.okayBtn.Size = new System.Drawing.Size(75, 23);
            this.okayBtn.TabIndex = 1;
            this.okayBtn.Text = "Okay";
            this.okayBtn.UseVisualStyleBackColor = true;
            // 
            // optionSelectionBox
            // 
            this.optionSelectionBox.FormattingEnabled = true;
            this.optionSelectionBox.Location = new System.Drawing.Point(51, 226);
            this.optionSelectionBox.Name = "optionSelectionBox";
            this.optionSelectionBox.Size = new System.Drawing.Size(121, 21);
            this.optionSelectionBox.TabIndex = 2;
            // 
            // EventDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.optionSelectionBox);
            this.Controls.Add(this.okayBtn);
            this.Controls.Add(this.eventText);
            this.Name = "EventDialog";
            this.Text = "EventDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label eventText;
        private System.Windows.Forms.Button okayBtn;
        private System.Windows.Forms.ComboBox optionSelectionBox;
    }
}