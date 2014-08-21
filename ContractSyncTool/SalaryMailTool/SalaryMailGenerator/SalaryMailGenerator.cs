using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;
using System.IO;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Outlook;
using SalaryMailTool.Properties;
using Exception = System.Exception;
using System.Drawing;
using SalaryMailTool.ContractSyncTool;


namespace SalaryMailTool
{
    public partial class SalaryMailGenerator
    {
        private int _columnCount = -1;
        private void SalaryMailGenerator_Load(object sender, RibbonUIEventArgs e)
        {
            _app = Globals.ThisAddIn.Application;

            var outputColumns = System.Configuration.ConfigurationManager.AppSettings["OutputColumns"];
            if (!string.IsNullOrEmpty(outputColumns))
            {
                int result = 0;
                if (Int32.TryParse(outputColumns, out result))
                {
                    if (result != 0)
                    {
                        this._columnCount = result;
                    }
                }
            }
        }

        private void btnGenSaleryStripe_Click(object sender, RibbonControlEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = null;
            Workbook excelWorkbook;
            Worksheet salaryStripes;
            try
            {
                var openFileDialog = new OpenFileDialog
                                         {
                                             Title = "打开工资文件",
                                             Filter = "Excel 2003 文件 (*.xls)|*.xls|Excel 2007/2010 文件 (*.xlsx)|*.xlsx",
                                             FilterIndex = 2,
                                             RestoreDirectory = true
                                         };
                var result = openFileDialog.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return;
                }
                var excelFile = openFileDialog.FileName;



                if (!File.Exists(excelFile))
                {
                    MessageBox.Show(string.Format("工资文件:{0}不存在！", excelFile));
                    return;
                }
                CreateSalaryMails(excelFile, out app, out excelWorkbook, out salaryStripes);
            }
            catch (Exception ex)
            {

                var message = ex.Message;
                throw;
            }
            finally
            {
                if (app != null)
                    app.Quit();
                app = null;
                excelWorkbook = null;
                salaryStripes = null;
                GC.Collect();
            }
            //Globals.ThisAddIn.Application.
        }
        private void CreateSalaryMails(string excelFile,
            out Microsoft.Office.Interop.Excel.Application app,
            out Workbook excelWorkbook,
            out Worksheet salaryStripes)
        {
            int employeeCount = 0;
            int mailCount = 0;
            app = new Microsoft.Office.Interop.Excel.Application();
            excelWorkbook = app.Workbooks.Open(excelFile, 0,
                                                              false, 5, System.Reflection.Missing.Value,
                                                              System.Reflection.Missing.Value,
                                                              false, System.Reflection.Missing.Value,
                                                              System.Reflection.Missing.Value, true, false,
                                                              System.Reflection.Missing.Value, false, false, false);

            salaryStripes =
               app.Sheets.Cast<Worksheet>().FirstOrDefault();
            if (salaryStripes == null)
            {
                MessageBox.Show(string.Format("工资文件格式不规范!"));
                return;
            }

            var contracts = GetContractItems();
            #region Debug
            //foreach (var item in contracts)
            //{
            //    var contract = item as Microsoft.Office.Interop.Outlook.ContactItem;
            //    if (contract == null)
            //    {
            //        continue;
            //    }
            //    var fullname = contract.FullName;
            //    var dept = contract.Department;
            //    var companyName = contract.CompanyName;
            //}
            #endregion

            //起始行号
            int rowNum = 3;
            //邮件内容:XXXX年XX月份工资
            var content = salaryStripes.UsedRange.Rows[rowNum] as Range;
            //string contentStr = (content.Value2).ToString();
            Dictionary<int, long> colorDic;
            string contentStr = GetExcelLineIntoStringArray(content, out colorDic).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            content = null;

            rowNum++;
            var titleLine = salaryStripes.UsedRange.Rows[rowNum] as Range;
            if (titleLine == null)
            {
                MessageBox.Show(Resources.Error_SalaryExcelDocFormatError);
                return;
            }//var titlevalue = titleLine.Value2; //Debug
            var title = GetExcelLineIntoStringArray(titleLine, out colorDic);
            //及时释放非托管资源
            titleLine = null;
            rowNum++;
            #region 获取全部人名的薪水信息并发送
            //      foreach (Range salaryInfo in salaryStripes.UsedRange.Rows[rowNum])
            for (var k = rowNum; k < salaryStripes.UsedRange.Rows.Count; k++)
            {
                //salaryInfo 代表一行员工薪资记录
                Range salaryInfo = salaryStripes.UsedRange.Rows[k];
                var salaryRecord = GetExcelLineIntoStringArray(salaryInfo, out colorDic);
                salaryInfo = null;

                //var employeeInfo = GetEmployeeInfo(salaryInfo);

                //var id = salaryRecord[1];
                //var name = salaryRecord[2];

                var code = salaryRecord[0];
                var dept = salaryRecord[1];
                var name = salaryRecord[3];
                //去掉合计行
                if (salaryRecord.Length < 3)
                    break;
                if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name))
                    continue;
                employeeCount++;
                //查找联系人,规则:员工号与姓名都相同,公司是TelChina
                //var contactItem = (Microsoft.Office.Interop.Outlook.ContactItem) contracts.
                //                                                                     Find(
                //                                                                         String.Format(
                //                                                                             " [CompanyName] = 'TelChina' And [Department]='{0}' And [FullName] = '{1}'"
                //                                                                             , dept, name));

