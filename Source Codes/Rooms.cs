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
    class Rooms
    {
        string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BarbershopDB.mdf;Integrated Security=True";

        string cmdString = string.Empty;

        public Rooms(string id, string category, string idBarber, DateTime date)
        {
            Boolean r1 = false;
            Boolean r2 = false;
            Boolean r3 = false;
            Boolean r4 = false;
            string room = string.Empty;
            int registerRoom = 0;
            if (category == "Normal")
            {
                int room1 = RoomNormal(1, date);
                int room2 = RoomNormal(2, date);

                if (room1 < 3)
                {
                    r1 = true;
                }
                else if (room2 < 3)
                {
                    r2 = true;
                }

                if (r1 == true && r2 == false){
                    registerRoom = 1;
                }
                else if (r1 == false && r2 == true)
                {
                    registerRoom = 2;
                }
                else
                {
                    MessageBox.Show("There are no available rooms at this time");
                }

                Appointment a1 = new Appointment();
                a1.add(id, idBarber, registerRoom, date);

            } else if (category == "VIP") {

                int room3 = RoomNormal(3, date);
                int room4 = RoomNormal(4, date);

                if (room3 < 1)
                {
                    r3 = true;
                }
                else if (room4 < 1)
                {
                    r4 = true;
                }

                if (r3 == true && r4 == false) {
                    registerRoom = 3;
                }
                else if (r3 == false && r4 == true)
                {
                    registerRoom = 4;
                }
                else
                {
                    MessageBox.Show("There are no available rooms at this time");
                }

                AppointmentVIP a = new AppointmentVIP();
                //MessageBox.Show(id);
                a.getPoints(id);
                a.add(id, idBarber, registerRoom, date);
                a.AddPoints(id);
            }
        }

        public int RoomNormal(int room, DateTime date)
        {
            int roomcount=0;

            cmdString = "SELECT Count([Room]) AS RoomCount FROM [dbo].[Appointments] WHERE [Date]='"+date+"' AND [Room]='"+room+"'";
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roomcount = Convert.ToInt32((reader["RoomCount"].ToString()));
                    }
                }
            }
            finally
            {
                con.Close();
            }

            return roomcount;
        }
    }
}
