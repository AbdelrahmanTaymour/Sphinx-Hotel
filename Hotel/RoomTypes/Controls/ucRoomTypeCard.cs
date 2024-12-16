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

namespace Hotel.RoomTypes.Controls
{
    public partial class ucRoomTypeCard : UserControl
    {
        int? _RoomTypeID = null;
        clsRoomType _RoomType = null;

        public int? RoomTypeID => _RoomTypeID;
        public clsRoomType RoomTypeInfo => _RoomType;

        public ucRoomTypeCard()
        {
            InitializeComponent();
        }

        void _FillRoomTypeData()
        {
            int TotalAllRooms = clsRoom.GetRoomsCount();
            int TotalRoomsWithSpecificRoomType = clsRoomType.GetRoomsCountByRoomTypeID(_RoomType.RoomTypeID);

            lblRoomTypeID.Text = _RoomType.RoomTypeID.ToString();
            lblRoomTypeTitle.Text = _RoomType.RoomTypeTitle;
            lblCapacity.Text = _RoomType.Capacity.ToString();
            lblDescription.Text = _RoomType.Description ?? "N/A";
            lblPricePerNight.Text = _RoomType.PricePerNight.ToString("C");
            lblRoomCount.Text = TotalRoomsWithSpecificRoomType.ToString() + "/" + TotalAllRooms.ToString();

            llEditRoomTypeInfo.Enabled = true;
        }

        public void Reset()
        {
            _RoomTypeID = null;
            _RoomType = null;

            lblRoomTypeID.Text = "[????]";
            lblRoomTypeTitle.Text = "[????]";
            lblCapacity.Text = "[????]";
            lblDescription.Text = "[????]";
            lblPricePerNight.Text = "[????]";
            lblRoomCount.Text = "[????]";
            llEditRoomTypeInfo.Enabled = false;
        }

        public void LoadRoomTypeInfo(int? RoomTypeID)
        {
            _RoomTypeID = RoomTypeID;

            if (!_RoomTypeID.HasValue)
            {
                MessageBox.Show("There is no Room Type!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Reset();

                return;
            }

            _RoomType = clsRoomType.Find(_RoomTypeID);

            if (_RoomType == null)
            {
                MessageBox.Show($"There is no Room Type with ID = {_RoomTypeID} !",
                    "Missing RoomType", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Reset();

                return;
            }

            _FillRoomTypeData();
        }

        private void llEditRoomTypeInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmEditRoomType frm = new frmEditRoomType(_RoomTypeID);
            frm.RoomTypeIDBack += LoadRoomTypeInfo;
            frm.ShowDialog();
        }
    }
}
