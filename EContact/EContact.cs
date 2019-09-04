using EContact.EcontactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EContact
{
    public partial class EContact : Form
    {
        public EContact()
        {
            InitializeComponent();
        }

        contactClass c = new contactClass();

        private void labelname_Click(object sender, EventArgs e)
        {
             
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            c.ContactName = textBoxName.Text;
            c.ContactNumber = textBoxContactNumber.Text;
            c.Address = textBoxaddress.Text;
            c.Gender = comboBox1.Text;

            bool success = c.Insert(c);
            if (success)
            {
                MessageBox.Show("New Contact Inserted");
                Clear();
            }
            else
            {
                MessageBox.Show("New Contact Failed");
            }

            DataTable dt = c.Select();
            dgvcontact.DataSource = dt;

        }

        private void EContact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvcontact.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            textBoxName.Text = "";
            textBoxContactNumber.Text = "";
            textBoxaddress.Text = "";
            comboBox1.Text = "";
                 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.ContactID = int.Parse(textBoxcontactID.Text);
            c.ContactName = textBoxName.Text;
            c.ContactNumber = textBoxContactNumber.Text;
            c.Address = textBoxaddress.Text;
            c.Gender = comboBox1.Text;

            bool success = c.update(c);
            if (success)
            {
                MessageBox.Show("Updated Successfully");
                Clear();
            }
            else
            {
                MessageBox.Show("Cannot Update");
            }

            DataTable dt = c.Select();
            dgvcontact.DataSource = dt;

        }

        private void dgvcontact_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            textBoxcontactID.Text = dgvcontact.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxName.Text = dgvcontact.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxContactNumber.Text = dgvcontact.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxaddress.Text = dgvcontact.Rows[rowIndex].Cells[3].Value.ToString();
            comboBox1.Text = dgvcontact.Rows[rowIndex].Cells[4].Value.ToString();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(textBoxcontactID.Text);
            bool success = c.Delete(c);
            if (success)
            {
                MessageBox.Show("Record Deleted Successfully");
                Clear();
            }
            else
            {
                MessageBox.Show("Cannot Delete");
            }

            DataTable dt = c.Select();
            dgvcontact.DataSource = dt;

        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE ContactName LIKE '%"+keyword+"%'",conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvcontact.DataSource = dt;
        }
    }
}
