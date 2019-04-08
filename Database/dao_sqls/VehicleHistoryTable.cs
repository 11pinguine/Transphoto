using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class VehicleHistoryTable
    {
        public static String SQL_SELECT = "SELECT * FROM Vehicle_History";
        public static String SQL_SELECT_ID = "SELECT * FROM Vehicle_History WHERE idVehicleHistory=@id";
        public static String SQL_INSERT = "INSERT INTO Vehicle_History VALUES (@evidence_id, @start_date, @end_date, @car_license_plate, @podtyp)";
        public static String SQL_DELETE_ID = "DELETE FROM Vehicle_History WHERE idVehicleHistory=@id";
        public static String SQL_UPDATE = "UPDATE Vehicle_History SET evidence_id=@evidence_id, start_date=@start_date, end_date=@end_date, car_license_plate=@car_license_plate, podtyp=@podtyp WHERE idVehicleHistory=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(VehicleHistory vehicleHistory, Database pDb = null)
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
            PrepareCommand(command, vehicleHistory);
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
        public static int Update(VehicleHistory vehicleHistory, Database pDb = null)
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
            PrepareCommand(command, vehicleHistory);
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
        public static Collection<VehicleHistory> Select(Database pDb = null)
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

            Collection<VehicleHistory> vehicleHistories = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return vehicleHistories;
        }

        /// <summary>
        /// Select the record.
        /// </summary>
        /// <param name="id">user id</param>
        public static VehicleHistory Select(int id, Database pDb = null)
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

            Collection<VehicleHistory> VehicleHistories = Read(reader);
            VehicleHistory VehicleHistory = null;
            if (VehicleHistories.Count == 1)
            {
                VehicleHistory = VehicleHistories[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return VehicleHistory;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        /// <param name="idVehicleHistory">user id</param>
        /// <returns></returns>
        public static int Delete(int idVehicleHistory, Database pDb = null)
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

            command.Parameters.AddWithValue("@id", idVehicleHistory);
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
        private static void PrepareCommand(SqlCommand command, VehicleHistory VehicleHistory)
        {
            command.Parameters.AddWithValue("@id", VehicleHistory.Id);
            command.Parameters.AddWithValue("@evidence_id", VehicleHistory.EvidenceId);
            command.Parameters.AddWithValue("@start_date", VehicleHistory.StartDate);
            command.Parameters.AddWithValue("@end_date", VehicleHistory.EndDate);
            command.Parameters.AddWithValue("@car_license_plate", VehicleHistory.CarLicensePlate);
            command.Parameters.AddWithValue("@podtyp", VehicleHistory.Podtyp);


        }

        private static Collection<VehicleHistory> Read(SqlDataReader reader)
        {
            Collection<VehicleHistory> vehicleHistories = new Collection<VehicleHistory>();

            while (reader.Read())
            {
                int i = -1;
                VehicleHistory vehicleHistory = new VehicleHistory();
                vehicleHistory.Id = reader.GetInt32(++i);
                vehicleHistory.EvidenceId = reader.GetString(++i);
                vehicleHistory.StartDate = reader.GetDateTime(++i);
                vehicleHistory.StartDate = reader.GetDateTime(++i);
                vehicleHistory.CarLicensePlate = reader.GetString(++i);
                vehicleHistory.Podtyp = reader.GetString(++i);






                vehicleHistories.Add(vehicleHistory);
            }
            return vehicleHistories;
        }

        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        public static string CheckVehicleHistory(int idVehicleHistory, Database pDb)
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
            SqlCommand command = db.CreateCommand("CheckVehicleHistory");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idVehicleHistory";
            input.DbType = DbType.Int32;
            input.Value = idVehicleHistory;
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