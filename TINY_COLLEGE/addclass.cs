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
    public partial class addclass: Form
    {
        public addclass()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsDigitsOnly(textBox1.Text) || !IsDigitsOnly(textBox4.Text) || !IsDigitsOnly(textBox5.Text) || !IsDigitsOnly(textBox6.Text) || !IsDigitsOnly(textBox7.Text))
            {
                MessageBox.Show("CLASS_CODE, CRS_CODE, PROF_NUM, ROOM_CODE, and SEMESTER_CODE must contain only numbers.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("CLASS_SECTION cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("CLASS_TIME cannot be empty.");
                return;
            }

            // Define the connection string
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "INSERT INTO CLASS (CLASS_CODE, CLASS_SECTION, CLASS_TIME, CRS_CODE, PROF_NUM, ROOM_CODE, SEMESTER_CODE) " +
                           "VALUES (@CLASS_CODE, @CLASS_SECTION, @CLASS_TIME, @CRS_CODE, @PROF_NUM, @ROOM_CODE, @SEMESTER_CODE)";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CLASS_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@CLASS_SECTION", textBox2.Text);
                    cmd.Parameters.AddWithValue("@CLASS_TIME", textBox3.Text);
                    cmd.Parameters.AddWithValue("@CRS_CODE", textBox4.Text);
                    cmd.Parameters.AddWithValue("@PROF_NUM", textBox5.Text);
                    cmd.Parameters.AddWithValue("@ROOM_CODE", textBox6.Text);
                    cmd.Parameters.AddWithValue("@SEMESTER_CODE", textBox7.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Class added successfully!");
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
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            @class form = new @class();
            form.Show();
            this.Hide();
        }
    }
}
