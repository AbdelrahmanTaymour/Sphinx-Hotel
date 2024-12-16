using Guna.UI2.WinForms;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hotel.People
{
    public partial class frmAddEditPerson : Form
    {
        public Action<int?> PersonIDBack;

        enum _enMode { AddNew, Update };
        _enMode _Mode = _enMode.AddNew;

        int? _PersonID = null;
        clsPerson _Person = null;

        public frmAddEditPerson()
        {
            InitializeComponent();

            _Mode = _enMode.AddNew;
        }
        public frmAddEditPerson(int? PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
            _Mode = _enMode.Update;
        }

        void _FillCountriesComboBox()
        {
            DataTable Countries = clsCountry.GetAllCountries();

            foreach(DataRow country in Countries.Rows)
            {
                cbCountry.Items.Add(country["CountryName"].ToString());
            }
        }
        void _ResetFields()
        {
            foreach(Control ctrl in this.Controls)
            {
                if (ctrl is Guna2TextBox txtBox)
                    txtBox.Clear();

                if (ctrl is Guna2ComboBox comboBox)
                    comboBox.SelectedIndex = cbCountry.FindString("Egypt");
            }

            rbMale.Checked = true;
            pbGender.Image = Resources.gender_male;
            pbPersonImage.Image = Resources.default_male;
        }
        void _ResetDefualtValues()
        {
            _FillCountriesComboBox();

            if(_Mode == _enMode.AddNew)
            {
                lblTitle.Text = this.Text = "Add New Person";
                _Person = new clsPerson();
                _ResetFields();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            this.Text = lblTitle.Text;

            //set default image for the customer
            if (rbMale.Checked)
                pbPersonImage.Image = Resources.default_male;
            else
                pbPersonImage.Image = Resources.default_female;

            //hide/show the remove link in case there is no image for the customer
            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);


            dtpDateOfBirth.Value = DateTime.Now;
            //should not allow adding age more than 130 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-130);

            //this will set default country to Egypt
            cbCountry.SelectedIndex = cbCountry.FindString("Egypt");
        }
        void _FillConrolsWithPersonInfo()
        {
            lblPersonID.Text = _Person.PersonID.ToString();
            txtFullName.Text = _Person.FullName;
            txtNationalNo.Text = _Person.NationalNo;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if(_Person.Gender == clsPerson.enGender.Male)
            {
                rbMale.Checked = true;
                pbPersonImage.Image = Resources.default_male;
            }
            else
            {
                rbFemale.Checked = true;
                pbPersonImage.Image = Resources.default_female;
            }

            }
        void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);

            if(_Person == null)
            {
                MessageBox.Show($"There is no person with ID = {_PersonID} !",
                  "Missing Person", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }

            _FillConrolsWithPersonInfo();

            //load person image in case it was set.
            if (_Person.ImagePath != null)
                pbPersonImage.ImageLocation = _Person.ImagePath;

            //hide/show the remove link in case there is no image for the person
            llRemoveImage.Visible = (_Person.ImagePath != null);

        }
        bool _HandlePersonImage()
        {
            // this procedure will handle the person image,
            // it will take care of deleting the old image from the folder
            // in case the image changed, and it will rename the new image with guid and 
            // place it in the images folder.

            // _Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {

                if (_Person.ImagePath != null)
                {
                    // first we delete the old image from the folder in case there is any.
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException iox)
                    {
                        clsLogger.LogError("IO Exception", iox);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        clsLogger.LogError("General Exception", ex);
                        return false;
                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    // then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;

                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }
        void _FillPersonObjectWithFieldsData()
        {
            _Person.FullName = txtFullName.Text.Trim();
            _Person.Email = string.IsNullOrWhiteSpace(txtEmail.Text.Trim()) ? null : txtEmail.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Address = string.IsNullOrWhiteSpace(txtAddress.Text.Trim()) ? null : txtAddress.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.CountryID = clsCountry.Find(cbCountry.Text).CountryID;
            _Person.Gender = (rbMale.Checked) ? clsPerson.enGender.Male : clsPerson.enGender.Female;
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = null;
        }
        void _SavePerson()
        {
            _FillPersonObjectWithFieldsData();

            if(_Person.Save())
            {
                lblTitle.Text = "Update Person";
                this.Text = lblTitle.Text;
                lblPersonID.Text = _Person.PersonID.ToString();

                // change form mode to update
                _Mode = _enMode.Update;

                MessageBox.Show("Data Saved Successfully", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Trigger the event to send data back to the caller form
                PersonIDBack?.Invoke(_Person.PersonID);
            }
            else
            {
                MessageBox.Show("Data Saved Failed", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Guna2TextBox)sender).Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(((Guna2TextBox)sender), "This field is required!");
            }
            else
            {
                errorProvider1.SetError(((Guna2TextBox)sender), null);
            }
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
                return;

            //validate email format
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {

                errorProvider1.SetError(txtEmail, "This Email is not valid");

            }
            else
                errorProvider1.SetError(txtEmail, null);
        }
        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }

            // Make sure the national number is not used by another person
            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.DoesPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This National No. is used for another person");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == _enMode.Update)
                _LoadData();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandlePersonImage())
                return;

            _SavePerson();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            pbGender.Image = Resources.gender_male;

            // change the default image to male in case there is no image set.
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.default_male;
        }
        private void lblMale_Click(object sender, EventArgs e)
        {
            rbMale.Checked = true;

            pbGender.Image = Resources.gender_male;

            // change the default image to male in case there is no image set.
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.default_male;
        }
        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            pbGender.Image = Resources.gender_female;

            // change the default image to female in case there is no image set.
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.default_female;
        }
        private void lblFemale_Click(object sender, EventArgs e)
        {
            rbFemale.Checked = true;

            pbGender.Image = Resources.gender_female;

            // change the default image to female in case there is no image set.
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.default_female;
        }
        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string FilePath = openFileDialog1.FileName;
                pbPersonImage.Load(FilePath);
                llRemoveImage.Visible = true;
            }
        }
        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.default_male;
            else
                pbPersonImage.Image = Resources.default_female;

            llRemoveImage.Visible = false;
        }


    }
}
