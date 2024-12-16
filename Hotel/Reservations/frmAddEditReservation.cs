using Guna.UI2.WinForms;
using Hotel.Global;
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
using static HotelDatabase_Buisness.clsReservation;

namespace Hotel.Reservations
{
    public partial class frmAddEditReservation : Form
    {
        public Action<int?> ReservationIDBack;
        enum _enMode { AddNew, Update };
        _enMode _Mode = _enMode.AddNew;

        int? _ReservationID = null;
        clsReservation _Reservation = null;

        int? _SelectedPersonID = null;
        int? _GuestID = null;

        public frmAddEditReservation()
        {
            InitializeComponent();
            _Mode = _enMode.AddNew;
        }
        public frmAddEditReservation(int? reservationID)
        {
            InitializeComponent();

            _ReservationID = reservationID;
            _Mode = _enMode.Update;
        }

        void _cbWithNumberOfPeopleCounter(byte NumberOfPeople)
        {
            cbNumberOfPeople.Items.Clear();

            for (byte i = 1; i <= NumberOfPeople; i++)
            {
                cbNumberOfPeople.Items.Add(i);
            }

            if (cbNumberOfPeople.Items.Count > 0)
                cbNumberOfPeople.SelectedIndex = 0;
        }

        void _FillComboBoxWithNumberOfPeople()
        {
            switch (cbRoomTypes.Text)
            {
                case "Single":
                    _cbWithNumberOfPeopleCounter(clsRoomType.RoomTypeCapacities[clsRoom.enRoomTypes.Single]);
                    break;

                case "Double":
                    _cbWithNumberOfPeopleCounter(clsRoomType.RoomTypeCapacities[clsRoom.enRoomTypes.Double]);
                    break;

                case "Deluxe Suite":
                    _cbWithNumberOfPeopleCounter(clsRoomType.RoomTypeCapacities[clsRoom.enRoomTypes.DeluxeSuite]);
                    break;

                case "Family Room":
                    _cbWithNumberOfPeopleCounter(clsRoomType.RoomTypeCapacities[clsRoom.enRoomTypes.FamilyRoom]);
                    break;
            }
        }

        void _FillComboBoxWithRoomTypeTitles()
        {
            DataTable dtRoomTypeTitles = clsRoomType.GetAllRoomTypes();

            foreach(DataRow row in dtRoomTypeTitles.Rows)
            {
                cbRoomTypes.Items.Add(row["RoomTypeTitle"].ToString());
            }

            if(cbRoomTypes.Items.Count > 0)
                cbRoomTypes.SelectedIndex = 0;
        }

        void _FillComboBoxWithAvailableRooms(int? RoomType, DateTime ReservedForDate, DateTime ReservedToDate)
        {
            if (cbRoomTypes.Items.Count <= 0)
                return;

            cbAvailableRooms.Items.Clear();

            DataTable dtAvaliableRooms = clsRoom.GetAllAvailableRooms(RoomType, ReservedForDate, ReservedToDate);

            foreach(DataRow row in dtAvaliableRooms.Rows)
            {
                cbAvailableRooms.Items.Add(row["RoomNumber"].ToString());
            }

            if (cbAvailableRooms.Items.Count > 0)
                cbAvailableRooms.SelectedIndex = 0;
        }

        void _ResetFields()
        {
            ucPersonCardWithFilter1.Clear();

            dtpReservedForDate.MinDate = DateTime.Now;
            dtpReservedForDate.Value = DateTime.Now;
            dtpReservedToDate.Value = DateTime.Now.AddDays(5);

            lblReservationID.Text = "[????]";
            lblStatus.Text = "Pending";
            //lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;
        }

        void _ResetDefaultValues()
        {
            _FillComboBoxWithRoomTypeTitles();

            if (_Mode == _enMode.AddNew)
            {
                lblTitle.Text = "Add New Reservation";
                _Reservation = new clsReservation();

                tpReservationInfo.Enabled = false;
                ucPersonCardWithFilter1.FilterFocus();

                _ResetFields();
            }
            else
            {
                lblTitle.Text = "Update Reservation";
            }

            this.Text = lblTitle.Text;
        }
       