                var contactItem = (Microsoft.Office.Interop.Outlook.ContactItem)contracts.
                                                                                   Find(
                                                                                       String.Format(
                    //" [CompanyName] = 'TelChina' And [FullName] = '{0}'", name));
                    " [CompanyName] = 'TelChina' And [OrganizationalIDNumber] = '{0}'"
                    , code));

                if (contactItem == null)
                {
                    var createMailResult = MessageBox.Show(
                        string.Format(
@"部门:{0} 下员工:{1} 对应的联系人信息没有找到,
是否生成邮件并手工填写邮件地址?
'是'创建工资条邮件,'否'不创建",
                        dept, name),
                        "警告!", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);
                    if (createMailResult == DialogResult.No)
                        continue;
                }
                else if (contactItem.FullName != name)
                {
                    var createMailResult = MessageBox.Show(
                       string.Format(
@"员工号为:{0} 的员工(姓名:{1}) 与对应联系人的姓名({2})不一致,
是否生成邮件并手工填写邮件地址?
'是'创建工资条邮件,'否'不创建",
                       code, name, contactItem.FullName),
                       "警告!", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button2);
                    if (createMailResult == DialogResult.No)
                        continue;
                    else
                    {
                        contactItem = null;
                    }
                }
                CreateMail(salaryRecord, contactItem, title, contentStr, colorDic);
                contactItem = null;
                mailCount++;
                //    oMail.Send();
            }
            MessageBox.Show(string.Format("邮件生成完成,共发现员工:{0}人,生成邮件{1}封", employeeCount, mailCount), "提示");

            #endregion
        }
        private static void CreateMail(string[] salaryRecord, ContactItem contactItem,
            string[] title, string contentStr, Dictionary<int, long> colorDic)
        {
            var oMail =
                (Microsoft.Office.Interop.Outlook.MailItem)Globals.ThisAddIn.Application.CreateItem(
                    Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

            oMail.Subject = "工资条";

            if (contactItem != null)
                oMail.To = contactItem.Email1Address;


            var titleRow = ToTableRow(title, null);
            var salaryRow = ToTableRow(salaryRecord, colorDic);
            var body = new StringBuilder(1024);
            body.Append(string.Format("<h1>{0}</h1>\n", contentStr));
            body.Append("<table border=1><tr>");
            body.Append(titleRow);
            body.Append("</tr><tr>");
            body.Append(salaryRow);
            body.Append("</tr></table>");
            oMail.HTMLBody = body.ToString();
            oMail.DeferredDeliveryTime = DateTime.Now;
            oMail.Save();

            oMail = null;
        }

        private static string ToTableRow(string[] title, Dictionary<int, long> colorDic)
        {
            var body = new StringBuilder(128);
            int columnCount = title.Length;
            for (int i = 0; i < columnCount; i++)
            {
                if (colorDic != null && colorDic.ContainsKey(i))
                    body.Append("<td style='background:" + colorDic[i] + "'>{" + i + "}</td>");
                else
                    body.Append("<td>{" + i + "}</td>");
            }
            return string.Format(body.ToString(), title);
        }

        /// <summary>
        /// 将Excel行转换为string数组
        /// </summary>
        /// <param name="line">Excel行</param>
        /// <returns></returns>
        private string[] GetExcelLineIntoStringArray(Range line, out Dictionary<int, long> colorDic)
        {
            colorDic = new Dictionary<int, long>();
            string[] result = new string[line.Cells.Count];
            for (var i = 0; i < line.Cells.Count; i++)
            {
                var cell = line.Cells[i + 1] as Range;
                result[i] = (cell.Text ?? "").ToString().Trim();
                //var text = cell.Value2;
                var color = cell.Interior.Color;
                if (color != 16777215.0)
                {
                    colorDic.Add(i, (long)color);
                }
                //System.Drawing.Color.FromName();
                //range.Cells.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 153).ToArgb(); 
                if (this._columnCount > 0 && i > this._columnCount)
                {
                    break;
                }
            }
            return result;
        }
        [Obsolete("使用ToTableRow代替", true)]
        private static string[] GetEmployeeInfo(Range salaryInfo)
        {
            string[] employeeInfo = new string[3];
            int index = 0;
            foreach (Range cell in (salaryInfo as Range).Cells)
            {
                if (cell.Value2 == null) //去除合计行
                    break;
                employeeInfo[index] = cell.Value2.ToString();
                index++;
                if (index == 3)
                {
                    break;
                }
            }
            return employeeInfo;
        }

        private static Items GetContractItems()
        {
            Microsoft.Office.Interop.Outlook.NameSpace outlookNameSpace = Globals.ThisAddIn.Application.GetNamespace("MAPI");
            Microsoft.Office.Interop.Outlook.MAPIFolder contactsFolder =
                outlookNameSpace.GetDefaultFolder(
                    Microsoft.Office.Interop.Outlook.
                        OlDefaultFolders.olFolderContacts);

            return contactsFolder.Items;
        }

        private void btnSendAll_Click(object sender, RibbonControlEventArgs e)
        {
            var sendingMails = GetMails();
            if (sendingMails == null || sendingMails.Count == 0)
            {
                MessageBox.Show(Resources.SalaryMailGenerator_btnSendAll_Click_NoSendingmails);
                return;
            }
            var mailCount = 0;
            //每发送一封邮件,sendingMail中的元素就会减1,如果使用foreach的话,虽然不抛出异常,
            //但是已经修改了集合的内容,导致无法完全遍历集合
            //OutLook 对象集合的下标访问都是从"1"开始到Count结束的集合,使用0下标访问会越界
            for (var i = sendingMails.Count; i > 0; i--)
            {
                var mail = sendingMails[i] as Microsoft.Office.Interop.Outlook.MailItem;
                if (mail == null || string.IsNullOrEmpty(mail.To))
                {
                    continue;
                }
                mail.Send();
                mailCount++;
                mail = null;
            }
            sendingMails = null;

            MessageBox.Show(string.Format("邮件发送完成,共{0}封", mailCount));
        }
        private static Items GetMails()
        {
            Microsoft.Office.Interop.Outlook.NameSpace outlookNameSpace = Globals.ThisAddIn.Application.GetNamespace("MAPI");
            Microsoft.Office.Interop.Outlook.MAPIFolder contactsFolder =
                outlookNameSpace.GetDefaultFolder(
                    Microsoft.Office.Interop.Outlook.
                        OlDefaultFolders.olFolderDrafts);

            return contactsFolder.Items;
        }

        #region Contract
        private Microsoft.Office.Interop.Outlook.Application _app;

        /// <summary>
        /// 同步通讯录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportFromExcel_Click(object sender, RibbonControlEventArgs e)
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

        #endregion


    }
}
