namespace TalkerFrontend {
    partial class ImageGenOptions {
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
            this.Workflow = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Model = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Steps = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.KillKobold = new System.Windows.Forms.CheckBox();
            this.Negative = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Resolution = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LocationWeight = new System.Windows.Forms.CheckBox();
            this.autogen = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Workflow
            // 
            this.Workflow.BackColor = System.Drawing.Color.Navy;
            this.Workflow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Workflow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Workflow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Workflow.ForeColor = System.Drawing.Color.White;
            this.Workflow.FormattingEnabled = true;
            this.Workflow.Location = new System.Drawing.Point(15, 32);
            this.Workflow.Name = "Workflow";
            this.Workflow.Size = new System.Drawing.Size(448, 28);
            this.Workflow.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(302, 20);
            this.label2.TabIndex = 40;
            this.label2.Text = "Workflow to Use (ImageGen*.json files)";
            // 
            // Model
            // 
            this.Model.BackColor = System.Drawing.Color.Navy;
            this.Model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Model.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Model.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Model.ForeColor = System.Drawing.Color.White;
            this.Model.FormattingEnabled = true;
            this.Model.Location = new System.Drawing.Point(15, 86);
            this.Model.Name = "Model";
            this.Model.Size = new System.Drawing.Size(448, 28);
            this.Model.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 42;
            this.label1.Text = "Model to Use";
            // 
            // Steps
            // 
            this.Steps.BackColor = System.Drawing.Color.Navy;
            this.Steps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Steps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Steps.ForeColor = System.Drawing.Color.White;
            this.Steps.Location = new System.Drawing.Point(157, 126);
            this.Steps.Name = "Steps";
            this.Steps.Size = new System.Drawing.Size(85, 26);
            this.Steps.TabIndex = 45;
            this.Steps.Text = "16";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 20);
            this.label7.TabIndex = 44;
            this.label7.Text = "Generation Steps";
            // 
            // KillKobold
            // 
            this.KillKobold.AutoSize = true;
            this.KillKobold.Checked = true;
            this.KillKobold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KillKobold.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KillKobold.Location = new System.Drawing.Point(12, 158);
            this.KillKobold.Name = "KillKobold";
            this.KillKobold.Size = new System.Drawing.Size(403, 24);
            this.KillKobold.TabIndex = 46;
            this.KillKobold.Text = "Kill KoboldCpp During Generation (VRAM freeing)";
            this.KillKobold.UseVisualStyleBackColor = true;
            // 
            // Negative
            // 
            this.Negative.BackColor = System.Drawing.Color.Navy;
            this.Negative.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Negative.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Negative.ForeColor = System.Drawing.Color.White;
            this.Negative.Location = new System.Drawing.Point(15, 217);
            this.Negative.Multiline = true;
            this.Negative.Name = "Negative";
            this.Negative.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Negative.Size = new System.Drawing.Size(448, 105);
            this.Negative.TabIndex = 48;
            this.Negative.Text = "disfigured, gross, bad quality, bad hands, blurry, deformed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 20);
            this.label3.TabIndex = 47;
            this.label3.Text = "Negative Prompt";
            // 
            // Resolution
            // 
            this.Resolution.BackColor = System.Drawing.Color.Navy;
            this.Resolution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Resolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resolution.ForeColor = System.Drawing.Color.White;
            this.Resolution.Location = new System.Drawing.Point(106, 334);
            this.Resolution.Name = "Resolution";
            this.Resolution.Size = new System.Drawing.Size(123, 26);
            this.Resolution.TabIndex = 50;
            this.Resolution.Text = "896x896";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 336);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 49;
            this.label4.Text = "Resolution";
            // 
            // LocationWeight
            // 
            this.LocationWeight.AutoSize = true;
            this.LocationWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationWeight.Location = new System.Drawing.Point(12, 366);
            this.LocationWeight.Name = "LocationWeight";
            this.LocationWeight.Size = new System.Drawing.Size(452, 24);
            this.LocationWeight.TabIndex = 51;
            this.LocationWeight.Text = "SDXL Prompt Mode (Reduce Location Weight/Truncate)";
            this.LocationWeight.UseVisualStyleBackColor = true;
            // 
            // autogen
            // 
            this.autogen.BackColor = System.Drawing.Color.Navy;
            this.autogen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.autogen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.autogen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autogen.ForeColor = System.Drawing.Color.White;
            this.autogen.FormattingEnabled = true;
            this.autogen.Items.AddRange(new object[] {
            "Manual Request Only",
            "Automate 1 Per Response",
            "Continuous Generation"});
            this.autogen.Location = new System.Drawing.Point(12, 423);
            this.autogen.Name = "autogen";
            this.autogen.Size = new System.Drawing.Size(448, 28);
            this.autogen.TabIndex = 53;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 400);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(230, 20);
            this.label5.TabIndex = 52;
            this.label5.Text = "Automatic Image Generation?";
            // 
            // ImageGenOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(474, 473);
            this.Controls.Add(this.autogen);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LocationWeight);
            this.Controls.Add(this.Resolution);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Negative);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.KillKobold);
            this.Controls.Add(this.Steps);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Model);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Workflow);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ImageGenOptions";
            this.Text = "Image Generation Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageGenOptions_FormClosing);
            this.Load += new System.EventHandler(this.ImageGenOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Workflow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Model;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Steps;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox KillKobold;
        private System.Windows.Forms.TextBox Negative;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Resolution;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox LocationWeight;
        private System.Windows.Forms.ComboBox autogen;
        private System.Windows.Forms.Label label5;
    }
}