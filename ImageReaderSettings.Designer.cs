namespace TalkerFrontend {
    partial class ImageReaderSettings {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageReaderSettings));
            this.imagemode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.koboldvisualmodel = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.maxres = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.imageprompt = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lastImage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imagemode
            // 
            this.imagemode.BackColor = System.Drawing.Color.Navy;
            this.imagemode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imagemode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.imagemode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imagemode.ForeColor = System.Drawing.Color.White;
            this.imagemode.FormattingEnabled = true;
            this.imagemode.Items.AddRange(new object[] {
            "Use Same Model as Text",
            "Load Specific Visual Model"});
            this.imagemode.Location = new System.Drawing.Point(147, 5);
            this.imagemode.Name = "imagemode";
            this.imagemode.Size = new System.Drawing.Size(232, 28);
            this.imagemode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Submit Mode:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(314, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "KoboldCpp Specific Visual Model Config:";
            // 
            // koboldvisualmodel
            // 
            this.koboldvisualmodel.BackColor = System.Drawing.Color.Navy;
            this.koboldvisualmodel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.koboldvisualmodel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.koboldvisualmodel.ForeColor = System.Drawing.Color.White;
            this.koboldvisualmodel.Location = new System.Drawing.Point(12, 64);
            this.koboldvisualmodel.Name = "koboldvisualmodel";
            this.koboldvisualmodel.Size = new System.Drawing.Size(367, 26);
            this.koboldvisualmodel.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(239, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 36);
            this.button1.TabIndex = 4;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Max Image Size:";
            // 
            // maxres
            // 
            this.maxres.BackColor = System.Drawing.Color.Navy;
            this.maxres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maxres.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxres.ForeColor = System.Drawing.Color.White;
            this.maxres.Location = new System.Drawing.Point(147, 136);
            this.maxres.Name = "maxres";
            this.maxres.Size = new System.Drawing.Size(232, 26);
            this.maxres.TabIndex = 6;
            this.maxres.Text = "896";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(304, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Prompt for using Specific Visual Model:";
            // 
            // imageprompt
            // 
            this.imageprompt.BackColor = System.Drawing.Color.Navy;
            this.imageprompt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageprompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageprompt.ForeColor = System.Drawing.Color.White;
            this.imageprompt.Location = new System.Drawing.Point(12, 189);
            this.imageprompt.Multiline = true;
            this.imageprompt.Name = "imageprompt";
            this.imageprompt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.imageprompt.Size = new System.Drawing.Size(367, 77);
            this.imageprompt.TabIndex = 8;
            this.imageprompt.Text = "Describe the image in complete detail. What is in the image, what is happening, a" +
    "nd what details may be discerned from the image?";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "KoboldCpp Config|*.kcpps";
            // 
            // lastImage
            // 
            this.lastImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lastImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lastImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastImage.ForeColor = System.Drawing.Color.White;
            this.lastImage.Location = new System.Drawing.Point(9, 291);
            this.lastImage.Multiline = true;
            this.lastImage.Name = "lastImage";
            this.lastImage.ReadOnly = true;
            this.lastImage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lastImage.Size = new System.Drawing.Size(372, 97);
            this.lastImage.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 269);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Last Image Response:";
            // 
            // ImageReaderSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(134F, 134F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(393, 400);
            this.Controls.Add(this.lastImage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.imageprompt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maxres);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.koboldvisualmodel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imagemode);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ImageReaderSettings";
            this.ShowInTaskbar = false;
            this.Text = "Image Input Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageReaderSettings_FormClosing);
            this.Load += new System.EventHandler(this.ImageReaderSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox imagemode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox koboldvisualmodel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox maxres;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox imageprompt;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox lastImage;
        private System.Windows.Forms.Label label5;
    }
}