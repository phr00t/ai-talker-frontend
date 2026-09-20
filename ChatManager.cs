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

        public static bool PictureRequested, ChatRequested, ImagePromptRequested, KeywordsRequested;

        public static string[] BannedTalkTokens = new string[] {
            "[", "]", "(", ")", "_"
        };

        public static string[] StopSequences(bool forPictures) {
            List<string> stoppers = new List<string>() { "You:", "User:", "Human:", "Timestamp:", "Assistant:", "AI:", "P.S. ",
                                                         "Your Response:", "User's Response:", "Human's Response:", "Assistant's Response:", "AI's Response:" };
            var charnames = Integration.MainForm.GetCharacterList();
            stoppers.Add("Response Completed");
            stoppers.Add("RESPONSE COMPLETED");
            stoppers.Add("Completed Response");
            stoppers.Add("COMPLETED RESPONSE");
            for (int i=0; i<charnames.Count; i++) {
                string stop_to_use = charnames[i] + "'s Response:"; // charnames[i] + ":";
                if (stoppers.Contains(stop_to_use) == false) {
                    stoppers.Add(stop_to_use);
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
                    return Character.ProcessTags(GroupChatLog) ?? "";

                return Character.ProcessTags(SelectedCharacter.ChatLog) ?? "";
            }
            set {
                if (Integration.MainForm.GroupChatMode)
                    GroupChatLog = value;
                else if (SelectedCharacter is Character c)
                    c.ChatLog = value;
            }
        }

        /*public static int CurrentChatLogIndex {
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
        }*/

        public static void SetGroupChatMode() {
            if (Integration.MainForm.GroupChatMode) LoadGroupChat();
            Integration.MainForm.UpdateChatLog();
            //SelectedCharacter?.UpdateLongTerm();
        }

        public static void LoadGroupChat() {
            string gcf = Path.Combine(Integration.BaseDirectory, "groupchat.txt");
            if (File.Exists(gcf))
                GroupChatLog = File.ReadAllText(gcf);
            else
                GroupChatLog = "";
        }

        public static string previousYourPrompt, previousYourThink;
        public static void SendChat(string request, string think, bool send_pic, string image_description = null, string keywords_provided = null, Character group_person = null) {
            bool hasPicToSend = send_pic && MainForm.NewImageToSend && Integration.MainForm.GetImage != null;
            // if there isn't any request to get keywords from, skip that step (by providing a non-null variable)
            if (keywords_provided == null && (request == null || request.Trim().Length == 0)) keywords_provided = "";
            if (hasPicToSend && Integration.IMGConfig.UseExistingTextModel == false && !Integration.RemoteOnlyMode) {
                // uh oh, need to load the visual model to read this image before doing this!
                Integration.MainForm.DisableAutoTalk();
                Integration.KillKobold();
                GroupTalkingPerson = group_person;
                previousYourPrompt = request;
                previousYourThink = think;
                YourPrompt = null;
                YourThinkInjection = null;
                YourImageDescription = null;
                PictureRequested = false;
                ChatRequested = false;
                ImagePromptRequested = true;
                KeywordsRequested = false;
                AutoTalkTimer = 0;
                Integration.MainForm.SetStatus("Preprocessing Image");
                Task t = new Task(() => {
                    Thread.Sleep(3000);
                    Integration.EnsureKoboldCppMode(true, true, 500);
                    Thread.Sleep(3000);
                    Integration.SendTextPrompt(Integration.IMGConfig.ImagePrompt, "Image Description: ", 1024, true, Integration.SEND_PIC_TYPE.SendClear, false, StopSequences(false));
                });
                t.Start();
            } else if (Integration.MainForm.PostProcessPrompt && keywords_provided == null) {
                ChatRequested = false;
                KeywordsRequested = true;
                GroupTalkingPerson = group_person;
                ImagePromptRequested = false;
                AutoTalkTimer = 0;
                string prompt = PromptGenerator.GetRAGKeywords(request, out string preload);
                Integration.MainForm.ClearMonitor();
                int len = (int)Math.Round(0.5f * (request.Length / Integration.CharactersPerToken + 256));
                if (len > 512)
                    len = 512;
                else if (len < 128)
                    len = 128;
                Integration.SendTextPrompt(prompt, preload, len, true, Integration.SEND_PIC_TYPE.SendNoClear, false, new string[] { "Terms Finished", "Finished Terms", "TERMS FINISHED", "terms finished", "finished terms", "FINISHED TERMS" });
            } else if (Integration.MainForm.PostProcessPrompt == false || keywords_provided != null) {
                string MyName = group_person?.Name ?? GroupTalkingPerson?.Name ?? MeCharacter?.Name ?? Integration.MainForm.GetControl<ComboBox>("MyName").Text.Trim();
                string MyDescription = group_person?.PersistentDescription ?? GroupTalkingPerson?.PersistentDescription ?? Integration.MainForm.GetControl<TextBox>("MyRelation").Text.Trim();
                if (MeCharacter is Character cc) MyDescription += Character.ProcessTags(cc.PersistentDescription);
                (string prompt, string preload) = PromptGenerator.GetMasterPrompt(SelectedCharacter, request, MyName, MyDescription, out string append_log, image_description, keywords_provided);
                if (GroupTalkingPerson == null && group_person == null) {
                    // stuff you wrote (not written in a group chat)
                    CurrentChatLog += "\n\n" + append_log;
                    Integration.MainForm.UpdateChatLog();
                }
                PictureRequested = false;
                ChatRequested = true;
                KeywordsRequested = false;
                ImagePromptRequested = false;
                AutoTalkTimer = 0;
                WhoTalking = SelectedCharacter;
                YourThinkInjection = null;
                YourPrompt = null;
                GroupTalkingPerson = null;
                YourImageDescription = null;
                Integration.MainForm.ClearMonitor();
                Integration.SendTextPrompt(prompt, preload, null, false, hasPicToSend ? Integration.SEND_PIC_TYPE.SendClear : Integration.SEND_PIC_TYPE.None , false, StopSequences(false), BannedTalkTokens, think);
            }
        }

        public static void GetPicture() {
            Integration.autogen_timer = 50;
            PictureRequested = true;
            ChatRequested = false;
            KeywordsRequested = false;
            ImagePromptRequested = false;
            Integration.MainForm.ClearMonitor();
            string prompt = PromptGenerator.GetPicturePrompt(SelectedCharacter, Integration.MainForm.GetControl<ComboBox>("MyName").Text.Trim());
            Integration.MainForm.SetStatus("Picture Description Generate");
            Integration.SendTextPrompt(prompt, "Completed Formatted Picture Description:\n\nLocation: ", 512, true, Integration.SEND_PIC_TYPE.None, false, StopSequences(true), BannedTalkTokens);
        }

        public class AWAITSAY {
            public string What;
            public Character Who;
        }

        public static string YourPrompt = null, YourImageDescription = null, YourThinkInjection = null;
        public static Character GroupTalkingPerson = null;

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
            if (Integration.VerifyComfyUI(false) && hasVoiceReady) {
                if (!SayGenerating) {
                    AwaitingSay.RemoveAt(0);
                    Dictionary<string, string> repl = new Dictionary<string, string>();
                    repl["$SAMPLE_TEXT"] = asay.Who.VoiceText.Trim();
                    repl["$DIALOG"] = asay.What;
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
                    synthesizer.SelectVoiceByHints(asay.Who.VoiceDescription.IndexOf("female", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                                   asay.Who.VoiceDescription.IndexOf("girl", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                                   asay.Who.VoiceDescription.IndexOf("mother", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                                   asay.Who.VoiceDescription.IndexOf("grandma", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                                   asay.Who.VoiceDescription.IndexOf("woman", StringComparison.CurrentCultureIgnoreCase) >= 0 ? VoiceGender.Female : VoiceGender.Male);
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
            AwaitingSay.Add(new AWAITSAY() { What = message, Who = who });
        }
    }
}
