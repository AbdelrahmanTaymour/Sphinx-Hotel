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

namespace Hotel.Room.Controls
{
    public partial class ucRoomCard : UserControl
    {
        int? _RoomID = null;
        clsRoom _Room = null;

        int? RoomID => _RoomID;
        clsRoom RoomInfo => _Room;
        public ucRoomCard()
        {
            InitializeComponent();
        }

        void _FillRoomData()
        {
            lblRoomID.Text = _Room.RoomID.ToString();
            lblRoomTypeID.Text = _Room.RoomTypeID.ToString();
            lblRoomNumber.Text = _Room.RoomNumber.ToString();
            lblStatus.Text = _Room.RoomStatusName;
            lblRoomPhone.Text = _Room.RoomPhone;
            lblRoomSize.Text = _Room.Size.ToString();
            lblRoomFloor.Text = _Room.FloorNumber.ToString();
            lblIsSomkingAllowed.Text = _Room.IsSmokingAllowed ? "Yes" : "No";
            lblIsPetFriendly.Text = _Room.IsPetFriendly ? "Yes" : "No";
        }

        void Clear()
        {
            _RoomID = null;
            _Room = null;

            lblRoomID.Text = "[????]";
            lblRoomTypeID.Text = "[????]";
            lblRoomNumber.Text = "[????]";
            lblStatus.Text = "[????]";
            lblRoomPhone.Text = "[????]";
            lblRoomSize.Text = "[????]";
            lblRoomFloor.Text = "[????]";
            lblIsSomkingAllowed.Text = "[????]";
            lblIsPetFriendly.Text = "[????]";
        }

        public void LoadRoomInfo(int? RoomID)
        {
            _RoomID = RoomID;

            if (!_RoomID.HasValue)
            {
                MessageBox.Show("There is no Room!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Clear();
                return;
            }

            _Room = clsRoom.Find(_RoomID);

            if (_Room == null)
            {
                MessageBox.Show($"There is no Room with ID = {_RoomID} !",
                    "Missing Room", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Clear();
                return;
            }

            _FillRoomData();
        }
    }
}
