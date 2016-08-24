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
    /// Interaction logic for CustomersPanel.xaml
    /// </summary>
    public partial class CustomersPanel : UserControl
    {
        string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";
        string cmdString = string.Empty;
        string cellValue = "";

        public CustomersPanel()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            CustomerPanel2 customer2 = new CustomerPanel2();
            main.Children.Clear();
            main.Children.Add(customer2);
        }

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            CustomersPanel3 customer3 = new CustomersPanel3(cellValue);
            main.Children.Clear();
            main.Children.Add(customer3);
        }

        private void FillDataGrid()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            cmdString = "SELECT * FROM [Customers]";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Customers");
            sda.Fill(dt);
            customers_datagrid.ItemsSource = dt.DefaultView;

            con.Close();
        }

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            string search = "%"+search_txtbox.Text+"%";
            if (search!=null || search!=" ") {
                SearchTable(search);
            }
            else
            {
                FillDataGrid();
            }
        }

        private void SearchTable(string search)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            cmdString = "SELECT * FROM [Customers] WHERE [Customer ID] LIKE '"+search+"'";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Customers");
            sda.Fill(dt);
            customers_datagrid.ItemsSource = dt.DefaultView;

            con.Close();
        }

        private void SelectedRow()
        {
            int row = 0;
            DataRowView dataRow = (DataRowView)customers_datagrid.SelectedItem;
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

        private void customers_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedRow();
        }
    }
}
