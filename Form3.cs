using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class Form3 : Form
    {
        string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=places";

        MySqlConnection connection;
        MySqlDataAdapter adapter;
        DataTable table;
        public Form3()
        {
            InitializeComponent();

            connection = new MySqlConnection(connectionString);
            adapter = new MySqlDataAdapter();
            table = new DataTable();
            LoadData();
        }

        private void LoadData(string searchQuery = "")
        {
            try
            {
                connection.Open();
                string query = "SELECT name, image FROM hotels " +
                               "UNION ALL SELECT name, image FROM restaurants " +
                               "UNION ALL SELECT name, image FROM bakery " +
                               "UNION ALL SELECT name, image FROM temples";

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query = "SELECT name, image FROM hotels WHERE name LIKE @searchQuery " +
                            "UNION ALL SELECT name, image FROM restaurants WHERE name LIKE @searchQuery " +
                            "UNION ALL SELECT name, image FROM bakery WHERE name LIKE @searchQuery " +
                            "UNION ALL SELECT name, image FROM temples WHERE name LIKE @searchQuery";
                }

                MySqlCommand command = new MySqlCommand(query, connection);
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    command.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
                }

                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);

                listBox1.Items.Clear();
                foreach (DataRow row in table.Rows)
                {
                    listBox1.Items.Add(row["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                string selectedName = listBox1.SelectedItem.ToString();
                DataRow[] selectedRows = table.Select("name = '" + selectedName + "'");
                if (selectedRows.Length > 0)
                {
                    byte[] imageData = (byte[])selectedRows[0]["image"];
                    if (imageData != null)
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadData(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData(textBox1.Text);
        }

        private void pLACESToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hOTELSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm4WithItemName("HOTELS", "hotels");
            this.Hide();
        }

        private void bAKERYToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm4WithItemName("BAKERY", "bakery");
            this.Hide();    
        }

        private void rESTAURANTSToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm4WithItemName("RESTAURANTS", "restaurants");
            this.Hide();
        }

        private void tEMPLESToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm4WithItemName("TEMPLES", "temples");
            this.Hide();
        }
       
        private void OpenForm4WithItemName(string itemName, string tablename)
        {
            Form4 form4 = new Form4(tablename);
            form4.SetItemName(itemName);
            form4.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void fAVOURITEPLACESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int exitCode = 0;
            Environment.Exit(exitCode);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 form7=new Form7();    
            form7.Show();
            this.Hide();    
        }

        
    }
}
