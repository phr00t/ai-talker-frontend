using DShowNET.Dvd;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace TalkerFrontend {
    public class Character {

        public class CharFile {
            public string full_filename;
            public string short_name => Path.GetFileNameWithoutExtension(full_filename);

            public override string ToString() {
                return short_name;
            }
        }

        public static string GetCharacterVisuals(string name) {
            string filename = Path.Combine(Integration.CharDirectory, name.Trim() + ".txt");
            if (!File.Exists(filename)) return null;
            string data = File.ReadAllText(filename);
            string visuals = Integration.LoadTagged(data, "VisualDescription");
            if (visuals == null || visuals.Length <= 1) return null;
            return visuals + " named ";
        }

        public void AttemptLoad() {
            string filename = Path.Combine(Integration.CharDirectory, Name.Trim() + ".txt");
            if (File.Exists(filename)) {
                string data = File.ReadAllText(filename);
                Name = Integration.LoadTagged(data, "Name") ?? "Assistant";
                VoiceWAV = Integration.LoadTagged(data, "VoiceWAV") ?? "";
                VoiceText = Integration.LoadTagged(data, "VoiceText") ?? "";
                PictureFile = Integration.LoadTagged(data, "ImageFile") ?? "";
                VisualDescription = Integration.LoadTagged(data, "VisualDescription") ?? "";
                PersistentDescription = Integration.LoadTagged(data, "PersistentDesc") ?? "";
                LongTermMemory_Raw = Integration.LoadTagged(data, "LongTermMemory") ?? "";
                ChatLog = Integration.LoadTagged(data, "ChatLog") ?? "";
                LastLongTermLogUsed = Integration.LoadTagged(data, "LastLongTermLogUsed") ?? "";
                VoiceDescription = Integration.LoadTagged(data, "VoiceDescription") ?? "";
                ImageStyle = Integration.LoadTagged(data, "ImageStyle") ?? "";
            }
        }

        public static Character EfficientLoadCharacter(Character existing_loaded, string name, bool force) {
            Character c = new Character(name);
            c.AttemptLoad();
            if (existing_loaded != null && name == existing_loaded.Name)
                c.LongTermMemory = existing_loaded.LongTermMemory;
            c.UpdateLongTerm();
            return c;
        }

        public void UpdateLongTerm() {
            string long_term_string = (Integration.MainForm.GroupChatMode ? ProcessTags(ChatManager.GroupChatLog) + " " : "") + ProcessTags(LongTermMemory_Raw) + " " + ProcessTags(ChatLog);
            if (long_term_string.Length != LastLongTermLogUsed.Length ||
                long_term_string != LastLongTermLogUsed ||
                LongTermMemory == null) {
                LongTermMemory = StringProcessor.GenerateLongTerm(long_term_string);
                LastLongTermLogUsed = long_term_string;
                Save();
            }
        }

        public Character(string name) {
            Name = name;
        }

        public string GetFilename() {
            return Path.Combine(Integration.CharDirectory, Name.Trim() + ".txt");
        }

        public void Save() {
            string data = Integration.StringTagged(Name, "Name");
            data += Integration.StringTagged(PictureFile, "ImageFile");
            data += Integration.StringTagged(VoiceWAV, "VoiceWAV");
            data += Integration.StringTagged(VoiceText, "VoiceText");
            data += Integration.StringTagged(VisualDescription, "VisualDescription");
            data += Integration.StringTagged(PersistentDescription, "PersistentDesc");
            data += Integration.StringTagged(LongTermMemory_Raw, "LongTermMemory");
            data += Integration.StringTagged(ChatLog, "ChatLog");
            data += Integration.StringTagged(LastLongTermLogUsed, "LastLongTermLogUsed");
            data += Integration.StringTagged(ImageStyle, "ImageStyle");
            data += Integration.StringTagged(VoiceDescription, "VoiceDescription");
            Directory.CreateDirectory(Integration.CharDirectory);
            string fn = Path.Combine(Integration.CharDirectory, Name.Trim() + ".txt");
            File.WriteAllText(fn, data);
        }

        public string GetPicture {
            get {
                if (File.Exists(PictureFile))
                    return PictureFile;

                string pic = Path.Combine(Integration.CharDirectory, PictureFile);
                if (File.Exists(pic))
                    return pic;

                string pic2 = Path.Combine(Integration.CharDirectory, Name + ".png");
                if (File.Exists(pic2))
                    return pic2;

                string pic3 = Path.Combine(Integration.CharDirectory, Name + ".jpg");
                if (File.Exists(pic3))
                    return pic3;

                return "";
            }
        }

        public string GetWAV {
            get {
                if (File.Exists(VoiceWAV))
                    return VoiceWAV;

                string wav = Path.Combine(Integration.CharDirectory, VoiceWAV);
                if (File.Exists(wav))
                    return wav;

                string pic2 = Path.Combine(Integration.CharDirectory, Name + ".flac");
                if (File.Exists(pic2))
                    return pic2;

                string pic3 = Path.Combine(Integration.CharDirectory, Name + ".wav");
                if (File.Exists(pic3))
                    return pic3;

                return "";
            }
        }

        public string PictureFile, VisualDescription, VoiceWAV, VoiceText;
        public string Name, ChatLog, LongTermMemory_Raw, LastLongTermLogUsed;
        public string PersistentDescription, VoiceDescription, ImageStyle;
        public int ChatLogMemoryPosition, GroupChatLogMemoryPosition;
        public ConcurrentDictionary<string, List<string>> LongTermMemory;

        public string ProcessTags(string text) {
            if (text.Length > 36000) return text; // if we have tons of text, this probably is not tagged and a data dump
            return text.Replace("{{user}}", Integration.MainForm.UserName)
                       .Replace("{{User}}", Integration.MainForm.UserName)
                       .Replace("{{Char}}", Integration.MainForm.CharName)
                       .Replace("{{char}}", Integration.MainForm.CharName);
        }

    }
}
