using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;


namespace BarberShop
{
    /// <summary>
    /// Interaction logic for AppointmentPanel2.xaml
    /// </summary>
    public partial class AppointmentPanel2 : UserControl
    {
        string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";

        public AppointmentPanel2()
        {
            InitializeComponent();
            FillTimeCmb();
            DateRange();
            FillHairdresserCmb();
        }

        private void addnew_btn_Click(object sender, RoutedEventArgs e)
        {
            checkData();
            checkAppointment();
        }

        private void DateRange()
        {
            DateTime today = DateTime.Now;
            app_date.DisplayDateStart = today;
        }

        private void FillTimeCmb()
        {
            ComboBoxItem cmbItem1 = new ComboBoxItem();
            cmbItem1.Content = "9:00";
            time_cmbbox.Items.Add(cmbItem1);

            ComboBoxItem cmbItem2 = new ComboBoxItem();
            cmbItem2.Content = "10:00";
            time_cmbbox.Items.Add(cmbItem2);

            ComboBoxItem cmbItem3 = new ComboBoxItem();
            cmbItem3.Content = "11:00";
            time_cmbbox.Items.Add(cmbItem3);

            ComboBoxItem cmbItem4 = new ComboBoxItem();
            cmbItem4.Content = "12:00";
            time_cmbbox.Items.Add(cmbItem4);

            ComboBoxItem cmbItem5 = new ComboBoxItem();
            cmbItem5.Content = "13:00";
            time_cmbbox.Items.Add(cmbItem5);

            ComboBoxItem cmbItem6 = new ComboBoxItem();
            cmbItem6.Content = "14:00";
            time_cmbbox.Items.Add(cmbItem6);

            ComboBoxItem cmbItem7 = new ComboBoxItem();
            cmbItem7.Content = "15:00";
            time_cmbbox.Items.Add(cmbItem7);

            ComboBoxItem cmbItem8 = new ComboBoxItem();
            cmbItem8.Content = "16:00";
            time_cmbbox.Items.Add(cmbItem8);

            ComboBoxItem cmbItem9 = new ComboBoxItem();
            cmbItem9.Content = "17:00";
            time_cmbbox.Items.Add(cmbItem9);

            ComboBoxItem cmbItem10 = new ComboBoxItem();
            cmbItem10.Content = "18:00";
            time_cmbbox.Items.Add(cmbItem10);

            ComboBoxItem cmbItem11 = new ComboBoxItem();
            cmbItem11.Content = "19:00";
            time_cmbbox.Items.Add(cmbItem11);

            ComboBoxItem cmbItem12 = new ComboBoxItem();
            cmbItem12.Content = "20:00";
            time_cmbbox.Items.Add(cmbItem12);

            ComboBoxItem cmbItem13 = new ComboBoxItem();
            cmbItem13.Content = "21:00";
            time_cmbbox.Items.Add(cmbItem13);
        }

        private void FillHairdresserCmb()
        {
            DataTable dt = FillHairdresserDT();
            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                hairdresser_cmbbox.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }

        }

        private DataTable FillHairdresserDT(){
            DataSet ds = FillHairdresser();

            return ds.Tables[0];
        }

        private DataSet FillHairdresser() {
            string cmdString = "SELECT [Name] FROM [dbo].[Hairdresser]";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter (cmdString, con);
            DataSet dt = new DataSet("Hairdressers");
            sda.Fill(dt);
            con.Close();

            return dt;
        }

        private void checkData()
        {
            Boolean error = false;

            string id=string.Empty;

            string cmdString = "SELECT [Customer ID] FROM [dbo].[Customers] WHERE [Customer ID]='"+customer_txtbox.Text+"'";
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(cmdString, con);

            try
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = (reader["Customer ID"].ToString());
                    }
                }
            }
            finally
            {
                con.Close();
            }

            
            if (id == null || id == "" || id == " "){
                MessageBox.Show("There is no customer registered with such ID");
                error = true;
            }
            else
            {
                error = false;
            }



            if (error == false)
            {

            }
        }

        private void checkAppointment()
        {
            string id = string.Empty;
            string date = string.Empty;
            DateTime d = DateTime.Today;
            try
            {
                d = Convert.ToDateTime(app_date.SelectedDate.ToString());
                TimeSpan time = TimeSpan.Parse(time_cmbbox.Text.ToString() + ":00");
                d = d.Date.Add(time);

            }
            catch
            {

            }
            
            date = d + "";
            Boolean reserved = false;
            //MessageBox.Show(date);

            if (time_cmbbox.SelectedItem == null || app_date.SelectedDate == null || app_date.SelectedDate.ToString() == "")
            {
                MessageBox.Show("Select date and time for new appointment");
            }
            else
            {
                if (hairdresser_cmbbox.Text.ToString() == "" || hairdresser_cmbbox.Text.ToString() == null)
                {
                    MessageBox.Show("Select a hairdresser");
                }
                else
                {
                    
                    string cmdString = "SELECT a.[Date],a.[Hairdresser ID] FROM [dbo].[Appointments] a, [dbo].[Hairdresser] h WHERE a.[Date] = '" + date + "' AND h.[Hairdresser Id]=a.[Hairdresser ID] AND h.[Name]='" + hairdresser_cmbbox.SelectedItem.ToString() + "'";
                    SqlConnection con = new SqlConnection(conString);
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = (reader["Hairdresser ID"].ToString());
                                
                                //date = (reader["Date"].ToString());
                            }
                        }
                    }
                    finally
                    {
                        con.Close();
                    }

                    if (id == "" || id == null)
                    {
                        reserved = false;
                    }
                    else
                    {
                        reserved = true;
                    }

                    if (reserved == true)
                    {
                        MessageBox.Show("This hairdresser is not available at this time");
                    }
                    else
                    {
                        checkCategory(d);
                    }
                }
            }
        }



        private void checkCategory(DateTime date)
        {
            string idBarber = selectedBarber();
            string id = customer_txtbox.Text.ToString();
            string category = string.Empty;
            string points = string.Empty;
            string cmdString = "SELECT [Category],[Points] FROM [dbo].[Customers] WHERE [Customer ID] = '" + id + "'";
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        category = (reader["Category"].ToString());
                        points = (reader["Points"].ToString());
                    }
                }
            }
            finally
            {
                con.Close();
            }



            if (category == "Normal") {
                //MessageBox.Show("NORMAL");
                Rooms r = new Rooms(id,category,idBarber ,date);
                FillDataGrid();
            } else if (category == "VIP") {
                //MessageBox.Show("VIP");
                
                Rooms r = new Rooms(id,category,idBarber ,date);
                FillDataGrid();
            }
        }

        private string selectedBarber()
        {
            string barberName = hairdresser_cmbbox.Text.ToString();
            string barber = string.Empty;
            string cmdString = "SELECT [Hairdresser Id] FROM [dbo].[Hairdresser] WHERE [Name]='"+barberName+"'";
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        barber = (reader["Hairdresser Id"].ToString());
                        
                    }
                }
            }
            finally
            {
                con.Close();
            }

            return barber;
        }

        private void FillDataGrid()
        {
            AppointmentPanel a = new AppointmentPanel();
            main.Children.Clear();
            main.Children.Add(a);
        }
    }
}
