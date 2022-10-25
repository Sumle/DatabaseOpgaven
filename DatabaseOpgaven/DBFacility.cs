using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseOpgaven
{
    class DBFacility
    {
        string connectionString = @".....";
        private int GetMaxHotelNo(SqlConnection connection)
        {
            Console.WriteLine("Calling -> GetMaxHotelNo");

            string queryStringMaxHotelNo = "SELECT  MAX(HotelNo)  FROM DemoHotel";
            Console.WriteLine($"SQL applied: {queryStringMaxHotelNo}");

            SqlCommand command = new SqlCommand(queryStringMaxHotelNo, connection);
            SqlDataReader reader = command.ExecuteReader();

            int MaxHotelNo = 0;

            if (reader.Read())
            {
                MaxHotelNo = reader.GetInt32(0); 
            }

            reader.Close();

            Console.WriteLine($"Max hotel#: {MaxHotelNo}");
            Console.WriteLine();

            return MaxHotelNo;
        }

        private int DeleteHotel(SqlConnection connection, int hotelNo)
        {
            Console.WriteLine("Calling -> DeleteHotel");

            string deleteCommandString = $"DELETE FROM DemoHotel  WHERE HotelNo = {hotelNo}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Deleting hotel #{hotelNo}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int UpdateHotel(SqlConnection connection, Hotel hotel)
        {
            Console.WriteLine("Calling -> UpdateHotel");

            string updateCommandString = $"UPDATE DemoHotel SET Name='{hotel.Name}', Address='{hotel.Address}' WHERE HotelNo = {hotel.HotelNo}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating hotel #{hotel.HotelNo}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int InsertHotel(SqlConnection connection, Hotel hotel)
        {
            Console.WriteLine("Calling -> InsertHotel");

            string insertCommandString = $"INSERT INTO DemoHotel VALUES({hotel.HotelNo}, '{hotel.Name}', '{hotel.Address}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            SqlCommand command = new SqlCommand(insertCommandString, connection);

            Console.WriteLine($"Creating hotel #{hotel.HotelNo}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private List<Hotel> ListAllHotels(SqlConnection connection)
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

            List<Hotel> hotels = new List<Hotel>();
            while (reader.Read())
            {
                Hotel nextHotel = new Hotel()
                {
                    HotelNo = reader.GetInt32(0), 
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

        private Hotel GetHotel(SqlConnection connection, int hotelNo)
        {
            Console.WriteLine("Calling -> GetHotel");

            string queryStringOneHotel = $"SELECT * FROM DemoHotel WHERE hotelno = {hotelNo}";
            Console.WriteLine($"SQL applied: {queryStringOneHotel}");

            SqlCommand command = new SqlCommand(queryStringOneHotel, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding hotel#: {hotelNo}");

            if (!reader.HasRows)
            {
                Console.WriteLine("No hotels in database");
                reader.Close();

                return null;
            }

            Hotel hotel = null;
            if (reader.Read())
            {
                hotel = new Hotel()
                {
                    HotelNo = reader.GetInt32(0), 
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

                Hotel newHotel = new Hotel()
                {
                    HotelNo = GetMaxHotelNo(connection) + 1,
                    Name = "New Hotel",
                    Address = "Maglegaardsvej 2, 4000 Roskilde"
                };

                InsertHotel(connection, newHotel);
                ListAllHotels(connection);

                Hotel hotelToBeUpdated = GetHotel(connection, newHotel.HotelNo);

                hotelToBeUpdated.Name += "(updated)";
                hotelToBeUpdated.Address += "(updated)";

                UpdateHotel(connection, hotelToBeUpdated);

                ListAllHotels(connection);

                Hotel hotelToBeDeleted = GetHotel(connection, hotelToBeUpdated.HotelNo);

                DeleteHotel(connection, hotelToBeDeleted.HotelNo);

                ListAllHotels(connection);
            }
        }
    }
}
