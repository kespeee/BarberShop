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
    /// Interaction logic for CustomerPanel2.xaml
    /// </summary>
    public partial class CustomerPanel2 : UserControl
    {
 

        public CustomerPanel2()
        {
            InitializeComponent();
        }

        private void create_btn_Click(object sender, RoutedEventArgs e)
        {
            writeToDB();

        }

        private void writeToDB()
        {
            string name = name1_txtbox.Text+" "+name2_txtbox.Text;
            string category = string.Empty;
            string phone = phone_txtbox.Text;
            int points = 0;
            Boolean error = true;

            if (name1_txtbox.Text == null || name1_txtbox.Text == " " || name1_txtbox.Text =="" || name2_txtbox.Text =="" || name2_txtbox.Text == null || name2_txtbox.Text == " ")
            {
                MessageBox.Show("Please enter customer's name");
                error = true;

            }
            else 
            {
                error = false;
                //checks empty fields
                if (radio_btn1.IsChecked == true)
                {
                    category = radio_btn1.Content.ToString();
                    error = false;
                }
                else if (radio_btn2.IsChecked == true)
                {
                    category = radio_btn2.Content.ToString();
                    error = false;
                }
                else
                {
                    MessageBox.Show("Category must be selected");
                    error = true;
                }
            }

            if (error == false)
            {
                Customers newCustomer = new Customers();
                newCustomer.add(name, category, points, phone);
                FillDataGrid();
            }

           
        }

        private void FillDataGrid() {
            CustomersPanel c = new CustomersPanel();
            main.Children.Clear();
            main.Children.Add(c);
        }
       
    }
}
