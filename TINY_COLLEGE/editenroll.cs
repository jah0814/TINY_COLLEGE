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
    public partial class editenroll: Form
    {
        private string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
        private string originalClassCode;
        private string originalStuNum;
        public editenroll(string classCode, string stuNum, DateTime enrollDate, string enrollGrade)
        {
            InitializeComponent();
            originalClassCode = classCode;
            originalStuNum = stuNum;

            textBox1.Text = classCode;
            textBox2.Text = stuNum;
            textBox3.Text = enrollDate.ToString("yyyy-MM-dd");
            textBox4.Text = enrollGrade;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate fields
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("CLASS_CODE and STU_NUM cannot be empty.");
                return;
            }

            if (!DateTime.TryParse(textBox3.Text, out DateTime enrollDate))
            {
                MessageBox.Show("ENROLL_DATE must be a valid date.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("ENROLL_GRADE cannot be empty.");
                return;
            }

            if (textBox4.Text.Length > 5)
            {
                MessageBox.Show("ENROLL_GRADE must be 5 characters or fewer.");
                return;
            }

            string query = "UPDATE ENROLL SET CLASS_CODE = @CLASS_CODE, STU_NUM = @STU_NUM, ENROLL_DATE = @ENROLL_DATE, ENROLL_GRADE = @ENROLL_GRADE " +
                           "WHERE CLASS_CODE = @originalClassCode AND STU_NUM = @originalStuNum";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CLASS_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@STU_NUM", textBox2.Text);
                    cmd.Parameters.AddWithValue("@ENROLL_DATE", enrollDate);
                    cmd.Parameters.AddWithValue("@ENROLL_GRADE", textBox4.Text);
                    cmd.Parameters.AddWithValue("@originalClassCode", originalClassCode);
                    cmd.Parameters.AddWithValue("@originalStuNum", originalStuNum);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Enrollment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Or this.Hide() depending on your navigation
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            enroll form = new enroll();
            form.Show();
            this.Hide();
        }
    }
}
