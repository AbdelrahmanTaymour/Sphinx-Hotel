using Hotel.Grobal;
using Hotel.Properties;
using HotelDatabase_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.People.Controls
{
    public partial class ucPersonCard : UserControl
    {
        int? _PersonID = null;
        clsPerson _Person = null;

        public int? PersonID
        {
            get { return _PersonID; }
        }
        public clsPerson PersonInfo
        {
            get { return _Person; }
        }

        public ucPersonCard()
        {
            InitializeComponent();
        }
        void _LoadPersonImage()
        {
            if (_Person.Gender == clsPerson.enGender.Male)
                pbPersonImage.Image = Resources.default_male;
            else
                pbPersonImage.Image = Resources.default_female;


            if (_Person.ImagePath != null)
                if (File.Exists(_Person.ImagePath))
                    pbPersonImage.ImageLocation = _Person.ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " +
                        _Person.ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void _ResetDefualtValue()
        {
            _PersonID = null;
            _Person = null;

            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGender.Image = Resources.gender_male;
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.default_male;

            llEditPersonInfo.Enabled = false;
        }
        void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;

            lblPersonID.Text = _Person.PersonID.ToString();
            lblFullName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = _Person.GenderText;
            lblEmail.Text = _Person.Email ?? "N/A";
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = clsFormat.DateToShort(_Person.DateOfBirth);
            lblCountry.Text = _Person.CountryInfo.CountryName;
            lblAddress.Text = _Person.Address;

            pbGender.Image = (_Person.Gender == clsPerson.enGender.Male) ? Resources.gender_male : Resources.gender_female;

            _LoadPersonImage();

            llEditPersonInfo.Enabled = true;
        }

        public void LoadPersonInfo(int? PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if(_Person == null)
            {
                _ResetDefualtValue();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if (_Person == null)
            {
                _ResetDefualtValue();
                MessageBox.Show("No Person with National No.: " + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }
        public void Reset()
        {
            _ResetDefualtValue();
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_Person.PersonID);
            frm.PersonIDBack += LoadPersonInfo;
            frm.ShowDialog();
        }
    }
}
