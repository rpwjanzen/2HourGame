namespace TextureAssistant
{
    partial class MainForm
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
            this.TextBoxContentFolderLocation = new System.Windows.Forms.TextBox();
            this.RichTextBoxTextureCode = new System.Windows.Forms.RichTextBox();
            this.ButtonGenerateTextureCode = new System.Windows.Forms.Button();
            this.RichTextBoxLoads = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // TextBoxContentFolderLocation
            // 
            this.TextBoxContentFolderLocation.Location = new System.Drawing.Point(12, 12);
            this.TextBoxContentFolderLocation.Name = "TextBoxContentFolderLocation";
            this.TextBoxContentFolderLocation.Size = new System.Drawing.Size(466, 20);
            this.TextBoxContentFolderLocation.TabIndex = 0;
            this.TextBoxContentFolderLocation.Text = "C:\\Users\\Ian\\github\\2HourGame\\2HourGame\\Content\\";
            // 
            // RichTextBoxTextureCode
            // 
            this.RichTextBoxTextureCode.Location = new System.Drawing.Point(12, 38);
            this.RichTextBoxTextureCode.Name = "RichTextBoxTextureCode";
            this.RichTextBoxTextureCode.Size = new System.Drawing.Size(199, 495);
            this.RichTextBoxTextureCode.TabIndex = 1;
            this.RichTextBoxTextureCode.Text = "";
            // 
            // ButtonGenerateTextureCode
            // 
            this.ButtonGenerateTextureCode.Location = new System.Drawing.Point(485, 8);
            this.ButtonGenerateTextureCode.Name = "ButtonGenerateTextureCode";
            this.ButtonGenerateTextureCode.Size = new System.Drawing.Size(75, 23);
            this.ButtonGenerateTextureCode.TabIndex = 3;
            this.ButtonGenerateTextureCode.Text = "Generate";
            this.ButtonGenerateTextureCode.UseVisualStyleBackColor = true;
            this.ButtonGenerateTextureCode.Click += new System.EventHandler(this.GenerateTextureCode_Click);
            // 
            // RichTextBoxLoads
            // 
            this.RichTextBoxLoads.Location = new System.Drawing.Point(217, 38);
            this.RichTextBoxLoads.Name = "RichTextBoxLoads";
            this.RichTextBoxLoads.Size = new System.Drawing.Size(341, 495);
            this.RichTextBoxLoads.TabIndex = 4;
            this.RichTextBoxLoads.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 545);
            this.Controls.Add(this.RichTextBoxLoads);
            this.Controls.Add(this.ButtonGenerateTextureCode);
            this.Controls.Add(this.RichTextBoxTextureCode);
            this.Controls.Add(this.TextBoxContentFolderLocation);
            this.Name = "MainForm";
            this.Text = "Texture Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxContentFolderLocation;
        private System.Windows.Forms.RichTextBox RichTextBoxTextureCode;
        private System.Windows.Forms.Button ButtonGenerateTextureCode;
        private System.Windows.Forms.RichTextBox RichTextBoxLoads;
    }
}

