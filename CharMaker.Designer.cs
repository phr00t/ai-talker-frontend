namespace TalkerFrontend {
    partial class CharMaker {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharMaker));
            this.CharName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ImageFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.VisualDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PersistentDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LongTermMemory = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.VoiceWAV = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.VoiceText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.visual_style = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.raw_data = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.voiceDesc = new System.Windows.Forms.TextBox();
            this.charchatlog = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.CharPNG = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // CharName
            // 
            this.CharName.BackColor = System.Drawing.Color.Navy;
            this.CharName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CharName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CharName.ForeColor = System.Drawing.Color.White;
            this.CharName.Location = new System.Drawing.Point(50, 4);
            this.CharName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CharName.Name = "CharName";
            this.CharName.Size = new System.Drawing.Size(219, 21);
            this.CharName.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Name:";
            // 
            // ImageFile
            // 
            this.ImageFile.BackColor = System.Drawing.Color.Navy;
            this.ImageFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImageFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageFile.ForeColor = System.Drawing.Color.White;
            this.ImageFile.Location = new System.Drawing.Point(50, 26);
            this.ImageFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImageFile.Name = "ImageFile";
            this.ImageFile.Size = new System.Drawing.Size(219, 21);
            this.ImageFile.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Image:";
            // 
            // VisualDescription
            // 
            this.VisualDescription.BackColor = System.Drawing.Color.Navy;
            this.VisualDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VisualDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisualDescription.ForeColor = System.Drawing.Color.White;
            this.VisualDescription.Location = new System.Drawing.Point(11, 150);
            this.VisualDescription.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.VisualDescription.Multiline = true;
            this.VisualDescription.Name = "VisualDescription";
            this.VisualDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.VisualDescription.Size = new System.Drawing.Size(274, 73);
            this.VisualDescription.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 135);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Image Prompt/Description (for Request Picture):";
            // 
            // PersistentDesc
            // 
            this.PersistentDesc.BackColor = System.Drawing.Color.Navy;
            this.PersistentDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PersistentDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PersistentDesc.ForeColor = System.Drawing.Color.White;
            this.PersistentDesc.Location = new System.Drawing.Point(11, 241);
            this.PersistentDesc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PersistentDesc.Multiline = true;
            this.PersistentDesc.Name = "PersistentDesc";
            this.PersistentDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PersistentDesc.Size = new System.Drawing.Size(274, 201);
            this.PersistentDesc.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 224);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Persistent Description (Always Included):";
            // 
            // LongTermMemory
            // 
            this.LongTermMemory.BackColor = System.Drawing.Color.Navy;
            this.LongTermMemory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LongTermMemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LongTermMemory.ForeColor = System.Drawing.Color.White;
            this.LongTermMemory.Location = new System.Drawing.Point(289, 66);
            this.LongTermMemory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LongTermMemory.MaxLength = 2000000;
            this.LongTermMemory.Multiline = true;
            this.LongTermMemory.Name = "LongTermMemory";
            this.LongTermMemory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LongTermMemory.Size = new System.Drawing.Size(229, 379);
            this.LongTermMemory.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(289, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(226, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "Initial Long Term Memory (Recalled):";
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.Black;
            this.SaveButton.Location = new System.Drawing.Point(522, 4);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(250, 41);
            this.SaveButton.TabIndex = 24;
            this.SaveButton.Text = "SAVE";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // VoiceWAV
            // 
            this.VoiceWAV.BackColor = System.Drawing.Color.Navy;
            this.VoiceWAV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VoiceWAV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoiceWAV.ForeColor = System.Drawing.Color.White;
            this.VoiceWAV.Location = new System.Drawing.Point(84, 48);
            this.VoiceWAV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.VoiceWAV.Name = "VoiceWAV";
            this.VoiceWAV.Size = new System.Drawing.Size(201, 21);
            this.VoiceWAV.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 49);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 25;
            this.label6.Text = "Voice WAV:";
            // 
            // VoiceText
            // 
            this.VoiceText.BackColor = System.Drawing.Color.Navy;
            this.VoiceText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VoiceText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoiceText.ForeColor = System.Drawing.Color.White;
            this.VoiceText.Location = new System.Drawing.Point(75, 69);
            this.VoiceText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.VoiceText.Name = "VoiceText";
            this.VoiceText.Size = new System.Drawing.Size(210, 21);
            this.VoiceText.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 72);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 16);
            this.label7.TabIndex = 27;
            this.label7.Text = "WAV Text:";
            // 
            // visual_style
            // 
            this.visual_style.BackColor = System.Drawing.Color.Navy;
            this.visual_style.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.visual_style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visual_style.ForeColor = System.Drawing.Color.White;
            this.visual_style.Location = new System.Drawing.Point(86, 112);
            this.visual_style.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.visual_style.Name = "visual_style";
            this.visual_style.Size = new System.Drawing.Size(199, 21);
            this.visual_style.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 113);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 29;
            this.label8.Text = "Visual Style:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 92);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 16);
            this.label9.TabIndex = 30;
            this.label9.Text = "Voice Description:";
            // 
            // raw_data
            // 
            this.raw_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.raw_data.ForeColor = System.Drawing.Color.Black;
            this.raw_data.Location = new System.Drawing.Point(355, 4);
            this.raw_data.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.raw_data.Name = "raw_data";
            this.raw_data.Size = new System.Drawing.Size(73, 41);
            this.raw_data.TabIndex = 32;
            this.raw_data.Text = "Open Raw Data";
            this.raw_data.UseVisualStyleBackColor = true;
            this.raw_data.Click += new System.EventHandler(this.raw_data_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(433, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 41);
            this.button1.TabIndex = 33;
            this.button1.Text = "Reload Raw Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // voiceDesc
            // 
            this.voiceDesc.BackColor = System.Drawing.Color.Navy;
            this.voiceDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.voiceDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.voiceDesc.ForeColor = System.Drawing.Color.White;
            this.voiceDesc.Location = new System.Drawing.Point(118, 90);
            this.voiceDesc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.voiceDesc.Name = "voiceDesc";
            this.voiceDesc.Size = new System.Drawing.Size(167, 21);
            this.voiceDesc.TabIndex = 34;
            // 
            // charchatlog
            // 
            this.charchatlog.BackColor = System.Drawing.Color.Navy;
            this.charchatlog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.charchatlog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charchatlog.ForeColor = System.Drawing.Color.White;
            this.charchatlog.Location = new System.Drawing.Point(522, 65);
            this.charchatlog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.charchatlog.MaxLength = 2000000;
            this.charchatlog.Multiline = true;
            this.charchatlog.Name = "charchatlog";
            this.charchatlog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.charchatlog.Size = new System.Drawing.Size(251, 379);
            this.charchatlog.TabIndex = 36;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(519, 49);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(166, 16);
            this.label10.TabIndex = 35;
            this.label10.Text = "Personal Chat/Activity Log:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(273, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 41);
            this.button2.TabIndex = 37;
            this.button2.Text = "Import PNG/JSON";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CharPNG
            // 
            this.CharPNG.Filter = "Character Card|*.png|JSON|*.json";
            this.CharPNG.Title = "Load Character Card or JSON";
            // 
            // CharMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(780, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.charchatlog);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.voiceDesc);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.raw_data);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.visual_style);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.VoiceText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.VoiceWAV);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LongTermMemory);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PersistentDesc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.VisualDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ImageFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CharName);
            this.Controls.Add(this.label4);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "CharMaker";
            this.ShowInTaskbar = false;
            this.Text = "Char Editor";
            this.Load += new System.EventHandler(this.CharMaker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CharName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ImageFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox VisualDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PersistentDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LongTermMemory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox VoiceWAV;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox VoiceText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox visual_style;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button raw_data;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox voiceDesc;
        private System.Windows.Forms.TextBox charchatlog;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog CharPNG;
    }
}