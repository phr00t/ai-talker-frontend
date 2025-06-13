using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkerFrontend {
    public partial class ImageReaderSettings : Form {
        public ImageReaderSettings() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                koboldvisualmodel.Text = openFileDialog1.FileName;
            }
        }

        private void ImageReaderSettings_Load(object sender, EventArgs e) {
            lastImage.Text = Integration.IMGConfig.LastImagePromptResult;
            imagemode.SelectedIndex = Integration.IMGConfig.UseExistingTextModel ? 0 : 1;
            koboldvisualmodel.Text = Integration.IMGConfig.KoboldCppVisualModel;
            maxres.Text = Integration.IMGConfig.MaxResolution.ToString();
            imageprompt.Text = Integration.IMGConfig.ImagePrompt;
        }

        private void ImageReaderSettings_FormClosing(object sender, FormClosingEventArgs e) {
            Integration.IMGConfig.UseExistingTextModel = imagemode.SelectedIndex == 0;
            Integration.IMGConfig.KoboldCppVisualModel = koboldvisualmodel.Text;
            if (int.TryParse(maxres.Text, out int newmaxres))
                Integration.IMGConfig.MaxResolution = newmaxres;
            else
                Integration.IMGConfig.MaxResolution = 896;
            Integration.IMGConfig.ImagePrompt = imageprompt.Text;
            Integration.MainForm.SaveOptions();
        }
    }
}
