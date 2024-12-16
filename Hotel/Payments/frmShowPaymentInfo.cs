using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.Payments
{
    public partial class frmShowPaymentInfo : Form
    {
        public frmShowPaymentInfo(int? PaymentID)
        {
            InitializeComponent();
            ucPaymentCard1.LoadPaymentInfo(PaymentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
