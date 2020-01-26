using GMap.NET;

using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1_Map
{
    public partial class Form1 : Form

    {
        //private PointLatLng point;
        //List<PointLatLng> twopoints; //List??
        List<PointLatLng> twopoints = new List<PointLatLng>();
        List<GMapOverlay> markerlist = new List<GMapOverlay>();

        private PointLatLng point;
        private PointLatLng point2;
        double lat;
        double longi;
        double lat2;
        double longi2;

        public Form1()
        {
            InitializeComponent();
            //twopoints = new List<PointLatLng>();

            GMapProviders.GoogleMap.ApiKey = @"AIzaSyADeKwiuH - xz - jAT__3sYqNz8Ntb_i0fYs";

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void gMap_MouseClick(object sender, MouseEventArgs e) // Use Left MouseClick to load latitude and longitude
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gMap.Refresh();
            gMap.MapProvider = GMapProviders.GoogleMap;
            gMap.Position = new GMap.NET.PointLatLng(39.165325, -86.526382);//Set default location to Bloomington
            gMap.ShowCenter = false;// To remove the red cross at centre of map
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double lat = Convert.ToDouble(textlat.Text);// define lantitude
                double longi = Convert.ToDouble(textlong.Text);//define longitude
                gMap.Position = new PointLatLng(lat, longi); //set position at the current lat & long
                PointLatLng point = new PointLatLng(lat, longi);

                //add red marker
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
                GMapOverlay markers = new GMapOverlay("markers");
                markers.Markers.Add(marker);
                gMap.Overlays.Add(markers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nPlease enter the proper latitude and longitude with numbers");
            }
        }

        private void button2_Click(object sender, EventArgs e)//find destination button
        {
            try
            {
                double lat2 = Convert.ToDouble(textBox1.Text);// define lantitude
                double longi2 = Convert.ToDouble(textBox2.Text);//define longitude
                gMap.Position = new PointLatLng(lat2, longi2); //set position at the current lat & long

                PointLatLng point2 = new PointLatLng(lat2, longi2);


                //add red marker
                GMapMarker marker = new GMarkerGoogle(point2, GMarkerGoogleType.blue_pushpin);
                GMapOverlay markers = new GMapOverlay("markers");
                markers.Markers.Add(marker);
                gMap.Overlays.Add(markers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nPlease enter the proper latitude and longitude with numbers");
            }
        }

        private void button4_Click(object sender, EventArgs e)        //GET ROUTE
        {
            try
            {
                double lat = Convert.ToDouble(textlat.Text);// define lantitude
                double longi = Convert.ToDouble(textlong.Text);//define longitude
                double lat2 = Convert.ToDouble(textBox1.Text);// define lantitude2
                double longi2 = Convert.ToDouble(textBox2.Text);//define longitude2

                GMapOverlay routes = new GMapOverlay("route");
                //List<PointLatLng> twopoints = new List<PointLatLng>();//list of two points
                twopoints.Add(new PointLatLng(lat, longi));
                twopoints.Add(new PointLatLng(lat2, longi2));


                var route = GoogleMapProvider.Instance.GetRoute(twopoints[0], twopoints[1], false, false, 14);
                var r = new GMapRoute(route.Points, "My Route")
                {
                    Stroke = new Pen(Color.Red, 5)
                };
                var routess = new GMapOverlay("routes");
                routess.Routes.Add(r);
                gMap.Overlays.Add(routess);
                gMap.Refresh();

                label11.Text = "" + route.Distance;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\n" + "Please input proper locations and try again!");
            }


        }

        GMapOverlay markers = new GMapOverlay("markers");
        GMapOverlay markers2 = new GMapOverlay("markers");
        internal static object TextBox;

        private void gMap_MouseClick_1(object sender, MouseEventArgs e) //MouseClick Function
        {
            if (e.Button == MouseButtons.Left)
            {
                if (textlat.Text == "")//if user have not choose their source/from.
                {
                    var point = gMap.FromLocalToLatLng(e.X, e.Y);
                    double lat = point.Lat;
                    double longi = point.Lng;

                    textlat.Text = lat + "";
                    textlong.Text = longi + "";

                    //load location
                    gMap.Position = point;

                    //add red marker
                    GMapMarker marker1 = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
                    GMapOverlay markers = new GMapOverlay("markers");
                    markers.Markers.Add(marker1);
                    //markers2.Markers.Add(marker1);
                    gMap.Overlays.Add(markers);
                }


                else if (textBox1.Text == "" && textBox2.Text == "")
                //if user already have source, point goes to the destination textboxs
                {
                    var point2 = gMap.FromLocalToLatLng(e.X, e.Y);
                    double lat2 = point2.Lat;
                    double longi2 = point2.Lng;

                    textBox1.Text = lat2 + "";
                    textBox2.Text = longi2 + "";

                    //load location
                    gMap.Position = point2;

                    //add red marker
                    GMapMarker marker2 = new GMarkerGoogle(point2, GMarkerGoogleType.blue_pushpin);
                    GMapOverlay markers2 = new GMapOverlay("markers");
                    markers2.Markers.Add(marker2);
                    gMap.Overlays.Add(markers2);
                    gMap.Refresh();
                }

            }
        }

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

        private void button5_Click(object sender, EventArgs e) //dropbox confirm button
        {
            MySqlConnection conn = GetDBConnection();
            //check if connection works

            try
            {
                //Open Connection
                conn.Open();

                if (textlat.Text == "" && textlong.Text == "")
                //if user have not choose their start. "From" destination
                {

                    //Execute Qury
                    string location = comboBox1.SelectedItem.ToString(); //select item from the combo box
                    string query = "select latitude, longitude from map where location_name = " + "\"" + location + "\"";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    MySqlDataReader myReader = cmd.ExecuteReader();
                    while (myReader.Read())
                    {
                        textlat.Text = myReader.GetString(0);
                        textlong.Text = myReader.GetString(1);
                    }


                    double lat = Convert.ToDouble(textlat.Text);// define lantitude
                    double longi = Convert.ToDouble(textlong.Text);//define longitude
                    //gMap.Position = new PointLatLng(lat, longi);
                    //gMap.Position = point; //set position at the current lat & long

                    PointLatLng point = new PointLatLng(lat, longi);
                    gMap.Position = new PointLatLng(lat, longi); //set position at the current lat & long
                                                                 //gMap.Position = point;

                    //add red marker
                    //PointLatLng point = new PointLatLng(lat, longi);
                    //add red marker
                    GMapMarker marker1 = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
                    GMapOverlay markers = new GMapOverlay("markers");
                    markers.Markers.Add(marker1);
                    //markers2.Markers.Add(marker1);
                    gMap.Overlays.Add(markers);

                    //markerlist.Add(markers);//add marker to marker list
                    myReader.Close();
                    conn.Close();
                }
                else if (textBox1.Text == "" && textBox2.Text == "")
                //if user already have source, point goes to the destination textboxs
                //"To" destination
                {
                    //Execute Qury
                    string location = comboBox1.SelectedItem.ToString(); //select item from the combo box
                    string query = "select latitude, longitude from map where location_name = " + "\"" + location + "\"";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    MySqlDataReader myReader = cmd.ExecuteReader();
                    while (myReader.Read())
                    {
                        textBox1.Text = myReader.GetString(0);
                        textBox2.Text = myReader.GetString(1);
                    }

                    double lat = Convert.ToDouble(textBox1.Text);// define lantitude
                    double longi = Convert.ToDouble(textBox2.Text);//define longitude
                    //gMap.Position = new PointLatLng(lat, longi);
                    //gMap.Position = point; //set position at the current lat & long
                    //PointLatLng start = new PointLatLng(lat, longi);

                    PointLatLng point = new PointLatLng(lat, longi);

                    gMap.Position = new PointLatLng(lat, longi); //set position at the current lat & long
                                                                 //gMap.Position = point;


                    //add red marker
                    GMapMarker marker2 = new GMarkerGoogle(point, GMarkerGoogleType.blue_pushpin);
                    GMapOverlay markers2 = new GMapOverlay("markers");
                    markers2.Markers.Add(marker2);
                    //markers2.Markers.Add(marker1);
                    gMap.Overlays.Add(markers2);

                    //markerlist.Add(markers);//add marker to marker list
                    myReader.Close();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {

            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //dropdown box!
        {

        }

        private void button3_Click_1(object sender, EventArgs e)  //clear button
        {
            try
            {
                twopoints.Clear();//I think the point is cleared based on test, but markere still left on the map...

                if (textBox1.Text == "" && textBox2.Text == "")
                {
                    gMap.Overlays.RemoveAt(0);
                    gMap.Refresh();
                    textlat.Text = "";
                    textlong.Text = "";

                }
                else if (textBox1.Text != "" && textBox2.Text != "")
                {
                    gMap.Overlays.RemoveAt(1);
                    gMap.Refresh();
                    //markers.Markers.RemoveAt(markers.Markers.Count - 1);
                    textBox1.Text = "";
                    textBox2.Text = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\n" + "Please check again your inputs!");
            }


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e) //view reservation summary button
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                MessageBox.Show(" Please Select a Type of Car to begin estimate");
                return;
            }
            // Rate Calculation
            label29.Text = "Pickup Time * Date is:" + dateTimePicker1.Value.ToString();
            label31.Text = "Number of luggage is: " + numericUpDown1.Value.ToString();
            label21.Text = "Distance is: " + Convert.ToString(label11.Text) + "miles";
            label32.Text = "Payment mode:" + comboBox2.Text;

            int Addcharge;
            double Discount;
            int additionalrate;

            DateTime timecal = dateTimePicker2.Value;
            DateTime start1 = DateTime.Parse("08:00:00");
            DateTime start2 = DateTime.Parse("09:00:00");
            DateTime start3 = DateTime.Parse("17:00:00");
            DateTime start4 = DateTime.Parse("18:00:00");

            if (start1.TimeOfDay.CompareTo(timecal.TimeOfDay) <= 0 && (start2.TimeOfDay.CompareTo(timecal.TimeOfDay) >= 0) || (start3.TimeOfDay.CompareTo(timecal.TimeOfDay) <= 0 && (start4.TimeOfDay.CompareTo(timecal.TimeOfDay) >= 0)))

            {
                additionalrate = 2; // $2 as additionalcharges will be added to rate for peak hours
            }
            else
            {
                additionalrate = 0;
            }
            if (numericUpDown1.Value >= 3)
            {
                Addcharge = 2;  // Addcharge = luggage charge 
            }
            else
            {
                Addcharge = 0;
            }
            if (comboBox2.Text == "ApplePay (Additional 3% Discount before tax)")
            {
                Discount = 0.03;
            }
            else
            {
                Discount = 0;
            }
            double initialrate = 2.5;
            double rate = 0;
            if (Convert.ToDouble(label11.Text) > 2)
            {
                rate = (1 * (Convert.ToDouble(label11.Text) - 2));
            }
            if (radioButton1.Checked == true) // for sedan
            {
                label22.Text = "Rate is: $2.5 for initial 2 miles and $1 per mile afterwards ";
                label23.Text = "SubTotal Price is : $ " + (additionalrate + initialrate + rate);
                double discount = ((Addcharge + additionalrate + initialrate + rate) * Discount);
                label27.Text = "Discount applied to your trip is: $ " + discount;
                double tax = ((initialrate + rate + Addcharge + additionalrate) * 0.07);
                label25.Text = "Tax is : $" + Convert.ToString(tax);
                label33.Text = "Extra Charges applied : $" + Convert.ToString(Addcharge + additionalrate); // additional charges for luggage & rush hour
                double Total = tax + (additionalrate + initialrate + rate + Addcharge) - (discount);

                label26.Text = "Your total payable amount is : $ " + Convert.ToString(Total);
                label30.Text = "Selected ride is : Sedan";

            }


            if (radioButton3.Checked == true)
            {
                int initialcarpoolrate = 5;
                double Carpoolrate = 0;
                if (Convert.ToDouble(label11.Text) > 5) //distance is more than 5 miles
                {
                    Carpoolrate = 0.5 * (Convert.ToDouble(label11.Text) - 5);
                    label22.Text = "Rate is: $ " + (initialcarpoolrate) + " within 5 miles and $ " + Carpoolrate + " for all additional miles";
                }
                else
                {
                    label22.Text = "Rate is: $ " + (initialcarpoolrate) + " within 5 miles"; //Carpoolrate is 0 here

                }

                label23.Text = "SubTotal Price is : $ " + (initialcarpoolrate + Carpoolrate);
                double discount = (additionalrate + initialcarpoolrate + Carpoolrate + Addcharge) * Discount;
                label27.Text = "Discount applied to your trip is: $ " + discount;
                double tax = (additionalrate + initialcarpoolrate + Carpoolrate + Addcharge) * 0.07;
                label25.Text = "Tax is : $" + Convert.ToString(tax);
                label33.Text = "Extra Charges appliedis : $" + Addcharge;
                double Total = tax + (additionalrate + initialcarpoolrate + Carpoolrate + Addcharge) - (discount);

                label26.Text = "Your total payable amount is : $ " + Convert.ToString(Total);
                label30.Text = "Selected ride is : Carpool";

            }
            if (radioButton2.Checked == true)
            {

                label22.Text = "Rate is: $2.5 for initial 2 miles and $1 per mile afterwards ";
                label23.Text = "SubTotal Price is : $ " + (additionalrate + initialrate + rate);
                double discount = (Addcharge + 1 + additionalrate + initialrate + rate) * Discount;
                label27.Text = "Discount applied to your trip is: $ " + discount;
                double tax = (initialrate + rate + (Addcharge + 1) + additionalrate) * 0.07;
                label25.Text = "Tax is : $" + Convert.ToString(tax);
                label33.Text = "Extra Charges applied : $" + Convert.ToString((Addcharge + 1) + additionalrate); // additional charges for luggage & rush hour
                double Total = tax + (additionalrate + initialrate + rate + (Addcharge + 1)) - discount;

                label26.Text = "Your total payable amount is : $ " + Convert.ToString(Total);
                label30.Text = "Selected ride is : SUV ";


            }


        }

        private void dateTimePicker2_MouseDown(object sender, MouseEventArgs e) //choose time 
        {
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "HH:mm:ss";
            // dateTimePicker2.CustomFormat = "hh:mm:ss:tt";

            dateTimePicker2.ShowUpDown = true;

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
        //Values to pass to Form2(Invoice)
        public static string MyTextBoxValue;
        public static string MyTotalValue;
        public static string Mylabel29;
        public static string Mylabel30;
        public static string Mylabel21;
        public static string Mylabel22;
        public static string Mylabel31;
        public static string Mylabel32;
        public static string Mylabel33;
        public static string Mylabel23;
        public static string Mylabel25;
        public static string Mylabel27;
        public static string MyPhone;
        public static string MyEmail;


        private void button7_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = GetDBConnection();


            try
            {
                //Open Connection
                conn.Open();


                //Execute query
                string query = "update reservation set status = 'reserved' where reservation_id = " + Convert.ToInt32(textBox3.Text) + ";" + "select * from reservation where reservation_id = " + Convert.ToInt32(textBox3.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader myReader = cmd.ExecuteReader();

                //Load Data
                DataTable reservation = new DataTable();
                reservation.Load(myReader);


                //print table
                dataGridView1.DataSource = reservation;


                myReader.Close();
                myReader.Dispose();
                conn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nPlease enter the reservation_id from the table into here!");
            }
            if (textBox3.Text == "Please input only one number (ex: 1, 2, ,,, )")
            {
                MessageBox.Show("Please choose and enter a taxi id as you prefer");
                return;
            }
            else if (textBox4.Text == "8120000000" || textBox5.Text == "taxismart@taxi.com")
            {
                MessageBox.Show("Please enter your phone number and email to complete your order");
                return;
            }
            
            else
            {
                MyTextBoxValue = textBox3.Text;
                MyTotalValue = label26.Text;
                Mylabel29 = label29.Text;
                Mylabel21 = label21.Text;
                Mylabel22 = label22.Text;
                Mylabel23 = label23.Text;
                Mylabel25 = label25.Text;
                Mylabel27 = label27.Text;
                Mylabel29 = label29.Text;
                Mylabel30 = label30.Text;
                Mylabel31 = label31.Text;
                Mylabel32 = label32.Text;
                Mylabel33 = label33.Text;
                MyPhone = textBox4.Text;
                MyEmail = textBox5.Text;

                Invoice fm2 = new Invoice(this);
                //Invoice.MyParentForm = this;
                fm2.ShowDialog();
                fm2.Dispose();
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label19_Click(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void label17_Click(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void label14_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/document/d/1qLlgnOjnTpW4S6TgTANagjNlF15pzgCQ4JQHit0cEBI/edit");

        }


        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = GetDBConnection();


            try
            {
                //Open Connection
                conn.Open();

                //Execute query
                if (radioButton1.Checked == true)
                {
                    string query = "select * from reservation where status = 'available' and car_type = 'sedan'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader myReader = cmd.ExecuteReader();
                    //Load Data
                    DataTable reservation = new DataTable();
                    reservation.Load(myReader);
                    dataGridView1.DataSource = reservation;
                    //print table

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
                else if (radioButton2.Checked == true)
                {
                    string query = "select * from reservation where status = 'available' and car_type = 'SUV'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader myReader = cmd.ExecuteReader();
                    //Load Data
                    DataTable reservation = new DataTable();
                    reservation.Load(myReader);
                    dataGridView1.DataSource = reservation;
                    //print table

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
                else
                {
                    string query = "select * from reservation where status = 'available' and car_type = 'carpool'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader myReader = cmd.ExecuteReader();
                    //Load Data
                    DataTable reservation = new DataTable();
                    reservation.Load(myReader);
                    dataGridView1.DataSource = reservation;
                    //print table

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


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {

            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Please input only one number (ex: 1, 2, ,,, )")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Please input only one number (ex: 1, 2, ,,, )";
                textBox3.ForeColor = Color.DarkGray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "8120000000")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "8120000000";
                textBox4.ForeColor = Color.DarkGray;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "taxismart@taxi.com")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "taxismart@taxi.com";
                textBox5.ForeColor = Color.DarkGray;
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }
    }


}


