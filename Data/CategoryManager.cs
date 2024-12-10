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
            DBHandler db = new DBHandler("category");
            db.InsertCategoryDB(name);
            return "Added Successfully";
        }

        public static List<Category> GetCategories()
        {
            DBHandler db = new DBHandler("category");
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

        public static string GetCategoryName(int categoryId)
        {
            DBHandler db = new DBHandler("category");
            //List<Category> categories = new List<Category>();
            categories = db.LoadCategoriesFromDB();
            foreach (Category category in categories)
            {
                if (category.CategoryId == categoryId) { return category.Name; }
            }
            return "";
        }
        public static int GetCategoryId(string categoryName)
        {
            DBHandler db = new DBHandler("category");
            //List<Category> categories = new List<Category>();
            categories = db.LoadCategoriesFromDB();
            foreach (Category category in categories)
            {
                if (category.Name.ToLower() == categoryName.ToLower()) { return category.CategoryId; }
            }
            return 0;
        }
    }
}
