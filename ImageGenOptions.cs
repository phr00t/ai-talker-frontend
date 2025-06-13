using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkerFrontend {
    public partial class ImageGenOptions : Form {
        public ImageGenOptions() {
            InitializeComponent();
        }


        private void RefreshModelsWorkflows() {
            string model_dir_cp = Path.Combine(Integration.ComfyUIDir, "models/checkpoints");
            string model_dir_unet = Path.Combine(Integration.ComfyUIDir, "models/unet");
            List<string> models = new List<string>();
            if (Directory.Exists(model_dir_cp)) {
                models.AddRange(Directory.GetFiles(model_dir_cp, "*.safetensors", SearchOption.AllDirectories));
                models.AddRange(Directory.GetFiles(model_dir_cp, "*.gguf", SearchOption.AllDirectories));
            }
            if (Directory.Exists(model_dir_unet)) {
                models.AddRange(Directory.GetFiles(model_dir_unet, "*.safetensors", SearchOption.AllDirectories));
                models.AddRange(Directory.GetFiles(model_dir_unet, "*.gguf", SearchOption.AllDirectories));
            }
            Model.Items.Clear();
            for (int i=0; i<models.Count; i++) {
                models[i] = models[i].Replace(model_dir_cp + "\\", "").Replace(model_dir_unet + "\\", "");
                Model.Items.Add(models[i]);
            }
            Workflow.Items.Clear();
            string wf_dir = Path.Combine(Integration.BaseDirectory, "workflows");
            string[] wfs = Directory.GetFiles(wf_dir, "ImageGen*.json");
            foreach (var wf in wfs)
                Workflow.Items.Add(Path.GetFileName(wf));
        }

        private void LoadOptions() {
            if (Model.Items.Contains(Integration.CurrentImageOptions.Model ?? ""))
                Model.SelectedItem = Integration.CurrentImageOptions.Model;
            Resolution.Text = Integration.CurrentImageOptions.Size;
            Negative.Text = Integration.CurrentImageOptions.Negative;
            KillKobold.Checked = Integration.CurrentImageOptions.KillKobold;
            LocationWeight.Checked = Integration.CurrentImageOptions.LocationWeight;
            Steps.Text = Integration.CurrentImageOptions.Steps.ToString();
            if (Workflow.Items.Contains(Integration.CurrentImageOptions.Workflow))
                Workflow.SelectedItem = Integration.CurrentImageOptions.Workflow;
        }

        private void SaveOptions() {
            try {
                Integration.CurrentImageOptions.Workflow = Workflow.SelectedItem?.ToString() ?? "";
                Integration.CurrentImageOptions.Negative = Negative.Text;
                Integration.CurrentImageOptions.KillKobold = KillKobold.Checked;
                Integration.CurrentImageOptions.LocationWeight = LocationWeight.Checked;
                Integration.CurrentImageOptions.Size = Resolution.Text;
                Integration.CurrentImageOptions.Model = Model.SelectedItem?.ToString() ?? "";
                Integration.CurrentImageOptions.Steps = int.Parse(Steps.Text);
            } catch { }
            Integration.MainForm.SaveOptions();
        }

        private void ImageGenOptions_Load(object sender, EventArgs e) {
            RefreshModelsWorkflows();
            LoadOptions();
        }

        private void ImageGenOptions_FormClosing(object sender, FormClosingEventArgs e) {
            SaveOptions();
        }
    }
}
