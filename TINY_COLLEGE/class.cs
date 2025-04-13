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
    public partial class @class: Form
    {
        public @class()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            select form = new select();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM class";

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
                    MessageBox.Show("Error loading Class data: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addclass form = new addclass();
            form.Show();
          
        }

        private void class_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row's CLASS_CODE (assuming CLASS_CODE is the primary key in your 'class' table)
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                string classCodeToDelete = dataGridView1.Rows[selectedRowIndex].Cells["CLASS_CODE"].Value.ToString();

                // Ask the user for confirmation before deleting
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Delete the record from the database
                    string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
                    string query = "DELETE FROM class WHERE CLASS_CODE = @classCode";

                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@classCode", classCodeToDelete);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record deleted successfully.");
                                // Refresh the DataGridView to reflect the change
                                LoadClassData();
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

        private void LoadClassData()
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM class";

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
                    MessageBox.Show("Error loading Class data: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                string classCode = dataGridView1.Rows[rowIndex].Cells["CLASS_CODE"].Value.ToString();
                string section = dataGridView1.Rows[rowIndex].Cells["CLASS_SECTION"].Value.ToString();
                string time = dataGridView1.Rows[rowIndex].Cells["CLASS_TIME"].Value.ToString();
                string crsCode = dataGridView1.Rows[rowIndex].Cells["CRS_CODE"].Value.ToString();
                string profNum = dataGridView1.Rows[rowIndex].Cells["PROF_NUM"].Value.ToString();
                string roomCode = dataGridView1.Rows[rowIndex].Cells["ROOM_CODE"].Value.ToString();
                string semesterCode = dataGridView1.Rows[rowIndex].Cells["SEMESTER_CODE"].Value.ToString();

                editclass form = new editclass(classCode, section, time, crsCode, profNum, roomCode, semesterCode);
                form.ShowDialog();

                LoadClassData(); // your refresh method
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
                LoadClassData(); // Show all if nothing entered
                return;
            }

            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = @"SELECT * FROM class 
                     WHERE CLASS_CODE LIKE @keyword 
                     OR CLASS_SECTION LIKE @keyword 
                     OR CLASS_TIME LIKE @keyword 
                     OR CRS_CODE LIKE @keyword 
                     OR PROF_NUM LIKE @keyword 
                     OR ROOM_CODE LIKE @keyword 
                     OR SEMESTER_CODE LIKE @keyword";

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
                    MessageBox.Show("Error searching Class data: " + ex.Message);
                }
            }
        }
    }
}
