using CSCore.Codecs;
using CSCore;
using CSCore.SoundOut;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Globalization;
using System.Windows.Forms;
using CSCore.CoreAudioAPI;
using SimpleFeedReader;

/*
 * x support remote use
 * - process chat prompt keyword extraction *NEEDS TESTING*
 * - add qwen edit support (basically a workflow to take the input image of the character) *NEEDS TESTING*
 */

namespace TalkerFrontend {
    public class Integration {

        public enum AUTOGEN_IMAGE {
            MANUAL = 0,
            AUTO_SINGLE = 1,
            AUTO_CONTINUOUS = 2
        }

        public static bool RemoteOnlyMode = false;

        public class ImageGenSettings {
            public string Model = "", Size;
            public string Workflow = "ImageGen-Chroma.json";
            public int Steps;
            public AUTOGEN_IMAGE AutogenMode;
            public string Negative;
            public bool KillKobold, LocationWeight;
            public Size GetSize {
                get {
                    try {
                        string[] spl = Size.Split('x');
                        return new Size(int.Parse(spl[0]), int.Parse(spl[1]));
                    } catch {
                        return new Size(896, 896);
                    }
                }
            }
        }

        public static ImageGenSettings CurrentImageOptions = new ImageGenSettings() {
            Workflow = "ImageGen-Chroma.json",
            Model = "",
            Steps = 16,
            Negative = "disfigured, gross, bad quality, bad hands, blurry, deformed",
            KillKobold = true,
            Size = "896x896",
            AutogenMode = AUTOGEN_IMAGE.MANUAL,
            LocationWeight = false
        };

