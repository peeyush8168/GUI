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

namespace Assignment1
{
    public partial class Form4 : Form
    {
        private string tableName;
        private string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=places";
        public Form4(string tableName)
        {
            InitializeComponent();
            this.tableName = tableName;
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                string query = $"SELECT name FROM {tableName}";

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                listBox1.Items.Clear();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data from {tableName}: " + ex.Message);
            }
        }
        public void label1_Click(object sender, EventArgs e)
        {
            
        }
        public void fun(string label)
        {
            label1.Text = label;
        }
        public void SetItemName(string itemName)
        {
            label1.Text = itemName;
            Form5 form5 = new Form5(itemName);
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedItem = listBox1.SelectedItem.ToString();

                Form5 form5 = new Form5(tableName);
                form5.SetItemName(selectedItem);
                form5.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 fom3=new Form3(); 
            fom3.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
