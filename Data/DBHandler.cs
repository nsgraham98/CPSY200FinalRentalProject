using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace CPSY200FinalRentalProject.Data
{
    public class DBHandler
    {
        static List<Customer> CustomerList = new List<Customer>();
        static List<Equipment> EquipmentList = new List<Equipment>();
        static List<Rental> RentalList = new List<Rental>();
        static List<EquipmentInRental> EqInRentalList = new List<EquipmentInRental>();
        static List<Category> CategoryList = new List<Category>();

        static string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string dbPath = Path.Combine(baseDirectory, "village_rental_system.db");
        static string connectionString = $"Data Source={dbPath}";

        public DBHandler()
        {
            //CreateTableDB();
            LoadCustomerFromDB();
            LoadEquipmentFromDB();
            LoadRentalFromDB();
            LoadEquipmentInRentalFromDB();
            LoadCategoryFromDB();
        }

        public DBHandler(string loadType)
        {
            if (loadType.ToLower() == "customer")
            {
                LoadCustomerFromDB();
            }
            if (loadType.ToLower() == "equipment")
            {
                LoadEquipmentFromDB(); 
            }
            if (loadType.ToLower() == "rental")
            {
                LoadRentalFromDB();
            }
            if (loadType.ToLower() == "equipmentinrental")
            {
                LoadEquipmentInRentalFromDB();
            }
            if (loadType.ToLower() == "category")
            {
                LoadCategoryFromDB();
            }
        }

        //protected bool IsTableExist()
        //{
        //    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        //    {
        //        connection.Open();
        //        string sql = $"Select name from sqlite_master where type='table' and name='products1';";
        //        using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
        //        {
        //            using (SQLiteDataReader reader = cmd.ExecuteReader())
        //            {
        //                return reader.Read();
        //            }
        //        }
        //    }
        //}

        //private void CreateTableDB()
        //{
        //    if (!IsTableExist())
        //    {
        //        SQLiteConnection connection = new SQLiteConnection(connectionString);
        //        connection.Open();
        //        string sql = "Create table products1(ProdId INTEGER(8) PRIMARY KEY, ProdName TEXT(30), ProdQty INTEGER(8), ProdPrice INTEGER(8))";
        //        using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
        //        {
        //            cmd.ExecuteNonQuery();
        //        }
        //        connection.Close();
        //    }
        //}

        // LOAD DB METHODS
        public List<Customer> LoadCustomersFromDB()
        {
            // CreateTableDB();
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
            // CreateTableDB();
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
                        string category = reader.GetString(1);
                        string name = reader.GetString(2);
                        string description = reader.GetString(3);
                        double rentalCost = reader.GetDouble(4);
                        string availability = reader.GetString(5);

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
            // CreateTableDB();
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
            // CreateTableDB();
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
            // CreateTableDB();
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
        public int GetNextId(string tableName, string idColumnName)
        {
            int res = 0;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = $"Select MAX({idColumnName}) AS MaxId from {tableName}";
                using (SQLiteCommand selectCmd = new SQLiteCommand(selectQuery, connection))
                {
                    var result = selectCmd.ExecuteScalar();
                    return result == DBNull.Value ? 1 : Convert.ToInt32(result) + 1;
                    connection.Close();
                }
            }
        }


        // INSERT DB METHODS
        public void InsertCustomerDB(string LastName, string FirstName, string Phone, string Email)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            int newId = GetNextId("customers", "customer_id");
            string sql = $"INSERT into customers values(@cId, @cLastName, @cFirstName, @cPhone, @cEmail, @cIsBanned)";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                cmd.Parameters.AddWithValue("@cId", newId);
                cmd.Parameters.AddWithValue("@cLastName", LastName);
                cmd.Parameters.AddWithValue("@cFirstName", FirstName);
                cmd.Parameters.AddWithValue("@cPhone", Phone);
                cmd.Parameters.AddWithValue("@cEmail", Email);
                cmd.Parameters.AddWithValue("@cIsBanned", false);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void InsertEquipmentDB(int category, string name, string description, double rentalCost, string availability)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            int newId = GetNextId("equipment", "equipment_id");
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
            int newId = GetNextId("rentals", "rental_id");
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

        public void InsertEquipmentInRental(int rentalId, int equipmentId)
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

        public void InsertCategory(int categoryId, string name)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"INSERT INTO category VALUES(@catId, @catName)";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            using (cmd)
            {
                cmd.Parameters.AddWithValue("@catId", categoryId);
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

        public static string UpdateEquipmentInRentalDB(int rentalId, int equipmentId)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE equipment_in_rental SET RentalId=@rentalId, EquipmentId=@equipmentId WHERE RentalId=@rentalId AND EquipmentId=@equipmentId";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@rentalId", rentalId);
                        cmd.Parameters.AddWithValue("@equipmentId", equipmentId);

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
