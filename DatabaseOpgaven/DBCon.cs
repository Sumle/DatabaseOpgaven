using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseOpgaven
{
    class DBCon
    {
        string connectionString = @".....";
        private int GetMaxHotelNo(SqlConnection connection)
        {
            Console.WriteLine("Calling -> GetMaxHotelNo");

            string queryStringMaxHotelNo = "SELECT  MAX(Hotel_No)  FROM DemoHotel";
            Console.WriteLine($"SQL applied: {queryStringMaxHotelNo}");

            SqlCommand command = new SqlCommand(queryStringMaxHotelNo, connection);
            SqlDataReader reader = command.ExecuteReader();

            int MaxHotel_No = 0;

            if (reader.Read())
            {
                MaxHotel_No = reader.GetInt32(0); 
            }

            reader.Close();

            Console.WriteLine($"Max hotel#: {MaxHotel_No}");
            Console.WriteLine();

            return MaxHotel_No;
        }

        private int DeleteHotel(SqlConnection connection, int hotel_no)
        {
            Console.WriteLine("Calling -> DeleteHotel");

            string deleteCommandString = $"DELETE FROM DemoHotel  WHERE Hotel_No = {hotel_no}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Deleting hotel #{hotel_no}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int UpdateHotel(SqlConnection connection, DBCon hotel)
        {
            Console.WriteLine("Calling -> UpdateHotel");

            string updateCommandString = $"UPDATE DemoHotel SET Name='{hotel.Name}', Address='{hotel.Address}' WHERE Hotel_No = {hotel.Hotel_No}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating hotel #{hotel.Hotel_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int InsertHotel(SqlConnection connection, DBCon hotel)
        {
            Console.WriteLine("Calling -> InsertHotel");

            string insertCommandString = $"INSERT INTO DemoHotel VALUES({hotel.Hotel_No}, '{hotel.Name}', '{hotel.Address}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            SqlCommand command = new SqlCommand(insertCommandString, connection);

            Console.WriteLine($"Creating hotel #{hotel.Hotel_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private List<DBCon> ListAllHotels(SqlConnection connection)
        {
            Console.WriteLine("Calling -> ListAllHotels");

            string queryStringAllHotels = "SELECT * FROM DemoHotel";
            Console.WriteLine($"SQL applied: {queryStringAllHotels}");

            SqlCommand command = new SqlCommand(queryStringAllHotels, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Listing all hotels:");

            if (!reader.HasRows)
            {
                Console.WriteLine("No hotels in database");
                reader.Close();

                return null;
            }

            List<DBCon> hotels = new List<DBCon>();
            while (reader.Read())
            {
                DBCon nextHotel = new DBCon()
                {
                    Hotel_No = reader.GetInt32(0), 
                    Name = reader.GetString(1),    
                    Address = reader.GetString(2)  
                };

                hotels.Add(nextHotel);

                Console.WriteLine(nextHotel);
            }

            reader.Close();
            Console.WriteLine();

            return hotels;
        }

        private DBCon GetHotel(SqlConnection connection, int hotel_no)
        {
            Console.WriteLine("Calling -> GetHotel");

            string queryStringOneHotel = $"SELECT * FROM DemoHotel WHERE hotel_no = {hotel_no}";
            Console.WriteLine($"SQL applied: {queryStringOneHotel}");

            SqlCommand command = new SqlCommand(queryStringOneHotel, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding hotel#: {hotel_no}");

            if (!reader.HasRows)
            {
                Console.WriteLine("No hotels in database");
                reader.Close();

                return null;
            }

            DBCon hotel = null;
            if (reader.Read())
            {
                hotel = new DBCon()
                {
                    Hotel_No = reader.GetInt32(0), 
                    Name = reader.GetString(1),    
                    Address = reader.GetString(2)  
                };

                Console.WriteLine(hotel);
            }

            reader.Close();
            Console.WriteLine();

            return hotel;
        }
        public void Start()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                ListAllHotels(connection);

                DBCon newHotel = new DBCon()
                {
                    Hotel_No = GetMaxHotelNo(connection) + 1,
                    Name = "New Hotel",
                    Address = "Maglegaardsvej 2, 4000 Roskilde"
                };

                InsertHotel(connection, newHotel);
                ListAllHotels(connection);

                DBCon hotelToBeUpdated = GetHotel(connection, newHotel.Hotel_No);

                hotelToBeUpdated.Name += "(updated)";
                hotelToBeUpdated.Address += "(updated)";

                UpdateHotel(connection, hotelToBeUpdated);

                ListAllHotels(connection);

                DBCon hotelToBeDeleted = GetHotel(connection, hotelToBeUpdated.Hotel_No);

                DeleteHotel(connection, hotelToBeDeleted.Hotel_No);

                ListAllHotels(connection);
            }
        }
    }
}
