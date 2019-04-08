using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class CategoryTable
    {
        public static String TABLE_NAME = "Category";

        public static String SQL_SELECT = "SELECT *, (SELECT count(*) FROM Auction WHERE category=c.idCategory) FROM Category c";
        public static String SQL_SELECT_ID = "SELECT * FROM Category WHERE idCategory=@idCategory";
        public static String SQL_SELECT_NAME = "SELECT * FROM Category WHERE name=@name";
        public static String SQL_INSERT = "INSERT INTO Category VALUES (@name, @description)";
        public static String SQL_DELETE_ID = "DELETE FROM Category WHERE idCategory=@idCategory";
        public static String SQL_UPDATE = "UPDATE Category SET name=@name, description=@description WHERE idCategory=@idCategory";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Category Category)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, Category);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public static int Update(Category Category)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, Category);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }


        /// <summary>
        /// Select records.
        /// </summary>
        public static Collection<Category> Select()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<Category> Categorys = Read(reader, true);
            reader.Close();
            db.Close();
            return Categorys;
        }

        /// <summary>
        /// Select records for category.
        /// </summary>
        public static Category Select(int idCategory)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@idCategory", idCategory);
            SqlDataReader reader = db.Select(command);

            Collection<Category> categories = Read(reader);
            Category category = null;
            if (categories.Count == 1)
            {
                category = categories[0];
            }
            reader.Close();
            db.Close();
            return category;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        public static int Delete(int id)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@idCategory", id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        /// <summary>
        /// Prepare a command.
        /// </summary>
        private static void PrepareCommand(SqlCommand command, Category Category)
        {
            command.Parameters.AddWithValue("@idCategory", Category.IdCategory);
            command.Parameters.AddWithValue("@name",  Category.Name);
            command.Parameters.AddWithValue("@description",Category.Description == null ? DBNull.Value: (object)Category.Description);
        }

        /// <summary>
        /// Select the record for a name.
        /// </summary>
        public static Category SelectForName(string pName, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_NAME);

            command.Parameters.AddWithValue("@name", pName);
            SqlDataReader reader = db.Select(command);

            Collection<Category> categories = Read(reader);
            Category category = null;
            if (categories.Count == 1)
            {
                category = categories[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return category;
        }


        private static Collection<Category> Read(SqlDataReader reader,bool withItemsCount = false)
        {
            Collection<Category> categories = new Collection<Category>();

            while (reader.Read())
            {
                Category category = new Category();
                int i = -1;
                category.IdCategory = reader.GetInt32(++i);
                category.Name = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    category.Description = reader.GetString(i);
                }
                if (withItemsCount)
                    category.ItemsCount = reader.GetInt32(++i);
                categories.Add(category);
            }
            return categories;
        }
    }
}