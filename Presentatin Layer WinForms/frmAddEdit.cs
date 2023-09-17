using ConsolApp_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsWFApp
{
    public partial class frmAddEdit : Form
    {
        enum enMode { AddNew, Update};

        private int _ContactID;
        private enMode _Mode;
        private clsContact _Contact;
        public frmAddEdit(int ContactID)
        {
            InitializeComponent();
            _ContactID = ContactID;

            if(ContactID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }
        private void _FillCountriesCBX()
        {
            DataTable dt = clsCountry.GetAllCountries();

            foreach (DataRow dr in dt.Rows)
            {
                if(!cbCountry.Items.Contains(dr["CountryName"]))
                    cbCountry.Items.Add(dr["CountryName"].ToString());
            }
        }
        private void _LoadData()
        {

            _FillCountriesCBX();
            cbCountry.SelectedIndex = 0;

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Contact";
                _Contact = new clsContact();
                return;
            }
            else
            {
                lblTitle.Text = "Edit Contact with ID " + _ContactID;

                _Contact = clsContact.Find(_ContactID);

                lblID.Text = _Contact.ContactID.ToString();
                tbFirstName.Text = _Contact.FirstName;
                tbLastName.Text = _Contact.LastName;
                tbPhone.Text = _Contact.Phone;
                tbEmail.Text = _Contact.Email;
                tbAddress.Text = _Contact.Address;
                dtpDateOfBirth.Value = _Contact.DateOfBirth;
                cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Contact.CountryID).Name);

                return;

            }
        }

        private void frmAddEdit_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Contact.FirstName = tbFirstName.Text;
            _Contact.LastName = tbLastName.Text;
            _Contact.Email = tbEmail.Text;
            _Contact.Phone = tbPhone.Text;
            _Contact.Address = tbAddress.Text;
            _Contact.CountryID = clsCountry.Find(cbCountry.Text).ID;

            if (_Contact.Save())
            {
                MessageBox.Show("Contact Saved Successfully", "Save Contact");
            }
            else
            {
                MessageBox.Show("Failed To Save Contact", "Save Contact");
            }

            _Mode = enMode.Update;
            lblTitle.Text = "Edit Contact With ID: " + _Contact.ContactID;
            lblID.Text = _Contact.ContactID.ToString();
        }
    }
}
