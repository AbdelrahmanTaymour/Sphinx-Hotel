using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.Items
{
    public partial class frmShowItemInfo : Form
    {
        public frmShowItemInfo(int? ItemID, bool EnableUpdateInfo = true)
        {
            InitializeComponent();

            ucItemInfoCard1.LoadItemInfo(ItemID);
            ucItemInfoCard1.EnableUpdateInfo = EnableUpdateInfo;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
