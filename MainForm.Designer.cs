namespace TalkerFrontend {
    partial class MainForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.WhoPicture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.WhoList = new System.Windows.Forms.ComboBox();
            this.EditWho = new System.Windows.Forms.Button();
            this.AddWho = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.MyRelation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CBVoice = new System.Windows.Forms.CheckBox();
            this.CBAutoTalk = new System.Windows.Forms.CheckBox();
            this.YourPic = new System.Windows.Forms.PictureBox();
            this.PicPaste = new System.Windows.Forms.Button();
            this.PicLoad = new System.Windows.Forms.Button();
            this.PicTake = new System.Windows.Forms.Button();
            this.ResponseText = new System.Windows.Forms.TextBox();
            this.SendText = new System.Windows.Forms.TextBox();
            this.ListenButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.ReplayButton = new System.Windows.Forms.Button();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.ComfyURL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ComfyUI_Textbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.button1 = new System.Windows.Forms.Button();
            this.KoboldPY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.StatusText = new System.Windows.Forms.Label();
            this.CBCreative = new System.Windows.Forms.CheckBox();
            this.chat_tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.prtab = new System.Windows.Forms.TabPage();
            this.partial_response = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CBFillContext = new System.Windows.Forms.CheckBox();
            this.AdvWordRecall = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.AdvExtraStops = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.AdvMaxTokens = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.AdvDryBase = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.AdvDryMult = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.AdvTopP = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.AdvMinP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.AdvTemperature = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CBUseRecommended = new System.Windows.Forms.CheckBox();
            this.pickKoboldConfig = new System.Windows.Forms.OpenFileDialog();
            this.CBGroupChat = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.OpenImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.MyName = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.KoboldFinder = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.WhoPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YourPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.chat_tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.prtab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // WhoPicture
            // 
            this.WhoPicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.WhoPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WhoPicture.Location = new System.Drawing.Point(12, 12);
            this.WhoPicture.Name = "WhoPicture";
            this.WhoPicture.Size = new System.Drawing.Size(694, 656);
            this.WhoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.WhoPicture.TabIndex = 0;
            this.WhoPicture.TabStop = false;
            this.WhoPicture.Click += new System.EventHandler(this.WhoPicture_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(709, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Who are you talking with?";
            // 
            // WhoList
            // 
            this.WhoList.BackColor = System.Drawing.Color.Navy;
            this.WhoList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WhoList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WhoList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WhoList.ForeColor = System.Drawing.Color.White;
            this.WhoList.FormattingEnabled = true;
            this.WhoList.Location = new System.Drawing.Point(712, 171);
            this.WhoList.Name = "WhoList";
            this.WhoList.Size = new System.Drawing.Size(298, 28);
            this.WhoList.TabIndex = 2;
            this.WhoList.SelectedIndexChanged += new System.EventHandler(this.WhoList_SelectedIndexChanged);
            this.WhoList.SelectedValueChanged += new System.EventHandler(this.WhoList_SelectedValueChanged);
            // 
            // EditWho
            // 
            this.EditWho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.EditWho.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditWho.ForeColor = System.Drawing.Color.Black;
            this.EditWho.Location = new System.Drawing.Point(816, 205);
            this.EditWho.Name = "EditWho";
            this.EditWho.Size = new System.Drawing.Size(74, 43);
            this.EditWho.TabIndex = 3;
            this.EditWho.Text = "Edit";
            this.EditWho.UseVisualStyleBackColor = false;
            this.EditWho.Click += new System.EventHandler(this.EditWho_Click);
            // 
            // AddWho
            // 
            this.AddWho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.AddWho.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddWho.ForeColor = System.Drawing.Color.Black;
            this.AddWho.Location = new System.Drawing.Point(712, 205);
            this.AddWho.Name = "AddWho";
            this.AddWho.Size = new System.Drawing.Size(98, 42);
            this.AddWho.TabIndex = 4;
            this.AddWho.Text = "Add New";
            this.AddWho.UseVisualStyleBackColor = false;
            this.AddWho.Click += new System.EventHandler(this.AddWho_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(710, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Who are you?";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // MyRelation
            // 
            this.MyRelation.BackColor = System.Drawing.Color.Navy;
            this.MyRelation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MyRelation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MyRelation.ForeColor = System.Drawing.Color.White;
            this.MyRelation.Location = new System.Drawing.Point(712, 85);
            this.MyRelation.Multiline = true;
            this.MyRelation.Name = "MyRelation";
            this.MyRelation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MyRelation.Size = new System.Drawing.Size(299, 62);
            this.MyRelation.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(712, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Additional known details about you:";
            // 
            // CBVoice
            // 
            this.CBVoice.AutoSize = true;
            this.CBVoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBVoice.Location = new System.Drawing.Point(1025, 199);
            this.CBVoice.Name = "CBVoice";
            this.CBVoice.Size = new System.Drawing.Size(163, 24);
            this.CBVoice.TabIndex = 9;
            this.CBVoice.Text = "Auto Speak Voice";
            this.CBVoice.UseVisualStyleBackColor = true;
            // 
            // CBAutoTalk
            // 
            this.CBAutoTalk.AutoSize = true;
            this.CBAutoTalk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBAutoTalk.Location = new System.Drawing.Point(1025, 171);
            this.CBAutoTalk.Name = "CBAutoTalk";
            this.CBAutoTalk.Size = new System.Drawing.Size(181, 24);
            this.CBAutoTalk.TabIndex = 11;
            this.CBAutoTalk.Text = "Auto Trigger Talking";
            this.CBAutoTalk.UseVisualStyleBackColor = true;
            this.CBAutoTalk.CheckedChanged += new System.EventHandler(this.CBAutoTalk_CheckedChanged);
            // 
            // YourPic
            // 
            this.YourPic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.YourPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.YourPic.Location = new System.Drawing.Point(712, 257);
            this.YourPic.Name = "YourPic";
            this.YourPic.Size = new System.Drawing.Size(299, 264);
            this.YourPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.YourPic.TabIndex = 15;
            this.YourPic.TabStop = false;
            // 
            // PicPaste
            // 
            this.PicPaste.BackColor = System.Drawing.Color.Cyan;
            this.PicPaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PicPaste.ForeColor = System.Drawing.Color.Black;
            this.PicPaste.Location = new System.Drawing.Point(712, 524);
            this.PicPaste.Name = "PicPaste";
            this.PicPaste.Size = new System.Drawing.Size(149, 80);
            this.PicPaste.TabIndex = 16;
            this.PicPaste.Text = "Paste Picture";
            this.PicPaste.UseVisualStyleBackColor = false;
            this.PicPaste.Click += new System.EventHandler(this.PicPaste_Click);
            // 
            // PicLoad
            // 
            this.PicLoad.BackColor = System.Drawing.Color.Cyan;
            this.PicLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PicLoad.ForeColor = System.Drawing.Color.Black;
            this.PicLoad.Location = new System.Drawing.Point(713, 610);
            this.PicLoad.Name = "PicLoad";
            this.PicLoad.Size = new System.Drawing.Size(148, 54);
            this.PicLoad.TabIndex = 17;
            this.PicLoad.Text = "Load Picture";
            this.PicLoad.UseVisualStyleBackColor = false;
            this.PicLoad.Click += new System.EventHandler(this.PicLoad_Click);
            // 
            // PicTake
            // 
            this.PicTake.BackColor = System.Drawing.Color.Cyan;
            this.PicTake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PicTake.ForeColor = System.Drawing.Color.Black;
            this.PicTake.Location = new System.Drawing.Point(866, 524);
            this.PicTake.Name = "PicTake";
            this.PicTake.Size = new System.Drawing.Size(145, 80);
            this.PicTake.TabIndex = 18;
            this.PicTake.Text = "Take Picture";
            this.PicTake.UseVisualStyleBackColor = false;
            this.PicTake.Click += new System.EventHandler(this.PicTake_Click);
            // 
            // ResponseText
            // 
            this.ResponseText.BackColor = System.Drawing.Color.Black;
            this.ResponseText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResponseText.ForeColor = System.Drawing.Color.White;
            this.ResponseText.Location = new System.Drawing.Point(0, 1);
            this.ResponseText.MaxLength = 2000000;
            this.ResponseText.Multiline = true;
            this.ResponseText.Name = "ResponseText";
            this.ResponseText.ReadOnly = true;
            this.ResponseText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResponseText.Size = new System.Drawing.Size(465, 565);
            this.ResponseText.TabIndex = 19;
            // 
            // SendText
            // 
            this.SendText.BackColor = System.Drawing.Color.Navy;
            this.SendText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SendText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendText.ForeColor = System.Drawing.Color.White;
            this.SendText.Location = new System.Drawing.Point(168, 674);
            this.SendText.Multiline = true;
            this.SendText.Name = "SendText";
            this.SendText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SendText.Size = new System.Drawing.Size(723, 153);
            this.SendText.TabIndex = 20;
            this.SendText.TextChanged += new System.EventHandler(this.SendText_TextChanged);
            // 
            // ListenButton
            // 
            this.ListenButton.BackColor = System.Drawing.Color.Yellow;
            this.ListenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListenButton.ForeColor = System.Drawing.Color.Black;
            this.ListenButton.Location = new System.Drawing.Point(897, 674);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(114, 49);
            this.ListenButton.TabIndex = 21;
            this.ListenButton.Text = "Listen...";
            this.ListenButton.UseVisualStyleBackColor = false;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendButton.ForeColor = System.Drawing.Color.Black;
            this.SendButton.Location = new System.Drawing.Point(897, 729);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(114, 98);
            this.SendButton.TabIndex = 22;
            this.SendButton.Text = "SEND";
            this.SendButton.UseVisualStyleBackColor = false;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ReplayButton
            // 
            this.ReplayButton.BackColor = System.Drawing.Color.Yellow;
            this.ReplayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReplayButton.ForeColor = System.Drawing.Color.Black;
            this.ReplayButton.Location = new System.Drawing.Point(1377, 178);
            this.ReplayButton.Name = "ReplayButton";
            this.ReplayButton.Size = new System.Drawing.Size(120, 58);
            this.ReplayButton.TabIndex = 23;
            this.ReplayButton.Text = "Say Whole Response";
            this.ReplayButton.UseVisualStyleBackColor = false;
            this.ReplayButton.Click += new System.EventHandler(this.ReplayButton_Click);
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // ComfyURL
            // 
            this.ComfyURL.BackColor = System.Drawing.Color.Navy;
            this.ComfyURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ComfyURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComfyURL.ForeColor = System.Drawing.Color.White;
            this.ComfyURL.Location = new System.Drawing.Point(1021, 135);
            this.ComfyURL.Name = "ComfyURL";
            this.ComfyURL.Size = new System.Drawing.Size(350, 27);
            this.ComfyURL.TabIndex = 25;
            this.ComfyURL.Text = "http://127.0.0.1:8188";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1018, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(241, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "ComfyUI Backend Server URL:";
            // 
            // ComfyUI_Textbox
            // 
            this.ComfyUI_Textbox.BackColor = System.Drawing.Color.Navy;
            this.ComfyUI_Textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ComfyUI_Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComfyUI_Textbox.ForeColor = System.Drawing.Color.White;
            this.ComfyUI_Textbox.Location = new System.Drawing.Point(1021, 82);
            this.ComfyUI_Textbox.Name = "ComfyUI_Textbox";
            this.ComfyUI_Textbox.Size = new System.Drawing.Size(350, 27);
            this.ComfyUI_Textbox.TabIndex = 29;
            this.ComfyUI_Textbox.Text = "D:/ComfyUI_windows_portable/ComfyUI/";
            this.ComfyUI_Textbox.TextChanged += new System.EventHandler(this.ComfyUI_Textbox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1018, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(237, 20);
            this.label6.TabIndex = 28;
            this.label6.Text = "ComfyUI Installation Directory:";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(12, 674);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 91);
            this.button1.TabIndex = 30;
            this.button1.Text = "REQUEST PICTURE";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // KoboldPY
            // 
            this.KoboldPY.BackColor = System.Drawing.Color.Navy;
            this.KoboldPY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KoboldPY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KoboldPY.ForeColor = System.Drawing.Color.White;
            this.KoboldPY.Location = new System.Drawing.Point(1021, 34);
            this.KoboldPY.Name = "KoboldPY";
            this.KoboldPY.Size = new System.Drawing.Size(350, 27);
            this.KoboldPY.TabIndex = 32;
            this.KoboldPY.Text = "D:/KoboldCPP/koboldcpp.py";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1018, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(344, 20);
            this.label7.TabIndex = 31;
            this.label7.Text = "KoboldCpp Unpacked koboldcpp.py Location:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(1377, 75);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 45);
            this.button2.TabIndex = 33;
            this.button2.Text = "ABORT";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // StatusText
            // 
            this.StatusText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.StatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusText.ForeColor = System.Drawing.Color.Black;
            this.StatusText.Location = new System.Drawing.Point(1378, 12);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(119, 60);
            this.StatusText.TabIndex = 34;
            this.StatusText.Text = "STATUS STATUS";
            this.StatusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBCreative
            // 
            this.CBCreative.AutoSize = true;
            this.CBCreative.Checked = true;
            this.CBCreative.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBCreative.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBCreative.Location = new System.Drawing.Point(1208, 196);
            this.CBCreative.Name = "CBCreative";
            this.CBCreative.Size = new System.Drawing.Size(139, 24);
            this.CBCreative.TabIndex = 35;
            this.CBCreative.Text = "Creative Mode";
            this.CBCreative.UseVisualStyleBackColor = true;
            // 
            // chat_tabs
            // 
            this.chat_tabs.Controls.Add(this.tabPage1);
            this.chat_tabs.Controls.Add(this.prtab);
            this.chat_tabs.Controls.Add(this.tabPage2);
            this.chat_tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chat_tabs.Location = new System.Drawing.Point(1021, 230);
            this.chat_tabs.Name = "chat_tabs";
            this.chat_tabs.SelectedIndex = 0;
            this.chat_tabs.Size = new System.Drawing.Size(476, 597);
            this.chat_tabs.TabIndex = 36;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ResponseText);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(468, 564);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chat Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // prtab
            // 
            this.prtab.Controls.Add(this.partial_response);
            this.prtab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prtab.Location = new System.Drawing.Point(4, 29);
            this.prtab.Name = "prtab";
            this.prtab.Padding = new System.Windows.Forms.Padding(3);
            this.prtab.Size = new System.Drawing.Size(468, 564);
            this.prtab.TabIndex = 1;
            this.prtab.Text = "Response Monitor";
            this.prtab.UseVisualStyleBackColor = true;
            // 
            // partial_response
            // 
            this.partial_response.BackColor = System.Drawing.Color.Black;
            this.partial_response.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partial_response.ForeColor = System.Drawing.Color.White;
            this.partial_response.Location = new System.Drawing.Point(0, 1);
            this.partial_response.Multiline = true;
            this.partial_response.Name = "partial_response";
            this.partial_response.ReadOnly = true;
            this.partial_response.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.partial_response.Size = new System.Drawing.Size(465, 565);
            this.partial_response.TabIndex = 20;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.CBFillContext);
            this.tabPage2.Controls.Add(this.AdvWordRecall);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.AdvExtraStops);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.AdvMaxTokens);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.AdvDryBase);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.AdvDryMult);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.AdvTopP);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.AdvMinP);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.AdvTemperature);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.CBUseRecommended);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(468, 564);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Advanced";
            // 
            // CBFillContext
            // 
            this.CBFillContext.AutoSize = true;
            this.CBFillContext.BackColor = System.Drawing.Color.Black;
            this.CBFillContext.Checked = true;
            this.CBFillContext.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBFillContext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBFillContext.Location = new System.Drawing.Point(24, 523);
            this.CBFillContext.Name = "CBFillContext";
            this.CBFillContext.Size = new System.Drawing.Size(418, 24);
            this.CBFillContext.TabIndex = 56;
            this.CBFillContext.Text = "Maximize Context Usage (slower, better responses)";
            this.CBFillContext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CBFillContext.UseVisualStyleBackColor = false;
            // 
            // AdvWordRecall
            // 
            this.AdvWordRecall.BackColor = System.Drawing.Color.Navy;
            this.AdvWordRecall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvWordRecall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvWordRecall.ForeColor = System.Drawing.Color.White;
            this.AdvWordRecall.Location = new System.Drawing.Point(299, 469);
            this.AdvWordRecall.Name = "AdvWordRecall";
            this.AdvWordRecall.Size = new System.Drawing.Size(80, 27);
            this.AdvWordRecall.TabIndex = 55;
            this.AdvWordRecall.Text = "48";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(21, 463);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(272, 60);
            this.label14.TabIndex = 54;
            this.label14.Text = "Words Per Memory Recall:\r\n(higher = more depth, less breadth)\r\n\r\n";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AdvExtraStops
            // 
            this.AdvExtraStops.BackColor = System.Drawing.Color.Navy;
            this.AdvExtraStops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvExtraStops.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvExtraStops.ForeColor = System.Drawing.Color.White;
            this.AdvExtraStops.Location = new System.Drawing.Point(24, 385);
            this.AdvExtraStops.Multiline = true;
            this.AdvExtraStops.Name = "AdvExtraStops";
            this.AdvExtraStops.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AdvExtraStops.Size = new System.Drawing.Size(408, 67);
            this.AdvExtraStops.TabIndex = 53;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(24, 364);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(355, 20);
            this.label13.TabIndex = 52;
            this.label13.Text = "Extra Stop Tokens (Separate with | character):";
            // 
            // AdvMaxTokens
            // 
            this.AdvMaxTokens.BackColor = System.Drawing.Color.Navy;
            this.AdvMaxTokens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvMaxTokens.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvMaxTokens.ForeColor = System.Drawing.Color.White;
            this.AdvMaxTokens.Location = new System.Drawing.Point(24, 335);
            this.AdvMaxTokens.Name = "AdvMaxTokens";
            this.AdvMaxTokens.Size = new System.Drawing.Size(350, 27);
            this.AdvMaxTokens.TabIndex = 51;
            this.AdvMaxTokens.Text = "2048";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(21, 314);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(178, 20);
            this.label12.TabIndex = 50;
            this.label12.Text = "Max Generate Tokens:";
            // 
            // AdvDryBase
            // 
            this.AdvDryBase.BackColor = System.Drawing.Color.Navy;
            this.AdvDryBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvDryBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvDryBase.ForeColor = System.Drawing.Color.White;
            this.AdvDryBase.Location = new System.Drawing.Point(24, 284);
            this.AdvDryBase.Name = "AdvDryBase";
            this.AdvDryBase.Size = new System.Drawing.Size(350, 27);
            this.AdvDryBase.TabIndex = 49;
            this.AdvDryBase.Text = "1.75";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(21, 263);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 20);
            this.label11.TabIndex = 48;
            this.label11.Text = "DRY Base:";
            // 
            // AdvDryMult
            // 
            this.AdvDryMult.BackColor = System.Drawing.Color.Navy;
            this.AdvDryMult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvDryMult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvDryMult.ForeColor = System.Drawing.Color.White;
            this.AdvDryMult.Location = new System.Drawing.Point(24, 233);
            this.AdvDryMult.Name = "AdvDryMult";
            this.AdvDryMult.Size = new System.Drawing.Size(350, 27);
            this.AdvDryMult.TabIndex = 47;
            this.AdvDryMult.Text = "0.8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(21, 212);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 20);
            this.label9.TabIndex = 46;
            this.label9.Text = "DRY Multiplier:";
            // 
            // AdvTopP
            // 
            this.AdvTopP.BackColor = System.Drawing.Color.Navy;
            this.AdvTopP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvTopP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvTopP.ForeColor = System.Drawing.Color.White;
            this.AdvTopP.Location = new System.Drawing.Point(24, 185);
            this.AdvTopP.Name = "AdvTopP";
            this.AdvTopP.Size = new System.Drawing.Size(350, 27);
            this.AdvTopP.TabIndex = 45;
            this.AdvTopP.Text = "1.0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(21, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 20);
            this.label10.TabIndex = 44;
            this.label10.Text = "Top P:";
            // 
            // AdvMinP
            // 
            this.AdvMinP.BackColor = System.Drawing.Color.Navy;
            this.AdvMinP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvMinP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvMinP.ForeColor = System.Drawing.Color.White;
            this.AdvMinP.Location = new System.Drawing.Point(24, 138);
            this.AdvMinP.Name = "AdvMinP";
            this.AdvMinP.Size = new System.Drawing.Size(350, 27);
            this.AdvMinP.TabIndex = 43;
            this.AdvMinP.Text = "0.15";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(21, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 20);
            this.label8.TabIndex = 42;
            this.label8.Text = "Min P:";
            // 
            // AdvTemperature
            // 
            this.AdvTemperature.BackColor = System.Drawing.Color.Navy;
            this.AdvTemperature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvTemperature.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvTemperature.ForeColor = System.Drawing.Color.White;
            this.AdvTemperature.Location = new System.Drawing.Point(24, 90);
            this.AdvTemperature.Name = "AdvTemperature";
            this.AdvTemperature.Size = new System.Drawing.Size(350, 27);
            this.AdvTemperature.TabIndex = 41;
            this.AdvTemperature.Text = "1.25";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.134328F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 20);
            this.label4.TabIndex = 40;
            this.label4.Text = "Temperature:";
            // 
            // CBUseRecommended
            // 
            this.CBUseRecommended.AutoSize = true;
            this.CBUseRecommended.BackColor = System.Drawing.Color.Black;
            this.CBUseRecommended.Checked = true;
            this.CBUseRecommended.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBUseRecommended.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBUseRecommended.Location = new System.Drawing.Point(24, 12);
            this.CBUseRecommended.Name = "CBUseRecommended";
            this.CBUseRecommended.Size = new System.Drawing.Size(408, 44);
            this.CBUseRecommended.TabIndex = 39;
            this.CBUseRecommended.Text = "Use Recommended Settings from \"Creative Mode\"\r\n(handles Temp/MinP/TopP/DRY)\r\n";
            this.CBUseRecommended.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CBUseRecommended.UseVisualStyleBackColor = false;
            // 
            // pickKoboldConfig
            // 
            this.pickKoboldConfig.Filter = "KoboldConfig|*.kcpps";
            this.pickKoboldConfig.Title = "Open KoboldCpp Config for Backend";
            // 
            // CBGroupChat
            // 
            this.CBGroupChat.AutoSize = true;
            this.CBGroupChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBGroupChat.Location = new System.Drawing.Point(1208, 171);
            this.CBGroupChat.Name = "CBGroupChat";
            this.CBGroupChat.Size = new System.Drawing.Size(163, 24);
            this.CBGroupChat.TabIndex = 37;
            this.CBGroupChat.Text = "Group Chat Mode";
            this.CBGroupChat.UseVisualStyleBackColor = true;
            this.CBGroupChat.CheckedChanged += new System.EventHandler(this.CBGroupChat_CheckedChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(1377, 123);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 53);
            this.button3.TabIndex = 38;
            this.button3.Text = "Open Group\r\nChat Log";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // OpenImageDialog
            // 
            this.OpenImageDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;.png";
            // 
            // MyName
            // 
            this.MyName.BackColor = System.Drawing.Color.Navy;
            this.MyName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MyName.ForeColor = System.Drawing.Color.White;
            this.MyName.FormattingEnabled = true;
            this.MyName.Location = new System.Drawing.Point(713, 32);
            this.MyName.Name = "MyName";
            this.MyName.Size = new System.Drawing.Size(298, 28);
            this.MyName.TabIndex = 39;
            this.MyName.SelectedIndexChanged += new System.EventHandler(this.MyName_SelectedIndexChanged);
            this.MyName.TextChanged += new System.EventHandler(this.MyName_TextChanged);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(867, 610);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 54);
            this.button4.TabIndex = 40;
            this.button4.Text = "Picture Config";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(896, 205);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(114, 42);
            this.button5.TabIndex = 41;
            this.button5.Text = "Chars Dir";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Location = new System.Drawing.Point(12, 771);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(150, 56);
            this.button6.TabIndex = 42;
            this.button6.Text = "Image Gen Config";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // KoboldFinder
            // 
            this.KoboldFinder.Filter = "KoboldCpp.py|*.py";
            this.KoboldFinder.Title = "Locate Unpacked KoboldCpp.py";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(134F, 134F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1499, 839);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.ReplayButton);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.MyName);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.CBGroupChat);
            this.Controls.Add(this.chat_tabs);
            this.Controls.Add(this.CBCreative);
            this.Controls.Add(this.StatusText);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.KoboldPY);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ComfyUI_Textbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ComfyURL);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ListenButton);
            this.Controls.Add(this.SendText);
            this.Controls.Add(this.PicTake);
            this.Controls.Add(this.PicLoad);
            this.Controls.Add(this.PicPaste);
            this.Controls.Add(this.YourPic);
            this.Controls.Add(this.CBAutoTalk);
            this.Controls.Add(this.CBVoice);
            this.Controls.Add(this.MyRelation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AddWho);
            this.Controls.Add(this.EditWho);
            this.Controls.Add(this.WhoList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WhoPicture);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Phr00t\'s AI Talker Frontend v1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WhoPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YourPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.chat_tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.prtab.ResumeLayout(false);
            this.prtab.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox WhoPicture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox WhoList;
        private System.Windows.Forms.Button EditWho;
        private System.Windows.Forms.Button AddWho;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MyRelation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CBVoice;
        private System.Windows.Forms.CheckBox CBAutoTalk;
        private System.Windows.Forms.PictureBox YourPic;
        private System.Windows.Forms.Button PicPaste;
        private System.Windows.Forms.Button PicLoad;
        private System.Windows.Forms.Button PicTake;
        private System.Windows.Forms.TextBox ResponseText;
        private System.Windows.Forms.TextBox SendText;
        private System.Windows.Forms.Button ListenButton;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Button ReplayButton;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.TextBox ComfyURL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ComfyUI_Textbox;
        private System.Windows.Forms.Label label6;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox KoboldPY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label StatusText;
        private System.Windows.Forms.CheckBox CBCreative;
        private System.Windows.Forms.TabControl chat_tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage prtab;
        private System.Windows.Forms.TextBox partial_response;
        private System.Windows.Forms.OpenFileDialog pickKoboldConfig;
        private System.Windows.Forms.CheckBox CBGroupChat;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog OpenImageDialog;
        private System.Windows.Forms.ComboBox MyName;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox AdvTemperature;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CBUseRecommended;
        private System.Windows.Forms.TextBox AdvDryBase;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox AdvDryMult;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox AdvTopP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox AdvMinP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox AdvMaxTokens;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox AdvExtraStops;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox AdvWordRecall;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.OpenFileDialog KoboldFinder;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox CBFillContext;
    }
}

