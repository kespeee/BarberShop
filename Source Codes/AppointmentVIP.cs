using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace BarberShop
{
    class AppointmentVIP : Appointment
    {

        private void pointCount(int point, string id)
        {
            if (point >= 100)
            {
                MessageBoxResult m1 = MessageBox.Show("You have " + point + " points available. Would you like to use your points to get a 50% discount?", "VIP Points", MessageBoxButton.YesNo);
                if (m1 == MessageBoxResult.Yes)
                {
                    point = point-100;
                    setPoints(point, id);
                    discount = true;
                }
                else
                {
                    discount = false;
                }

            }


        }

        public void getPoints(string id)
        {
            int point=0;
            string strPoint = string.Empty;
            string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";
            string cmdString = "SELECT [Points] FROM [dbo].[Customers] WHERE [Customer ID]='" + id + "'";

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(cmdString, con);
          
            try {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        strPoint = (reader["Points"].ToString());
                    }
                }
            }
            finally
            {
                con.Close();
            }
            point = Convert.ToInt32(strPoint);
            pointCount(point, id);
        }

        private void setPoints(int point, string id)
        {
            string cmdString = "UPDATE [dbo].[Customers] SET [Points]=@points WHERE [Customer ID]=@id";
            string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            using (SqlCommand cmd = new SqlCommand(cmdString))
            {
                cmd.Connection = con;
                cmd.Parameters.Add("@points", SqlDbType.VarChar).Value = point;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void AddPoints(string id)
        {
            int point = 0;
            string strPoint = string.Empty;
            string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";
            string cmdString = "SELECT [Points] FROM [dbo].[Customers] WHERE [Customer ID]='" + id + "'";

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(cmdString, con);

            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        strPoint = (reader["Points"].ToString());
                    }
                }
            }
            finally
            {
                con.Close();
            }

            point = Convert.ToInt32(strPoint) + 15;

            cmdString = "UPDATE [dbo].[Customers] SET [Points]=@points WHERE [Customer ID]='" + id + "'";
            con = new SqlConnection(conString);
            using (cmd = new SqlCommand(cmdString))
            {
                cmd.Connection = con;
                cmd.Parameters.Add("@points", SqlDbType.Int).Value = point;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
