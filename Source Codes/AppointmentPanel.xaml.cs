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
    /// Interaction logic for AppointmentPanel.xaml
    /// </summary>
    public partial class AppointmentPanel : UserControl
    {
        string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";
        string cmdString = string.Empty;
        string cellValue = "";

        public AppointmentPanel()
        {
            InitializeComponent();
            FillDataGrid();
            
            FillAppointmentCmb();
            cmbbox.SelectedIndex = 1;
            
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentPanel2 app2 = new AppointmentPanel2();
            main.Children.Clear();
            main.Children.Add(app2);

        }

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (cellValue != "")
            {
                Appointment a = new Appointment();
                a.edit(cellValue);
                MessageBox.Show("Appointment number " + cellValue + " has been successfully deleted");
                FillDataGrid();
                cmbbox.SelectedIndex = 1;
            }
        }

        private void FillDataGrid()
        {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                cmdString = "SELECT a.[Appointment ID],a.[Customer ID],c.[Name],c.[Category],h.[Name],a.[Room],a.[Date],a.[Discount] FROM [dbo].[Appointments] a, [dbo].[Customers] c, [dbo].[Hairdresser] h WHERE c.[Customer ID]=a.[Customer ID] AND a.[Hairdresser ID]=h.[Hairdresser Id]";
                SqlCommand cmd = new SqlCommand(cmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Appointments");
                sda.Fill(dt);
                dt.Columns[2].ColumnName = "Customer Name";
                dt.Columns[3].ColumnName = "Customer Category";
                dt.Columns[4].ColumnName = "Hairdresser";
                app_datagrid.ItemsSource = dt.DefaultView;
                con.Close();
                
                
        }

        private void FillDataGridToday()
        {
            DateTime today = DateTime.Today;
            TimeSpan time = TimeSpan.Parse("9:00:00");
            TimeSpan time2 = TimeSpan.Parse("21:00:00");
            today = today.Date.Add(time);
            DateTime today1 = DateTime.Today;
            today1 = today1.Date.Add(time2);
            
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            cmdString = "SELECT a.[Appointment ID],a.[Customer ID],c.[Name],c.[Category],h.[Name],a.[Room],a.[Date],a.[Discount] FROM [dbo].[Appointments] a, [dbo].[Customers] c, [dbo].[Hairdresser] h WHERE c.[Customer ID]=a.[Customer ID] AND a.[Hairdresser ID]=h.[Hairdresser Id] AND a.[Date] BETWEEN '"+today+"' AND '"+today1+"'";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Appointments");
            sda.Fill(dt);
            dt.Columns[2].ColumnName = "Customer Name";
            dt.Columns[3].ColumnName = "Customer Category";
            dt.Columns[4].ColumnName = "Hairdresser";
            app_datagrid.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void FillAppointmentCmb()
        {

            ComboBoxItem cmbItem1 = new ComboBoxItem();
            cmbItem1.Content = "Today";
            cmbbox.Items.Add(cmbItem1);
            
            ComboBoxItem cmbItem2 = new ComboBoxItem();
            cmbItem2.Content = "All";
            cmbbox.Items.Add(cmbItem2);
        }

        private void SelectedRow()
        {
            int row = 0;
            DataRowView dataRow = (DataRowView)app_datagrid.SelectedItem;
            try
            {
                cellValue = dataRow.Row.ItemArray[row].ToString();
            }
            catch
            {
                cellValue = "";
            }
            //MessageBox.Show(cellValue);
        }



        private void cmbbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(cmbbox.SelectedIndex+"");
            if (cmbbox.SelectedIndex == 1){
                FillDataGrid();
            }
            else if (cmbbox.SelectedIndex == 0)
            {
                FillDataGridToday();
            }
        }

        private void app_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedRow();
        }
    }
}
