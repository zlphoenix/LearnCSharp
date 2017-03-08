using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SolutionMaker.Core;
using SolutionMaker.Core.Model;

namespace SolutionMaker.UI
{
    public partial class MainForm : Form
    {
        private const string _createSolutionText = "Create Solution";
        private const string _updateSolutionText = "Update Solution";
        private readonly string[] _projectTypes = { "C#", "VB", "F#", "C++", "Wix" };

        public MainForm()
        {
            InitializeComponent();

            switch (Utils.GetPreferredSolutionFileVersion())
            {
                case SolutionFileVersion.VisualStudio2008:
                    this.radioVersion2008.Checked = true;
                    break;

                default:
                case SolutionFileVersion.VisualStudio2010:
                    this.radioVersion2010.Checked = true;
                    break;

                case SolutionFileVersion.VisualStudio2012:
                    this.radioVersion2012.Checked = true;
                    break;
            }

            LoadProjectTypes();

            this.checkRecursive.Checked = true;
            this.radioReplace.Checked = true;
            this.radioUseProjectNames.Checked = true;
            this.radioCommonPrefixLevels.Checked = true;
            this.listProjectTypes.Visible = false;

            this.labelStatus.Text = string.Empty;

            ValidateControls();
        }

        private void ValidateControls()
        {
            bool solutionExists = this.textSolutionFile.Text.Length > 0 && File.Exists(this.textSolutionFile.Text);
            this.buttonExecute.Enabled = this.textSolutionFile.Text.Length > 0 && this.textProjectRoot.Text.Length > 0;
            this.buttonPreview.Enabled = buttonExecute.Enabled;
            this.groupUpdateSettings.Enabled = solutionExists;
            this.buttonExecute.Text = solutionExists ? _updateSolutionText : _createSolutionText;
        }

        private SolutionUpdateMode GetUpdateMode()
        {
            if (this.radioAdd.Checked)
            {
                return SolutionUpdateMode.Add;
            }
            else if (this.radioAddRemove.Checked)
            {
                return SolutionUpdateMode.AddRemove;
            }
            else if (this.radioReplace.Checked)
            {
                return SolutionUpdateMode.Replace;
            }
            else
            {
                return 0;
            }
        }

        private void SetUpdateMode(SolutionUpdateMode updateMode)
        {
            switch (updateMode)
            {
                case SolutionUpdateMode.Add:
                    this.radioAdd.Checked = true;
                    break;
                case SolutionUpdateMode.AddRemove:
                    this.radioAddRemove.Checked = true;
                    break;
                case SolutionUpdateMode.Replace:
                    this.radioReplace.Checked = true;
                    break;
            }
        }

        private SolutionOptions LoadSettings(string settingsPath)
        {
            using (var reader = new StreamReader(settingsPath))
            {
                string settings = reader.ReadToEnd();
                return Serializer.FromXmlString<SolutionOptions>(settings);
            }
        }

        private void SaveSettings(SolutionOptions options, string settingsPath)
        {
            string settings = Serializer.ToXmlString(options);
            using (var writer = new StreamWriter(settingsPath))
            {
                writer.Write(settings);
            }
        }

        private void AssignOptionsToControls(SolutionOptions options)
        {
            this.textProjectRoot.Text = options.ProjectRootFolderPath;
            this.textIncludeFilter.Text = options.IncludeFilter;
            this.textExcludeFilter.Text = options.ExcludeFilter;
            this.checkRecursive.Checked = options.Recursive;
            this.checkIncludeReferences.Checked = options.IncludeReferences;
            this.textProjectTypes.Text = FormatProjectTypesOption(options.ProjectTypes);
            this.checkOverwriteReadonly.Checked = options.OverwriteReadOnlyFile;
            this.radioVersion2008.Checked = options.SolutionFileVersion == SolutionFileVersion.VisualStudio2008;
            this.radioVersion2010.Checked = options.SolutionFileVersion == SolutionFileVersion.VisualStudio2010;
            this.radioVersion2012.Checked = options.SolutionFileVersion == SolutionFileVersion.VisualStudio2012;
            this.numericSolutionFolderLevels.Value = options.SolutionFolderLevels;
            this.radioUseProjectNames.Checked = options.FolderNamingMode == SolutionFolderNamingMode.Project;
            this.radioUseAssemblyNames.Checked = options.FolderNamingMode == SolutionFolderNamingMode.Assembly;
            this.radioUseMostQualifiedNames.Checked = options.FolderNamingMode == SolutionFolderNamingMode.MostQualified;
            this.numericCommonPrefixLevels.Value = options.CommonPrefixLevels;
            this.radioCommonPrefixes.Checked = !string.IsNullOrEmpty(options.CommonPrefixes);
            this.textCommonPrefixes.Text = options.CommonPrefixes;

            SelectProjectTypes();
            SetUpdateMode(options.UpdateMode);
        }

