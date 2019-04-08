using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class PhotoTable
    {
        public static String SQL_SELECT = "SELECT * FROM Photo";
        public static String SQL_SELECT_ID = "SELECT * FROM Photo WHERE idPhoto=@id";
        public static String SQL_INSERT = "INSERT INTO Photo VALUES (@name, @date, @coord_n, @coord_e, @path)";
        public static String SQL_DELETE_ID = "DELETE FROM Photo WHERE idPhoto=@id";
        public static String SQL_UPDATE = "UPDATE Photo SET name=@name, date = @date, coord_n=@coord_n, coord_e=@coord_e, path=@path WHERE idUser=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Photo photo, Database pDb = null)
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
            PrepareCommand(command, photo);
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
        public static int Update(Photo photo, Database pDb = null)
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
            PrepareCommand(command, photo);
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
        public static Collection<Photo> Select(Database pDb = null)
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

            Collection<Photo> photos = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return photos;
        }

        /// <summary>
        /// Select the record.
        /// </summary>
        /// <param name="id">user id</param>
        public static Photo Select(int id, Database pDb = null)
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

            Collection<Photo> Photos = Read(reader);
            Photo Photo = null;
            if (Photos.Count == 1)
            {
                Photo = Photos[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Photo;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        /// <param name="idPhoto">user id</param>
        /// <returns></returns>
        public static int Delete(int idPhoto, Database pDb = null)
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

            command.Parameters.AddWithValue("@id", idPhoto);
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
        private static void PrepareCommand(SqlCommand command, Photo Photo)
        {
            command.Parameters.AddWithValue("@id", Photo.Id);
            command.Parameters.AddWithValue("@date", Photo.Date);
            command.Parameters.AddWithValue("@coord_n", Photo.CoordN);
            command.Parameters.AddWithValue("@coord_e", Photo.CoordE);
            command.Parameters.AddWithValue("@path", Photo.Path);



        }

        private static Collection<Photo> Read(SqlDataReader reader)
        {
            Collection<Photo> photos = new Collection<Photo>();

            while (reader.Read())
            {
                int i = -1;
                Photo photo = new Photo();
                photo.Id = reader.GetInt32(++i);
                photo.Date = reader.GetDateTime(++i);
                photo.CoordN = reader.GetFloat(++i);
                photo.CoordE = reader.GetFloat(++i);
                photo.Path = reader.GetString(++i);




                photos.Add(photo);
            }
            return photos;
        }

        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        public static string CheckPhoto(int idPhoto, Database pDb)
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
            SqlCommand command = db.CreateCommand("CheckPhoto");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idPhoto";
            input.DbType = DbType.Int32;
            input.Value = idPhoto;
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