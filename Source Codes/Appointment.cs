using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace BarberShop
{
    class Appointment
    {
        string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";
        string cmdString = string.Empty;
        public Boolean discount = false;

        public Appointment()
        {

        }

        public void add(string customerID, string hairdresserID, int room, DateTime date)
        {
            cmdString = "INSERT INTO [dbo].[Appointments] ([Customer ID],[Hairdresser ID],[Room],[Date],[Discount]) VALUES (@customerid,@hairdresserid,@room,@date,@discount)";
            SqlConnection con = new SqlConnection(conString);
            using (SqlCommand cmd = new SqlCommand(cmdString))
                
            {
                cmd.Connection = con;
                cmd.Parameters.Add("@customerid",SqlDbType.Int).Value = Convert.ToInt32(customerID);
                cmd.Parameters.Add("@hairdresserid", SqlDbType.Int).Value = Convert.ToInt32(hairdresserID);
                cmd.Parameters.Add("@room", SqlDbType.Int).Value = room;
                cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = date;
                cmd.Parameters.Add("@discount", SqlDbType.Bit).Value = discount;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

             

        }

        public void edit(string appointmentID)
        {
            cmdString = "DELETE FROM [dbo].[Appointments] WHERE [Appointment ID]=@appID";
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(cmdString,con);
            con.Open();
            cmd.Parameters.Add("@appID", SqlDbType.Int).Value = Convert.ToInt32(appointmentID);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
