﻿using System;
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
    /// Interaction logic for HairdresserPanel3.xaml
    /// </summary>
    public partial class HairdresserPanel3 : UserControl
    {
      

        string index = string.Empty;
        string name = string.Empty;
        string phone = string.Empty;

        string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";
        string cmdString = string.Empty;

        public HairdresserPanel3(string index){
            InitializeComponent();
            this.index = index;
            getInfo();
        }

        private void displayInfo(string ID, string Name, string Phone)
        {
            hairdresserid_txtbox.Text = ID;
            name_txtbox.Text=name;
            phone_txtbox.Text = phone;
        }

        private void getInfo(){
            
            cmdString = "SELECT [Name],[Contact Number] FROM [dbo].[Hairdresser] WHERE [Hairdresser Id]='"+index+"'";
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(cmdString, con);

            try
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = (reader["Name"].ToString());
                        phone = (reader["Contact Number"].ToString());
                    }
                }
            }
            finally
            {
                con.Close();
            }

            displayInfo(index, name, phone);
            
        }

        private void save_btn_Click_1(object sender, RoutedEventArgs e)
        {
            Hairdresser hairdresser = new Hairdresser();
            hairdresser.edit(Convert.ToInt32(index), phone_txtbox.Text.ToString());
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            HairdressersPanel h = new HairdressersPanel();
            main.Children.Clear();
            main.Children.Add(h);
        }

    }
}
