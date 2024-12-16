using HotelDatabase_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HotelDatabase_Buisness.clsReservation;

namespace Hotel.Reservations
{
    public partial class frmManageReservation : Form
    {
        DataTable _dtReservations;
        public frmManageReservation()
        {
            InitializeComponent();
        }

        void _RefreshReservationList()
        {
            _dtReservations = clsReservation.GetAllReservations();
            dgvReservations.DataSource = _dtReservations;

            if(dgvReservations.Rows.Count > 0)
            {
                dgvReservations.Columns[0].HeaderText = "Reservation ID";
                dgvReservations.Columns[0].Width = 100;

                dgvReservations.Columns[1].HeaderText = "Reserved By";
                dgvReservations.Columns[1].Width = 220;

                dgvReservations.Columns[2].HeaderText = "Room Number";
                dgvReservations.Columns[2].Width = 100;

                dgvReservations.Columns[3].HeaderText = "People No.";
                dgvReservations.Columns[3].Width = 100;

                dgvReservations.Columns[4].HeaderText = "Reserved From";
                dgvReservations.Columns[4].Width = 140;

                dgvReservations.Columns[5].HeaderText = "Reserved To";
                dgvReservations.Columns[5].Width = 140;

                dgvReservations.Columns[6].HeaderText = "Status";
                dgvReservations.Columns[6].Width = 110;

            }
        }
        int? _GetGuestIDFromReservationID()
        {
            clsReservation Reservation = clsReservation.Find(_GetReservationIDFromDGV());

            if (Reservation == null)
                return null;

            return Reservation.GuestID;
        }
        string _GetRealColumnNameInDB()
        {
            switch (cbFilterBy.Text)
            {
                case "Reservation ID":
                    return "ReservationID";

                case "Reserved By":
                    return "ReservedBy";

                case "Room Number":
                    return "RoomNumber";

                case "Status":
                    return "Status";

                default:
                    return "None";
            }
        }
        int? _GetReservationIDFromDGV()
        {
            return (int?)dgvReservations.CurrentRow.Cells["ReservationID"].Value;
        }
        string _GetReservationStatusFromDGV()
        {
            return dgvReservations.CurrentRow.Cells["Status"].Value.ToString();
        }
        int _GetRoomNumberFromDGV()
        {
            return (int)dgvReservations.CurrentRow.Cells["RoomNumber"].Value;
        }
        DateTime _GetResevedFroDateFromDGV()
        {
            return (DateTime)dgvReservations.CurrentRow.Cells["ReservedForDate"].Value;
        }
        DateTime _GetReservedToDateFromDGV()
        {
            return (DateTime)dgvReservations.CurrentRow.Cells["ReservedToDate"].Value;
        }

        private void frmManageReservation_Load(object sender, EventArgs e)
        {
            _RefreshReservationList();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None") && (cbFilterBy.Text != "Status");

            cbReservationStatus.Visible = (cbReservationStatus.Text == "Status");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

            if (cbReservationStatus.Visible)
            {
                cbReservationStatus.SelectedIndex = 0;
            }
        }
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (_dtReservations.Rows.Count == 0)
                return;