        public static bool VerifyComfyUI() {
            if (Directory.Exists(ComfyUIDir) == false) {
                MessageBox.Show("ComfyUI not found at location. Do you have ComfyUI installed and running already?", "ComfyUI Installation Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static float UpdateRSSFeedTimer = 0f;

        public class TalkerSettings {
            public int MaxGeneration {
                get {
                    string maxgenstr = MainForm.GetControl<TextBox>("AdvMaxTokens").Text;
                    if (int.TryParse(maxgenstr, out int res)) {
                        if (res < 64) return 64;
                        if (res > 128000) return 128000;
                        return res;
                    }
                    return 2048;
                }
            }
            public List<string> ExtraStopTokens {
                get {
                    string eststr = MainForm.GetControl<TextBox>("AdvExtraStops").Text;
                    string[] splits = eststr.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    return new List<string>(splits);
                }
            }
            public float TopP, MinP, Temperature, DryBase, DryMultiplier;
        }

        public static TalkerSettings DefaultSettings = new TalkerSettings() {
            DryBase = 1.75f,
            DryMultiplier = 0.8f,
            MinP = 0.15f,
            Temperature = 1.25f,
            TopP = 1f,
        };

        public static TalkerSettings NonCreativeSettings = new TalkerSettings() {
            DryBase = 1.75f,
            DryMultiplier = 0.4f,
            MinP = 0.15f,
            Temperature = 0.5f,
            TopP = 1f,
        };

        public static float GetValue(string val, float def, float min, float max) {
            if (float.TryParse(val, out float v)) {
                if (v < min) return min;
                if (v > max) return max;
                return v;
            }

            return def;
        }

        public static TalkerSettings GetCurrentSettings(bool creative) {
            if (MainForm.UseRecommended)
                return creative ? DefaultSettings : NonCreativeSettings;

            return new TalkerSettings() {
                Temperature = GetValue(MainForm.GetControl<TextBox>("AdvTemperature").Text, 1.25f, 0f, 10f),
                MinP = GetValue(MainForm.GetControl<TextBox>("AdvMinP").Text, 0.15f, 0f, 1f),
                TopP = GetValue(MainForm.GetControl<TextBox>("AdvTopP").Text, 1f, 0f, 1f),
                DryMultiplier = GetValue(MainForm.GetControl<TextBox>("AdvDryMult").Text, 0.8f, 0f, 10f),
                DryBase = GetValue(MainForm.GetControl<TextBox>("AdvDryBase").Text, 1.75f, 1f, 10f) 
            };
        }

        public static MainForm MainForm;

        public static ConcurrentQueue<RestRequest> waitingRequestsComfy = new ConcurrentQueue<RestRequest>();
        public static ConcurrentQueue<RestRequest> waitingRequestsKobold = new ConcurrentQueue<RestRequest>();

        public static List<Task<RestResponse>> waitingKoboldResponses = new List<Task<RestResponse>>();
        public static List<Task<RestResponse>> waitingComfyResponses = new List<Task<RestResponse>>();

        public static int max_context_len;
        public static float TokenPerCharacter;
        public static RestClient ComfyAPI, KoboldAPI;
        public static string ModelName, KoboldURL;

        public static int GetMaxCharacterLength => (int)Math.Floor((max_context_len - 2560) * TokenPerCharacter);

        public static string BaseDirectory => Path.GetDirectoryName(Application.ExecutablePath);
        public static string CharDirectory => Path.Combine(BaseDirectory, "Characters/");

        public static Bitmap ResizeImage(Image image, int width, int height) {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage)) {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes()) {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public class ImageInputConfig {
            public int MaxResolution = 896;
            public string ImagePrompt = "Describe the image in complete detail. What is in the image, what is happening, and what details may be discerned from the image? If any text is visible, also include what the text says.";
            public string KoboldCppVisualModel;
            public bool UseExistingTextModel = true;
            public string LastImagePromptResult;
        }

        public static ImageInputConfig IMGConfig = new ImageInputConfig();

        public static string EncodeImage(Image image, float max_res) {
            // resize highest res to 896
            float hs = max_res / image.Height;
            float ws = max_res / image.Width;
            float scale = Math.Min(hs, ws);
            if (scale < 1f)
                image = ResizeImage(image, (int)Math.Round(image.Width * scale), (int)Math.Round(image.Height * scale));

            // Save the image to a memory stream in PNG format
            using (var memoryStream = new MemoryStream()) {
                image.Save(memoryStream, ImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();

                // Convert byte array to Base64 string
                return Convert.ToBase64String(imageBytes);
            }
        }

        public static string StringTagged(string data, string tag) {
            return "[[[" + tag + "]]]" + data + "[[[/" + tag + "]]]\n";
        }

        public static string LoadTagged(string data, string tag) {
            string fulltag = "[[[" + tag + "]]]";
            int io = data.IndexOf(fulltag);
            if (io > -1)
                return data.Substring(fulltag.Length + io, data.IndexOf("[[[/" + tag + "]]]", io) - fulltag.Length - io);
            return null;
        }

        public static void ReadKoboldConfig(string fn) {
            if (File.Exists(fn)) {
                string json = File.ReadAllText(fn);
                JsonElement properties = JsonSerializer.Deserialize<JsonElement>(json);
                max_context_len = properties.GetProperty("contextsize").GetInt32();
                int port = properties.GetProperty("port").GetInt32();
                string host = properties.GetProperty("host").GetString();
                if (host.Length == 0) host = "127.0.0.1";
                KoboldURL = "http://" + host + ":" + port;
                if (port > 0) Initialize(false);
            }
        }

        public static RestClient GetClientFor(RestRequest rr) {
            if (rr.Resource.StartsWith("/api"))
                return KoboldAPI;
            else {
                if (ComfyAPI != null) ComfyAPI.Dispose();
                var ttsoptions = new RestClientOptions(MainForm.GetControl<TextBox>("ComfyURL").Text) {
                    Timeout = TimeSpan.FromHours(12),
                };
                ComfyAPI = new RestClient(ttsoptions);
                return ComfyAPI;
            }
        }

        public static Process KoboldProcess;
        public static void EnsureKoboldCppMode(bool enabled, bool imagemodel = false, int msdelay = 7000) {
            if (KoboldProcess?.HasExited ?? false) KoboldProcess = null; 
            if ((KoboldProcess != null) == enabled) return; // already set
            // need to swap!
            if (enabled) {
                // need to start kobold
                string kpy = MainForm.GetControl<TextBox>("KoboldPY").Text.Trim().Replace("\\", "/");
                ProcessStartInfo psi = new ProcessStartInfo() {
                    UseShellExecute = true,
                    FileName = "python",
                    WindowStyle = ProcessWindowStyle.Minimized,                     
                    WorkingDirectory = Path.GetDirectoryName(kpy),
                    Arguments = "koboldcpp.py --singleinstance --config \"" + (imagemodel && File.Exists(IMGConfig.KoboldCppVisualModel) ? IMGConfig.KoboldCppVisualModel : MainForm.GetKoboldConfig.Trim()).Replace("\\", "/") + "\""
                };
                KoboldProcess = new Process();
                KoboldProcess.StartInfo = psi;
                // free comfyui memory
                SendComfyRequest(Path.Combine(BaseDirectory, "workflows/Free-Memory.json"), null);
                Task t = new Task(() => {
                    Thread.Sleep(msdelay); // wait for comfy to free memory
                    KoboldProcess.Start();
                });
                t.Start();
            } else {
                // need to kill kobold
                if (KoboldProcess != null)
                    KoboldProcess.Kill();
                KoboldProcess = null;
            }
        }

        public static void ExecuteRequest(RestRequest req) {
            var client = GetClientFor(req);
            if (client == null) return;
            req.Timeout = TimeSpan.FromHours(12.0);
            req.Version = new Version(1, 1);
            if (client == KoboldAPI)
                waitingRequestsKobold.Enqueue(req);
            else 
                waitingRequestsComfy.Enqueue(req);
        }

        public static void Abort() {
            ChatManager.YourPrompt = null;
            ChatManager.YourImageDescription = null;
            autogen_timer = 0;
            MainForm.DisableAutoTalk();
            ChatManager.SayTalking = false;
            ChatManager.SayGenerating = false;
            MainForm.PrepareWatcher(null);
            ChatManager.AwaitingSay.Clear();
            ChatManager.NeedVoiceFor.Clear();
            MainForm.SetStatus("Aborted");
            AwaitingAudioFiles.Clear();
            ChatManager.previousYourPrompt = null;
            ChatManager.PictureRequested = false;
            ChatManager.ChatRequested = false;
            ChatManager.ImagePromptRequested = false;
            ChatManager.KeywordsRequested = false;
            waitingKoboldResponses.Clear();
            waitingComfyResponses.Clear();
            ExecuteRequest(new RestRequest("/api/extra/abort", Method.Post));
            ExecuteRequest(new RestRequest("/interrupt", Method.Post));
        }

        public static void Initialize(bool getContextSize) {
            ChatManager.LoadGroupChat();
            Abort();
            try {
                if (KoboldAPI != null) KoboldAPI.Dispose();
                //if (TTSServer != null) TTSServer.Dispose();
            } catch (Exception) { }
            var options = new RestClientOptions(KoboldURL) {
                Timeout = TimeSpan.FromHours(12),
            };
            KoboldAPI = new RestClient(options);
            //ExecuteRequest(new RestRequest("/api/v1/model"));
            if (getContextSize) ExecuteRequest(new RestRequest("/api/extra/true_max_context_length"));
            SendTestString();
        }

        public static void ProcessLLMResponse(string text) {
            text = text.Replace("<think>", " ").Replace("</think>", " ").Replace("/no-think", " ").Replace("/no_think", " ");
            if (ChatManager.KeywordsRequested) {
                ChatManager.KeywordsRequested = false;
                ChatManager.SendChat(ChatManager.YourPrompt, true, ChatManager.YourImageDescription, text);
            } else if (ChatManager.ImagePromptRequested) {
                IMGConfig.LastImagePromptResult = text;
                // switch back to regular text model
                KillKobold();
                Task t = new Task(() => {
                    Thread.Sleep(3000);
                    EnsureKoboldCppMode(true, false, 500);
                    Thread.Sleep(3000);
                    ChatManager.ImagePromptRequested = false;
                    ChatManager.YourImageDescription = IMGConfig.LastImagePromptResult;
                    ChatManager.YourPrompt = ChatManager.previousYourPrompt;
                });
                t.Start();
            } else if (ChatManager.PictureRequested) {
                string[] separate = text.Split(new string[] { "Who:", "What:" }, StringSplitOptions.None);
                if (separate.Length == 3 && separate[0].Length > 2 && separate[1].Length > 2 && separate[2].Length > 2) {
                    string raw_location = StringProcessor.TruncateStringByWordCount(separate[0].Trim(), CurrentImageOptions.LocationWeight ? 16 : 24);
                    string location = CurrentImageOptions.LocationWeight ? "(" + raw_location + ":0.66)" : raw_location;
                    string[] who_words = StringProcessor.ReplaceWholeWord(separate[1].Trim(), MainForm.UserName, "").Split(new string[] { ",", " and ", " with ", " near ", " by ", "." }, StringSplitOptions.RemoveEmptyEntries);
                    string who = "";
                    foreach (string who_word in who_words) {
                        string visuals_found = Character.GetCharacterVisuals(who_word);
                        if (visuals_found != null) who += visuals_found + ", ";
                    }
                    string what = StringProcessor.ReplaceWholeWord(separate[2].Trim(), MainForm.UserName + "'s", "POV");
                    what = StringProcessor.ReplaceWholeWord(what, MainForm.UserName, "POV");
                    string image_prompt = StringProcessor.TruncateStringByWordCount(who, CurrentImageOptions.LocationWeight ? 32 : 48) + ", " +
                                          StringProcessor.TruncateStringByWordCount(what, CurrentImageOptions.LocationWeight ? 32 : 48) + ". " +
                                          location + ". " + ChatManager.SelectedCharacter.ImageStyle;
                    Dictionary<string, string> repl = new Dictionary<string, string>();
                    var size = CurrentImageOptions.GetSize;
                    repl["$START_PROMPT"] = image_prompt;
                    repl["$STEPS"] = CurrentImageOptions.Steps.ToString();
                    repl["$MODEL"] = CurrentImageOptions.Model;
                    repl["$SIZE_X"] = size.Width.ToString();
                    repl["$SIZE_Y"] = size.Height.ToString();
                    string img_in = ChatManager.SelectedCharacter.GetPicture;
                    repl["$INPUT_IMAGE"] = img_in;
                    if (File.Exists(img_in)) {
                        repl["$VALID_IMAGE"] = ",\r\n      \"image1\": [\r\n        \"1\",\r\n        0\r\n      ]";
                    } else {
                        repl["$VALID_IMAGE"] = "";
                    }
                    repl["$SEED"] = MainForm.Random.Next(99999999).ToString();
                    repl["$OUTPUT_PATH"] = "talker/imgreq";
                    repl["$NEG_PROMPT"] = CurrentImageOptions.Negative;
                    if (CurrentImageOptions.KillKobold && !RemoteOnlyMode) EnsureKoboldCppMode(false);
                    MainForm.SetStatus("Generating Image");
                    MainForm.PrepareWatcher(Path.Combine(ComfyUIDir, "output/talker/"));
                    SendComfyRequest(Path.Combine(BaseDirectory, "workflows/" + CurrentImageOptions.Workflow), repl);
                } else {
                    // got garbage, try again
                    ChatManager.GetPicture();
                }
            } else {
                ChatManager.ChatRequested = false;
                ChatManager.CurrentChatLog += "\n\n" + ChatManager.WhoTalking.Name + ": " + text;
                File.WriteAllText(Path.Combine(BaseDirectory, "groupchat.txt"), ChatManager.GroupChatLog);
                ChatManager.WhoTalking.Save();
                MainForm.SetMonitorExact(text);
                MainForm.UpdateChatLog();
                MainForm.SetStatus("Ready");
                ChatManager.ResetAutotalk();
                if (CurrentImageOptions.AutogenMode != AUTOGEN_IMAGE.MANUAL) {
                    ChatManager.GetPicture();
                }
            }
        }

        public static Queue<string> AwaitingAudioFiles = new Queue<string>();
        private static IWaveSource _waveSource;
        private static ISoundOut _soundOut;
        public static void PlaySound(string fn) {
            AwaitingAudioFiles.Enqueue(fn);
        }

        private static void _soundOut_Stopped(object sender, PlaybackStoppedEventArgs e) {
            if (_waveSource != null)
                _waveSource.Dispose();
            if (_soundOut != null)
                _soundOut.Dispose();
            _waveSource = null;
            _soundOut = null;
            ChatManager.SayTalking = false;
        }

        public static FeedReader RSSFeedReader = new FeedReader();
        public static Task<IEnumerable<FeedItem>> feedEntries;
        public static string LatestRSSFeedCompiled;
        public static int monitordelay = 30, autogen_timer = 0;
        public static string LastAudioFile;
        public static List<Task<RestResponse>> all_response_tasks = new List<Task<RestResponse>>();
        public static void Update() {
            UpdateRSSFeedTimer -= 0.1f;
            string rss_feed_addr = MainForm.GetControl<TextBox>("rss_feed").Text;
            string rss_feed_count = MainForm.GetControl<TextBox>("rss_feed_count").Text;
            int.TryParse(rss_feed_count, out int rss_count);
            if (rss_count > 0 && rss_feed_addr.Length > 0) {
                if (UpdateRSSFeedTimer < 0f && feedEntries == null) {
                    feedEntries = RSSFeedReader.RetrieveFeedAsync(rss_feed_addr);
                    UpdateRSSFeedTimer = 60f * 2f; // retry every 2 minutes
                }
                if (feedEntries != null && feedEntries.IsCompleted) {
                    if ((feedEntries?.Result?.Count() ?? 0) > 0) {
                        string compile_news = "";
                        compile_news = "\n\nExternally provided current events (which may or may not be related to the discussion):\n";
                        foreach (var entry in feedEntries.Result) {
                            compile_news += entry.PublishDate.Value.DateTime.ToShortDateString() + ": " +
                                                     entry.Title + " (" + entry.Summary + ")\n";
                            rss_count--;
                            if (rss_count <= 0) break;
                        }
                        compile_news += "(end of current events list)\n\n";
                        LatestRSSFeedCompiled = compile_news;
                        UpdateRSSFeedTimer = 60f * 15f; // 15 minutes to update RSS feed
                    } else
                        UpdateRSSFeedTimer = 60f; // try again in a minute
                    feedEntries = null;
                }
            } else {
                LatestRSSFeedCompiled = "";
            }
            if (monitor_count > 0) {
                monitor_count--;
                if (monitor_count <= 0 && monitoring_file != null) {
                    string final_filename = monitoring_file.FullPath.Replace("\\", "/");
                    string extension = Path.GetExtension(final_filename).ToLower();
                    switch (extension) {
                        case ".png":
                            MainForm.GetControl<PictureBox>("WhoPicture").ImageLocation = final_filename;
                            MainForm.SetStatus("Ready");
                            ChatManager.PictureRequested = false;
                            break;
                        case ".txt":
                            // whisper transcription
                            MainForm.GetControl<TextBox>("SendText").AppendText(File.ReadAllText(final_filename).Trim() + " ");
                            break;
                        case ".flac":
                        case ".wav":
                            // wait, was this a generated voice?
                            int voicegen = final_filename.IndexOf("voicegen-");
                            if (voicegen >= 0) {
                                int end_name = final_filename.IndexOf("--", voicegen);
                                string charname = final_filename.Substring(voicegen + 9, end_name - voicegen - 9);
                                // generated voice, set for selected character
                                Character got_voice_for = null;
                                lock (ChatManager.NeedVoiceFor) {
                                    foreach (var c in ChatManager.NeedVoiceFor) {
                                        if (c.Name == charname) {
                                            got_voice_for = c;
                                            break;
                                        }
                                    }
                                    if (got_voice_for != null) {
                                        string final_voice_place = Path.Combine(CharDirectory, got_voice_for.Name + Path.GetExtension(final_filename));
                                        File.Copy(final_filename, final_voice_place, true);
                                        got_voice_for.VoiceWAV = final_voice_place;
                                        got_voice_for.VoiceText = "The quick brown fox jumps over the lazy dog, doesn't he, well, maybe he can't, or perhaps not, it depends on the circumstances, you see!";
                                        got_voice_for.Save();
                                        ChatManager.NeedVoiceFor.Remove(got_voice_for);
                                    }
                                }
                            } else {
                                ChatManager.SayGenerating = false;
                                PlaySound(final_filename);
                            }
                            break;
                    }
                }
            }
            if (MainForm.IsMonitoring || MainForm.IsVoiceOn) {
                monitordelay--;
                if (monitordelay < 0) {
                    monitordelay = 30;
                    RestRequest rrc = new RestRequest("/api/extra/generate/check");
                    waitingKoboldResponses.Add(GetClientFor(rrc).ExecuteAsync(rrc));
                }
            }
            if (AwaitingAudioFiles.Count > 0 && !ChatManager.SayTalking) {
                if (_soundOut == null) {
                    ChatManager.SayTalking = true;
                    _waveSource = CodecFactory.Instance.GetCodec(AwaitingAudioFiles.Dequeue());
                    _soundOut = new WasapiOut() { Latency = 100, Device = MMDeviceEnumerator.DefaultAudioEndpoint(DataFlow.Render, Role.Multimedia) };
                    _soundOut.Initialize(_waveSource);
                    _soundOut.Stopped += _soundOut_Stopped;
                    _soundOut.Play();
                }
            }
            if (waitingComfyResponses.Count == 0 && waitingRequestsComfy.TryDequeue(out var rr))
                waitingComfyResponses.Add(GetClientFor(rr).ExecuteAsync(rr));
            if (waitingKoboldResponses.Count == 0 && waitingRequestsKobold.TryDequeue(out var rrk))
                waitingKoboldResponses.Add(GetClientFor(rrk).ExecuteAsync(rrk));
            ChatManager.UpdateSay();
            all_response_tasks.Clear();
            all_response_tasks.AddRange(waitingKoboldResponses);
            all_response_tasks.AddRange(waitingComfyResponses);
            for (int i = 0; i < all_response_tasks.Count; i++) {
                var checkResponse = all_response_tasks[i];
                if (checkResponse.IsCompleted && checkResponse.Result is RestResponse rrsp) {
                    all_response_tasks.RemoveAt(i);
                    waitingKoboldResponses.Remove(checkResponse);
                    waitingComfyResponses.Remove(checkResponse);
                    i--;
                    if (rrsp.IsSuccessful) {
                        try {
                            if (rrsp.Content.Length > 0) {
                                JsonElement properties = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(rrsp.Content);
                                switch (rrsp.Request.Resource) {
                                    case "/api/extra/generate/check":
                                        string partial_result = properties.GetProperty("results")[0].GetProperty("text").ToString().Trim();
                                        partial_result = partial_result.Replace("<think>", " ").Replace("</think>", " ").Replace("/no-think", " ").Replace("/no_think", " ");
                                        MainForm.SetMonitor(partial_result);
                                        break;
                                    case "/api/v1/model":
                                        ModelName = properties.GetProperty("result").ToString();
                                        break;
                                    case "/api/v1/generate":
                                        ProcessLLMResponse(properties.GetProperty("results")[0].GetProperty("text").ToString().Trim());
                                        break;
                                    case "/api/extra/true_max_context_length":
                                        max_context_len = properties.GetProperty("value").GetInt32();
                                        break;
                                    case "/api/extra/tokencount":
                                        int token_count = properties.GetProperty("value").GetInt32();
                                        TokenPerCharacter = TestString.Length / (float)token_count;
                                        MainForm.SetStatus("Ready");
                                        break;
                                }
                            }
                        } catch {
                            HandleError(rrsp);
                        }
                    } else HandleError(rrsp);
                }
            }
            if (CurrentImageOptions.AutogenMode == AUTOGEN_IMAGE.AUTO_CONTINUOUS) {
                MainForm.DisableAutoTalk();
                if (MainForm.ReadyOrNot(true, true, true) == "" && ChatManager.CurrentChatLog.Length > 16) {
                    if (autogen_timer > 0) {
                        autogen_timer--;
                        if (autogen_timer <= 0) ChatManager.GetPicture();
                    }
                }
            }
        }

        public static void HandleError(RestResponse rrsp) {
            // if we are just not connected, then don't keep trying
            //if (rrsp.ErrorMessage != null && rrsp.ErrorMessage.Contains("No connection")) {
                // if this is waiting for TTS to startup, wait a second then go again
                //if (rrsp.Request.Resource.Equals("/tts") && StoryGenerator.SoVITTS_Process != null)
                //    Thread.Sleep(500);
                //else
            //    return;
            //}
            // did we send a message that wasn't found? dont keep trying this either
            if (rrsp.ErrorException?.Message.Contains("NotFound") ?? false)
                return;
            // dont resend checks/cancel requests
            if (rrsp.Request.Resource.Equals("/api/extra/generate/check") ||
                rrsp.Request.Resource.Equals("/interrupt"))
                return;
            var client = GetClientFor(rrsp.Request);
            (client == KoboldAPI ? waitingKoboldResponses : waitingComfyResponses).Add(client.ExecuteAsync(rrsp.Request)); // otherwise try resending the request
        }

        public static void SendTestString() {
            MainForm.SetStatus("Getting token estimates");
            if (!RemoteOnlyMode) EnsureKoboldCppMode(true, false, 500);
            var rr = new RestRequest("/api/extra/tokencount", Method.Post);
            rr.RequestFormat = DataFormat.Json;
            var requestBody = new {
                prompt = TestString
            };
            string jsonBody = System.Text.Json.JsonSerializer.Serialize(requestBody);
            rr.AddParameter("application/json", jsonBody, ParameterType.RequestBody); //Important
            ExecuteRequest(rr);
        }

        public static string ComfyUIDir {
            get {
                string cdir = MainForm.GetControl<TextBox>("ComfyUI_Textbox").Text.Trim();
                string base_comfy_dir = Path.Combine(cdir, "ComfyUI");
                if (Directory.Exists(base_comfy_dir))
                    return base_comfy_dir;
                return cdir;
            }
        }

        public static void SendComfyRequest(string json, Dictionary<string, string> replacements) {
            var rr = new RestRequest("/prompt", Method.Post);
            rr.RequestFormat = DataFormat.Json;

            string info = "-------[ ComfyUI Request: " + json + " ]------------\n";
            if (replacements != null) {
                foreach (var pair in replacements) {
                    info += pair.Key + ": " + pair.Value + "\n";
                }
            }
            info += "--------------------------------------------------------";
            Console.WriteLine(info);
            System.Diagnostics.Debug.WriteLine(info);

            try {
                string json_template = File.ReadAllText(json);

                if (replacements != null) {
                    foreach (var pair in replacements) {
                        if (pair.Key == "$PROMPT_TRAVEL" || pair.Key == "$VALID_IMAGE")
                            json_template = json_template.Replace(pair.Key, pair.Value.Replace("\n", " "));
                        else
                            json_template = json_template.Replace(pair.Key, pair.Value.Replace("\"", "'").Replace("\n", " ").Replace("\\", "\\\\"));
                    }
                }

                try {
                    File.WriteAllText(Path.Combine(BaseDirectory, "last_comfy_workflow.json"), json_template);
                } catch (Exception ex) { }

                rr.AddParameter("application/json", "{\"prompt\": " + json_template + "}", ParameterType.RequestBody);
            } catch (Exception e) { }

            ExecuteRequest(rr);
        }

        public static void KillKobold() {
            try {
                if (KoboldProcess != null)
                    KoboldProcess.Kill();
            } catch { }
        }

        public static FileSystemEventArgs monitoring_file;
        public static int monitor_count;
        public static void FileWatcherDetection(FileSystemEventArgs args) {
            if (args.ChangeType == WatcherChangeTypes.Deleted ||
                args.ChangeType == WatcherChangeTypes.Renamed) return; // dont care about these types
            // is this just a directory change?
            if (File.Exists(args.FullPath) == false) return;
            // if the file is zero sized and just created, ignore and wait for changed with actual size
            if ((new System.IO.FileInfo(args.FullPath)).Length == 0) return;
            // is this a filetype we care about?
            if (args.FullPath.EndsWith(".flac") || args.FullPath.EndsWith(".mp4") || args.FullPath.EndsWith(".png") || args.FullPath.EndsWith(".txt")) {
                // ok, let's monitor this, might be interesting
                monitoring_file = args;
                monitor_count = 5;
            }
        }

        //private static string last_prompt_sent;
        //private static int last_prompt_size;
        //private static bool last_prompt_notcreative;
        public static void SendTextPrompt(string prompt, int? max_len = null, bool not_creative = false, bool send_pic = false, bool skip_eos = false, string[] extra_stop_sequences = null, string[] banned_tokens = null) {
            if (!RemoteOnlyMode) EnsureKoboldCppMode(true);
            var rr = new RestRequest("/api/v1/generate", Method.Post);
            //last_prompt_sent = prompt;
            //last_prompt_notcreative = not_creative;
            rr.RequestFormat = DataFormat.Json;

            // clean up newlines
            prompt = prompt.Replace("\n", "   ").Replace("\r", "");

            TalkerSettings cursettings = not_creative ? NonCreativeSettings : GetCurrentSettings(MainForm.IsCreative);

            //last_prompt_size = max_len ?? cursettings.MaxGeneration;
            List<string> final_stops = extra_stop_sequences == null ? new List<string>() : new List<string>(extra_stop_sequences);
            final_stops.AddRange(cursettings.ExtraStopTokens);

            List<string> images = new List<string>();
            if (send_pic && MainForm.GetImage != null) {
                MainForm.NewImageToSend = false;
                images.Add(EncodeImage(MainForm.GetImage, IMGConfig.MaxResolution));
            }

            // Create the request body object.  Using anonymous objects is fine
            // if you don't need to reuse the type.  For more complex scenarios,
            // create a dedicated class.
            var requestBody = new {
                max_context_length = max_context_len,
                stop_sequence = final_stops.ToArray(),
                max_length = max_len ?? cursettings.MaxGeneration,
                prompt = prompt,         // Use provided value
                quiet = false,
                rep_pen = 1.0,
                rep_pen_range = 1024,
                rep_pen_slope = 0.7f,
                temperature = cursettings.Temperature,
                dry_multiplier = cursettings.DryMultiplier,
                dry_base = cursettings.DryBase,
                dry_allowed_length = 2,
                dry_sequence_breakers = new string[] { ":", "\n", "|" },
                banned_tokens = banned_tokens != null ? banned_tokens : new string[] { },
                top_a = 0,
                images = images.ToArray(),
                top_k = 0,
                min_p = cursettings.MinP,
                sampler_seed = -1,
                bypass_eos = skip_eos,
                top_p = cursettings.TopP //not_creative ? 0.7f : Program.MainForm.TopP
            };

            // Serialize the object to JSON and set it as the request body.
            // Option 1: System.Text.Json (preferred if available)
            string jsonBody = System.Text.Json.JsonSerializer.Serialize(requestBody);
            rr.AddParameter("application/json", jsonBody, ParameterType.RequestBody); //Important
            ExecuteRequest(rr);
        }

        public static string TestString = 
            "Another ancient form of short story popular during the Roman Empire was the anecdote, a brief realistic narrative that embodies a point. Many surviving Roman anecdotes were collected in the 13th or 14th century as the Gesta Romanorum. Anecdotes remained popular throughout Europe well into the 18th century with the publication of the fictional anecdotal letters of Sir Roger de Coverley.\n\nIn Europe, the oral story-telling tradition began to develop into written form in the early 14th century, most notably with Giovanni Boccaccio's Decameron and Geoffrey Chaucer's Canterbury Tales. Both of these books are composed of individual short stories, which range from farce or humorous anecdotes to well-crafted literary fiction, set within a larger narrative story (a frame story), although the frame-tale device was not adopted by all writers. At the end of the 16th century, some of the most popular short stories in Europe were the darkly tragic \"novella\" of Italian author Matteo Bandello, especially in their French translation.\n\nThe mid 17th century in France saw the development of a refined short novel, the \"nouvelle\", by such authors as Madame de Lafayette. Traditional fairy tales began to be published in the late 17th century; one of the most famous collections was by Charles Perrault. The appearance of Antoine Galland's first modern translation of the 1001 Arabian Nights, a storehouse of Middle Eastern folk and fairy tales, is the Thousand and One Nights (or Arabian Nights) (from 1704; another translation appeared in 1710–12). His translation would have an enormous influence on the 18th-century European short stories of Voltaire, Diderot and others.\n\nIn India, there is a rich heritage of ancient folktales as well as a compiled body of short fiction which shaped the sensibility of modern Indian short story. Some of the famous Sanskrit collections of legends, folktales, fairy tales, and fables are Panchatantra, Hitopadesha and Kathasaritsagara. Jataka tales, originally written in Pali, is a compilation of tales concerning the previous births of Lord Gautama Buddha. The Frame story, also known as the frame narrative or story within a story, is a narrative technique that probably originated in ancient Indian works such as Panchatantra.";
    }
}
