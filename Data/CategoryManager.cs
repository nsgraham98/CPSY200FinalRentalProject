using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSY200FinalRentalProject.Data
{
    public class CategoryManager
    {
        public static List<Category> categories = new List<Category>();

        // maybe change return
        public static string AddCategory(string name)
        {
            DBHandler db = new DBHandler();
            db.InsertCategoryDB(name);
            return "Added Successfully";
        }

        public static List<Category> GetCategories()
        {
            DBHandler db = new DBHandler();
            //List<Category> categories = new List<Category>();
            categories = db.LoadCategoriesFromDB();
            return categories;
        }

        public static string EditCustomer(int categoryId, string name)
        {
            string message = DBHandler.UpdateCategoryDB(categoryId, name);
            GetCategories();
            return message;
        }
    }
}
