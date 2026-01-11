using CSCore;
using CSCore.Codecs.WAV;
using CSCore.SoundIn;
using CSCore.SoundOut;
using CSCore.Streams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCam;

namespace TalkerFrontend {
    public partial class MainForm : Form {
        public MainForm() {
            Integration.MainForm = this;
            InitializeComponent();
            CharMaker.instance = new CharMaker();
        }

        public static Random Random = new Random();
        public Dictionary<string, Control> AllControls;
        public static string OptionsFile => Path.Combine(Integration.BaseDirectory, "options.txt");

        public int WordsPerRecall {
            get {
                string wprstr = AdvWordRecall.Text;
                if (int.TryParse(wprstr, out int res)) {
                    if (res < 8) return 8;
                    if (res > 512) return 512;
                    return res;
                }
                return 48;
            }
        }

        public void SetMonitor(string text) {
            int len = text.Length - partial_response.Text.Length;
            if (len > 0) {
                int new_text_start_pos = text.Length - len;
                string new_text = text.Substring(new_text_start_pos);
                partial_response.AppendText(new_text);
                if (new_text.Contains(".") ||
                    new_text.Contains("!") ||
                    new_text.Contains("?") ||
                    new_text.Contains("\n"))
                    ChatManager.UpdateLiveVoice(partial_response.Text, false);
            }
        }

        public void SetMonitorExact(string text) {
            partial_response.Text = text;
            ChatManager.UpdateLiveVoice(text, true);    
        }

        public void SelectNextCharacter() {
            int char_count = WhoList.Items.Count;
            if (char_count > 1) {
                int next_option = (WhoList.SelectedIndex + 1) % char_count;
                if (next_option < 0)
                    next_option = char_count - 1;
                if (next_option > char_count - 1)
                    next_option = 0;
                WhoList.SelectedIndex = next_option;
            }
        }

