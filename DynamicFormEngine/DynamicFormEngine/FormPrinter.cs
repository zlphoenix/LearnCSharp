using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynamicFormEngine
{
    class FormPrinter
    {
        private PrintDocument doc;
        private Bitmap memoryImage;
 
        public FormPrinter()
        {
            doc = new PrintDocument();
            doc.DefaultPageSettings.Landscape = true;
            doc.PrintPage += Doc_PrintPage;
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        public void Print(Form form)
        {
            CaptureScreen(form);
            doc.Print();
        }
        private void CaptureScreen(Form form)
        {
            memoryImage = new Bitmap(form.Width, form.Height);
            form.DrawToBitmap(memoryImage, new Rectangle(0, 0, memoryImage.Width, memoryImage.Height));

        }


       
    }



    

}