        void _ShowReservationNotFoundMessage(int? reservationID)
        {
            MessageBox.Show($"No reservation found with ID = {reservationID}", "Reservation Not Found",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        void _PopulateReservationDetails(clsReservation reservation)
        {
            lblReservationID.Text = reservation.ReservationID.ToString();
            dtpReservedForDate.Value = reservation.ReservedForDate;
            dtpReservedToDate.Value = reservation.ReservedToDate;
            lblStatus.Text = reservation.ReservationStatusName;
            lblCreatedByUser.Text = reservation.CreatedByUserInfo.Username;

            ucPersonCardWithFilter1.LoadPersonInfo(reservation.GuestInfo.PersonID);

            cbRoomTypes.SelectedIndex = cbRoomTypes.FindString(reservation.RoomInfo.RoomTypeName);
            cbAvailableRooms.SelectedIndex = cbAvailableRooms.FindString(reservation.RoomInfo.RoomNumber.ToString());
            cbNumberOfPeople.SelectedIndex = cbNumberOfPeople.FindString(
                clsRoomType.RoomTypeCapacities[reservation.RoomInfo.RoomTypeID].ToString());
        }
        
        void _FillReservationObjectWithFieldsData()
        {
            _Reservation.GuestID = _GuestID;
            _Reservation.RoomID = clsRoom.FindByRoomNumber(int.Parse(cbAvailableRooms.Text)).RoomID;
            _Reservation.ReservedForDate = dtpReservedForDate.Value;
            _Reservation.ReservedToDate = dtpReservedToDate.Value;
            _Reservation.NumberOfPeople = int.Parse(cbNumberOfPeople.Text);
            _Reservation.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

        int? _GetRoomTypeIDByTitle()
        {
            return clsRoomType.GetRoomTypeIDByTitle(cbRoomTypes.Text);
        }

        void _LoadData()
        {
            _Reservation = clsReservation.Find(_ReservationID);
            ucPersonCardWithFilter1.FilterEnabled = false;

            if (_Reservation == null)
            {
                _ShowReservationNotFoundMessage(_ReservationID);
                this.Close();
                return;
            }

            _PopulateReservationDetails(_Reservation);
        }

        bool _AddNewGuest()
        {
            clsGuest NewGuest = new clsGuest();
            NewGuest.PersonID = _SelectedPersonID;

            if (!NewGuest.Save())
                return false;

            _GuestID = NewGuest.GuestID;
            return true;
        }

        void _SaveReservation()
        {
            if(!_AddNewGuest())
            {
                MessageBox.Show("Data Saved Failed", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillReservationObjectWithFieldsData();

            if (_Reservation.Save())
            {
                lblTitle.Text = "Update Reservation";
                this.Text = lblTitle.Text;
                lblReservationID.Text = _Reservation.ReservationID.ToString();

                // change form mode to update
                _Mode = _enMode.Update;

                MessageBox.Show("Data Saved Successfully", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Trigger the event to send data back to the caller form
                ReservationIDBack?.Invoke(_Reservation.ReservationID);
            }
            else
            {
                MessageBox.Show("Data Saved Failed", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditReservation_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == _enMode.Update)
                _LoadData();
        }

        private void ucPersonCardWithFilter1_OnPersonSelected(object sender, People.Controls.ucPersonCardWithFilter.PersonSelectedEventArgs e)
        {
            if (!e.PersonID.HasValue && e.NotionalNo == null)
            {
                btnNext.Enabled = false;
                tpReservationInfo.Enabled = false;
                btnSave.Enabled = false;

                return;
            }

            _SelectedPersonID = e.PersonID;

            btnNext.Enabled = true;
            tpReservationInfo.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tcAddEditReservation.SelectedTab = tcAddEditReservation.TabPages["tpReservationInfo"];
        }

        private void cbRoomTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FillComboBoxWithNumberOfPeople();

            int? RoomTypeID = _GetRoomTypeIDByTitle();
            _FillComboBoxWithAvailableRooms(RoomTypeID, dtpReservedForDate.Value, dtpReservedToDate.Value);
        }

        private void dtpReservedForDate_ValueChanged(object sender, EventArgs e)
        {
            int? RoomTypeID = _GetRoomTypeIDByTitle();
            _FillComboBoxWithAvailableRooms(RoomTypeID, dtpReservedForDate.Value, dtpReservedToDate.Value);
        }

        private void dtpReservedToDate_ValueChanged(object sender, EventArgs e)
        {
            int? RoomTypeID = _GetRoomTypeIDByTitle();
            _FillComboBoxWithAvailableRooms(RoomTypeID, dtpReservedForDate.Value, dtpReservedToDate.Value);
        }

        private void dtpReservedForDate_Validating(object sender, CancelEventArgs e)
        {
            if (_Mode == _enMode.Update &&
                _Reservation.ReservedForDate != dtpReservedForDate.Value &&
                dtpReservedForDate.Value.Date < DateTime.Now.Date)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpReservedForDate, "You cannot reserve in the past date !");
                return;
            }
            else
            {
                errorProvider1.SetError(dtpReservedForDate, null);
            }


            if (dtpReservedForDate.Value == dtpReservedToDate.Value)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpReservedForDate, "The date should cover a night at least!");
                return;
            }
            else
            {
                errorProvider1.SetError(dtpReservedForDate, null);
            }
        }

        private void dtpReservedToDate_Validating(object sender, CancelEventArgs e)
        {
            if (dtpReservedToDate.Value < dtpReservedForDate.Value)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpReservedToDate, "The 'Reserved To' can not be before Rederved From!");
                return;
            }
            else
            {
                errorProvider1.SetError(dtpReservedToDate, null);
            }

            if (dtpReservedForDate.Value == dtpReservedToDate.Value)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpReservedToDate, "The date should cover a night at least!");
                return;
            }
            else
            {
                errorProvider1.SetError(dtpReservedToDate, null);
            }
        }

        private void frmAddEditReservation_Activated(object sender, EventArgs e)
        {
            ucPersonCardWithFilter1.FilterFocus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _SaveReservation();
        }

        private void ComboBoxes_Validating(object sender, CancelEventArgs e)
        {
            Guna2ComboBox comboBox = (Guna2ComboBox)sender;

            if(comboBox.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(comboBox, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(comboBox, null);
            }
        }
    }
}