        private SolutionOptions CreateOptionsFromControls()
        {
            SolutionOptions options = new SolutionOptions
            {
                SolutionFolderPath = Path.GetDirectoryName(this.textSolutionFile.Text),
                ProjectRootFolderPath = this.textProjectRoot.Text,
                IncludeFilter = this.textIncludeFilter.Text,
                ExcludeFilter = this.textExcludeFilter.Text,
                Recursive = this.checkRecursive.Checked,
                IncludeReferences = this.checkIncludeReferences.Checked,
                ProjectTypes = ParseProjectTypesOption(this.textProjectTypes.Text),
                OverwriteReadOnlyFile = this.checkOverwriteReadonly.Checked,
                SolutionFileVersion = this.radioVersion2012.Checked ? 
                    SolutionFileVersion.VisualStudio2012 : this.radioVersion2010.Checked ? 
                    SolutionFileVersion.VisualStudio2010 : 
                    SolutionFileVersion.VisualStudio2008,
                SolutionFolderLevels = (int)this.numericSolutionFolderLevels.Value,
                FolderNamingMode = this.radioUseProjectNames.Checked ? 
                    SolutionFolderNamingMode.Project : this.radioUseAssemblyNames.Checked ? 
                    SolutionFolderNamingMode.Assembly : 
                    SolutionFolderNamingMode.MostQualified,
                CommonPrefixLevels = (int)this.numericCommonPrefixLevels.Value,
                CommonPrefixes = this.textCommonPrefixes.Text,

                UpdateMode = GetUpdateMode()
            };
            return options;
        }

        private void LoadProjectTypes()
        {
            foreach (var projectType in _projectTypes)
            {
                this.listProjectTypes.Items.Add(projectType, true);
            }
        }

        private void SelectProjectTypes()
        {
            var items = this.textProjectTypes.Text.Split(',');
            for (int index = 0; index < this.listProjectTypes.Items.Count; index++)
            {
                var check = items.Contains("All") || items.Contains(this.listProjectTypes.Items[index].ToString());
                this.listProjectTypes.SetItemChecked(index, check);
            }
        }

        private void UpdateProjectTypes()
        {
            if (this.listProjectTypes.Visible)
            {
                if (this.listProjectTypes.CheckedItems.Count == this.listProjectTypes.Items.Count)
                    this.textProjectTypes.Text = "All";
                else
                    this.textProjectTypes.Text = string.Join(",", (from object item in this.listProjectTypes.CheckedItems select item.ToString()).ToArray());
                this.listProjectTypes.Visible = false;
                this.textProjectTypes.Visible = true;
                buttonProjectTypes.Text = "...";
            }
            else
            {
                this.textProjectTypes.Visible = false;
                this.listProjectTypes.Visible = true;
                buttonProjectTypes.Text = "X";
            }
        }

        private string FormatProjectTypesOption(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "All";
            }
            else
            {
                var result = _projectTypes.Aggregate(text, (current, projectType) => current.Replace(projectType, projectType + ","));
                if (result.EndsWith(","))
                    result = result.Substring(0, result.Length - 1);
                return result;
            }
        }

        private string ParseProjectTypesOption(string text)
        {
            return text.Replace(",", "");
        }

        private void textSolutionFile_TextChanged(object sender, EventArgs e)
        {
            ValidateControls();
        }

        private void textProjectRoot_TextChanged(object sender, EventArgs e)
        {
            ValidateControls();
        }

