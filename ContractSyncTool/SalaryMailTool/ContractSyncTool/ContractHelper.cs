using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zlphoenix.OfficeTool;
using System.Windows.Forms;
using SalaryMailTool.Properties;
using System.IO;
using Microsoft.Office.Interop.Outlook;

namespace SalaryMailTool.ContractSyncTool
{
    internal class ContractHelper
    {
        public ContractHelper(Microsoft.Office.Interop.Outlook.Application app)
        {
            this._app = app;
        }

        private Microsoft.Office.Interop.Outlook.Application _app;
        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="contractFilePath"></param>
        /// <returns></returns>
        public static IEnumerable<Contract> GetContracts(string contractFilePath)
        {
            var app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = app.Workbooks.Open(contractFilePath, 0,
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

        /// <summary>
        /// 打开对话框选择Excle文件
        /// </summary>
        /// <returns></returns>
        public static string GetContractFile()
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

        /// <summary>
        /// 同步通讯录
        /// </summary>
        /// <param name="contracts"></param>
        public void SyncContract(IEnumerable<Contract> contracts)
        {
            var contactItems = GetContractItems();

            ImportContracts(contracts, contactItems);

            DistinguishTheLeft(contracts, contactItems);
        }

        /// <summary>
        /// 处理离职员工
        /// </summary>
        /// <param name="contracts"></param>
        /// <param name="contactItems"></param>
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

        /// <summary>
        /// 获取当前联系人列表
        /// </summary>
        /// <returns></returns>
        private Items GetContractItems()
        {
            Microsoft.Office.Interop.Outlook.NameSpace outlookNameSpace = Globals.ThisAddIn.Application.GetNamespace("MAPI");
            Microsoft.Office.Interop.Outlook.MAPIFolder contactsFolder =
                outlookNameSpace.GetDefaultFolder(
                    Microsoft.Office.Interop.Outlook.
                        OlDefaultFolders.olFolderContacts);

            return contactsFolder.Items;
        }

        /// <summary>
        /// 导入联系人
        /// </summary>
        /// <param name="contracts"></param>
        /// <param name="contactItems"></param>
        private void ImportContracts(IEnumerable<Contract> contracts, Items contactItems)
        {
            foreach (var contract in contracts)
            {
                try
                {
                    var item = FindContractFromOLAddrBook(contactItems, contract);
                    if (item == null)
                    {
                        item = (Microsoft.Office.Interop.Outlook.ContactItem)
                               _app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olContactItem);
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
                catch (System.Exception ex)
                {
                    var t = ex.StackTrace;
                    throw;
                }
            }
        }


        /// <summary>
        /// 从现有的联系人列表中找出匹配的联系人
        /// </summary>
        /// <param name="contactItems"></param>
        /// <param name="contract"></param>
        /// <returns></returns>
        private static ContactItem FindContractFromOLAddrBook(
          Items contactItems, Contract contract)
        {
            Microsoft.Office.Interop.Outlook.ContactItem contactItem = null;
            if (string.IsNullOrEmpty(contract.Code))
            {
                contactItem = contactItems.Find(
                 String.Format("[FullName]='{0}' AND  [Department] ='{1}'",
                 contract.Name, contract.Dept))
                 as Microsoft.Office.Interop.Outlook.ContactItem;
            }
            else
            {
                contactItem = contactItems.Find(
                    String.Format("[OrganizationalIDNumber]='{0}'", contract.Code))
                    as Microsoft.Office.Interop.Outlook.ContactItem;
            }

            if (contactItem != null)
            {
                return contactItem;
            }
            else
                return null;
        }
    }
}
