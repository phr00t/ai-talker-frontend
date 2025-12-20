using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkerFrontend {
    public class ChatManager {

        public static int AutoTalkTimer;

        public static void ResetAutotalk() {
            AutoTalkTimer = (Integration.MainForm.GroupChatMode ? MainForm.Random.Next(3, 6) : MainForm.Random.Next(30, 150)) * 10;
        }

        public static string GroupChatLog;

        public static Character SelectedCharacter, MeCharacter;

        public static bool PictureRequested, ChatRequested, ImagePromptRequested;

        public static string[] BannedTalkTokens = new string[] {
            "[", "]", "(", ")", "_"
        };

        public static string[] StopSequences(bool forPictures) {
            List<string> stoppers = new List<string>() { "You:", "User:", "Human:", "Timestamp:", "Assistant:", "AI:", "P.S. ", "</think>",
                                                         "Your Response:", "User's Response:", "Human's Response:", "Assistant's Response:", "AI's Response:" };
            var charnames = Integration.MainForm.GetCharacterList();
            for (int i=0; i<charnames.Count; i++) {
                string stop_to_use = charnames[i] + ":";
                if (stoppers.Contains(stop_to_use) == false) {
                    stoppers.Add(stop_to_use);
                    stoppers.Add(charnames[i] + "'s Response:");
                    stoppers.Add(charnames[i] + "'s response:");
                    stoppers.Add(charnames[i] + "'s says:");
                    stoppers.Add(charnames[i] + " Response:");
                    stoppers.Add(charnames[i] + " response:");
                    stoppers.Add(charnames[i] + " recalled the following that");
                }
            }
            if (forPictures) {
                stoppers.Add("Location:");
                stoppers.Add("location:");
                stoppers.Add("END RESPONSE");
                stoppers.Add("RESPONSE END");
                stoppers.Add("End Response");
                stoppers.Add("Response End");
                stoppers.Add("response end");
            }
            return stoppers.ToArray();
        }

        public static string CurrentChatLog {
            get {
                if (Integration.MainForm.GroupChatMode)
                    return SelectedCharacter?.ProcessTags(GroupChatLog) ?? "";

                return SelectedCharacter?.ProcessTags(SelectedCharacter.ChatLog) ?? "";
            }
            set {
                if (Integration.MainForm.GroupChatMode)
                    GroupChatLog = value;
                else if (SelectedCharacter is Character c)
                    c.ChatLog = value;
            }
        }

        public static int CurrentChatLogIndex {
            get {
                if (Integration.MainForm.GroupChatMode)
                    return SelectedCharacter?.GroupChatLogMemoryPosition ?? 0;

                return SelectedCharacter?.ChatLogMemoryPosition ?? 0;
            }
            set {
                if (SelectedCharacter is Character c) {
                    if (Integration.MainForm.GroupChatMode)
                        c.GroupChatLogMemoryPosition = value;
                    else
                        c.ChatLogMemoryPosition = value;
                }
            }
        }

        public static void SetGroupChatMode() {
            if (Integration.MainForm.GroupChatMode) LoadGroupChat();
            Integration.MainForm.UpdateChatLog();
            SelectedCharacter?.UpdateLongTerm();
        }

        public static void LoadGroupChat() {
            string gcf = Path.Combine(Integration.BaseDirectory, "groupchat.txt");
            if (File.Exists(gcf))
                GroupChatLog = File.ReadAllText(gcf);
            else
                GroupChatLog = "";
        }

        public static void SendContinue() {
            Integration.MainForm.SelectNextCharacter();
            PictureRequested = false;
            ChatRequested = true;
            string prompt = PromptGenerator.GetMasterPrompt(SelectedCharacter, Integration.MainForm.GetControl<TextBox>("partial_response").Text, WhoTalking?.Name ?? "",
                                                            WhoTalking?.PersistentDescription ?? "", out _);
            WhoTalking = SelectedCharacter;
            Integration.MainForm.ClearMonitor();
            Integration.SendTextPrompt(prompt, null, false, false, false, StopSequences(false), BannedTalkTokens);
        }

        public static string previousYourPrompt;
        public static void SendChat(string request, bool send_pic, string image_description = null) {
            bool hasPicToSend = send_pic && MainForm.NewImageToSend && Integration.MainForm.GetImage != null;
            if (hasPicToSend && Integration.IMGConfig.UseExistingTextModel == false && !Integration.RemoteOnlyMode) {
                // uh oh, need to load the visual model to read this image before doing this!
                Integration.MainForm.DisableAutoTalk();
                Integration.KillKobold();
                previousYourPrompt = request;
                YourPrompt = null;
                YourImageDescription = null;
                PictureRequested = false;
                ChatRequested = false;
                ImagePromptRequested = true;
                AutoTalkTimer = 0;
                Integration.MainForm.SetStatus("Preprocessing Image");
                Task t = new Task(() => {
                    Thread.Sleep(3000);
                    Integration.EnsureKoboldCppMode(true, true, 500);
                    Thread.Sleep(3000);
                    Integration.SendTextPrompt(Integration.IMGConfig.ImagePrompt, 1024, true, true, false, StopSequences(false));
                });
                t.Start();
            } else {
                string MyName = MeCharacter?.Name ?? Integration.MainForm.GetControl<ComboBox>("MyName").Text.Trim();
                string MyDescription = Integration.MainForm.GetControl<TextBox>("MyRelation").Text.Trim();
                if (MeCharacter is Character cc) MyDescription += cc.ProcessTags(cc.PersistentDescription);
                string prompt = PromptGenerator.GetMasterPrompt(SelectedCharacter, request, MyName, MyDescription, out string append_log, image_description);
                CurrentChatLog += "\n\n" + append_log;
                Integration.MainForm.UpdateChatLog();
                PictureRequested = false;
                ChatRequested = true;
                ImagePromptRequested = false;
                AutoTalkTimer = 0;
                WhoTalking = SelectedCharacter;
                YourPrompt = null;
                YourImageDescription = null;
                Integration.MainForm.ClearMonitor();
                Integration.SendTextPrompt(prompt, null, false, hasPicToSend, false, StopSequences(false), BannedTalkTokens);
            }
        }

        public static void GetPicture() {
            Integration.autogen_timer = 50;
            PictureRequested = true;
            ChatRequested = false;
            ImagePromptRequested = false;
            Integration.MainForm.ClearMonitor();
            string prompt = PromptGenerator.GetPicturePrompt(SelectedCharacter, Integration.MainForm.GetControl<ComboBox>("MyName").Text.Trim());
            Integration.MainForm.SetStatus("Picture Description Generate");
            Integration.SendTextPrompt(prompt, 512, true, false, false, StopSequences(true), BannedTalkTokens);
        }

        public class AWAITSAY {
            public string What;
            public Character Who;
        }

        public static string YourPrompt = null, YourImageDescription = null;

        public static List<AWAITSAY> AwaitingSay = new List<AWAITSAY>();
        public static Character WhoTalking;
        public static HashSet<string> AlreadySaid = new HashSet<string>();

        public static List<AWAITSAY> ConsolidateAwaitSay() {
            List<AWAITSAY> consolidated = new List<AWAITSAY> {
                AwaitingSay[0]
            };
            Character whoIsSaying = consolidated[0].Who;
            for (int i=1;i<AwaitingSay.Count; i++) {
                if (AwaitingSay[i].Who == whoIsSaying) {
                    consolidated[consolidated.Count - 1].What += " " + AwaitingSay[i].What;
                } else {
                    consolidated.Add(AwaitingSay[i]);
                    whoIsSaying = AwaitingSay[i].Who;
                }
            }
            return consolidated;
        }

        public static void UpdateLiveVoice(string text, bool final) {
            if (Integration.MainForm.IsVoiceOn == false || text == null || text.Length <= 0) return;
            if (PictureRequested || ImagePromptRequested) return; // dont talk about picture descriptions
            string[] stops = new string[] { ".", "!", "?", "\n" };
            string[] sentences = text.Split(stops, StringSplitOptions.RemoveEmptyEntries);
            string full_line_to_say = "";
            for (int i = 0; i < sentences.Length - (final ? 0 : 1); i++) {
                if (AlreadySaid.Contains(sentences[i])) continue;
                full_line_to_say += sentences[i] + " ";
                AlreadySaid.Add(sentences[i]);
            }
            if (final) {
                AlreadySaid.Clear();
                AwaitingSay.Clear();
            }
            SayLine(full_line_to_say, WhoTalking);
        }

        public static bool SayTalking = false;
        public static bool SayGenerating = false;
        public static void UpdateSay() {
            // ready to talk?
            if (AwaitingSay.Count == 0) return;
            // ok, send comfy request to talk
            AwaitingSay = ConsolidateAwaitSay();
            AWAITSAY asay = AwaitingSay[0];
            string voicefn = asay.Who.GetWAV;
            bool hasVoiceReady = File.Exists(voicefn);// && asay.Who.VoiceText.Length > 0;
            if (asay.Who.VoiceDescription.Length > 0 || hasVoiceReady) {
                if (!SayGenerating && hasVoiceReady) {
                    AwaitingSay.RemoveAt(0);
                    Dictionary<string, string> repl = new Dictionary<string, string>();
                    repl["$SAMPLE_TEXT"] = asay.Who.VoiceText.Trim();
                    repl["$DIALOG"] = asay.What; //.ToLower(); // need tolower as all caps breaks F5-TTS
                    repl["$SEED"] = MainForm.Random.Next(99999999).ToString();
                    repl["$REF_AUDIO"] = voicefn.Replace("\\", "/");
                    repl["$SAVE_PREFIX"] = "talker/dialog";
                    Integration.MainForm.PrepareWatcher(Path.Combine(Integration.ComfyUIDir, "output/talker/"));
                    Integration.SendComfyRequest(Path.Combine(Integration.BaseDirectory, "workflows/VoiceGen-Dialog.json"), repl);
                    SayGenerating = true;
                }
            } else if (SayTalking == false) {
                AwaitingSay.RemoveAt(0);
                if (synthesizer == null) {
                    synthesizer = new SpeechSynthesizer();
                    synthesizer.SetOutputToDefaultAudioDevice();
                }
                SayTalking = true;
                Task t = new Task(() => {
                    synthesizer.Speak(asay.What.ToLower());
                    SayTalking = false;
                });
                t.Start();
            }
        }

        public static HashSet<Character> NeedVoiceFor = new HashSet<Character>();
        public static SpeechSynthesizer synthesizer;
        public static void SayLine(string message, Character who) {
            if (who == null || message == null || message.Length < 2) return;
            // remove all stop tokens from this
            foreach (string stopper in StopSequences(true))
                message = message.Replace(stopper, ".");
            // do we have an F5-ready voice?
            if (File.Exists(who.GetWAV) == false && who.VoiceDescription.Length > 0) {
                lock (NeedVoiceFor) {
                    if (NeedVoiceFor.Contains(who) == false) {
                        NeedVoiceFor.Add(who);
                        Dictionary<string, string> repl = new Dictionary<string, string>();
                        repl["${VOICE_DESCRIPTION}"] = who.VoiceDescription;
                        repl["${SAVE_PREFIX}"] = "talker/voicegen-" + who.Name + "--";
                        Integration.MainForm.SetStatus("Generating Voice");
                        Integration.MainForm.PrepareWatcher(Path.Combine(Integration.ComfyUIDir, "output/talker/"));
                        Integration.SendComfyRequest(Path.Combine(Integration.BaseDirectory, "workflows/VoiceGen-Voice.json"), repl);
                    }
                }
            }
            AwaitingSay.Add(new AWAITSAY() { What = message, Who = who });
        }
    }
}
