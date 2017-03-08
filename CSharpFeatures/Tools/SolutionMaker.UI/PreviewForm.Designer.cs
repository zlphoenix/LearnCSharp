namespace SolutionMaker.UI
{
    partial class PreviewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewForm));
            this.treeSolution = new System.Windows.Forms.TreeView();
            this.labelPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeSolution
            // 
            this.treeSolution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeSolution.Indent = 19;
            this.treeSolution.ItemHeight = 18;
            this.treeSolution.Location = new System.Drawing.Point(12, 12);
            this.treeSolution.Name = "treeSolution";
            this.treeSolution.ShowRootLines = false;
            this.treeSolution.Size = new System.Drawing.Size(392, 299);
            this.treeSolution.TabIndex = 0;
            this.treeSolution.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSolution_AfterSelect);
            // 
            // labelPath
            // 
            this.labelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(12, 314);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(51, 13);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "labelPath";
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 336);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.treeSolution);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreviewForm";
            this.Text = "Solution Preview";
            this.Load += new System.EventHandler(this.PreviewForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeSolution;
        private System.Windows.Forms.Label labelPath;

    }
}