namespace ContractSyncTool
{
    partial class ContractSync : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ContractSync()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.groupContractSync = this.Factory.CreateRibbonGroup();
            this.btnSyncFromAttachment = this.Factory.CreateRibbonButton();
            this.btnSyncFromExcel = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.groupContractSync.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.groupContractSync);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // groupContractSync
            // 
            this.groupContractSync.Items.Add(this.btnSyncFromAttachment);
            this.groupContractSync.Items.Add(this.btnSyncFromExcel);
            this.groupContractSync.Label = "联系人同步";
            this.groupContractSync.Name = "groupContractSync";
            // 
            // btnSyncFromAttachment
            // 
            this.btnSyncFromAttachment.Label = "从附件中同步联系人";
            this.btnSyncFromAttachment.Name = "btnSyncFromAttachment";
            this.btnSyncFromAttachment.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSyncFromAttachment_Click);
            // 
            // btnSyncFromExcel
            // 
            this.btnSyncFromExcel.Label = "从文件中同步联系人";
            this.btnSyncFromExcel.Name = "btnSyncFromExcel";
            this.btnSyncFromExcel.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSyncFromExcel_Click);
            // 
            // ContractSync
            // 
            this.Name = "ContractSync";
            this.RibbonType = "Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ContractSync_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.groupContractSync.ResumeLayout(false);
            this.groupContractSync.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupContractSync;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSyncFromAttachment;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSyncFromExcel;
    }

    partial class ThisRibbonCollection
    {
        internal ContractSync ContractSync
        {
            get { return this.GetRibbon<ContractSync>(); }
        }
    }
}
