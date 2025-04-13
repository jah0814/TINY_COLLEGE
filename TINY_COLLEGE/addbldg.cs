using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINY_COLLEGE
{
    public partial class addbldg: Form
    {
        public addbldg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate if the fields contain only the required characters
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("BLDG_CODE and BLDG_NAME cannot be empty.");
                return;
            }

            if (!int.TryParse(textBox1.Text, out _))
            {
                MessageBox.Show("BLDG_CODE must contain numbers only.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("BLDG_LOCATION cannot be empty.");
                return;
            }

            // Define the connection string
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "INSERT INTO BUILDING (BLDG_CODE, BLDG_NAME, BLDG_LOCATION) " +
                           "VALUES (@BLDG_CODE, @BLDG_NAME, @BLDG_LOCATION)";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BLDG_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@BLDG_NAME", textBox2.Text);
                    cmd.Parameters.AddWithValue("@BLDG_LOCATION", textBox3.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Building added successfully!");
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

        }
        

        // Helper function inside the form
        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bldg form = new bldg();
            form.Show();
            this.Hide();
        }
    }
}
