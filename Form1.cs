using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Assignment1
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string mysqlCon = "datasource=localhost;port=3306;username=root;password=root;database=places";
            MySqlConnection mysqlconnection = new MySqlConnection(mysqlCon);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 db =new Form3();
            db.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    
}
