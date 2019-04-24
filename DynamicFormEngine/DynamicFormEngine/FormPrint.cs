using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynamicFormEngine
{
    public partial class FormPrint : Form
    {
        public FormPrint()
        {
            InitializeComponent();

            var cdg = new CustomDataGridView();
            cdg.BackgroundColor = Color.White;
            
            panelHeader.Controls.Add(cdg);
            cdg.Dock = DockStyle.Fill;

            Dictionary<string, object> entity = new Dictionary<string, object> {
                {"CPU","Intel"},
                {"Drives",new Dictionary<string,object>
                    {
                        {"HDD","HT 1TB@SATA" },
                          {"SSD","SAMSUNG 256GB@NVME" }
                    }
                }
            };


            var items = new List<Entity>()
            {
                new Entity { Code = "Allen test set set set sets 13456中国人民站起来了", Name = "test1" ,IsEnabled =false},
                new Entity { Code = "Ada", Name = "test2" }
            };
            for (int i = 0; i < 100; i++)
            {
                items.Add(new Entity { Code = "I am " + i, Name = "I coming..." });
            }
            var jObject = JsonConvert.DeserializeObject<DataSet>(@"{
                'Header':[{
                    'CPU': 'Intel',
                    'VGA': 'ATI R7',
                }],
                'Driver': [
                    {'Type': 'HDD','Specifications':'1TB@SATA','Manufacturer':'HT'},
                    {'Type': 'SSD','Specifications':'256GB@NVME','Manufacturer':'SAMSUNG'}
                    ]
                }");

            //lbText.DataBindings.Add("Text", jObject.Tables["Header"], "CPU");


            //dgContent.DataSource = jObject.Tables["Driver"];
            dgContent.DataSource = items;

            //dgContent.DataMember = "";
            //dgContent.DataBindings.Add(item);
            tbxText.DataBindings.Add("Text", items[0], "Code");
            lbText.DataBindings.Add("Visible", items[0], "IsEnabled");

            dgContent.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgContent.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //dgContent.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //this.dgContent.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //this.dgContent.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            //this.dgContent.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void dgContent_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void dgContent_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dg = (sender as DataGridView);
            if (dg == null)
            {
                return;
            }

            int totalHeight = dg.ColumnHeadersHeight;

            foreach (DataGridViewRow row in dg.Rows)
           
                
                
                
                
                {
                totalHeight += row.Height;
            }
            dg.Height = totalHeight;
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
          
        }

        private void lbText_Click(object sender, EventArgs e)
        {





        }
    }
}

