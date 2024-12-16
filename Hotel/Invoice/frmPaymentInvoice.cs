using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.Invoice
{
    public partial class frmPaymentInvoice : Form
    {
        public frmPaymentInvoice(int? PaymentID, int? InvoiceID)
        {
            InitializeComponent();

            ucPaymentCard1.LoadPaymentInfo(PaymentID, true);
            ucInvoiceCard1.LoadInvoiceInfo(InvoiceID);

            _LoadHotelInfo();
        }

        private void _LoadHotelInfo()
        {
            lblHotelName.Text = ConfigurationManager.AppSettings["HotelName"];
            lblPhone.Text = ConfigurationManager.AppSettings["Phone"];
            lblEmail.Text = ConfigurationManager.AppSettings["Email"];
            lblAddress.Text = ConfigurationManager.AppSettings["Address"];
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int w = this.Width + 20;
            int h = this.Height + 30;

            Bitmap bmp = new Bitmap(w, h);
            Rectangle rec = new Rectangle(0, 0, w, h);
            this.DrawToBitmap(bmp, rec);

            e.Graphics.DrawImage(bmp, rec);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            btnPrint.Visible = false;

            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }

            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            btnPrint.Visible = true;
        }

        private void frmPaymentInvoice_Load(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("MyPaper", this.Height, this.Width);
            printDocument1.DefaultPageSettings.Landscape = true;
        }
    }
}
