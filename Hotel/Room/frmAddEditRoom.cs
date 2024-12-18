﻿using Guna.UI2.WinForms;
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

namespace Hotel.Room
{
    public partial class frmAddEditRoom : Form
    {
        public Action<int?> RoomIDBack;

        enum _enMode { AddNew, Update }
        _enMode _Mode = _enMode.AddNew;

        int? _RoomID = null;
        clsRoom _Room = null;
        public frmAddEditRoom()
        {
            InitializeComponent();

            _Mode = _enMode.AddNew;
        }
        public frmAddEditRoom(int? RoomID)
        {
            InitializeComponent();

            _RoomID = RoomID;
            _Mode = _enMode.Update;
        }

        void _FillComboBoxWithRoomTypeTitles()
        {
            DataTable dtRoomTypesTitle = clsRoomType.GetAllRoomTypesTitle();

            foreach (DataRow drTitle in dtRoomTypesTitle.Rows)
            {
                cbRoomTypes.Items.Add(drTitle["RoomTypeTitle"].ToString());
            }

            if (cbRoomTypes.Items.Count > 0)
                cbRoomTypes.SelectedIndex = 0;
        }

        void _ResetFields()
        {
            lblRoomID.Text = "N\\A";
            txtRoomSize.Clear();
            txtRoomNumber.Clear();
            txtRoomPhone.Clear();
            numericRoomFloor.Value = 1;
            chkIsSmokingAllowed.Checked = false;
            chkIsPetFriendly.Checked = false;
            lblStatus.Text = "Available";
        }

        void _ResetDefaultValues()
        {
            _FillComboBoxWithRoomTypeTitles();

            if (_Mode == _enMode.AddNew)
            {
                lblTitle.Text = "Add New Room";
                _Room = new clsRoom();

                _ResetFields();
            }
            else
            {
                lblTitle.Text = "Update Room";
            }

            this.Text = lblTitle.Text;
        }

        void _LoadData()
        {
            _Room = clsRoom.Find(_RoomID);

            if (_Room == null)
            {
                MessageBox.Show("No Room with ID = " + _Room, "Room Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.Close();

                return;
            }

            lblRoomID.Text = _Room.RoomID.ToString();
            lblStatus.Text = _Room.RoomStatusName;
            txtRoomNumber.Text = _Room.RoomNumber.ToString();
            txtRoomSize.Text = _Room.Size.ToString();
            txtRoomPhone.Text = _Room.RoomPhone;
            numericRoomFloor.Value = _Room.FloorNumber;
            chkIsSmokingAllowed.Checked = _Room.IsSmokingAllowed;
            chkIsPetFriendly.Checked = _Room.IsPetFriendly;
            cbRoomTypes.SelectedIndex = cbRoomTypes.FindString(_Room.RoomTypeName);
        }

        void _FillRoomObjectWithFieldsData()
        {
            _Room.RoomTypeID = (clsRoom.enRoomTypes)clsRoomType.Find(cbRoomTypes.Text).RoomTypeID;
            _Room.RoomNumber = int.Parse(txtRoomNumber.Text);
            _Room.FloorNumber = (byte)numericRoomFloor.Value;
            _Room.Size = decimal.Parse(txtRoomSize.Text);
            _Room.RoomPhone = txtRoomPhone.Text;
            _Room.IsSmokingAllowed = chkIsSmokingAllowed.Checked;
            _Room.IsPetFriendly = chkIsPetFriendly.Checked;
        }

        void _SaveRoom()
        {
            _FillRoomObjectWithFieldsData();

            if (_Room.Save())
            {
                lblTitle.Text = "Update Room";
                this.Text = lblTitle.Text;
                lblRoomID.Text = _Room.RoomID.ToString();

                // change form mode to update
                _Mode = _enMode.Update;

                MessageBox.Show("Data Saved Successfully", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Trigger the event to send data back to the caller form
                RoomIDBack?.Invoke(_Room.RoomID);
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

        private void frmAddEditRoom_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == _enMode.Update)
                _LoadData();
        }

        private void txtRoomNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRoomSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            char inputChar = e.KeyChar;

            // Allow digits, the decimal point, and the backspace.
            bool isDigit = char.IsDigit(inputChar);
            bool isDecimalPoint = (inputChar == '.');
            bool isBackspace = (inputChar == '\b');

            // If the input character is not a digit, decimal point, or backspace, suppress it.
            if (!isDigit && !isDecimalPoint && !isBackspace)
            {
                e.Handled = true;
            }

            // Make sure there is only one decimal point in the input.
            if (isDecimalPoint && ((sender as Guna2TextBox).Text.Contains(".") || (sender as Guna2TextBox).Text.Length == 0))
            {
                e.Handled = true;
            }
        }

        private void txtRoomSize_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Guna2TextBox)sender).Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError((Guna2TextBox)sender, "This field cannot be empty!");
            }
            else
            {
                errorProvider1.SetError((Guna2TextBox)sender, null);
            }
        }
        private void txtRoomNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtRoomNumber, "This field cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtRoomNumber, null);
            }

            
            int RoomNumber = int.Parse(txtRoomNumber.Text.Trim().ToString());

            if(clsRoom.IsRoomNumberExists(RoomNumber))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtRoomNumber, "This Room Number Exists, Enter a new one please!");
            }
            else
            {
                errorProvider1.SetError(txtRoomNumber, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _SaveRoom();
        }
    }
}
