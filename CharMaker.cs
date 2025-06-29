﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using XmpCore.Impl;

namespace TalkerFrontend {
    public partial class CharMaker : Form {

        public static CharMaker instance;

        public CharMaker() {
            instance = this;
            InitializeComponent();
        }

        public Character LoadedCharacter;

        public void LoadCharacter(Character c) {
            LoadedCharacter = c;
            CharName.Text = c.Name;
            ImageFile.Text = LoadedCharacter.PictureFile;
            VisualDescription.Text = LoadedCharacter.VisualDescription?.Replace("\n", "\r\n") ?? "";
            PersistentDesc.Text = LoadedCharacter.PersistentDescription?.Replace("\n", "\r\n") ?? "";
            LongTermMemory.Text = LoadedCharacter.LongTermMemory_Raw?.Replace("\n", "\r\n") ?? "";
            VoiceText.Text = LoadedCharacter.VoiceText;
            VoiceWAV.Text = LoadedCharacter.VoiceWAV;
            voiceDesc.Text = LoadedCharacter.VoiceDescription;
            visual_style.Text = LoadedCharacter.ImageStyle;
            charchatlog.Text = LoadedCharacter.ChatLog?.Replace("\n", "\r\n") ?? "";
        }

        public void LoadOrCreateCharacter(string name = "") {
            LoadedCharacter = Character.EfficientLoadCharacter(LoadedCharacter, name, false);
            LoadCharacter(LoadedCharacter);
        }

        private void CharMaker_Load(object sender, EventArgs e) {

        }

        private void SaveButton_Click(object sender, EventArgs e) {
            if (CharName.Text.Trim().Length > 0 && LoadedCharacter != null) {
                LoadedCharacter.Name = CharName.Text;
                LoadedCharacter.VisualDescription = VisualDescription.Text.Replace("\r\n", "\n");
                LoadedCharacter.PersistentDescription = PersistentDesc.Text.Replace("\r\n", "\n");
                LoadedCharacter.PictureFile = ImageFile.Text;
                LoadedCharacter.LongTermMemory_Raw = LongTermMemory.Text.Replace("\r\n", "\n");
                LoadedCharacter.VoiceWAV = VoiceWAV.Text;
                LoadedCharacter.VoiceText = VoiceText.Text;
                LoadedCharacter.VoiceDescription = voiceDesc.Text;
                LoadedCharacter.ImageStyle = visual_style.Text;
                LoadedCharacter.ChatLog = charchatlog.Text.Replace("\r\n", "\n");
                LoadedCharacter.Save();
                Integration.MainForm.RefreshNames(false);
                Close();
            } else {
                MessageBox.Show("No name supplied for character, can't save...", "No Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void raw_data_Click(object sender, EventArgs e) {
            new Process {
                StartInfo = new ProcessStartInfo(LoadedCharacter.GetFilename()) {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void button1_Click(object sender, EventArgs e) {
            LoadOrCreateCharacter(CharName.Text);
        }

        private string SafeTryGet(JsonElement props, string tag) {
            if (props.TryGetProperty(tag, out var val))
                return val.GetString();

            return "";
        }

        private string SafeTryGetArray(JsonElement props, string tag) {
            if (props.TryGetProperty(tag, out var val)) {
                string retval = "";
                foreach(var obj in val.EnumerateArray()) {
                    retval += obj.GetString().Replace("\n", "\r\n");
                }
                return retval;
            }

            return "";
        }

        private void LoadJson(string json_data, string imagefn) {
            JsonElement properties = JsonSerializer.Deserialize<JsonElement>(json_data);
            string name = "";
            if (properties.TryGetProperty("data", out var dataproperties) &&
                dataproperties.TryGetProperty("name", out _)) {
                properties = dataproperties;
            }
            if (properties.TryGetProperty("name", out _) == false) {
                // still not sure on format... try kobold?
                PersistentDesc.Text = SafeTryGet(properties, "memory");
                if (properties.TryGetProperty("savedsettings", out var ss))
                    name = CharName.Text = SafeTryGet(ss, "chatopponent");
                else name = "UnknownName";
                charchatlog.Text = SafeTryGet(properties, "prompt") + "\r\n\r\n" + SafeTryGetArray(properties, "actions").Replace("{{[OUTPUT]}}", "\r\n\r\n" + name + ": ");
                LongTermMemory.Text = SafeTryGet(properties, "documentdb_data");
            } else {
                // typical format
                name = CharName.Text = SafeTryGet(properties, "name");
                string personality = SafeTryGet(properties, "personality");
                if (personality.Length == 0)
                    PersistentDesc.Text = SafeTryGet(properties, "description");
                else
                    PersistentDesc.Text = SafeTryGet(properties, "description") + "\n\n" + personality;
                charchatlog.Text = SafeTryGet(properties, "first_mes");
                LongTermMemory.Text = SafeTryGet(properties, "scenario");
            }
            if (imagefn != null && name.Length > 0) {
                string target_file = Path.Combine(Integration.CharDirectory, name + ".png");
                if (File.Exists(target_file) == false) File.Copy(imagefn, target_file);
                ImageFile.Text = name + ".png";
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (CharPNG.ShowDialog() == DialogResult.OK) {
                try {
                    if (CharPNG.FileName.ToLower().EndsWith("png")) {
                        string pnginfo = File.ReadAllText(CharPNG.FileName, Encoding.UTF8);
                        // find base64 string
                        int ext = pnginfo.IndexOf("EXt");
                        int base64start = pnginfo.IndexOf("\0", ext) + 1;
                        string b64data = StringProcessor.GetBase64(pnginfo, base64start);
                        if (b64data.Length > 0) {
                            var base64EncodedBytes = System.Convert.FromBase64String(b64data);
                            string json = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                            LoadJson(json, CharPNG.FileName);
                        }
                    } else {
                        // try to load json directly
                        LoadJson(File.ReadAllText(CharPNG.FileName), null);
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "Error Loading PNG/JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
