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
    public partial class editclass: Form
    {
        private string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
        private string originalClassCode;
        public editclass(string classCode, string section, string time, string crsCode, string profNum, string roomCode, string semesterCode)
        {
            InitializeComponent();
            originalClassCode = classCode;

            textBox1.Text = classCode;
            textBox2.Text = section;
            textBox3.Text = time;
            textBox4.Text = crsCode;
            textBox5.Text = profNum;
            textBox6.Text = roomCode;
            textBox7.Text = semesterCode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsDigitsOnly(textBox1.Text) || !IsDigitsOnly(textBox4.Text) ||
            !IsDigitsOnly(textBox5.Text) || !IsDigitsOnly(textBox6.Text) || !IsDigitsOnly(textBox7.Text))
            {
                MessageBox.Show("CLASS_CODE, CRS_CODE, PROF_NUM, ROOM_CODE, and SEMESTER_CODE must contain only numbers.");
                return;
            }

            // Validate text fields
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

            string query = "UPDATE CLASS SET CLASS_SECTION = @CLASS_SECTION, CLASS_TIME = @CLASS_TIME, CRS_CODE = @CRS_CODE, " +
                           "PROF_NUM = @PROF_NUM, ROOM_CODE = @ROOM_CODE, SEMESTER_CODE = @SEMESTER_CODE " +
                           "WHERE CLASS_CODE = @originalClassCode";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CLASS_SECTION", textBox2.Text);
                    cmd.Parameters.AddWithValue("@CLASS_TIME", textBox3.Text);
                    cmd.Parameters.AddWithValue("@CRS_CODE", textBox4.Text);
                    cmd.Parameters.AddWithValue("@PROF_NUM", textBox5.Text);
                    cmd.Parameters.AddWithValue("@ROOM_CODE", textBox6.Text);
                    cmd.Parameters.AddWithValue("@SEMESTER_CODE", textBox7.Text);
                    cmd.Parameters.AddWithValue("@originalClassCode", originalClassCode);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Class updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // or this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            @class form = new @class();
            form.Show();
            this.Hide();
        }
    }
}
   