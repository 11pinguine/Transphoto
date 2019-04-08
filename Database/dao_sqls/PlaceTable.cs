using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class PlaceTable
    {
        public static String SQL_SELECT = "SELECT * FROM Place";
        public static String SQL_SELECT_ID = "SELECT * FROM Place WHERE idPlace=@id";
        public static String SQL_INSERT = "INSERT INTO Place VALUES (@name)";
        public static String SQL_DELETE_ID = "DELETE FROM Place WHERE idPlace=@id";
        public static String SQL_UPDATE = "UPDATE Place SET name=@name WHERE idPlace=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Place place, Database pDb = null)
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
            PrepareCommand(command, place);
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
        public static int Update(Place place, Database pDb = null)
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
            PrepareCommand(command, place);
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
        public static Collection<Place> Select(Database pDb = null)
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

            Collection<Place> places = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return places;
        }

        /// <summary>
        /// Select the record.
        /// </summary>
        /// <param name="id">user id</param>
        public static Place Select(int id, Database pDb = null)
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

            Collection<Place> Places = Read(reader);
            Place Place = null;
            if (Places.Count == 1)
            {
                Place = Places[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Place;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        /// <param name="idPlace">user id</param>
        /// <returns></returns>
        public static int Delete(int idPlace, Database pDb = null)
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

            command.Parameters.AddWithValue("@id", idPlace);
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
        private static void PrepareCommand(SqlCommand command, Place Place)
        {
            command.Parameters.AddWithValue("@id", Place.Id);
            command.Parameters.AddWithValue("@name", Place.Name);


        }

        private static Collection<Place> Read(SqlDataReader reader)
        {
            Collection<Place> places = new Collection<Place>();

            while (reader.Read())
            {
                int i = -1;
                Place place = new Place();
                place.Id = reader.GetInt32(++i);
                place.Name = reader.GetString(++i);




                places.Add(place);
            }
            return places;
        }

        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        public static string CheckPlace(int idPlace, Database pDb)
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
            SqlCommand command = db.CreateCommand("CheckPlace");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idPlace";
            input.DbType = DbType.Int32;
            input.Value = idPlace;
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