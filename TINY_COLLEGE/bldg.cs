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
    public partial class bldg: Form
    {
        public bldg()
        {
            InitializeComponent();
        }

        private void bldg_Load(object sender, EventArgs e)
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
            addbldg form = new addbldg();
            form.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT BLDG_CODE, BLDG_NAME, BLDG_LOCATION FROM building";

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
                    MessageBox.Show("Error loading Building data: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row's BLDG_CODE
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                string buildingCodeToDelete = dataGridView1.Rows[selectedRowIndex].Cells["BLDG_CODE"].Value.ToString();

                // Ask the user for confirmation before deleting
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this building?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Delete the record from the database
                    string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
                    string query = "DELETE FROM building WHERE BLDG_CODE = @buildingCode";

                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@buildingCode", buildingCodeToDelete);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Building deleted successfully.");
                                // Refresh the DataGridView to reflect the change
                                LoadBuildingData();
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

        private void LoadBuildingData()
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM building";

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
                    MessageBox.Show("Error loading Building data: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                string bldgCode = dataGridView1.Rows[rowIndex].Cells["BLDG_CODE"].Value.ToString();
                string bldgName = dataGridView1.Rows[rowIndex].Cells["BLDG_NAME"].Value.ToString();
                string bldgLocation = dataGridView1.Rows[rowIndex].Cells["BLDG_LOCATION"].Value.ToString();

                editbldg form = new editbldg(bldgCode, bldgName, bldgLocation);
                form.ShowDialog();

                LoadBuildingData(); // your refresh method
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
                LoadBuildingData(); // Reload all data if search box is empty
                return;
            }

            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM building WHERE BLDG_CODE LIKE @keyword OR BLDG_NAME LIKE @keyword OR BLDG_LOCATION LIKE @keyword";

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
           
        
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
