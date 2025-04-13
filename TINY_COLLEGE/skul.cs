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
    public partial class skul: Form
    {
        public skul()
        {
            InitializeComponent();
        }

        private void skul_Load(object sender, EventArgs e)
        {

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
            string query = "SELECT * FROM school";

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
                    MessageBox.Show("Error loading School data: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addskul form = new addskul();
            form.Show();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row's SCHOOL_CODE
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                string schoolCodeToDelete = dataGridView1.Rows[selectedRowIndex].Cells["SCHOOL_CODE"].Value.ToString();

                // Ask the user for confirmation before deleting
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this school?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Delete the record from the database
                    string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
                    string query = "DELETE FROM school WHERE SCHOOL_CODE = @schoolCode";

                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@schoolCode", schoolCodeToDelete);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("School deleted successfully.");
                                // Refresh the DataGridView to reflect the change
                                LoadSchoolData();
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

        private void LoadSchoolData()
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM school";

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
                    MessageBox.Show("Error loading School data: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                string schoolCode = dataGridView1.Rows[rowIndex].Cells["SCHOOL_CODE"].Value.ToString();
                string schoolName = dataGridView1.Rows[rowIndex].Cells["SCHOOL_NAME"].Value.ToString();
                string profNum = dataGridView1.Rows[rowIndex].Cells["PROF_NUM"].Value?.ToString() ?? "";

                editskul form = new editskul(schoolCode, schoolName, profNum);
                form.ShowDialog();

                LoadSchoolData(); // Refresh after editing
            }
            else
            {
                MessageBox.Show("Please select a school to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadSchoolData(); // Load all if search is empty
                return;
            }

            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM school WHERE SCHOOL_CODE LIKE @keyword OR SCHOOL_NAME LIKE @keyword OR PROF_NUM LIKE @keyword";

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
                    MessageBox.Show("Search error: " + ex.Message);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
