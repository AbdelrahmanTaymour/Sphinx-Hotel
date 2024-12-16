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
using static HotelDatabase_Buisness.clsRoom;

namespace Hotel.Room
{
    public partial class frmManageRooms : Form
    {
        DataTable _dtRooms;
        public frmManageRooms()
        {
            InitializeComponent();
        }

        void _FillComboBoxWithRoomTypeTitles()
        {
            cbRoomTypes.Items.Clear();
            cbRoomTypes.Items.Add("All");
            cbRoomTypes.SelectedIndex = 0;

            DataTable dtRoomTypesTitle = clsRoomType.GetAllRoomTypesTitle();

            foreach (DataRow drTitle in dtRoomTypesTitle.Rows)
            {
                cbRoomTypes.Items.Add(drTitle["RoomTypeTitle"].ToString());
            }
        }

        string _GetRealColumnNameInDB()
        {
            switch (cbFilterBy.Text)
            {
                case "Room ID":
                    return "RoomID";

                case "Room Type":
                    return "RoomTypeTitle";

                case "Room Number":
                    return "RoomNumber";

                case "Floor Number":
                    return "FloorNumber";

                case "Size":
                    return "Size";

                case "Status":
                    return "Status";

                case "Is Smoking Allowed":
                    return "IsSmokingAllowed";

                case "Is Pet Friendly":
                    return "IsPetFriendly";

                default:
                    return "None";
            }

        }

        void _RefreshRoomList()
        {
            _dtRooms = clsRoom.GetAllRooms();
            dgvRoomsList.DataSource = _dtRooms;

            if (dgvRoomsList.Rows.Count > 0)
            {
                dgvRoomsList.Columns[0].HeaderText = "Room ID";
                dgvRoomsList.Columns[0].Width = 110;

                dgvRoomsList.Columns[1].HeaderText = "Room Type";
                dgvRoomsList.Columns[1].Width = 160;

                dgvRoomsList.Columns[2].HeaderText = "Room Number";
                dgvRoomsList.Columns[2].Width = 130;

                dgvRoomsList.Columns[3].HeaderText = "Floor Number";
                dgvRoomsList.Columns[3].Width = 130;

                dgvRoomsList.Columns[4].HeaderText = "Size";
                dgvRoomsList.Columns[4].Width = 100;

                dgvRoomsList.Columns[5].HeaderText = "Status";
                dgvRoomsList.Columns[5].Width = 130;

                dgvRoomsList.Columns[6].HeaderText = "Is Smoking Allowed";
                dgvRoomsList.Columns[6].Width = 150;

                dgvRoomsList.Columns[7].HeaderText = "Is Pet Friendly";
                dgvRoomsList.Columns[7].Width = 150;
            }
        }

        int? _GetRoomIDFromDGV()
        {
            return (int?)dgvRoomsList.CurrentRow.Cells["RoomID"].Value;
        }

        byte? _GetRoomTypeIDFromRoomID(int? RoomID)
        {
            return (byte?)clsRoom.Find(RoomID)?.RoomTypeID;
        }

        private void frmManageRooms_Load(object sender, EventArgs e)
        {
            _RefreshRoomList();
            _FillComboBoxWithRoomTypeTitles();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None") && (cbFilterBy.Text != "Status") && (cbFilterBy.Text != "Room Type");

            cbRoomStatus.Visible = (cbFilterBy.Text == "Status");
            cbRoomTypes.Visible = (cbFilterBy.Text == "Room Type");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

            if (cbRoomStatus.Visible)
            {
                cbRoomStatus.SelectedIndex = 0;
            }

            if (cbRoomTypes.Visible)
            {
                cbRoomTypes.SelectedIndex = 0;
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (_dtRooms.Rows.Count == 0)
                return;

            string ColumnName = _GetRealColumnNameInDB();

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) ||
                cbFilterBy.Text == "None")
            {
                _dtRooms.DefaultView.RowFilter = "";
                return;
            }

