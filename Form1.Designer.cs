using System.Windows.Forms;

namespace SubTaskCreator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ticketId = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            projectFolderPath = new TextBox();
            releaseVersion = new TextBox();
            label4 = new Label();
            logTextBox = new RichTextBox();
            qaListBox = new ListBox();
            SuspendLayout();
            // 
            // ticketId
            // 
            ticketId.Location = new Point(140, 25);
            ticketId.Name = "ticketId";
            ticketId.Size = new Size(222, 23);
            ticketId.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 33);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 1;
            label1.Text = "Parent Ticket Id";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 70);
            label2.Name = "label2";
            label2.Size = new Size(107, 15);
            label2.TabIndex = 3;
            label2.Text = "Project Folder Path";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 171);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 5;
            label3.Text = "QA List";
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(33, 325);
            button1.Name = "button1";
            button1.Size = new Size(329, 29);
            button1.TabIndex = 7;
            button1.Text = "Submit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // projectFolderPath
            // 
            projectFolderPath.Location = new Point(140, 67);
            projectFolderPath.Name = "projectFolderPath";
            projectFolderPath.Size = new Size(222, 23);
            projectFolderPath.TabIndex = 8;
            // 
            // releaseVersion
            // 
            releaseVersion.Location = new Point(140, 112);
            releaseVersion.Name = "releaseVersion";
            releaseVersion.Size = new Size(222, 23);
            releaseVersion.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 115);
            label4.Name = "label4";
            label4.Size = new Size(87, 15);
            label4.TabIndex = 10;
            label4.Text = "Release Version";
            // 
            // logTextBox
            // 
            logTextBox.Location = new Point(34, 382);
            logTextBox.Name = "logTextBox";
            logTextBox.Size = new Size(589, 292);
            logTextBox.TabIndex = 11;
            logTextBox.Text = "";
            // 
            // qaListBox
            // 
            qaListBox.FormattingEnabled = true;
            qaListBox.ItemHeight = 15;
            qaListBox.Location = new Point(140, 171);
            qaListBox.Name = "qaListBox";
            qaListBox.Size = new Size(222, 139);
            qaListBox.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(635, 686);
            Controls.Add(qaListBox);
            Controls.Add(logTextBox);
            Controls.Add(label4);
            Controls.Add(releaseVersion);
            Controls.Add(projectFolderPath);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ticketId);
            Name = "Form1";
            Text = "Release Test Creator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ticketId;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private TextBox projectFolderPath;
        private TextBox releaseVersion;
        private Label label4;
        private RichTextBox logTextBox;
        private ListBox qaListBox;
    }
}
