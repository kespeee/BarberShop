using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BarberShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void appointments_btn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentPanel app_panel = new AppointmentPanel();
            center_panel.Children.Clear();
            center_panel.Children.Add(app_panel);
        }

        private void customers_btn_Click(object sender, RoutedEventArgs e)
        {
            CustomersPanel customers_panel = new CustomersPanel();
            center_panel.Children.Clear();
            center_panel.Children.Add(customers_panel);
        }

        private void hairdressers_btn_Click(object sender, RoutedEventArgs e)
        {
            HairdressersPanel hairdressers_panel = new HairdressersPanel();
            center_panel.Children.Clear();
            center_panel.Children.Add(hairdressers_panel);
        }


    }
}