            string ColumnName = _GetRealColumnNameInDB();

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) ||
                cbFilterBy.Text == "None")
            {
                _dtReservations.DefaultView.RowFilter = "";
                return;
            }

            if (cbFilterBy.Text == "Reservation ID" || cbFilterBy.Text == "Room Number")
            {
                // search with numbers
                _dtReservations.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilterBy.Text.Trim());
            }
            else
            {
                // search with string
                _dtReservations.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, txtFilterBy.Text.Trim());
            }
        }
        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Reservation ID" || cbFilterBy.Text == "Room Number")
                // make sure that the user can only enter the numbers
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void cbReservationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtReservations.Rows.Count == 0)
                return;

            if (cbReservationStatus.Text == "All")
            {
                _dtReservations.DefaultView.RowFilter = "";
                return;
            }

            _dtReservations.DefaultView.RowFilter =
                string.Format("[{0}] like '{1}%'", "Status", cbReservationStatus.Text);

        }
        private void dgvReservations_DoubleClick(object sender, EventArgs e)
        {
            frmShowReservationInfo ShowReservationInfo = new frmShowReservationInfo(_GetReservationIDFromDGV());
            ShowReservationInfo.ShowDialog();

            _RefreshReservationList();
        }
        private void cmsReservationForm_Opening(object sender, CancelEventArgs e)
        {
            cmsReservationForm.Enabled = dgvReservations.Rows.Count > 0;

            string Status = _GetReservationStatusFromDGV();
            int? ReservationID = _GetReservationIDFromDGV();

            cmsEditReservation.Enabled =
            cmsConfirmReservation.Enabled =
            (Status == enReservationStatus.Pending.ToString());

            cmsCancelReservation.Enabled =
            cmsDeleteReservation.Enabled =
            (Status == enReservationStatus.Pending.ToString()) ||
            (Status == enReservationStatus.Invalid.ToString());

            cmsCheckIn.Enabled =
            (Status == enReservationStatus.Confirmed.ToString()) &&
            !(clsReservation.IsReservationCheckedIn(ReservationID));

        }
        
        DialogResult _ShowRoomUnavailableMessage()
        {
            return MessageBox.Show("This room is no longer available :(\nDo you want to change the date or the room?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }
        DialogResult _ShowConfirmReservationMessage()
        {
            return MessageBox.Show("Are you sure you want to confirm this reservation?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        DialogResult _ShowCancelReservationMessage()
        {
            return MessageBox.Show("Are you sure you want to cancel this reservation?", "Confirm",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        DialogResult _ShowDeleteReservationMessage()
        {
            return MessageBox.Show("Are you sure you want to delete this reservation?", "Confirm",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        void _ShowConfirmReservationSuccessMessage()
        {
            MessageBox.Show("Reservation confirmed successfully! You can now proceed " +
                "with the check-in process", "Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void _ShowSuccessMessage(string action)
        {
            MessageBox.Show($"Reservation {action} successfully!", "Confirmed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void _ShowFailureMessage(string action)
        {
            MessageBox.Show($"Reservation {action} failed!", "Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnAddNewReservation_Click(object sender, EventArgs e)
        {
            frmAddEditReservation frm = new frmAddEditReservation();
            frm.ShowDialog();
            frmManageReservation_Load(null, null);
        }
        private void cmsShowReservationDetails_Click(object sender, EventArgs e)
        {
            frmShowReservationInfo frm = new frmShowReservationInfo(_GetReservationIDFromDGV());
            frm.ShowDialog();

            frmManageReservation_Load(null, null);
        }
        private void cmsEditReservation_Click(object sender, EventArgs e)
        {
            frmAddEditReservation frm = new frmAddEditReservation(_GetReservationIDFromDGV());
            frm.ShowDialog();

            frmManageReservation_Load(null, null);
        }
        private void cmsDeleteReservation_Click(object sender, EventArgs e)
        {
            if (_ShowDeleteReservationMessage() == DialogResult.Yes)
            {
                if (clsReservation.DeleteReservation(_GetReservationIDFromDGV()))
                {
                    _ShowSuccessMessage("deleted");
                    frmManageReservation_Load(null, null);
                }
                else
                {
                    _ShowFailureMessage("deletion");
                }
            }
        }
        private void cmsConfirmReservation_Click(object sender, EventArgs e)
        {
            if(!clsRoom.CheckRoomAvailability(_GetRoomNumberFromDGV(), _GetResevedFroDateFromDGV(), _GetReservedToDateFromDGV()))
            {
                if(_ShowRoomUnavailableMessage() == DialogResult.Yes)
                {
                    // 
                    // Edit Reservation Form....
                    // 

                    frmAddEditReservation frm = new frmAddEditReservation(_GetReservationIDFromDGV());
                    frm.ShowDialog();
                    frmManageReservation_Load(null, null);
                }

                return;
            }

            if (_ShowConfirmReservationMessage() == DialogResult.Yes)
            {
                if (clsReservation.SetNewStatus(_GetReservationIDFromDGV(), clsReservation.enReservationStatus.Confirmed))
                {
                    _ShowConfirmReservationSuccessMessage();
                    frmManageReservation_Load(null, null);
                }
                else
                {
                    _ShowFailureMessage("confirmation");
                }
            }
        }
        private void cmsCancelReservation_Click(object sender, EventArgs e)
        {
            if (_ShowCancelReservationMessage() == DialogResult.Yes)
            {
                if (clsReservation.SetNewStatus(_GetReservationIDFromDGV(), clsReservation.enReservationStatus.Canceled))
                {
                    _ShowSuccessMessage("canceled");
                    frmManageReservation_Load(null, null);
                }
                else
                {
                    _ShowFailureMessage("cancellation");
                }
            }
        }
        private void cmsCheckIn_Click(object sender, EventArgs e)
        {
            frmCheckIn frm = new frmCheckIn(_GetReservationIDFromDGV());
            frm.ShowDialog();

            frmManageReservation_Load(null, null);
        }



    }
}
