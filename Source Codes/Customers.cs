using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BarberShop
{
    class Customers
    {
        string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";
        

        public Customers()
        {
           
        }

        public void add(string name, string category, int points, string phone)
        {
            string cmdString = "INSERT INTO [dbo].[Customers] ([Name], [Category], [Points], [Contact Number]) VALUES (@name, @category, @points, @phone)";

            SqlConnection con = new SqlConnection(conString);
            con.Open();
            using (SqlCommand cmd = new SqlCommand(cmdString))
            {
             
                    cmd.Connection = con;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category;
                    cmd.Parameters.Add("@points", SqlDbType.Int).Value = points;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
                    cmd.ExecuteNonQuery();
                    con.Close();
            }
        }

        public void edit(string id, string contact)
        {
            string cmdString = "UPDATE [dbo].[Customers] SET [Contact Number]=@phone WHERE [Customer ID]=@id";
            //string cString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            using (SqlCommand cmd = new SqlCommand(cmdString))
            {
                cmd.Connection = con;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = contact;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(id);
                int rows = cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
