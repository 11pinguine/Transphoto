using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class CityTable
    {
        public static String SQL_SELECT = "SELECT * FROM City";
        public static String SQL_SELECT_ID = "SELECT * FROM City WHERE idCity=@id";
        public static String SQL_INSERT = "INSERT INTO City VALUES (@name, @region, @countryid)";
        public static String SQL_DELETE_ID = "DELETE FROM City WHERE idCity=@id";
        public static String SQL_UPDATE = "UPDATE City SET name=@name, region=@region, countryid=@country_id WHERE idCity=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(City city, Database pDb = null)
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
            PrepareCommand(command, city);
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
        public static int Update(City city, Database pDb = null)
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
            PrepareCommand(command, city);
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
        public static Collection<City> Select(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<City> cities = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return cities;
        }

        /// <summary>
        /// Select the record.
        /// </summary>
        /// <param name="id">user id</param>
        public static City Select(int id, Database pDb = null)
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

            Collection<City> Cities = Read(reader);
            City City = null;
            if (Cities.Count == 1)
            {
                City = Cities[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return City;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        /// <param name="idCity">user id</param>
        /// <returns></returns>
        public static int Delete(int idCity, Database pDb = null)
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

            command.Parameters.AddWithValue("@id", idCity);
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
        private static void PrepareCommand(SqlCommand command, City City)
        {
            command.Parameters.AddWithValue("@id", City.Id);
            command.Parameters.AddWithValue("@name", City.Name);
            command.Parameters.AddWithValue("@region", City.Region);
            command.Parameters.AddWithValue("@country_id", City.CountryID);


        }

        private static Collection<City> Read(SqlDataReader reader)
        {
            Collection<City> cities = new Collection<City>();

            while (reader.Read())
            {
                int i = -1;
                City city = new City();
                city.Id = reader.GetInt32(++i);
                city.Name = reader.GetString(++i);
                city.Region = reader.GetString(++i);
                
                

                cities.Add(city);
            }
            return cities;
        }

        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        public static string CheckCity(int idCity, Database pDb)
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
            SqlCommand command = db.CreateCommand("CheckCity");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idCity";
            input.DbType = DbType.Int32;
            input.Value = idCity;
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