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
    public partial class student: Form
    {
        public student()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM student";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading student data: " + ex.Message);
                }
            }
        }

        private void student_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            select form = new select();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addstud form = new addstud();
            form.Show();
            
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row's STU_NUM
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                string studentNumToDelete = dataGridView1.Rows[selectedRowIndex].Cells["STU_NUM"].Value.ToString();

                // Ask the user for confirmation before deleting
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this student?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Delete the record from the database
                    string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
                    string query = "DELETE FROM student WHERE STU_NUM = @studentNum";

                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@studentNum", studentNumToDelete);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Student deleted successfully.");
                                // Refresh the DataGridView to reflect the change
                                LoadStudentData();
                            }
                            else
                            {
                                MessageBox.Show("Error deleting record.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void LoadStudentData()
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM student";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Student data: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int row = dataGridView1.SelectedRows[0].Index;

                string stuNum = dataGridView1.Rows[row].Cells["STU_NUM"].Value.ToString();
                string deptCode = dataGridView1.Rows[row].Cells["DEPT_CODE"].Value?.ToString() ?? "";
                string stuLName = dataGridView1.Rows[row].Cells["STU_LNAME"].Value?.ToString() ?? "";
                string stuFName = dataGridView1.Rows[row].Cells["STU_FNAME"].Value?.ToString() ?? "";
                string stuInitial = dataGridView1.Rows[row].Cells["STU_INITIAL"].Value?.ToString() ?? "";
                string stuEmail = dataGridView1.Rows[row].Cells["STU_EMAIL"].Value?.ToString() ?? "";
                string profNum = dataGridView1.Rows[row].Cells["PROF_NUM"].Value?.ToString() ?? "";

                editstud form = new editstud(stuNum, deptCode, stuLName, stuFName, stuInitial, stuEmail, profNum);
                form.ShowDialog();

                LoadStudentData(); // Refresh after editing
            }
            else
            {
                MessageBox.Show("Please select a student to edit.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadStudentData(); // Reload all data if search box is empty
                return;
            }

            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = @"SELECT * FROM student 
                     WHERE STU_NUM LIKE @keyword 
                     OR STU_LNAME LIKE @keyword 
                     OR STU_FNAME LIKE @keyword 
                     OR STU_INITIAL LIKE @keyword 
                     OR STU_EMAIL LIKE @keyword 
                     OR DEPT_CODE LIKE @keyword 
                     OR PROF_NUM LIKE @keyword";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching Student data: " + ex.Message);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
