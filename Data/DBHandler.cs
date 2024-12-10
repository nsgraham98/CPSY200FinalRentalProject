using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

namespace CPSY200FinalRentalProject.Data
{
    public class DBHandler
    {
        static List<Customer> CustomerList = new List<Customer>();
        static List<Equipment> EquipmentList = new List<Equipment>();
        static List<Rental> RentalList = new List<Rental>();
        static List<EquipmentInRental> EqInRentalList = new List<EquipmentInRental>();
        static List<Category> CategoryList = new List<Category>();

        // Database file path (should not be a connection string)
        static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "village_rental_system.db");
        static SQLiteConnection database;
        static string connectionString = $"Data Source={dbPath}";

        public static SQLiteConnection GetDatabaseConnection()
        {
            if (database == null)
            {
                database = new SQLiteConnection($"Data Source={dbPath}");
            }
            return database;
        }

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                using var stream = FileSystem.OpenAppPackageFileAsync("village_rental_system.db").Result;
                using var newFile = File.Create(dbPath);
                stream.CopyTo(newFile);
            }
            GetDatabaseConnection();
            database.Close();
        }


        public DBHandler()
        {
            LoadCustomersFromDB();
            LoadEquipmentFromDB();
            LoadRentalsFromDB();
            LoadEquipmentInRentalsFromDB();
            LoadCategoriesFromDB();
        }

        public DBHandler(string loadType)
        {
            if (loadType.ToLower() == "customer")
            {
                LoadCustomersFromDB();
            }
            if (loadType.ToLower() == "equipment")
            {
                LoadEquipmentFromDB(); 
            }
            if (loadType.ToLower() == "rental")
            {
                LoadRentalsFromDB();
            }
            if (loadType.ToLower() == "equipmentinrental")
            {
                LoadEquipmentInRentalsFromDB();
            }
            if (loadType.ToLower() == "category")
            {
                LoadCategoriesFromDB();
            }
        }

        // LOAD DB METHODS
        public List<Customer> LoadCustomersFromDB()
        {
            CustomerList.Clear();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = "Select * from customers";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int CustomerId = reader.GetInt32(0);
                        string LastName = reader.GetString(1);
                        string FirstName = reader.GetString(2);
                        string Phone = reader.GetString(3);
                        string Email = reader.GetString(4);
                        bool IsBanned = reader.GetBoolean(5);

                        Customer customer = new Customer(CustomerId, LastName, FirstName, Phone, Email, IsBanned);
                        CustomerList.Add(customer);
                    }
                }
            }
            connection.Close();
            return CustomerList;
        }
        public List<Equipment> LoadEquipmentFromDB()
        {
            EquipmentList.Clear();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = "Select * from equipment";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int equipmentId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string description = reader.GetString(2);
                        double rentalCost = reader.GetDouble(3);
                        string availability = reader.GetString(4);
                        int category = reader.GetInt32(5);

                        Equipment equipment = new Equipment(equipmentId, category, name, description, rentalCost, availability);
                        EquipmentList.Add(equipment);
                    }
                }
            }
            connection.Close();
            return EquipmentList;
        }

        public List<Rental> LoadRentalsFromDB()
        {
            RentalList.Clear();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = "Select * from rentals";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int rentalId = reader.GetInt32(0);
                        DateTime startDate = reader.GetDateTime(1);
                        DateTime endDate = reader.GetDateTime(2);
                        int customerId = reader.GetInt32(3);

                        Rental rental = new Rental(rentalId, startDate, endDate, customerId);
                        RentalList.Add(rental);
                    }
                }
            }
            connection.Close();
            return RentalList;
        }

        public List<EquipmentInRental> LoadEquipmentInRentalsFromDB()
        {
            EqInRentalList.Clear();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = "Select * from equipment_in_rental";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int rentalId = reader.GetInt32(0);
                        int equipmentId = reader.GetInt32(1);

                        EquipmentInRental eqInRental = new EquipmentInRental(rentalId, equipmentId);
                        EqInRentalList.Add(eqInRental);
                    }
                }
            }
            connection.Close();
            return EqInRentalList;
        }

        public List<Category> LoadCategoriesFromDB()    
        {
            CategoryList.Clear();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = "Select * from category";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int categoryId = reader.GetInt32(0);
                        string name = reader.GetString(1);

                        Category category = new Category(categoryId, name);
                        CategoryList.Add(category);
                    }
                }
            }
            connection.Close();
            return CategoryList;
        }

        // gets next available ID from specified table
        // used for insert commands
        public int GetNextId(string tableName, string idColumnName, SQLiteConnection connection)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string selectQuery = $"SELECT MAX({idColumnName}) AS MaxId FROM {tableName}";

            using (SQLiteCommand selectCmd = new SQLiteCommand(selectQuery, connection))
            {
                var result = selectCmd.ExecuteScalar();
                return result == DBNull.Value ? 1 : Convert.ToInt32(result) + 1;
            }
        }


        // INSERT DB METHODS
        public void InsertCustomerDB(string LastName, string FirstName, string Phone, string Email)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                int newId = GetNextId("customers", "customerId", connection);
                string sql = $"INSERT into customers values(@cId, @cLastName, @cFirstName, @cPhone, @cEmail, @cIsBanned)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@cId", newId);
                    cmd.Parameters.AddWithValue("@cLastName", LastName);
                    cmd.Parameters.AddWithValue("@cFirstName", FirstName);
                    cmd.Parameters.AddWithValue("@cPhone", Phone);
                    cmd.Parameters.AddWithValue("@cEmail", Email);
                    cmd.Parameters.AddWithValue("@cIsBanned", false);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertEquipmentDB(int category, string name, string description, double rentalCost, string availability)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            int newId = GetNextId("equipment", "equipmentId", connection);
            string sql = $"INSERT INTO equipment VALUES(@eId, @eCategory, @eName, @eDescription, @eRentalCost, @eAvailability)";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                cmd.Parameters.AddWithValue("@eId", newId);
                cmd.Parameters.AddWithValue("@eCategory", category);
                cmd.Parameters.AddWithValue("@eName", name);
                cmd.Parameters.AddWithValue("@eDescription", description);
                cmd.Parameters.AddWithValue("@eRentalCost", rentalCost);
                cmd.Parameters.AddWithValue("@eAvailability", availability);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void InsertRentalDB(DateTime startDate, DateTime endDate, int customerId)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            int newId = GetNextId("rentals", "rentalId", connection);
            string sql = $"INSERT INTO rentals VALUES(@rId, @rStartDate, @rEndDate, @rCustomerId)";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                cmd.Parameters.AddWithValue("@rId", newId);
                cmd.Parameters.AddWithValue("@rStartDate", startDate);
                cmd.Parameters.AddWithValue("@rEndDate", endDate);
                cmd.Parameters.AddWithValue("@rCustomerId", customerId);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void InsertEquipmentInRentalDB(int rentalId, int equipmentId)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"INSERT INTO equipment_in_rental VALUES(@eirRentalId, @eirEquipmentId)";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                cmd.Parameters.AddWithValue("@eirRentalId", rentalId);
                cmd.Parameters.AddWithValue("@eirEquipmentId", equipmentId);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void InsertCategoryDB(string name)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            int newId = GetNextId("category", "categoryId", connection);
            string sql = $"INSERT INTO category VALUES(@catId, @catName)";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                cmd.Parameters.AddWithValue("@catId", newId);
                cmd.Parameters.AddWithValue("@catName", name);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }


        // UPDATE DB METHODS
        public static string UpdateCustomerDB(int customerId, string lastName, string firstName, string phone, string email, bool isBanned)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE customers SET LastName=@lastName, FirstName=@firstName, Phone=@phone, Email=@email, IsBanned=@isBanned WHERE CustomerId=@customerId";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@isBanned", isBanned);
                        cmd.Parameters.AddWithValue("@customerId", customerId);

                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return "Updated Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string UpdateEquipmentDB(int equipmentId, int category, string name, string description, double rentalCost, string availability)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE equipment SET Category=@category, Name=@name, Description=@description, RentalCost=@rentalCost WHERE EquipmentId=@equipmentId";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@rentalCost", rentalCost);
                        cmd.Parameters.AddWithValue("@equipmentId", equipmentId);
                        cmd.Parameters.AddWithValue("@availability", availability);

                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return "Updated Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string UpdateRentalDB(int rentalId, DateTime startDate, DateTime endDate, int customerId)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE rentals SET StartDate=@startDate, EndDate=@endDate, CustomerId=@customerId WHERE RentalId=@rentalId";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@rentalId", rentalId);

                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return "Updated Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string UpdateEquipmentInRentalDB(int rentalId, int equipmentId, bool isReturned)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE equipment_in_rental SET RentalId=@rentalId, EquipmentId=@equipmentId, IsReturned=@isReturned WHERE RentalId=@rentalId AND EquipmentId=@equipmentId AND IsReturned=@isReturned";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@RentalId", rentalId);
                        cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                        cmd.Parameters.AddWithValue("@IsReturned", isReturned);

                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return "Updated Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string UpdateCategoryDB(int categoryId, string name)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE category SET CategoryId=@catId, Name=@catName WHERECategoryId=@catId AND Name=@catName";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@catId", categoryId);
                        cmd.Parameters.AddWithValue("@catName", name);

                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return "Updated Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        public string DeleteEquipmentDB(int equipmentId, string category, string name, string description, double rentalCost)
        {
            try
            {
                SQLiteConnection connection = new SQLiteConnection(connectionString);
                connection.Open();
                string sql = "DELETE FROM equipment WHERE EquipmentId=@equipmentId";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@equipmentId", equipmentId);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
