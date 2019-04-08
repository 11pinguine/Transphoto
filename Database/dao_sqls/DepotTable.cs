using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class DepotTable
    {
        public static String SQL_SELECT = "SELECT * FROM Depot";
        public static String SQL_SELECT_ID = "SELECT * FROM Depot WHERE idDepot=@id";
        public static String SQL_INSERT = "INSERT INTO Depot VALUES (@name)";
        public static String SQL_DELETE_ID = "DELETE FROM Depot WHERE idDepot=@id";
        public static String SQL_UPDATE = "UPDATE Depot SET name=@name WHERE idDepot=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Depot depot, Database pDb = null)
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
            PrepareCommand(command, depot);
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
        public static int Update(Depot depot, Database pDb = null)
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
            PrepareCommand(command, depot);
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
        public static Collection<Depot> Select(Database pDb = null)
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

            Collection<Depot> depots = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return depots;
        }

        /// <summary>
        /// Select the record.
        /// </summary>
        /// <param name="id">user id</param>
        public static Depot Select(int id, Database pDb = null)
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

            Collection<Depot> Depots = Read(reader);
            Depot Depot = null;
            if (Depots.Count == 1)
            {
                Depot = Depots[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Depot;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        /// <param name="idDepot">user id</param>
        /// <returns></returns>
        public static int Delete(int idDepot, Database pDb = null)
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

            command.Parameters.AddWithValue("@id", idDepot);
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
        private static void PrepareCommand(SqlCommand command, Depot Depot)
        {
            command.Parameters.AddWithValue("@id", Depot.Id);
            command.Parameters.AddWithValue("@name", Depot.Name);


        }

        private static Collection<Depot> Read(SqlDataReader reader)
        {
            Collection<Depot> depots = new Collection<Depot>();

            while (reader.Read())
            {
                int i = -1;
                Depot depot = new Depot();
                depot.Id = reader.GetInt32(++i);
                depot.Name = reader.GetString(++i);




                depots.Add(depot);
            }
            return depots;
        }

        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        public static string CheckDepot(int idDepot, Database pDb)
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
            SqlCommand command = db.CreateCommand("CheckDepot");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idDepot";
            input.DbType = DbType.Int32;
            input.Value = idDepot;
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