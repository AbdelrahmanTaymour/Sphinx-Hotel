using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.Orders
{
    public partial class frmShowBookingOrdersList : Form
    {
        public frmShowBookingOrdersList(int? BookingID)
        {
            InitializeComponent();
            ucBookingOrdersCard1.LoadBookingOrdersInfo(BookingID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
