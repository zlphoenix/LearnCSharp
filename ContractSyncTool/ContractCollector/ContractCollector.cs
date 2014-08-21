using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace Zlphoenix.OfficeTool
{
    public class ContractCollector
    {
        public static List<Contract> GetContracts(Microsoft.Office.Interop.Excel.Application excelApplication)
        {
            var result = new List<Contract>(300);
            Worksheet contractList =
              excelApplication.Sheets.Cast<Worksheet>().FirstOrDefault();

            if (contractList == null)
            {
                return result;
            }
            //从正文开始计算
            int rowNum = 3;
            int blankLine = 0;
            string dept = "";

            while (blankLine < 10)
            {
                //循环体每执行一次代表一行
                Contract contract = null;
                //var cells = contractList.UsedRange.Cells["A1", "H10"];
                foreach (Range cell in (contractList.UsedRange.Rows[rowNum] as Range).Cells)
                {
                    var cellCount = cell.Count;
                    var rowIndex = cell.Row;
                    var columnIndex = cell.Column;

                    //循环体每执行一次代表一个单元格
                    if (cell.Value2 == null)
                    {
                        //已经到行尾
                        continue;
                    }
                    var cellValue = GetCellValue(cell);


                    var seq = 0;
                    //部门 第一列不是数字
                    if (columnIndex == 1 && !Int32.TryParse(cellValue, out seq))
                    {
                        dept = cellValue;
                        break;
                    }
                    if (contract == null)
                    {
                        blankLine = 0;
                        contract = new Contract() { Dept = dept };
                    }

                    SetContract(seq, cellValue, columnIndex, contract);
                }

                //if (contract == null)
                //    break;
                //else
                //{
                if (contract != null)
                {
                    result.Add(contract);
                }
                else
                {
                    //超过连续10行空行,退出
                    blankLine++;
                }
                //}
                rowNum++;
                contract = null;
            }
            contractList = null;

            return result;
        }

        private static void SetContract(int seq, string cellValue, int columnIndex, Contract contract)
        {
            switch (columnIndex)
            {
                case 1:
                    contract.Seq = seq;
                    break;
                case 2:
                    break;
                case 3:
                    contract.Code = cellValue;
                    break;
                case 4:
                    contract.Name = cellValue;
                    break;
                case 5:
                    contract.Title = cellValue;
                    break;
                case 6:
                    contract.Tel = cellValue;
                    break;
                case 7:
                    contract.Mobile = cellValue;
                    break;
                case 8:
                    contract.Email = cellValue;
                    break;
                default:
                    break;
            }
        }

        private static string GetCellValue(Range cell)
        {
            string cellValue = cell.Value2.ToString();
            //去掉空格
            cellValue = cellValue.Replace("　", "");
            cellValue = cellValue.Replace(" ", "");
            return cellValue;
        }
    }
}
