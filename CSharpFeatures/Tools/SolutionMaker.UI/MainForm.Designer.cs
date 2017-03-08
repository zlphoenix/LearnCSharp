namespace SolutionMaker.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textSolutionFile = new System.Windows.Forms.TextBox();
            this.buttonSolutionFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textProjectRoot = new System.Windows.Forms.TextBox();
            this.buttonProjectRoot = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textIncludeFilter = new System.Windows.Forms.TextBox();
            this.textExcludeFilter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericSolutionFolderLevels = new System.Windows.Forms.NumericUpDown();
            this.radioAdd = new System.Windows.Forms.RadioButton();
            this.groupUpdateSettings = new System.Windows.Forms.GroupBox();
            this.radioReplace = new System.Windows.Forms.RadioButton();
            this.radioAddRemove = new System.Windows.Forms.RadioButton();
            this.checkOverwriteReadonly = new System.Windows.Forms.CheckBox();
            this.checkSaveSettings = new System.Windows.Forms.CheckBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkRecursive = new System.Windows.Forms.CheckBox();
            this.groupSolutionFileVersion = new System.Windows.Forms.GroupBox();
            this.radioVersion2012 = new System.Windows.Forms.RadioButton();
            this.radioVersion2010 = new System.Windows.Forms.RadioButton();
            this.radioVersion2008 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textCommonPrefixes = new System.Windows.Forms.TextBox();
            this.numericCommonPrefixLevels = new System.Windows.Forms.NumericUpDown();
            this.radioCommonPrefixes = new System.Windows.Forms.RadioButton();
            this.radioCommonPrefixLevels = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioUseMostQualifiedNames = new System.Windows.Forms.RadioButton();
            this.radioUseProjectNames = new System.Windows.Forms.RadioButton();
            this.radioUseAssemblyNames = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listProjectTypes = new System.Windows.Forms.CheckedListBox();
            this.buttonProjectTypes = new System.Windows.Forms.Button();
            this.checkIncludeReferences = new System.Windows.Forms.CheckBox();
            this.textProjectTypes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericSolutionFolderLevels)).BeginInit();
            this.groupUpdateSettings.SuspendLayout();
            this.groupSolutionFileVersion.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCommonPrefixLevels)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Solution file:";
            // 
            // textSolutionFile
            // 
            this.textSolutionFile.Location = new System.Drawing.Point(14, 45);
            this.textSolutionFile.Name = "textSolutionFile";
            this.textSolutionFile.Size = new System.Drawing.Size(383, 20);
            this.textSolutionFile.TabIndex = 1;
            this.textSolutionFile.TextChanged += new System.EventHandler(this.textSolutionFile_TextChanged);
            // 
            // buttonSolutionFile
            // 
            this.buttonSolutionFile.Location = new System.Drawing.Point(322, 17);
            this.buttonSolutionFile.Name = "buttonSolutionFile";
            this.buttonSolutionFile.Size = new System.Drawing.Size(75, 23);
            this.buttonSolutionFile.TabIndex = 2;
            this.buttonSolutionFile.Text = "Select...";
            this.buttonSolutionFile.UseVisualStyleBackColor = true;
            this.buttonSolutionFile.Click += new System.EventHandler(this.buttonSolutionFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Project root path:";
            // 
            // textProjectRoot
            // 
            this.textProjectRoot.Location = new System.Drawing.Point(14, 102);
            this.textProjectRoot.Name = "textProjectRoot";
            this.textProjectRoot.Size = new System.Drawing.Size(383, 20);
            this.textProjectRoot.TabIndex = 3;
            this.textProjectRoot.TextChanged += new System.EventHandler(this.textProjectRoot_TextChanged);
            // 
            // buttonProjectRoot
            // 
            this.buttonProjectRoot.Location = new System.Drawing.Point(322, 71);
            this.buttonProjectRoot.Name = "buttonProjectRoot";
            this.buttonProjectRoot.Size = new System.Drawing.Size(75, 23);
            this.buttonProjectRoot.TabIndex = 4;
            this.buttonProjectRoot.Text = "Browse...";
            this.buttonProjectRoot.UseVisualStyleBackColor = true;
            this.buttonProjectRoot.Click += new System.EventHandler(this.buttonProjectRoot_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Project include filter (semicolon-separated):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Project exclude filter (semicolon-separated):";
            // 
            // textIncludeFilter
            // 
            this.textIncludeFilter.Location = new System.Drawing.Point(14, 204);
            this.textIncludeFilter.Name = "textIncludeFilter";
            this.textIncludeFilter.Size = new System.Drawing.Size(383, 20);
            this.textIncludeFilter.TabIndex = 6;
            // 
            // textExcludeFilter
            // 
            this.textExcludeFilter.Location = new System.Drawing.Point(14, 250);
            this.textExcludeFilter.Name = "textExcludeFilter";
            this.textExcludeFilter.Size = new System.Drawing.Size(383, 20);
            this.textExcludeFilter.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Solution folder levels:";
            // 
            // numericSolutionFolderLevels
            // 
            this.numericSolutionFolderLevels.Location = new System.Drawing.Point(124, 19);
            this.numericSolutionFolderLevels.Name = "numericSolutionFolderLevels";
            this.numericSolutionFolderLevels.Size = new System.Drawing.Size(30, 20);
            this.numericSolutionFolderLevels.TabIndex = 7;
            // 
            // radioAdd
            // 
            this.radioAdd.AutoSize = true;
            this.radioAdd.Location = new System.Drawing.Point(14, 23);
            this.radioAdd.Name = "radioAdd";
            this.radioAdd.Size = new System.Drawing.Size(300, 17);
            this.radioAdd.TabIndex = 11;
            this.radioAdd.TabStop = true;
            this.radioAdd.Text = "Only add matching projects that are not part of the solution";
            this.radioAdd.UseVisualStyleBackColor = true;
            // 
            // groupUpdateSettings
            // 
            this.groupUpdateSettings.Controls.Add(this.radioReplace);
            this.groupUpdateSettings.Controls.Add(this.radioAddRemove);
            this.groupUpdateSettings.Controls.Add(this.checkOverwriteReadonly);
            this.groupUpdateSettings.Controls.Add(this.radioAdd);
            this.groupUpdateSettings.Location = new System.Drawing.Point(437, 236);
            this.groupUpdateSettings.Name = "groupUpdateSettings";
            this.groupUpdateSettings.Size = new System.Drawing.Size(410, 119);
            this.groupUpdateSettings.TabIndex = 10;
            this.groupUpdateSettings.TabStop = false;
            this.groupUpdateSettings.Text = "Select how to update existing solution file";
            // 
            // radioReplace
            // 
            this.radioReplace.AutoSize = true;
            this.radioReplace.Location = new System.Drawing.Point(14, 69);
            this.radioReplace.Name = "radioReplace";
            this.radioReplace.Size = new System.Drawing.Size(342, 17);
            this.radioReplace.TabIndex = 13;
            this.radioReplace.TabStop = true;
            this.radioReplace.Text = "Discard and re-create solution content based on the specified filters";
            this.radioReplace.UseVisualStyleBackColor = true;
            // 
            // radioAddRemove
            // 
            this.radioAddRemove.AutoSize = true;
            this.radioAddRemove.Location = new System.Drawing.Point(14, 46);
            this.radioAddRemove.Name = "radioAddRemove";
            this.radioAddRemove.Size = new System.Drawing.Size(296, 17);
            this.radioAddRemove.TabIndex = 12;
            this.radioAddRemove.TabStop = true;
            this.radioAddRemove.Text = "Add matching projects and remove non-matching projects";
            this.radioAddRemove.UseVisualStyleBackColor = true;
            // 
            // checkOverwriteReadonly
            // 
            this.checkOverwriteReadonly.AutoSize = true;
            this.checkOverwriteReadonly.Location = new System.Drawing.Point(14, 92);
            this.checkOverwriteReadonly.Name = "checkOverwriteReadonly";
            this.checkOverwriteReadonly.Size = new System.Drawing.Size(133, 17);
            this.checkOverwriteReadonly.TabIndex = 21;
            this.checkOverwriteReadonly.Text = "Overwrite read-only file";
            this.checkOverwriteReadonly.UseVisualStyleBackColor = true;
            // 
            // checkSaveSettings
            // 
            this.checkSaveSettings.AutoSize = true;
            this.checkSaveSettings.Location = new System.Drawing.Point(26, 365);
            this.checkSaveSettings.Name = "checkSaveSettings";
            this.checkSaveSettings.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkSaveSettings.Size = new System.Drawing.Size(145, 17);
            this.checkSaveSettings.TabIndex = 22;
            this.checkSaveSettings.Text = "Save solution file settings";
            this.checkSaveSettings.UseVisualStyleBackColor = true;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(27, 394);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(37, 13);
            this.labelStatus.TabIndex = 26;
            this.labelStatus.Text = "Status";
            // 
            // checkRecursive
            // 
            this.checkRecursive.AutoSize = true;
            this.checkRecursive.Location = new System.Drawing.Point(14, 138);
            this.checkRecursive.Name = "checkRecursive";
            this.checkRecursive.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkRecursive.Size = new System.Drawing.Size(203, 17);
            this.checkRecursive.TabIndex = 5;
            this.checkRecursive.Text = "Scan project root directory recursively";
            this.checkRecursive.UseVisualStyleBackColor = true;
            // 
            // groupSolutionFileVersion
            // 
            this.groupSolutionFileVersion.Controls.Add(this.radioVersion2012);
            this.groupSolutionFileVersion.Controls.Add(this.radioVersion2010);
            this.groupSolutionFileVersion.Controls.Add(this.radioVersion2008);
            this.groupSolutionFileVersion.Location = new System.Drawing.Point(12, 302);
            this.groupSolutionFileVersion.Name = "groupSolutionFileVersion";
            this.groupSolutionFileVersion.Size = new System.Drawing.Size(410, 53);
            this.groupSolutionFileVersion.TabIndex = 8;
            this.groupSolutionFileVersion.TabStop = false;
            this.groupSolutionFileVersion.Text = "Select Visual Studio solution file format for newly created solution file";
            // 
            // radioVersion2012
            // 
            this.radioVersion2012.AutoSize = true;
            this.radioVersion2012.Location = new System.Drawing.Point(290, 23);
            this.radioVersion2012.Name = "radioVersion2012";
            this.radioVersion2012.Size = new System.Drawing.Size(113, 17);
            this.radioVersion2012.TabIndex = 2;
            this.radioVersion2012.TabStop = true;
            this.radioVersion2012.Text = "Visual Studio 2012";
            this.radioVersion2012.UseVisualStyleBackColor = true;
            // 
            // radioVersion2010
            // 
            this.radioVersion2010.AutoSize = true;
            this.radioVersion2010.Location = new System.Drawing.Point(152, 23);
            this.radioVersion2010.Name = "radioVersion2010";
            this.radioVersion2010.Size = new System.Drawing.Size(113, 17);
            this.radioVersion2010.TabIndex = 1;
            this.radioVersion2010.TabStop = true;
            this.radioVersion2010.Text = "Visual Studio 2010";
            this.radioVersion2010.UseVisualStyleBackColor = true;
            // 
            // radioVersion2008
            // 
            this.radioVersion2008.AutoSize = true;
            this.radioVersion2008.Location = new System.Drawing.Point(14, 23);
            this.radioVersion2008.Name = "radioVersion2008";
            this.radioVersion2008.Size = new System.Drawing.Size(113, 17);
            this.radioVersion2008.TabIndex = 0;
            this.radioVersion2008.TabStop = true;
            this.radioVersion2008.Text = "Visual Studio 2008";
            this.radioVersion2008.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericSolutionFolderLevels);
            this.groupBox1.Location = new System.Drawing.Point(437, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 211);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solution folders";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textCommonPrefixes);
            this.panel2.Controls.Add(this.numericCommonPrefixLevels);
            this.panel2.Controls.Add(this.radioCommonPrefixes);
            this.panel2.Controls.Add(this.radioCommonPrefixLevels);
            this.panel2.Location = new System.Drawing.Point(7, 118);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(397, 83);
            this.panel2.TabIndex = 15;
            // 
            // textCommonPrefixes
            // 
            this.textCommonPrefixes.Location = new System.Drawing.Point(7, 57);
            this.textCommonPrefixes.Name = "textCommonPrefixes";
            this.textCommonPrefixes.Size = new System.Drawing.Size(379, 20);
            this.textCommonPrefixes.TabIndex = 3;
            // 
            // numericCommonPrefixLevels
            // 
            this.numericCommonPrefixLevels.Location = new System.Drawing.Point(356, 6);
            this.numericCommonPrefixLevels.Name = "numericCommonPrefixLevels";
            this.numericCommonPrefixLevels.Size = new System.Drawing.Size(30, 20);
            this.numericCommonPrefixLevels.TabIndex = 2;
            // 
            // radioCommonPrefixes
            // 
            this.radioCommonPrefixes.AutoSize = true;
            this.radioCommonPrefixes.Location = new System.Drawing.Point(7, 32);
            this.radioCommonPrefixes.Name = "radioCommonPrefixes";
            this.radioCommonPrefixes.Size = new System.Drawing.Size(254, 17);
            this.radioCommonPrefixes.TabIndex = 1;
            this.radioCommonPrefixes.TabStop = true;
            this.radioCommonPrefixes.Text = "Exclude common prefixes (semicolon-separated):";
            this.radioCommonPrefixes.UseVisualStyleBackColor = true;
            // 
            // radioCommonPrefixLevels
            // 
            this.radioCommonPrefixLevels.AutoSize = true;
            this.radioCommonPrefixLevels.Location = new System.Drawing.Point(7, 9);
            this.radioCommonPrefixLevels.Name = "radioCommonPrefixLevels";
            this.radioCommonPrefixLevels.Size = new System.Drawing.Size(325, 17);
            this.radioCommonPrefixLevels.TabIndex = 0;
            this.radioCommonPrefixLevels.TabStop = true;
            this.radioCommonPrefixLevels.Text = "Common prefix levels to be excluded from solution folder names:";
            this.radioCommonPrefixLevels.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioUseMostQualifiedNames);
            this.panel1.Controls.Add(this.radioUseProjectNames);
            this.panel1.Controls.Add(this.radioUseAssemblyNames);
            this.panel1.Location = new System.Drawing.Point(6, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 72);
            this.panel1.TabIndex = 14;
            // 
            // radioUseMostQualifiedNames
            // 
            this.radioUseMostQualifiedNames.AutoSize = true;
            this.radioUseMostQualifiedNames.Location = new System.Drawing.Point(8, 49);
            this.radioUseMostQualifiedNames.Name = "radioUseMostQualifiedNames";
            this.radioUseMostQualifiedNames.Size = new System.Drawing.Size(277, 17);
            this.radioUseMostQualifiedNames.TabIndex = 13;
            this.radioUseMostQualifiedNames.TabStop = true;
            this.radioUseMostQualifiedNames.Text = "Create solution folders based on most qualified names";
            this.radioUseMostQualifiedNames.UseVisualStyleBackColor = true;
            // 
            // radioUseProjectNames
            // 
            this.radioUseProjectNames.AutoSize = true;
            this.radioUseProjectNames.Location = new System.Drawing.Point(8, 3);
            this.radioUseProjectNames.Name = "radioUseProjectNames";
            this.radioUseProjectNames.Size = new System.Drawing.Size(261, 17);
            this.radioUseProjectNames.TabIndex = 11;
            this.radioUseProjectNames.TabStop = true;
            this.radioUseProjectNames.Text = "Create solution folders based on project file names";
            this.radioUseProjectNames.UseVisualStyleBackColor = true;
            // 
            // radioUseAssemblyNames
            // 
            this.radioUseAssemblyNames.AutoSize = true;
            this.radioUseAssemblyNames.Location = new System.Drawing.Point(8, 26);
            this.radioUseAssemblyNames.Name = "radioUseAssemblyNames";
            this.radioUseAssemblyNames.Size = new System.Drawing.Size(324, 17);
            this.radioUseAssemblyNames.TabIndex = 12;
            this.radioUseAssemblyNames.TabStop = true;
            this.radioUseAssemblyNames.Text = "Create solution folders based on project output assembly names";
            this.radioUseAssemblyNames.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listProjectTypes);
            this.groupBox2.Controls.Add(this.buttonProjectTypes);
            this.groupBox2.Controls.Add(this.checkIncludeReferences);
            this.groupBox2.Controls.Add(this.textProjectTypes);
            this.groupBox2.Controls.Add(this.textSolutionFile);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.buttonSolutionFile);
            this.groupBox2.Controls.Add(this.checkRecursive);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textProjectRoot);
            this.groupBox2.Controls.Add(this.buttonProjectRoot);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textExcludeFilter);
            this.groupBox2.Controls.Add(this.textIncludeFilter);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 283);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solution projects";
            // 
            // listProjectTypes
            // 
            this.listProjectTypes.CheckOnClick = true;
            this.listProjectTypes.FormattingEnabled = true;
            this.listProjectTypes.Location = new System.Drawing.Point(226, 156);
            this.listProjectTypes.Name = "listProjectTypes";
            this.listProjectTypes.Size = new System.Drawing.Size(107, 79);
            this.listProjectTypes.TabIndex = 36;
            this.listProjectTypes.Leave += new System.EventHandler(this.listProjectTypes_Leave);
            // 
            // buttonProjectTypes
            // 
            this.buttonProjectTypes.Location = new System.Drawing.Point(373, 154);
            this.buttonProjectTypes.Name = "buttonProjectTypes";
            this.buttonProjectTypes.Size = new System.Drawing.Size(24, 23);
            this.buttonProjectTypes.TabIndex = 38;
            this.buttonProjectTypes.Text = "...";
            this.buttonProjectTypes.UseVisualStyleBackColor = true;
            this.buttonProjectTypes.Click += new System.EventHandler(this.buttonProjectTypes_Click);
            // 
            // checkIncludeReferences
            // 
            this.checkIncludeReferences.AutoSize = true;
            this.checkIncludeReferences.Location = new System.Drawing.Point(14, 159);
            this.checkIncludeReferences.Name = "checkIncludeReferences";
            this.checkIncludeReferences.Size = new System.Drawing.Size(155, 17);
            this.checkIncludeReferences.TabIndex = 6;
            this.checkIncludeReferences.Text = "Include referenced projects";
            this.checkIncludeReferences.UseVisualStyleBackColor = true;
            // 
            // textProjectTypes
            // 
            this.textProjectTypes.Location = new System.Drawing.Point(226, 156);
            this.textProjectTypes.Name = "textProjectTypes";
            this.textProjectTypes.ReadOnly = true;
            this.textProjectTypes.Size = new System.Drawing.Size(141, 20);
            this.textProjectTypes.TabIndex = 37;
            this.textProjectTypes.Text = "All";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(223, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Include project types:";
            // 
            // buttonPreview
            // 
            this.buttonPreview.Location = new System.Drawing.Point(322, 390);
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(100, 23);
            this.buttonPreview.TabIndex = 33;
            this.buttonPreview.Text = "Preview";
            this.buttonPreview.UseVisualStyleBackColor = true;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(437, 390);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(100, 23);
            this.buttonExecute.TabIndex = 34;
            this.buttonExecute.Text = "Create Solution";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 432);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupSolutionFileVersion);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.checkSaveSettings);
            this.Controls.Add(this.groupUpdateSettings);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.buttonPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "SolutionMaker Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericSolutionFolderLevels)).EndInit();
            this.groupUpdateSettings.ResumeLayout(false);
            this.groupUpdateSettings.PerformLayout();
            this.groupSolutionFileVersion.ResumeLayout(false);
            this.groupSolutionFileVersion.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCommonPrefixLevels)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSolutionFile;
        private System.Windows.Forms.Button buttonSolutionFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textProjectRoot;
        private System.Windows.Forms.Button buttonProjectRoot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textIncludeFilter;
        private System.Windows.Forms.TextBox textExcludeFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericSolutionFolderLevels;
        private System.Windows.Forms.RadioButton radioAdd;
        private System.Windows.Forms.GroupBox groupUpdateSettings;
        private System.Windows.Forms.RadioButton radioAddRemove;
        private System.Windows.Forms.RadioButton radioReplace;
        private System.Windows.Forms.CheckBox checkSaveSettings;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox checkRecursive;
        private System.Windows.Forms.CheckBox checkOverwriteReadonly;
        private System.Windows.Forms.GroupBox groupSolutionFileVersion;
        private System.Windows.Forms.RadioButton radioVersion2010;
        private System.Windows.Forms.RadioButton radioVersion2008;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioUseMostQualifiedNames;
        private System.Windows.Forms.RadioButton radioUseProjectNames;
        private System.Windows.Forms.RadioButton radioUseAssemblyNames;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textCommonPrefixes;
        private System.Windows.Forms.NumericUpDown numericCommonPrefixLevels;
        private System.Windows.Forms.RadioButton radioCommonPrefixes;
        private System.Windows.Forms.RadioButton radioCommonPrefixLevels;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.CheckBox checkIncludeReferences;
        private System.Windows.Forms.RadioButton radioVersion2012;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox listProjectTypes;
        private System.Windows.Forms.TextBox textProjectTypes;
        private System.Windows.Forms.Button buttonProjectTypes;
    }
}