        public static string ShowDialog(string text, string caption, string start_text = "", string helper_text = "") {
            Form prompt = new Form() {
                Width = 500,
                Height = 300,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 10, Top = 20, Text = text + helper_text, AutoSize = false, Width = 480, Height = 100 };
            TextBox textBox = new TextBox() { Left = 10, Top = 150, Width = 450, Text = start_text };
            Button confirmation = new Button() { Text = "OK", Left = 200, Width = 100, Top = 200, Height = 30, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            string final_result = prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            return final_result.Trim();
        }

        public void ClearMonitor() {
            partial_response.Text = "";
        }

        public string MonitorText => partial_response.Text;
        public bool IsVoiceOn => CBVoice.Checked;
        public bool IsMonitoring => chat_tabs.SelectedTab.Name == "prtab";
        public string UserName => MyName.Text;
        public string CharName => WhoList.Text;
        public bool UseRecommended => CBUseRecommended.Checked;
        public bool FillContext => CBFillContext.Checked;
        public bool PostProcessPrompt => postprocess_prompt.Checked;

        private void Form1_Load(object sender, EventArgs e) {
            AllControls = new Dictionary<string, Control>();
            foreach (Control control in Controls) {
                if (control is PictureBox ||
                    control is TextBox ||
                    control is ComboBox ||
                    control is Button)
                    AllControls.Add(control.Name, control);
            }
            foreach(TabPage tp in chat_tabs.TabPages) {
                foreach(Control c in tp.Controls) {
                    if (c is PictureBox ||
                        c is TextBox ||
                        c is ComboBox ||
                        c is Button)
                        AllControls.Add(c.Name, c);
                }
            }
            RefreshNames(false);
            if (File.Exists(OptionsFile)) {
                try {
                    string optdata = File.ReadAllText(OptionsFile);
                    KoboldPY.Text = Integration.LoadTagged(optdata, "KoboldPY") ?? KoboldPY.Text;
                    ComfyURL.Text = Integration.LoadTagged(optdata, "ComfyURL") ?? ComfyURL.Text;
                    ComfyUI_Textbox.Text = Integration.LoadTagged(optdata, "ComfyUI_Textbox") ?? ComfyUI_Textbox.Text;
                    Integration.IMGConfig.KoboldCppVisualModel = Integration.LoadTagged(optdata, "KoboldCppVisualModel") ?? Integration.IMGConfig.KoboldCppVisualModel;
                    Integration.IMGConfig.UseExistingTextModel = (Integration.LoadTagged(optdata, "UseExistingTextModel") ?? "true") == "true";
                    Integration.IMGConfig.MaxResolution = int.Parse(Integration.LoadTagged(optdata, "MaxResolution") ?? "896");
                    Integration.IMGConfig.ImagePrompt = Integration.LoadTagged(optdata, "ImagePrompt") ?? Integration.IMGConfig.ImagePrompt;
                    ComfyURL.Text = Integration.LoadTagged(optdata, "ComfyURL") ?? ComfyURL.Text;
                    ComfyUI_Textbox.Text = Integration.LoadTagged(optdata, "ComfyUI_Textbox") ?? ComfyUI_Textbox.Text;
                    AdvDryBase.Text = Integration.LoadTagged(optdata, "AdvDryBase") ?? AdvDryBase.Text;
                    AdvDryMult.Text = Integration.LoadTagged(optdata, "AdvDryMult") ?? AdvDryMult.Text;
                    AdvMinP.Text = Integration.LoadTagged(optdata, "AdvMinP") ?? AdvMinP.Text;
                    AdvMaxTokens.Text = Integration.LoadTagged(optdata, "AdvMaxTokens") ?? AdvMaxTokens.Text;
                    rss_feed.Text = Integration.LoadTagged(optdata, "rss_feed") ?? rss_feed.Text;
                    rss_feed_count.Text = Integration.LoadTagged(optdata, "rss_feed_count") ?? rss_feed_count.Text;
                    AdvExtraStops.Text = Integration.LoadTagged(optdata, "AdvExtraStops") ?? AdvExtraStops.Text;
                    AdvTemperature.Text = Integration.LoadTagged(optdata, "AdvTemperature") ?? AdvTemperature.Text;
                    AdvWordRecall.Text = Integration.LoadTagged(optdata, "AdvWordRecall") ?? AdvWordRecall.Text;
                    AdvTopP.Text = Integration.LoadTagged(optdata, "AdvTopP") ?? AdvTopP.Text;
                    CBUseRecommended.Checked = (Integration.LoadTagged(optdata, "CBUseRecommended") ?? "true") == "true";
                    CBFillContext.Checked = (Integration.LoadTagged(optdata, "CBFillContext") ?? "true") == "true";
                    postprocess_prompt.Checked = (Integration.LoadTagged(optdata, "postprocess_prompt") ?? "true") == "true";
                    Integration.CurrentImageOptions.KillKobold = (Integration.LoadTagged(optdata, "KillKobold") ?? "true") == "true";
                    Integration.CurrentImageOptions.LocationWeight = (Integration.LoadTagged(optdata, "LocationWeight") ?? "true") == "true";
                    Integration.CurrentImageOptions.Workflow = Integration.LoadTagged(optdata, "Workflow") ?? Integration.CurrentImageOptions.Workflow;
                    Integration.CurrentImageOptions.Model = Integration.LoadTagged(optdata, "Model") ?? Integration.CurrentImageOptions.Model;
                    Integration.CurrentImageOptions.Negative = Integration.LoadTagged(optdata, "Negative") ?? Integration.CurrentImageOptions.Negative;
                    Integration.CurrentImageOptions.Size = Integration.LoadTagged(optdata, "Size") ?? Integration.CurrentImageOptions.Size;
                    Integration.CurrentImageOptions.AutogenMode = (Integration.AUTOGEN_IMAGE)int.Parse(Integration.LoadTagged(optdata, "AutogenMode") ?? ((int)Integration.CurrentImageOptions.AutogenMode).ToString());
                    Integration.CurrentImageOptions.Steps = int.Parse(Integration.LoadTagged(optdata, "Steps") ?? Integration.CurrentImageOptions.Steps.ToString());
                } catch { }
            }

            // TODO: need to pick whether remote or local
            // if remote, disable features like starting/killing kobold and probably whisper listening
            string result = ShowDialog("KoboldCpp IP:Port? Leave blank to use KoboldCpp Config locally", "KoboldCpp Connection Configuration", "127.0.0.1:5001");
            if (result == "") {
                Integration.RemoteOnlyMode = false;
                if (pickKoboldConfig.ShowDialog() != DialogResult.OK) {
                    Application.Exit();
                    return;
                }

                if (!File.Exists(KoboldPY.Text.Trim())) {
                    if (KoboldFinder.ShowDialog() != DialogResult.OK) {
                        Application.Exit();
                        return;
                    }
                    KoboldPY.Text = KoboldFinder.FileName;
                    SaveOptions();
                }

                Integration.ReadKoboldConfig(pickKoboldConfig.FileName);
            } else {
                Integration.RemoteOnlyMode = true;
                KoboldPY.Text = Integration.KoboldURL = "http://" + result;
                Integration.max_context_len = 16 * 1024; // default context length until read
                Integration.Initialize(true);
            }

            Directory.CreateDirectory(Integration.CharDirectory);
            CharWatcher.Path = Integration.CharDirectory;
            CharWatcher.Deleted += CharWatcher_Created;
            CharWatcher.Created += CharWatcher_Created;
            CharWatcher.Renamed += CharWatcher_Created;

            // need to pick 
            Enabled = true;
            Focus();
            Activate();
        }

        private void CharWatcher_Created(object sender, FileSystemEventArgs e) {
            RefreshNames(false);
        }

        public string GetKoboldConfig => pickKoboldConfig.FileName ?? "";
        public bool GroupChatMode => CBGroupChat.Checked;

        public void SaveOptions() {
            string optdata = Integration.StringTagged(KoboldPY.Text, "KoboldPY");
            optdata += Integration.StringTagged(ComfyURL.Text, "ComfyURL");
            optdata += Integration.StringTagged(ComfyUI_Textbox.Text, "ComfyUI_Textbox");
            optdata += Integration.StringTagged(AdvDryBase.Text, "AdvDryBase");
            optdata += Integration.StringTagged(AdvDryMult.Text, "AdvDryMult");
            optdata += Integration.StringTagged(AdvMinP.Text, "AdvMinP");
            optdata += Integration.StringTagged(AdvMaxTokens.Text, "AdvMaxTokens");
            optdata += Integration.StringTagged(AdvExtraStops.Text, "AdvExtraStops");
            optdata += Integration.StringTagged(AdvTemperature.Text, "AdvTemperature");
            optdata += Integration.StringTagged(AdvWordRecall.Text, "AdvWordRecall");
            optdata += Integration.StringTagged(rss_feed.Text, "rss_feed");
            optdata += Integration.StringTagged(rss_feed_count.Text, "rss_feed_count");
            optdata += Integration.StringTagged(AdvTopP.Text, "AdvTopP");
            optdata += Integration.StringTagged(CBUseRecommended.Checked ? "true" : "false", "CBUseRecommended");
            optdata += Integration.StringTagged(postprocess_prompt.Checked ? "true" : "false", "postprocess_prompt");
            optdata += Integration.StringTagged(CBFillContext.Checked ? "true" : "false", "CBFillContext");
            optdata += Integration.StringTagged(Integration.IMGConfig.KoboldCppVisualModel, "KoboldCppVisualModel");
            optdata += Integration.StringTagged(Integration.IMGConfig.ImagePrompt, "ImagePrompt");
            optdata += Integration.StringTagged(Integration.IMGConfig.UseExistingTextModel ? "true" : "false", "UseExistingTextModel");
            optdata += Integration.StringTagged(Integration.IMGConfig.MaxResolution.ToString(), "MaxResolution");
            optdata += Integration.StringTagged(Integration.CurrentImageOptions.KillKobold ? "true" : "false", "KillKobold");
            optdata += Integration.StringTagged(Integration.CurrentImageOptions.LocationWeight ? "true" : "false", "LocationWeight");
            optdata += Integration.StringTagged(Integration.CurrentImageOptions.Negative, "Negative");
            optdata += Integration.StringTagged(Integration.CurrentImageOptions.Workflow, "Workflow");
            optdata += Integration.StringTagged(Integration.CurrentImageOptions.Model, "Model");
            optdata += Integration.StringTagged(Integration.CurrentImageOptions.Size, "Size");
            optdata += Integration.StringTagged(((int)Integration.CurrentImageOptions.AutogenMode).ToString(), "AutogenMode");
            optdata += Integration.StringTagged(Integration.CurrentImageOptions.Steps.ToString(), "Steps");
            File.WriteAllText(OptionsFile, optdata);
        }

        public bool IsCreative => CBCreative.Checked;
        public Image GetImage => YourPic.Image;

        public T GetControl<T>(string name) where T : Control {
            return (T)AllControls[name];
        }

        private void MainTimer_Tick(object sender, EventArgs e) {
            Integration.Update();
            if (CBAutoTalk.Checked) {
                // check for autotalk error
                string err = ReadyOrNot(false, true, false);
                if (err != "") {
                    DisableAutoTalk();
                    MessageBox.Show("Cannot auto trigger talking:\n\n" + err, "Auto Trigger Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (ReadyOrNot(true, true, true) != "") return;
            if (ChatManager.YourPrompt != null && ChatManager.YourPrompt.Length > 0) {
                SetStatus("Processing Your Prompt");
                ChatManager.SendChat(ChatManager.YourPrompt, true, ChatManager.YourImageDescription);
            } else if (CBAutoTalk.Checked && ChatManager.AutoTalkTimer > 0) {
                ChatManager.AutoTalkTimer--;
                if (ChatManager.AutoTalkTimer <= 0) {
                    // no error, ready to auto-talk
                    SetStatus("Auto talk Triggered");
                    if (GroupChatMode)
                        ChatManager.SendContinue();
                    else
                        ChatManager.SendChat("*silence*", false); // send silence
                }
            }
        }

        private void AddWho_Click(object sender, EventArgs e) {
            CharMaker.instance = new CharMaker();
            CharMaker.instance.Show(this);
            CharMaker.instance.LoadOrCreateCharacter();
        }

        private volatile bool AlreadyRefreshingNames;
        public void RefreshNames(bool skipyou) {
            if (AlreadyRefreshingNames) return; 
            AlreadyRefreshingNames = true;
            string previous_you = MyName.Text.Trim();
            string previous_name = WhoList.Text.Trim();
            //ChatManager.MeCharacter = null;
            WhoList.Items.Clear();
            if (skipyou == false) {
                MyName.Items.Clear();
                MyName.Text = previous_you;
            }
            Directory.CreateDirectory(Integration.CharDirectory);
            string[] fn = Directory.GetFiles(Integration.CharDirectory, "*.txt");
            foreach (string _fn in fn) {
                Character.CharFile cf = new Character.CharFile() {  full_filename = _fn };
                if (cf.short_name != previous_you) {
                    WhoList.Items.Add(cf);
                    if (previous_name == cf.short_name)
                        WhoList.SelectedItem = cf;
                }
                if (skipyou == false) MyName.Items.Add(cf);
                if (cf.short_name == previous_you) {
                    MyName.SelectedItem = cf;
                    ChatManager.MeCharacter = Character.EfficientLoadCharacter(ChatManager.MeCharacter, cf.short_name, false); // new Character(cf.short_name);
                }
            }
            WhoList_SelectedValueChanged(null, null);
            AlreadyRefreshingNames = false;
        }

        private void EditWho_Click(object sender, EventArgs e) {
            if (ChatManager.SelectedCharacter != null) {
                CharMaker.instance = new CharMaker();
                CharMaker.instance.Show(this);
                CharMaker.instance.LoadCharacter(ChatManager.SelectedCharacter);
            }
        }

        private void WhoList_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void WhoList_SelectedValueChanged(object sender, EventArgs e) {
            string sel_name = WhoList.SelectedItem?.ToString() ?? "";
            if (sel_name.Length > 0) {
                string last_image = ChatManager.SelectedCharacter?.GetPicture ?? "";
                ChatManager.SelectedCharacter = Character.EfficientLoadCharacter(ChatManager.SelectedCharacter, sel_name, false);// new Character(sel_name);
                UpdateChatLog();
                // update picture?
                if (WhoPicture.Image == null || CBGroupChat.Checked == false ||
                    Integration.CurrentImageOptions.AutogenMode == Integration.AUTOGEN_IMAGE.MANUAL) {
                    string fn = ChatManager.SelectedCharacter.GetPicture;
                    if (File.Exists(fn)) {
                        try {
                            //WhoPicture.Image = Image.FromFile();
                            WhoPicture.ImageLocation = ChatManager.SelectedCharacter.GetPicture;
                        } catch {
                            WhoPicture = null;
                        }
                    } else WhoPicture.Image = null;
                }
            }
        }       

        public void UpdateChatLog() {
            var rt = Integration.MainForm.GetControl<TextBox>("ResponseText");
            rt.Text = ChatManager.CurrentChatLog.Replace("\n", "\r\n");
            rt.SelectionStart = rt.Text.Length;
            rt.ScrollToCaret();
        }

        public string ReadyOrNot(bool check_if_picture, bool no_prompt_needed, bool check_if_talking) {
            // lots of safety checks
            string err = "";
            if (MyName.Text.Length <= 0) err = "You need to provide your name!";
            else if (Integration.CharactersPerToken <= 0f) err = "Wait for Token Estimation result...\n\nIf this doesn't complete, make sure KoboldCpp started!";
            else if (WhoList.SelectedItem == null) err = "You need to pick someone to talk to!";
            else if (ChatManager.ImagePromptRequested) err = "Processing an Image Sent, please wait...";
            else if (ChatManager.KeywordsRequested) err = "Getting prompt keywords, please wait...";
            else if (check_if_talking && ChatManager.ChatRequested) err = "Text generation is currently happening!";
            else if (check_if_picture && ChatManager.PictureRequested) err = "We are waiting for a picture!";
            //else if (!File.Exists(KoboldCpp.Text)) err = "KoboldCpp Config file not found!";
            else if (Integration.RemoteOnlyMode == false && !File.Exists(KoboldPY.Text)) err = "koboldcpp.py file not found!";
            //else if (MyRelation.Text.Length <= 0) err = "You need to provide how you should be known!";
            else if (!no_prompt_needed && SendText.Text.Length <= 0) err = "You need to provide some text to send!";
            if (err == "") SaveOptions();
            return err;
        }

        private void SendButton_Click(object sender, EventArgs e) {
            string err = ReadyOrNot(false, false, false);
            if (err.Length == 0) {
                ChatManager.YourPrompt = SendText.Text.Trim();
                ChatManager.YourImageDescription = null;
                SendText.Text = "";
            } else {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendText_TextChanged(object sender, EventArgs e) {

        }

        private void CBAutoTalk_CheckedChanged(object sender, EventArgs e) {
            ChatManager.AutoTalkTimer = 0;
        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            if (Integration.VerifyComfyUI()) {
                string err = ReadyOrNot(true, true, true);
                if (err.Length == 0) {
                    Integration.MainForm.DisableAutoTalk();
                    ChatManager.GetPicture();
                } else {
                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SetStatus(string status) {
            StatusText.Text = status.Trim();
        }

        private void button2_Click(object sender, EventArgs e) {
            Integration.Abort();
        }

        /*private void KoboldCpp_TextChanged(object sender, EventArgs e) {
            Integration.ReadKoboldConfig(KoboldCpp.Text.Trim());
        }*/

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            Integration.KillKobold();
        }

        public void PrepareWatcher(string folder) {
            if (folder == null) {
                fileSystemWatcher1.EnableRaisingEvents = false;
            } else {
                Directory.CreateDirectory(folder);
                fileSystemWatcher1.Path = folder;
                fileSystemWatcher1.EnableRaisingEvents = true;
            }
        }

        private void ComfyUI_Textbox_TextChanged(object sender, EventArgs e) {

        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e) {
            Integration.FileWatcherDetection(e);
        }

        public WebCam.WebCam wCam = null;

        public static bool NewImageToSend;

        public void SetImage(Image img) {
            if (img != null) {
                YourPic.Image = img;
                NewImageToSend = true;
            } else {
                YourPic.Image = null;
            }
         }

        private void m_webCam_OnSnapshot(object sender, ImageArgs e) {
            SetImage(e.Image);
        }

        private void PicTake_Click(object sender, EventArgs e) {
            if (wCam == null) {
                wCam = new WebCam.WebCam();
                wCam.OnSnapshot += m_webCam_OnSnapshot;
                wCam.Open(wCam.VideoInputDevices[0], null, null);
            }
            wCam.GetImage();
        }

        public void DisableAutoTalk() {
            CBAutoTalk.Checked = false;
        }

        public List<string> GetCharacterList() {
            List<string> strings = new List<string>();
            for (int i = 0; i < WhoList.Items.Count; i++) {
                strings.Add(WhoList.Items[i].ToString());
            }
            if (strings.Contains(MyName.Text) == false) strings.Add(MyName.Text);
            // add first names, if needed
            for (int i = 0; i < strings.Count; i++) {
                string[] ss = strings[i].Split(' ');
                if (ss.Length > 1) strings.Add(ss[0]);
            }
            return strings;
        }

        private void PicPaste_Click(object sender, EventArgs e) {
            SetImage(Clipboard.GetImage());
        }

        private void ReplayButton_Click(object sender, EventArgs e) {
            if (Integration.VerifyComfyUI()) {
                ChatManager.AwaitingSay.Clear();
                ChatManager.SayLine(partial_response.Text, ChatManager.SelectedCharacter);
            }
        }

        private void WhoPicture_Click(object sender, EventArgs e) {
            if (WhoPicture.Image != null) Clipboard.SetImage(WhoPicture.Image);
        }

        private void ListenButton_Click(object sender, EventArgs e) {
            if (Integration.VerifyComfyUI()) {
                if (_soundIn == null) {
                    StartRecording();
                } else {
                    StopRecording();
                }
            }
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e) {
            // Append the recognized text to the textbox
            // Use Invoke to update the UI from a different thread
            this.Invoke((MethodInvoker)delegate {
                SendText.AppendText(e.Result.Text);
            });
        }

        private WasapiCapture _soundIn;
        private WaveWriter _waveWriter;
        private string _outputFilePath;
        private IWaveSource _source;
        public void StartRecording() {
            try {
                // Get the default capture device
                // You can enumerate devices using MMDeviceEnumerator.EnumerateAudioEndPoints
                // if you need to select a specific one.
                _soundIn = new WasapiCapture();

                _soundIn.DataAvailable += OnDataAvailable;

                _soundIn.Initialize();

                // Define the output file path
                _outputFilePath = Path.Combine(Integration.BaseDirectory, "recording.wav");

                _source = new SoundInSource(_soundIn);
                _waveWriter = new WaveWriter(_outputFilePath, _source.WaveFormat);
                DataCollector.Clear();
                _soundIn.Start(); // Start recording
                ListenButton.Text = "STOP";
            } catch (Exception ex) {

            }
        }

        public List<byte> DataCollector = new List<byte>();
        private void OnDataAvailable(object sender, DataAvailableEventArgs e) {
            if (_waveWriter == null) return;

            for (int i=e.Offset; i<e.Offset+e.ByteCount; i++)
                DataCollector.Add(e.Data[i]);
        }

        public void StopRecording() {
            if (_soundIn != null) {
                _soundIn.Stop(); // This will trigger OnRecordingStopped
                _soundIn.Dispose();
                _soundIn = null;
                _waveWriter.Write(DataCollector.ToArray(), 0, DataCollector.Count);
                _waveWriter.Dispose();
                _waveWriter = null;
                _source.Dispose();
                _source = null;

                // trigger whisper
                Dictionary<string, string> repl = new Dictionary<string, string>();
                repl["$AUDIO_FILE"] = _outputFilePath.Replace("\\", "/");
                Integration.MainForm.PrepareWatcher(Path.Combine(Integration.ComfyUIDir, "output/talker/"));
                Integration.SendComfyRequest(Path.Combine(Integration.BaseDirectory, "workflows/Whisper.json"), repl);

                ListenButton.Text = "Listen...";
            }
        }

        private void CBGroupChat_CheckedChanged(object sender, EventArgs e) {
            ChatManager.SetGroupChatMode();
        }

        private void button3_Click(object sender, EventArgs e) {
            string fn = Path.Combine(Integration.BaseDirectory, "groupchat.txt");
            if (File.Exists(fn) == false)
                File.WriteAllText(fn, " ");
            new Process {
                StartInfo = new ProcessStartInfo(fn) {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void PicLoad_Click(object sender, EventArgs e) {
            try {
                if (OpenImageDialog.ShowDialog(this) == DialogResult.OK)
                    SetImage(Image.FromFile(OpenImageDialog.FileName));
            }catch { }
        }

        private void MyName_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void MyName_TextChanged(object sender, EventArgs e) {
            RefreshNames(true);
        }

        private void button4_Click(object sender, EventArgs e) {
            (new ImageReaderSettings()).Show(this);
        }

        private void button5_Click(object sender, EventArgs e) {
            Directory.CreateDirectory(Integration.CharDirectory);
            new Process {
                StartInfo = new ProcessStartInfo(Integration.CharDirectory) {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void button6_Click(object sender, EventArgs e) {
            (new ImageGenOptions()).Show(this);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {

        }

        private void rss_feed_TextChanged(object sender, EventArgs e) {
            Integration.UpdateRSSFeedTimer = 5f;
        }
    }
}
