using System;
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
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = DatabaseOpgaven; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #region CRUS Methods Hotel
        private int GetMaxHotelNo(SqlConnection connection)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> GetMaxHotelNo");
            Console.ResetColor();

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

        private int DeleteHotel(SqlConnection connection, int hotel_No)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> DeleteHotel");
            Console.ResetColor();

            string deleteCommandString = $"DELETE FROM DemoHotel  WHERE Hotel_No = {hotel_No}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Deleting hotel #{hotel_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int UpdateHotel(SqlConnection connection, Hotel hotel)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> UpdateHotel");
            Console.ResetColor();

            string updateCommandString = $"UPDATE DemoHotel SET Name='{hotel.Name}', Address='{hotel.Address}' WHERE Hotel_No = {hotel.Hotel_No}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating hotel #{hotel.Hotel_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int InsertHotel(SqlConnection connection, Hotel hotel)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> InsertHotel");
            Console.ResetColor();

            string insertCommandString = $"INSERT INTO DemoHotel VALUES({hotel.Hotel_No}, '{hotel.Name}', '{hotel.Address}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            SqlCommand command = new SqlCommand(insertCommandString, connection);

            Console.WriteLine($"Creating hotel #{hotel.Hotel_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private List<Hotel> ListAllHotels(SqlConnection connection)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> ListAllHotels");
            Console.ResetColor();

            string queryStringAllHotels = "SELECT * FROM DemoHotel";
            Console.WriteLine($"SQL applied: {queryStringAllHotels}");

            SqlCommand command = new SqlCommand(queryStringAllHotels, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Listing all hotels:");

            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hotels in database");
                Console.ResetColor();
                reader.Close();

                return null;
            }

            List<Hotel> hotels = new List<Hotel>();
            while (reader.Read())
            {
                Hotel nextHotel = new Hotel()
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

        private Hotel GetHotel(SqlConnection connection, int hotel_No)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> GetHotel");
            Console.ResetColor();

            string queryStringOneHotel = $"SELECT * FROM DemoHotel WHERE hotel_No = {hotel_No}";
            Console.WriteLine($"SQL applied: {queryStringOneHotel}");

            SqlCommand command = new SqlCommand(queryStringOneHotel, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding hotel#: {hotel_No}");

            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hotels in database");
                Console.ResetColor();
                reader.Close();

                return null;
            }

            Hotel hotel = null;
            if (reader.Read())
            {
                hotel = new Hotel()
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
        #endregion

        #region CRUD Methods Facility
        private int GetMaxFacilityNo(SqlConnection connection)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> GetMaxFacilityNo");
            Console.ResetColor();

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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> DeleteFacility");
            Console.ResetColor();

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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> UpdateFacility");
            Console.ResetColor();

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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> InsertFacility");
            Console.ResetColor();

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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> ListAllFacilitys");
            Console.ResetColor();

            string queryStringAllFacilitys = "SELECT * FROM DemoFacility";
            Console.WriteLine($"SQL applied: {queryStringAllFacilitys}");

            SqlCommand command = new SqlCommand(queryStringAllFacilitys, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Listing all Facilitys:");
 
            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Facilitys in database");
                Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> GetFacility");
            Console.ResetColor();

            string queryStringOneFacility = $"SELECT * FROM DemoFacility WHERE FacilityNo = {FacilityNo}";
            Console.WriteLine($"SQL applied: {queryStringOneFacility}");

            SqlCommand command = new SqlCommand(queryStringOneFacility, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding Facility#: {FacilityNo}");

            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Facilitys in database");
                Console.ResetColor();
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

        #region CRUD Methods FacilityHotel
        private int DeleteFacilityHotel(SqlConnection connection, int facilityNo, int hotel_No)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> DeleteFacilityHotel");
            Console.ResetColor();

            string deleteCommandString = $"DELETE FROM DemoFacilityHotel WHERE FacilityNo = {facilityNo} AND Hotel_No = {hotel_No}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Deleting FacilityHotel facility: #{facilityNo}, hotel: {hotel_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int UpdateFacilityHotel(SqlConnection connection, FacilityHotel facilityHotel)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> UpdateFacilityHotel");
            Console.ResetColor();

            string updateCommandString = $"UPDATE DemoFacilityHotel SET FacilityNo = '{facilityHotel.FacilityNo}', Hotel_No = '{facilityHotel.Hotel_No}', Floor = '{facilityHotel.Floor}' WHERE FacilityNo = {facilityHotel.FacilityNo} and Hotel_No = {facilityHotel.Hotel_No}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating FacilityHotel. Facility: #{facilityHotel.FacilityNo}, hotel: {facilityHotel.Hotel_No}, {facilityHotel.Floor}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int InsertFacilityHotel(SqlConnection connection, FacilityHotel FacilityHotel)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> InsertFacilityHotel");
            Console.ResetColor();

            string insertCommandString = $"INSERT INTO DemoFacilityHotel VALUES('{FacilityHotel.FacilityNo}', '{FacilityHotel.Hotel_No}', '{FacilityHotel.Floor}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            SqlCommand command = new SqlCommand(insertCommandString, connection);

            Console.WriteLine($"Creating FacilityHotel #{FacilityHotel.FacilityNo}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private List<FacilityHotel> ListAllFacilitysAndHotels(SqlConnection connection)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> ListAllFacilitysAndHotels");
            Console.ResetColor();

            string queryStringAllFacilitysAndHotels = "SELECT * FROM DemoFacilityHotel";
            Console.WriteLine($"SQL applied: {queryStringAllFacilitysAndHotels}");

            SqlCommand command = new SqlCommand(queryStringAllFacilitysAndHotels, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Listing all FacilityHotels:");

            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No FacilityHotels in database");
                Console.ResetColor();
                reader.Close();

                return null;
            }

            List<FacilityHotel> FacilityHotels = new List<FacilityHotel>();
            while (reader.Read())
            {
                FacilityHotel nextFacilityHotel = new FacilityHotel()
                {
                    FacilityNo = reader.GetInt32(0),
                    Hotel_No = reader.GetInt32(1),
                    Floor = reader.GetString(2)
                };

                FacilityHotels.Add(nextFacilityHotel);

                Console.WriteLine(nextFacilityHotel);
            }

            reader.Close();
            Console.WriteLine();

            return FacilityHotels;
        }

        private FacilityHotel GetFacilityHotel(SqlConnection connection, int FacilityNo, int Hotel_No)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Calling -> GetFacilityHotel");
            Console.ResetColor();

            string queryStringOneFacilityHotel = $"SELECT * FROM DemoFacilityHotel WHERE FacilityNo = {FacilityNo} AND Hotel_No = {Hotel_No}";
            Console.WriteLine($"SQL applied: {queryStringOneFacilityHotel}");

            SqlCommand command = new SqlCommand(queryStringOneFacilityHotel, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding FacilityHotel #: FacilityNo: {FacilityNo}, HotelNo: {Hotel_No}");

            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("No FacilityHotels in database");
                Console.ResetColor();
                reader.Close();

                return null;
            }

            FacilityHotel FacilityHotel = null;
            if (reader.Read())
            {
                FacilityHotel = new FacilityHotel()
                {
                    FacilityNo = reader.GetInt32(0),
                    Hotel_No = reader.GetInt32(1),
                    Floor = reader.GetString(2)
                };

                Console.WriteLine(FacilityHotel);
            }

            reader.Close();
            Console.WriteLine();

            return FacilityHotel;
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
                    Hotel_No = GetMaxHotelNo(connection) + 1,
                    Name = "New Hotel",
                    Address = "Maglegaardsvej 2, 4000 Roskilde"
                };

                InsertHotel(connection, newHotel);
                ListAllHotels(connection);

                Hotel hotelToBeUpdated = GetHotel(connection, newHotel.Hotel_No);

                hotelToBeUpdated.Name += "(updated)";
                hotelToBeUpdated.Address += "(updated)";

                UpdateHotel(connection, hotelToBeUpdated);

                ListAllHotels(connection);

                Hotel hotelToBeDeleted = GetHotel(connection, hotelToBeUpdated.Hotel_No);

                DeleteHotel(connection, hotelToBeDeleted.Hotel_No);

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
                    FacilityNo = GetMaxFacilityNo(connection) + 1,
                    Name = "Trampolinpark"
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
            #region FACILITYHOTEL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                ListAllFacilitysAndHotels(connection);

                FacilityHotel newFacilityHotel = new FacilityHotel()
                {
                    FacilityNo =  3,
                    Hotel_No = 5,
                    Floor = "43th floor"
                };

                InsertFacilityHotel(connection, newFacilityHotel);

                ListAllFacilitysAndHotels(connection);

                FacilityHotel FacilityHotelToBeUpdated = GetFacilityHotel(connection, newFacilityHotel.FacilityNo, newFacilityHotel.Hotel_No);

                FacilityHotelToBeUpdated.Floor += "(updated)";

                UpdateFacilityHotel(connection, FacilityHotelToBeUpdated);

                ListAllFacilitysAndHotels(connection);

                FacilityHotel FacilityHotelToBeDeleted = GetFacilityHotel(connection, newFacilityHotel.FacilityNo, newFacilityHotel.Hotel_No);

                DeleteFacilityHotel(connection, FacilityHotelToBeDeleted.FacilityNo, FacilityHotelToBeDeleted.Hotel_No);

                ListAllFacilitysAndHotels(connection); 
            }
            #endregion
        }
    }
}
