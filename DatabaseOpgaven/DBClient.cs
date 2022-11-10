﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DatabaseOpgaven
{
    class DBClient
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseOpgaven;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #region CRUS Methods hotel
        private int GetMaxHotelNo(SqlConnection connection)
        {
            Console.WriteLine("Calling -> GetMaxHotelNo");

            string queryStringMaxHotelNo = "SELECT  MAX(Hotel_No)  FROM DemoHotel";
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

            string deleteCommandString = $"DELETE FROM DemoHotel  WHERE Hotel_No = {hotelNo}";
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

            string updateCommandString = $"UPDATE DemoHotel SET Name='{hotel.Name}', Address='{hotel.Address}' WHERE Hotel_No = {hotel.HotelNo}";
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

            string queryStringOneHotel = $"SELECT * FROM DemoHotel WHERE hotel_No = {hotelNo}";
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
        #endregion

        #region CRUD Methods facility
        private int GetMaxFacilityNo(SqlConnection connection)
        {
            Console.WriteLine("Calling -> GetMaxFacilityNo");

            string queryStringMaxFacilityNo = "SELECT  MAX(FacilityNo)  FROM DemoFacility";
            Console.WriteLine($"SQL applied: {queryStringMaxFacilityNo}");

            SqlCommand command = new SqlCommand(queryStringMaxFacilityNo, connection);
            SqlDataReader reader = command.ExecuteReader();

            int MaxFacilityNo = 0;

            if (reader.Read())
            {
                MaxFacilityNo = reader.GetInt32(0);
            }

            reader.Close();

            Console.WriteLine($"Max Facility#: {MaxFacilityNo}");
            Console.WriteLine();

            return MaxFacilityNo;
        }

        private int DeleteFacility(SqlConnection connection, int FacilityNo)
        {
            Console.WriteLine("Calling -> DeleteFacility");

            string deleteCommandString = $"DELETE FROM DemoFacility  WHERE FacilityNo = {FacilityNo}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Deleting Facility #{FacilityNo}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int UpdateFacility(SqlConnection connection, Facility Facility)
        {
            Console.WriteLine("Calling -> UpdateFacility");

            string updateCommandString = $"UPDATE DemoFacility SET Name='{Facility.Name}' WHERE FacilityNo = {Facility.FacilityNo}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating Facility #{Facility.FacilityNo}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int InsertFacility(SqlConnection connection, Facility Facility)
        {
            Console.WriteLine("Calling -> InsertFacility");

            string insertCommandString = $"INSERT INTO DemoFacility VALUES({Facility.FacilityNo}, '{Facility.Name}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            SqlCommand command = new SqlCommand(insertCommandString, connection);

            Console.WriteLine($"Creating Facility #{Facility.FacilityNo}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();
 
            return numberOfRowsAffected;
        }

        private List<Facility> ListAllFacilitys(SqlConnection connection)
        {
            Console.WriteLine("Calling -> ListAllFacilitys");

            string queryStringAllFacilitys = "SELECT * FROM DemoFacility";
            Console.WriteLine($"SQL applied: {queryStringAllFacilitys}");

            SqlCommand command = new SqlCommand(queryStringAllFacilitys, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Listing all Facilitys:");
 
            if (!reader.HasRows)
            {
                Console.WriteLine("No Facilitys in database");
                reader.Close();

                return null;
            }

            List<Facility> Facilitys = new List<Facility>();
            while (reader.Read())
            {
                Facility nextFacility = new Facility()
                {
                    FacilityNo = reader.GetInt32(0), 
                    Name = reader.GetString(1)
                };

                Facilitys.Add(nextFacility);

                Console.WriteLine(nextFacility);
            }

            reader.Close();
            Console.WriteLine();

            return Facilitys;
        }

        private Facility GetFacility(SqlConnection connection, int FacilityNo)
        {
            Console.WriteLine("Calling -> GetFacility");

            string queryStringOneFacility = $"SELECT * FROM DemoFacility WHERE FacilityNo = {FacilityNo}";
            Console.WriteLine($"SQL applied: {queryStringOneFacility}");

            SqlCommand command = new SqlCommand(queryStringOneFacility, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding Facility#: {FacilityNo}");

            if (!reader.HasRows)
            {
                Console.WriteLine("No Facilitys in database");
                reader.Close();

                return null;
            }

            Facility Facility = null;
            if (reader.Read())
            {
                Facility = new Facility()
                {
                    FacilityNo = reader.GetInt32(0), 
                    Name = reader.GetString(1)   
                };

                Console.WriteLine(Facility);
            }

            reader.Close();
            Console.WriteLine();

            return Facility;
        }
#endregion
        public void Start()
        {
            #region HOTEL
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
            #endregion
            #region FACILITY
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                ListAllFacilitys(connection);

                Facility newFacility = new Facility()
                {
                    //FacilityID = GetMaxFacilityNo(connection) + 1,
                    Name = "Bowling"
                };

                InsertFacility(connection, newFacility);

                ListAllFacilitys(connection);

                Facility FacilityToBeUpdated = GetFacility(connection, GetMaxFacilityNo(connection));

                FacilityToBeUpdated.Name += "(updated)";

                UpdateFacility(connection, FacilityToBeUpdated);

                ListAllFacilitys(connection);

                Facility FacilityToBeDeleted = GetFacility(connection, FacilityToBeUpdated.FacilityNo);

                DeleteFacility(connection, FacilityToBeDeleted.FacilityNo);

                ListAllFacilitys(connection);

            }
            #endregion
        }
    }
}
