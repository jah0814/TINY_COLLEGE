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
    public partial class adddept: Form
    {
        public adddept()
        {
            InitializeComponent();
        }

        private void adddept_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate if the fields contain only the required characters
            if (!int.TryParse(textBox1.Text, out _) || !int.TryParse(textBox4.Text, out _))
            {
                MessageBox.Show("DEPT_CODE and PROF_NUM must contain only numbers.");
                return;
            }

            if (!IsLettersOnly(textBox2.Text))
            {
                MessageBox.Show("DEPT_NAME must contain only letters.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out _))
            {
                MessageBox.Show("SCHOOL_CODE must contain only numbers.");
                return;
            }

            // Define the connection string
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "INSERT INTO DEPARTMENT (DEPT_CODE, DEPT_NAME, SCHOOL_CODE, PROF_NUM) " +
                           "VALUES (@DEPT_CODE, @DEPT_NAME, @SCHOOL_CODE, @PROF_NUM)";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DEPT_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@DEPT_NAME", textBox2.Text);
                    cmd.Parameters.AddWithValue("@SCHOOL_CODE", textBox3.Text);
                    cmd.Parameters.AddWithValue("@PROF_NUM", textBox4.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Department added successfully!");
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

// Helper functions inside the form
private bool IsLettersOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsLetter(c)) return false;
            }
            return true;
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dept form = new dept();
            form.Show();
            this.Hide();
        }
    }
 }