        private void buttonSolutionFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Solution files (*.sln)|*.sln";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textSolutionFile.Text = dialog.FileName;

                if (File.Exists(this.textSolutionFile.Text))
                {
                    SolutionReader reader = new SolutionReader();
                    var solution = reader.Read(this.textSolutionFile.Text);
                    if (solution.FileVersion == SolutionFileVersion.VisualStudio2008)
                    {
                        this.radioVersion2008.Checked = true;
                    }
                    else if (solution.FileVersion == SolutionFileVersion.VisualStudio2010)
                    {
                        this.radioVersion2010.Checked = true;
                    }
                    else if (solution.FileVersion == SolutionFileVersion.VisualStudio2012)
                    {
                        this.radioVersion2012.Checked = true;
                    }

                    string settingsPath = Path.ChangeExtension(this.textSolutionFile.Text, SolutionOptions.FileExtension);
                    if (File.Exists(settingsPath))
                    {
                        var options = LoadSettings(settingsPath);
                        AssignOptionsToControls(options);
                        this.checkSaveSettings.Checked = true;
                    }
                }
            }

            ValidateControls();
        }

        private void buttonProjectRoot_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (!string.IsNullOrEmpty(this.textProjectRoot.Text))
            {
                dialog.SelectedPath = this.textProjectRoot.Text;
            }
            else if (string.IsNullOrEmpty(this.textProjectRoot.Text) && !string.IsNullOrEmpty(this.textSolutionFile.Text))
            {
                dialog.SelectedPath = Path.GetDirectoryName(this.textSolutionFile.Text);
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textProjectRoot.Text = dialog.SelectedPath;
            }
            
            ValidateControls();
        }

        private void buttonProjectTypes_Click(object sender, EventArgs e)
        {
            UpdateProjectTypes();
        }

        private void listProjectTypes_Leave(object sender, EventArgs e)
        {
            if (!this.buttonProjectTypes.Focused)
                UpdateProjectTypes();
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            try
            {
                string message;

                SolutionOptions options = CreateOptionsFromControls();
                SolutionGenerator generator = new SolutionGenerator(new StatusLogger(this.labelStatus));
                var solution = generator.GenerateSolution(this.textSolutionFile.Text, options, true);
                this.labelStatus.Text = string.Empty;

                if (generator.NumberOfProjectsFound == 0)
                {
                    message = string.Format("No project files found in the specified location\r\nSolution file is not generated");
                    MessageBox.Show(message, "Execution Summary", MessageBoxButtons.OK);
                }
                else
                {
                    var previewForm = new PreviewForm();
                    previewForm.Solution = solution;
                    previewForm.SolutionFile = this.textSolutionFile.Text;
                    previewForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                string message;

                if (File.Exists(this.textSolutionFile.Text))
                {
                    var fileAttributes = File.GetAttributes(this.textSolutionFile.Text);
                    if ((fileAttributes & FileAttributes.ReadOnly) != 0)
                    {
                        message = "The solution file is read only\r\nDo you want to overwrite it?";
                        if (MessageBox.Show(message, "Confirm Overwrite", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            File.SetAttributes(this.textSolutionFile.Text, fileAttributes ^ FileAttributes.ReadOnly);
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                SolutionOptions options = CreateOptionsFromControls();
                SolutionGenerator generator = new SolutionGenerator(new StatusLogger(this.labelStatus));
                generator.GenerateSolution(this.textSolutionFile.Text, options);
                this.labelStatus.Text = string.Empty;

                if (generator.NumberOfProjectsFound > 0)
                {
                    message = string.Format("Solution file is generated\r\n\r\n{0} projects found\r\n{1} projects skipped\r\n{2} projects added\r\n{3} projects removed", 
                        generator.NumberOfProjectsFound, generator.NumberOfProjectsSkipped, generator.NumberOfProjectsAdded, generator.NumberOfProjectsRemoved);
                }
                else
                {
                    message = string.Format("No project files found in the specified location\r\nSolution file is not generated");
                }

                if (this.checkSaveSettings.Checked)
                {
                    SaveSettings(options, Path.ChangeExtension(this.textSolutionFile.Text, SolutionOptions.FileExtension));
                }

                MessageBox.Show(message, "Execution Summary", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
