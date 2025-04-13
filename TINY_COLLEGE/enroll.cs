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
    public partial class enroll: Form
    {
        public enroll()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
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
            addenroll form = new addenroll();
            form.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM enroll";

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
                    MessageBox.Show("Error loading Enroll data: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row's CLASS_CODE and STU_NUM
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                string classCodeToDelete = dataGridView1.Rows[selectedRowIndex].Cells["CLASS_CODE"].Value.ToString();
                string stuNumToDelete = dataGridView1.Rows[selectedRowIndex].Cells["STU_NUM"].Value.ToString();

                // Ask the user for confirmation before deleting
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this enrollment record?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Delete the record from the database
                    string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
                    string query = "DELETE FROM enroll WHERE CLASS_CODE = @classCode AND STU_NUM = @stuNum";

                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@classCode", classCodeToDelete);
                            cmd.Parameters.AddWithValue("@stuNum", stuNumToDelete);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Enrollment record deleted successfully.");
                                // Refresh the DataGridView to reflect the change
                                LoadEnrollData();
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

        private void LoadEnrollData()
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM enroll";

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
                    MessageBox.Show("Error loading Enrollment data: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                string classCode = dataGridView1.Rows[rowIndex].Cells["CLASS_CODE"].Value.ToString();
                string stuNum = dataGridView1.Rows[rowIndex].Cells["STU_NUM"].Value.ToString();
                DateTime enrollDate = Convert.ToDateTime(dataGridView1.Rows[rowIndex].Cells["ENROLL_DATE"].Value);
                string enrollGrade = dataGridView1.Rows[rowIndex].Cells["ENROLL_GRADE"].Value.ToString();

                editenroll form = new editenroll(classCode, stuNum, enrollDate, enrollGrade);
                form.ShowDialog();

                LoadEnrollData(); // Refresh after editing
            }
            else
            {
                MessageBox.Show("Please select a row to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
            
        

        private void button6_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadEnrollData(); // Show all records if nothing is typed
                return;
            }

            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = @"SELECT * FROM enroll 
                     WHERE CLASS_CODE LIKE @keyword 
                     OR STU_NUM LIKE @keyword 
                     OR ENROLL_GRADE LIKE @keyword";

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
                    MessageBox.Show("Error searching Enroll data: " + ex.Message);
                }
            }
        }
    }
}

