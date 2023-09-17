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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MainForm_Load();
        }

        private void MainForm_Load()
        {
            DGVListContacts.DataSource = clsContact.GetAllContacts();
        }
        private void DGVListContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnAddNewContact_Click(object sender, EventArgs e)
        {
            frmAddEdit frm = new frmAddEdit(-1);

            frm.ShowDialog();

            MainForm_Load();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEdit frm = new frmAddEdit((int)DGVListContacts.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
            MainForm_Load();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You sure you want to delete it?", "Delete Contact") == DialogResult.OK)
            {
                if (clsContact.DeleteContact((int)DGVListContacts.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully", "Delete Contact");
                }
                else
                {
                    MessageBox.Show("Failed to delete Contact", "Delete Contact");
                }
            }
            MainForm_Load();
        }
    }       
}
