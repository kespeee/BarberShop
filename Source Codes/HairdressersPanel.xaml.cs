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
    /// Interaction logic for HairdressersPanel.xaml
    /// </summary>
    public partial class HairdressersPanel : UserControl
    {
        string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";
        string cmdString = string.Empty;
        string cellValue = "";

        public HairdressersPanel()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            HairdresserPanel2 haird2 = new HairdresserPanel2();
            main.Children.Clear();
            main.Children.Add(haird2);
        }

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (cellValue != "")
            {
                HairdresserPanel3 haird3 = new HairdresserPanel3(cellValue);
                main.Children.Clear();
                main.Children.Add(haird3);
            }
        }

        private void FillDataGrid()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open(); 
            cmdString = "SELECT * FROM [dbo].[Hairdresser]";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Hairdressers");
            sda.Fill(dt);
            hairdressers_datagrid.ItemsSource = dt.DefaultView;
            
            con.Close();
        }

        private void SelectedRow()
        {
            int row = 0;
            DataRowView dataRow = (DataRowView)hairdressers_datagrid.SelectedItem;
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

        private void hairdressers_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedRow();
        }
    }
}
