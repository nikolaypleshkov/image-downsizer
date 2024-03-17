namespace ImageDownsizer
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
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newImageToolStripMenuItem = new ToolStripMenuItem();
            pictureBox1 = new PictureBox();
            loadingLabel = new Label();
            label2 = new Label();
            scaleFactorNumericField = new NumericUpDown();
            downsizeImageButton = new Button();
            parallelRun = new CheckBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scaleFactorNumericField).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 34F);
            label1.Location = new Point(0, 33);
            label1.Name = "label1";
            label1.Size = new Size(376, 62);
            label1.TabIndex = 1;
            label1.Text = "Image Downsizer";
            label1.Click += label1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(801, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newImageToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newImageToolStripMenuItem
            // 
            newImageToolStripMenuItem.Name = "newImageToolStripMenuItem";
            newImageToolStripMenuItem.Size = new Size(103, 22);
            newImageToolStripMenuItem.Text = "Open";
            newImageToolStripMenuItem.Click += newImageToolStripMenuItem_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 119);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(776, 405);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // loadingLabel
            // 
            loadingLabel.AutoSize = true;
            loadingLabel.Font = new Font("Segoe UI", 12F);
            loadingLabel.Location = new Point(12, 95);
            loadingLabel.Name = "loadingLabel";
            loadingLabel.Size = new Size(75, 21);
            loadingLabel.TabIndex = 4;
            loadingLabel.Text = "Loading...";
            loadingLabel.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 540);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 5;
            label2.Text = "Downscale %";
            // 
            // scaleFactorNumericField
            // 
            scaleFactorNumericField.Location = new Point(12, 558);
            scaleFactorNumericField.Name = "scaleFactorNumericField";
            scaleFactorNumericField.Size = new Size(120, 23);
            scaleFactorNumericField.TabIndex = 6;
            scaleFactorNumericField.ValueChanged += scaleFactorNumericField_ValueChanged;
            // 
            // downsizeImageButton
            // 
            downsizeImageButton.Location = new Point(12, 615);
            downsizeImageButton.Name = "downsizeImageButton";
            downsizeImageButton.Size = new Size(75, 23);
            downsizeImageButton.TabIndex = 7;
            downsizeImageButton.Text = "Downsize";
            downsizeImageButton.UseVisualStyleBackColor = true;
            downsizeImageButton.Click += downsizeImageButton_Click;
            // 
            // parallelRun
            // 
            parallelRun.AutoSize = true;
            parallelRun.Location = new Point(12, 590);
            parallelRun.Name = "parallelRun";
            parallelRun.Size = new Size(64, 19);
            parallelRun.TabIndex = 8;
            parallelRun.Text = "Parallel";
            parallelRun.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(801, 650);
            Controls.Add(parallelRun);
            Controls.Add(downsizeImageButton);
            Controls.Add(scaleFactorNumericField);
            Controls.Add(label2);
            Controls.Add(loadingLabel);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)scaleFactorNumericField).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newImageToolStripMenuItem;
        private PictureBox pictureBox1;
        private Label loadingLabel;
        private Label label2;
        private NumericUpDown scaleFactorNumericField;
        private Button downsizeImageButton;
        private CheckBox parallelRun;
    }
}
