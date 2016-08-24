using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BarberShop
{
    class Hairdresser
    {
        private string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";

        public Hairdresser()
        {

        }

        public void add( string name, string category, string phone)
        {
           string cmdString = "INSERT INTO [dbo].[Hairdresser] ([Name], [Category], [Contact Number]) VALUES (@name,@category,@phone)";

            SqlConnection con = new SqlConnection(conString);

            using (SqlCommand cmd = new SqlCommand(cmdString))
            {
                cmd.Connection = con;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void edit(int id, string phone)
        {
            string cmdString = "UPDATE [dbo].[Hairdresser] SET [Contact Number]=@phone WHERE [Hairdresser ID]=@id";

            SqlConnection con = new SqlConnection(conString);

            using (SqlCommand cmd = new SqlCommand(cmdString))
            {
                cmd.Connection = con;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
           
        }
    }
}
