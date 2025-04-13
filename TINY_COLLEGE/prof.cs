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
    public partial class prof: Form
    {
        public prof()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addprof form = new addprof();
            form.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM professor";

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
                    MessageBox.Show("Error loading Professor data: " + ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            select form = new select();
            form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row's PROF_NUM
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                string profNumToDelete = dataGridView1.Rows[selectedRowIndex].Cells["PROF_NUM"].Value.ToString();

                // Ask the user for confirmation before deleting
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this professor?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Delete the record from the database
                    string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
                    string query = "DELETE FROM PROFESSOR WHERE PROF_NUM = @profNum";

                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@profNum", profNumToDelete);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Professor deleted successfully.");
                                // Refresh the DataGridView to reflect the change
                                LoadProfessorData();
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

        private void LoadProfessorData()
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM PROFESSOR";

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
                    MessageBox.Show("Error loading Professor data: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int row = dataGridView1.SelectedRows[0].Index;

                string profNum = dataGridView1.Rows[row].Cells["PROF_NUM"].Value.ToString();
                string deptCode = dataGridView1.Rows[row].Cells["DEPT_CODE"].Value?.ToString() ?? "";
                string specialty = dataGridView1.Rows[row].Cells["PROF_SPECIALTY"].Value?.ToString() ?? "";
                string rank = dataGridView1.Rows[row].Cells["PROF_RANK"].Value?.ToString() ?? "";
                string lname = dataGridView1.Rows[row].Cells["PROF_LNAME"].Value?.ToString() ?? "";
                string fname = dataGridView1.Rows[row].Cells["PROF_FNAME"].Value?.ToString() ?? "";
                string initial = dataGridView1.Rows[row].Cells["PROF_INITIAL"].Value?.ToString() ?? "";
                string email = dataGridView1.Rows[row].Cells["PROF_EMAIL"].Value?.ToString() ?? "";

                editprof form = new editprof(profNum, deptCode, specialty, rank, lname, fname, initial, email);
                form.ShowDialog();

                LoadProfessorData(); // Refresh the table after editing
            }
            else
            {
                MessageBox.Show("Please select a professor to edit.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadProfessorData(); // Load all if nothing is searched
                return;
            }

            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = @"SELECT * FROM PROFESSOR 
                     WHERE PROF_NUM LIKE @keyword 
                     OR DEPT_CODE LIKE @keyword 
                     OR PROF_SPECIALTY LIKE @keyword 
                     OR PROF_RANK LIKE @keyword 
                     OR PROF_LNAME LIKE @keyword 
                     OR PROF_FNAME LIKE @keyword 
                     OR PROF_INITIAL LIKE @keyword 
                     OR PROF_EMAIL LIKE @keyword";

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
                    MessageBox.Show("Error searching Professor data: " + ex.Message);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
