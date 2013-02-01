using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContractSyncTool.Properties;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;
using Outlook = Microsoft.Office.Interop.Outlook;
using Excel = Microsoft.Office.Interop.Excel;
using Zlphoenix.OfficeTool;
using System.IO;
using Exception = System.Exception;
using System.Windows.Forms;

namespace ContractSyncTool
{
    public partial class ContractSync
    {
        private Outlook.Application _app;
        private void ContractSync_Load(object sender, RibbonUIEventArgs e)
        {
            _app = Globals.ThisAddIn.Application;
        }

        private void btnSyncFromAttachment_Click(object sender, RibbonControlEventArgs e)
        {
            var inspector = _app.ActiveInspector();
            var caption = inspector.Caption;
            var mail = inspector.CurrentItem as MailItem;
            //var session = inspector.Session;
            //var a = inspector.AttachmentSelection;
            if (mail == null)
            {
                return;
            }
            try
            {
                var contracts = GetContracts(mail);
                SyncContract(contracts);
            }
            finally
            {
                GC.Collect();
            }
            MessageBox.Show(Resources.Messagebox_Info_SyncFinished);
        }

        private void SyncContract(IEnumerable<Contract> contracts)
        {
            var contactItems = GetContractItems();

            ImportContracts(contracts, contactItems);

            DistinguishTheLeft(contracts, contactItems);
        }

        private void DistinguishTheLeft(IEnumerable<Contract> contracts, Items contactItems)
        {
            var first = contactItems.GetFirst() as ContactItem;
            first = contactItems.GetNext() as ContactItem;
            foreach (var i in contactItems)
            {
                var item = i as ContactItem;
                if (item == null)
                {
                    continue;
                }
                var exist = contracts.Any(c => !string.IsNullOrEmpty(c.Code)
                                      && c.Code == item.OrganizationalIDNumber);
                if (!exist)
                {
                    item.OrganizationalIDNumber = "已离职";
                }
            }
        }

        private void ImportContracts(IEnumerable<Contract> contracts, Items contactItems)
        {
            foreach (var contract in contracts)
            {
                try
                {
                    var item = FindContractFromOLAddrBook(contactItems, contract);
                    if (item == null)
                    {
                        item = (Outlook.ContactItem)
                               _app.CreateItem(Outlook.OlItemType.olContactItem);
                    }
                    item.FirstName = contract.Name.Substring(1, contract.Name.Length - 1);
                    item.LastName = contract.Name.Substring(0, 1);
                    //item.FullName = item.LastName + item.FirstName;
                    //var name = item.FullName;
                    item.Email1Address = contract.Email;
                    item.OrganizationalIDNumber = contract.Code;
                    item.BusinessTelephoneNumber = contract.Tel;
                    item.MobileTelephoneNumber = contract.Mobile;
                    item.Department = contract.Dept;
                    item.JobTitle = contract.Title;
                    item.User1 = contract.Seq.ToString();
                    item.CompanyName = "TelChina";
                    //item.Spouse
                    //item.MailingAddressStreet = "123 Main St.";
                    //item.MailingAddressCity = "Redmond";
                    //item.MailingAddressState = "WA";
                    item.Save();
                    //newContact.Display(true);
                }
                catch (Exception ex)
                {
                    var t = ex.StackTrace;

                    throw;
                }
            }
        }

        private static Items GetContractItems()
        {
            Outlook.NameSpace outlookNameSpace = Globals.ThisAddIn.Application.GetNamespace("MAPI");
            Outlook.MAPIFolder contactsFolder =
                outlookNameSpace.GetDefaultFolder(
                    Microsoft.Office.Interop.Outlook.
                        OlDefaultFolders.olFolderContacts);

            return contactsFolder.Items;
        }

        private static ContactItem FindContractFromOLAddrBook(
            Items contactItems, Contract contract)
        {
            Outlook.ContactItem contactItem = null;
            if (string.IsNullOrEmpty(contract.Code))
            {
                contactItem = contactItems.Find(
                 String.Format("[FullName]='{0}' AND  [Department] ='{1}'",
                 contract.Name, contract.Dept))
                 as Outlook.ContactItem;
            }
            else
            {
                contactItem = contactItems.Find(
                    String.Format("[OrganizationalIDNumber]='{0}'", contract.Code))
                    as Outlook.ContactItem;
            }

            if (contactItem != null)
            {
                return contactItem;
            }
            else
                return null;
        }
        private static IEnumerable<Contract> GetContracts(_MailItem mail)
        {
            var attachment = mail.Attachments[1];

            var strFilePath = Path.Combine(Path.GetTempPath(), attachment.FileName);
            var arrachmentType = attachment.Type;
            //var path = attachment.GetTemporaryFilePath();
            var attachmentClass = attachment.Class;
            var attachmentSession = attachment.Session;
            if (File.Exists(strFilePath))
            {
                try
                {
                    File.Delete(strFilePath);
                }
                catch (Exception e)
                {
                    MessageBox.Show(string.Format("附件临时文件{0}已被占用请删除后再执行同步操作.",
                        strFilePath));
                    var trace = e.StackTrace;
                    return new List<Contract>();
                }
            }
            attachment.SaveAsFile(strFilePath);


            var app = new Excel.Application();
            Excel.Workbook excelWorkbook = app.Workbooks.Open(strFilePath, 0,
                                                              false, 5, System.Reflection.Missing.Value,
                                                              System.Reflection.Missing.Value,
                                                              false, System.Reflection.Missing.Value,
                                                              System.Reflection.Missing.Value, true, false,
                                                              System.Reflection.Missing.Value, false, false, false);

            // 
            //app.Visible = true;
            var result = ContractCollector.GetContracts(app);
            app.Quit();
            return result;
        }
        private static IEnumerable<Contract> GetContracts(string ContractFilePath)
        {
            var app = new Excel.Application();
            Excel.Workbook excelWorkbook = app.Workbooks.Open(ContractFilePath, 0,
                                                              false, 5, System.Reflection.Missing.Value,
                                                              System.Reflection.Missing.Value,
                                                              false, System.Reflection.Missing.Value,
                                                              System.Reflection.Missing.Value, true, false,
                                                              System.Reflection.Missing.Value, false, false, false);

            // 
            //app.Visible = true;
            var result = ContractCollector.GetContracts(app);
            app.Quit();
            return result;
        }
        private string GetContractFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = Resources.ContractSync_OpenContractFile,
                Filter = Resources.ContractSync_ExcelFileType,
                FilterIndex = 2,
                RestoreDirectory = true
            };
            var result = openFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return string.Empty;
            }
            var excelFile = openFileDialog.FileName;
            if (!File.Exists(excelFile))
            {
                MessageBox.Show(string.Format("通讯录文件:{0}不存在！", excelFile));
                return string.Empty;
            }

            return excelFile;
        }

        private void btnSyncFromExcel_Click(object sender, RibbonControlEventArgs e)
        {
            var inspector = _app.ActiveInspector();
            var caption = inspector.Caption;
            var mail = inspector.CurrentItem as MailItem;
            //var session = inspector.Session;
            //var a = inspector.AttachmentSelection;
            if (mail == null)
            {
                return;
            }
            try
            {
                var contracts = ContractHelper.GetContracts(ContractHelper.GetContractFile());
                var helper = new ContractHelper(this._app);
                helper.SyncContract(contracts);
            }
            finally
            {
                GC.Collect();
            }
            MessageBox.Show(Resources.Messagebox_Info_SyncFinished);
        }

    }
}
