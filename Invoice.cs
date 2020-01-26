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

namespace WindowsFormsApp1_Map
{
    public partial class Invoice : Form
    {
        private string myTextBoxValue;
        private Form1 form1;

        public Invoice(string myTextBoxValue)
        {
            this.myTextBoxValue = myTextBoxValue;
            
        }
        
        public Invoice(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
            
        }

        public Form1 MyParentForm { get; internal set; }

        public static MySqlConnection GetDBConnection()  //Database connection
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();

            conn_string.Server = "db.ils.indiana.edu";
            conn_string.Port = 3306;
            conn_string.UserID = "haoh";
            conn_string.Password = "Nv7nZSRb";
            conn_string.Database = "haoh";


            MySqlConnection DBconn = new MySqlConnection(conn_string.ToString());
            return DBconn;

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label29_Click(object sender, EventArgs e)
        {
           
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = GetDBConnection();
            label26.Text = Form1.MyTotalValue;
            label21.Text = Form1.Mylabel21;
            label22.Text = Form1.Mylabel22;
            label23.Text = Form1.Mylabel23;
            label25.Text = Form1.Mylabel25;
            label27.Text = Form1.Mylabel27;
            label29.Text = Form1.Mylabel29;
            label30.Text = Form1.Mylabel30;
            label31.Text = Form1.Mylabel31;
            label32.Text = Form1.Mylabel32;
            label33.Text = Form1.Mylabel33;

            label3.Text = "Phone Number : " + Form1.MyPhone;
            label4.Text = "Email Address : " + Form1.MyEmail;
            
            try
            {
                //Open Connection
                conn.Open();


                //Execute query
                int input = Convert.ToInt32(Form1.MyTextBoxValue);
                string query = "select * from reservation where reservation_id = " + input + ";";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader myReader = cmd.ExecuteReader();

                //Load Data
                DataTable reservation = new DataTable();
                reservation.Load(myReader);


                //print table
                dataGridView1.DataSource = reservation;
                dataGridView1.Columns[0].Width = 80;
                dataGridView1.Columns[1].Width = 70;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].Width = 70;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].Width = 85;

                myReader.Close();
                myReader.Dispose();
                conn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

