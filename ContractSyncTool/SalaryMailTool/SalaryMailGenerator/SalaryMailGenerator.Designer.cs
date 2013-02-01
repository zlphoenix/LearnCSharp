namespace SalaryMailTool
{
    partial class SalaryMailGenerator : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SalaryMailGenerator()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalaryMailGenerator));
            this.TelChinaAddInToolsTab = this.Factory.CreateRibbonTab();
            this.RGSalaryMail = this.Factory.CreateRibbonGroup();
            this.btnGenSaleryStripe = this.Factory.CreateRibbonButton();
            this.btnSendAll = this.Factory.CreateRibbonButton();
            this.RGContract = this.Factory.CreateRibbonGroup();
            this.btnImportFromExcel = this.Factory.CreateRibbonButton();
            this.TelChinaAddInToolsTab.SuspendLayout();
            this.RGSalaryMail.SuspendLayout();
            this.RGContract.SuspendLayout();
            // 
            // TelChinaAddInToolsTab
            // 
            this.TelChinaAddInToolsTab.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.TelChinaAddInToolsTab.Groups.Add(this.RGSalaryMail);
            this.TelChinaAddInToolsTab.Groups.Add(this.RGContract);
            this.TelChinaAddInToolsTab.Label = "泰华电讯";
            this.TelChinaAddInToolsTab.Name = "TelChinaAddInToolsTab";
            // 
            // RGSalaryMail
            // 
            this.RGSalaryMail.Items.Add(this.btnGenSaleryStripe);
            this.RGSalaryMail.Items.Add(this.btnSendAll);
            this.RGSalaryMail.Label = "工资条";
            this.RGSalaryMail.Name = "RGSalaryMail";
            // 
            // btnGenSaleryStripe
            // 
            this.btnGenSaleryStripe.Image = ((System.Drawing.Image)(resources.GetObject("btnGenSaleryStripe.Image")));
            this.btnGenSaleryStripe.Label = "生成工资条";
            this.btnGenSaleryStripe.Name = "btnGenSaleryStripe";
            this.btnGenSaleryStripe.ShowImage = true;
            this.btnGenSaleryStripe.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnGenSaleryStripe_Click);
            // 
            // btnSendAll
            // 
            this.btnSendAll.Image = global::SalaryMailTool.Properties.Resources.索引;
            this.btnSendAll.Label = "批量发送";
            this.btnSendAll.Name = "btnSendAll";
            this.btnSendAll.ShowImage = true;
            this.btnSendAll.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSendAll_Click);
            // 
            // RGContract
            // 
            this.RGContract.Items.Add(this.btnImportFromExcel);
            this.RGContract.Label = "通讯录";
            this.RGContract.Name = "RGContract";
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.Image = global::SalaryMailTool.Properties.Resources.Contract;
            this.btnImportFromExcel.Label = "导入通讯录";
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.ShowImage = true;
            this.btnImportFromExcel.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnImportFromExcel_Click);
            // 
            // SalaryMailGenerator
            // 
            this.Name = "SalaryMailGenerator";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.TelChinaAddInToolsTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.SalaryMailGenerator_Load);
            this.TelChinaAddInToolsTab.ResumeLayout(false);
            this.TelChinaAddInToolsTab.PerformLayout();
            this.RGSalaryMail.ResumeLayout(false);
            this.RGSalaryMail.PerformLayout();
            this.RGContract.ResumeLayout(false);
            this.RGContract.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonGroup RGSalaryMail;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnGenSaleryStripe;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab TelChinaAddInToolsTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSendAll;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup RGContract;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnImportFromExcel;
    }

    partial class ThisRibbonCollection
    {
        internal SalaryMailGenerator SalaryMailGenerator
        {
            get { return this.GetRibbon<SalaryMailGenerator>(); }
        }
    }
}
