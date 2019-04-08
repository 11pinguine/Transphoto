using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class CountryTable
    {
        public static String SQL_SELECT = "SELECT * FROM Country";
        public static String SQL_SELECT_ID = "SELECT * FROM Country WHERE idCountry=@id";
        public static String SQL_INSERT = "INSERT INTO Country VALUES (@name, @continent)";
        public static String SQL_DELETE_ID = "DELETE FROM Country WHERE idCountry=@id";
        public static String SQL_UPDATE = "UPDATE Country SET name=@name, continent=@continent WHERE idCountry=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Country country, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, country);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Country country, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, country);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Country> Select(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<Country> countries = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return countries;
        }

        /// <summary>
        /// Select the record.
        /// </summary>
        /// <param name="id">user id</param>
        public static Country Select(int id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Country> Countries = Read(reader);
            Country Country = null;
            if (Countries.Count == 1)
            {
                Country = Countries[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Country;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        /// <param name="idCountry">user id</param>
        /// <returns></returns>
        public static int Delete(int idCountry, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@id", idCountry);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// <summary>
        ///  Prepare a command.
        /// </summary>
        private static void PrepareCommand(SqlCommand command, Country Country)
        {
            command.Parameters.AddWithValue("@id", Country.Id);
            command.Parameters.AddWithValue("@name", Country.Name);
            command.Parameters.AddWithValue("@continent", Country.Continent);


        }

        private static Collection<Country> Read(SqlDataReader reader)
        {
            Collection<Country> countries = new Collection<Country>();

            while (reader.Read())
            {
                int i = -1;
                Country country = new Country();
                country.Id = reader.GetInt32(++i);
                country.Name = reader.GetString(++i);
                country.Continent = reader.GetString(++i);



                countries.Add(country);
            }
            return countries;
        }

        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        public static string CheckCountry(int idCountry, Database pDb)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("CheckCountry");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idCountry";
            input.DbType = DbType.Int32;
            input.Value = idCountry;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            // 3. create output parameters
            SqlParameter output = new SqlParameter();
            output.ParameterName = "@result";
            output.DbType = DbType.String;
            output.Direction = ParameterDirection.Output;
            command.Parameters.Add(output);

            // 4. execute procedure
            int ret = db.ExecuteNonQuery(command);

            // 5. get values of the output parameters
            string result = command.Parameters["@result"].Value.ToString();

            if (pDb == null)
            {
                db.Close();
            }
            return result;
        }
    }
}