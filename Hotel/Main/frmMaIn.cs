using Bunifu.UI.WinForms.BunifuButton;
using Guna.UI2.WinForms;
using Hotel.Users;
using Hotel.People;
using Hotel.Reservations;
using HotelDatabase_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotel.Guests;
using Hotel.Properties;
using Hotel.Orders;
using Hotel.Items;
using Hotel.Room;
using Hotel.Bookings;
using Hotel.Payments;
using Hotel.RoomTypes;
using Hotel.Global;
using System.Resources;

namespace Hotel
{
    public partial class frmMainForm : Form
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainForm));

        private bool _CloseAllApplication = true;
        public delegate void OnApplicationClosed(bool CloseAllApplication);
        public event OnApplicationClosed ApplicationClose;

        BunifuButton _CurrentButton;
        Form _ActiveForm;

        void _ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (_CurrentButton != (BunifuButton)btnSender)
                {
                    _DisableMenuButton();
                    _CurrentButton = (BunifuButton)btnSender;
                }
            }
        }

        void _DisableMenuButton()
        {
            BunifuButton GunaButton = new BunifuButton();

            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(BunifuButton))
                {
                    GunaButton = (BunifuButton)previousBtn;
                }
            }
        }

        async void _OpenChildFormAsync(Form childForm, object btnSender)
        {
            await Task.Delay(100);

            if (_ActiveForm != null)
                _ActiveForm.Close();

            _ActivateButton(btnSender);
            _ActiveForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            if (childForm.Tag != null)
            {
                lblFormTitle.Text = childForm.Tag.ToString();
            }
            else
            {
                lblFormTitle.Text = childForm.Text;
            }
        }

        public frmMainForm()
        {
            InitializeComponent();
        }

        void _HandleUserImage()
        {
            if(clsGlobal.CurrentUser.PersonInfo.ImagePath != null)
            {
                pbCurrentUserImage.ImageLocation = clsGlobal.CurrentUser.PersonInfo?.ImagePath;
            }
            else
            {
                pbCurrentUserImage.Image = ((System.Drawing.Image)(resources.GetObject("pbCurrentUserImage.Image")));
            }
        }

        void _RefreshUserInfo()
        {
            string[] fullnameSplited = clsGlobal.CurrentUser.PersonInfo.FullName.Split(' ');
            lblUserName.Text = fullnameSplited[0]; //First Name
            lblUserEmail.Text = clsGlobal.CurrentUser.PersonInfo.Email.ToString();

            _HandleUserImage();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _RefreshUserInfo();
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            _OpenChildFormAsync(new frmManageReservation(), sender);
            picTitle.Image = Resources.Reservations_Icon;
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            _OpenChildFormAsync(new frmManageUsers(), sender);
            picTitle.Image = Resources.Users_Icon;
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            _OpenChildFormAsync(new frmManagePeople(), sender);
            picTitle.Image = Resources.People_400;
        }

        private void btnGuests_Click(object sender, EventArgs e)
        {
            _OpenChildFormAsync(new frmManageGuests(), sender);
            picTitle.Image = Resources.guests_Icon;
        }

        private void btnRoomTypes_Click(object sender, EventArgs e)
        {
            _OpenChildFormAsync(new frmManageRoomTypes(), sender);
            picTitle.Image = Resources.RoomType2_Icon;
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            _OpenChildFormAsync(new frmManagePayments(), sender);
            picTitle.Image = Resources.Payment_Icon;
        }

        private void btnBookings_Click(object sender, EventArgs e)
        {
            _OpenChildFormAsync(new frmManageBookings(), sender);
            picTitle.Image = Resources.Booking1_Icon;
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            _OpenChildFormAsync(new frmManageRooms(), sender);
            picTitle.Image = Resources.Rooms1_Icon;
        }

        private void btnDiningMenu_Click(object sender, EventArgs e)
        {
            _OpenChildFormAsync(new frmManageItems(), sender);
            picTitle.Image = Resources.Menu1_Icon;
        }

        private void btnGuestOrders_Click(object sender, EventArgs e)
        {
            _OpenChildFormAsync(new frmMangeOrders(), sender);
            picTitle.Image = Resources.order1_Icon;
        }

        private void btnRoomServices_Click(object sender, EventArgs e)
        {
            picTitle.Image = Resources.RoomServices1_Icon;
        }

        private void btnProfileArrow_Click(object sender, EventArgs e)
        {
            Point location = btnProfileArrow.PointToScreen(new Point(0, btnProfileArrow.Height));
            contextMenuStrip1.Show(location);
        }

        private void cmsShowProfile_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frm = new frmShowUserInfo(clsGlobal.CurrentUser?.UserID);
            frm.ShowDialog();
        }

        private void cmsChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser?.UserID);
            frm.ShowDialog();
        }

        private void cmsSignOut_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _CloseAllApplication = false;
            this.Close();
        }

        private void frmMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationClose(_CloseAllApplication);
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _CloseAllApplication = false;
            this.Close();
        }

    }
}