            if (cbFilterBy.Text == "Room ID" || cbFilterBy.Text == "Room Number" || cbFilterBy.Text == "Floor Number")
            {
                // search with numbers
                _dtRooms.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilterBy.Text.Trim());
            }
            else
            {
                // search with string
                _dtRooms.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, txtFilterBy.Text.Trim());
            }
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Room ID" || cbFilterBy.Text == "Room Number" || cbFilterBy.Text == "Floor Number")
                // make sure that the user can only enter the numbers
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbRoomStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtRooms.Rows.Count == 0)
                return;

            if (cbRoomStatus.Text == "All")
            {
                _dtRooms.DefaultView.RowFilter = "";
                return;
            }

            _dtRooms.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", "Status", cbRoomStatus.Text);

        }

        private void cbRoomTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtRooms.Rows.Count == 0)
                return;

            if (cbRoomTypes.Text == "All")
            {
                _dtRooms.DefaultView.RowFilter = "";
                return;
            }

            _dtRooms.DefaultView.RowFilter =
                string.Format("[{0}] like '{1}%'", "RoomTypeTitle", cbRoomTypes.Text);

        }

        private void cmsShowRoomDetails_Click(object sender, EventArgs e)
        {
            frmShowRoomInfo frm = new frmShowRoomInfo(_GetRoomIDFromDGV(), _GetRoomTypeIDFromRoomID(_GetRoomIDFromDGV()));
            frm.ShowDialog();
        }

        private void dgvRoomsList_DoubleClick(object sender, EventArgs e)
        {
            frmShowRoomInfo frm = new frmShowRoomInfo(_GetRoomIDFromDGV(), _GetRoomTypeIDFromRoomID(_GetRoomIDFromDGV()));
            frm.ShowDialog();
        }

        private void btnAddNewRoom_Click(object sender, EventArgs e)
        {
            frmAddEditRoom frm = new frmAddEditRoom();
            frm.ShowDialog();

            frmManageRooms_Load(null, null); // Refresh Manage Form
        }

        private void cmsAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditRoom frm = new frmAddEditRoom();
            frm.ShowDialog();

            frmManageRooms_Load(null, null); // Refresh Manage Form
        }

        private void cmsEditRoom_Click(object sender, EventArgs e)
        {
            frmAddEditRoom frm = new frmAddEditRoom(_GetRoomIDFromDGV());
            frm.ShowDialog();

            frmManageRooms_Load(null, null); // Refresh Manage Form
        }

        private void cmsDeleteRoom_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this room?", "Confirm",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsRoom.DeleteRoom(_GetRoomIDFromDGV()))
                {
                    MessageBox.Show("Room deleted successfully!", "Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshRoomList();
                }
                else
                {
                    MessageBox.Show("Room deleted failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmsPutUnderMaintenance_Click(object sender, EventArgs e)
        {
            int? RoomID = _GetRoomIDFromDGV();

            if (MessageBox.Show($"Are you sure you want to put this room with ID = {RoomID} under" +
                $" maintenance?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                 == DialogResult.No)
                return;

            if (clsRoom.ChangeRoomStatus(RoomID, clsRoom.enRoomStatus.UnderMaintenance))
            {
                MessageBox.Show($"The room with ID = {RoomID} has been successfully put under " +
                    $"maintenance.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _RefreshRoomList();
            }
            else
            {
                MessageBox.Show($"Failed to put the room with ID = {RoomID} under maintenance.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmsReleaseFromMaintenance_Click(object sender, EventArgs e)
        {
            int? RoomID = _GetRoomIDFromDGV();

            if (MessageBox.Show($"Are you sure you want to release this room with ID = {RoomID} from" +
                $" maintenance?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                 == DialogResult.No)
                return;

            if (clsRoom.ChangeRoomStatus(RoomID, clsRoom.enRoomStatus.Available))
            {
                MessageBox.Show($"The room with ID = {RoomID} has been successfully released from" +
                    $" maintenance.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _RefreshRoomList();
            }
            else
            {
                MessageBox.Show($"Failed to release the room with ID = {RoomID} from maintenance.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            string Status = (string)dgvRoomsList.CurrentRow.Cells["Status"].Value;

            if (Status == "Booked")
            {
                cmsPutUnderMaintenance.Enabled = cmsReleaseFromMaintenance.Enabled = false;
                return;
            }

            cmsPutUnderMaintenance.Enabled = (Status == "Available");
            cmsReleaseFromMaintenance.Enabled = !cmsPutUnderMaintenance.Enabled;

        }
    }
    
}
