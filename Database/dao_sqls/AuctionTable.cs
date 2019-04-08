using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class AuctionTable
    {
        public static String TABLE_NAME = "Auction";

        public static String SQL_SELECT = "SELECT a.idAuction,a.name,a.description,a.creation,a.\"end\",a.category,a.min_bid_value, a.max_bid_value, a.max_bid_user,c.name, u.\"login\"" +
                                          "FROM Auction a JOIN Category c ON a.category=c.idCategory LEFT JOIN \"User\" u ON u.idUser = a.max_bid_user WHERE a.owner=@owner;";
        public static String SQL_SELECT_ID = "SELECT a.*,c.name,u2.\"login\",u1.\"login\" FROM Auction a JOIN Category c ON a.category=c.idCategory JOIN \"User\" u1 ON u1.idUser=a.owner LEFT JOIN \"User\" u2 ON u2.idUser = a.max_bid_user WHERE a.idAuction=@idAuction;";
        public static String SQL_INSERT = "INSERT INTO Auction VALUES (@owner, @name, @description, @description_detail, " +
            "@creation, @end, @category, @min_bid_value, NULL, NULL)";
        public static String SQL_UPDATE = "UPDATE Auction SET name=@name, description=@description,description_detail=@description_detail,creation=@creation,\"end\"=@end,category=@category WHERE idAuction=@idAuction";
        public static String SQL_DELETE_ID = "DELETE FROM Auction WHERE idAuction=@idAuction";

        /// <summary>
        /// Insert
        /// </summary>
        public static int Insert(Auction auction)
        {
            return Insert(auction, null);
        }

        /// <summary>
        /// Insert
        /// </summary>
        public static int Insert(Auction auction, Database pDb)
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

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, auction);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;

        }

        /// <summary>
        /// Update
        /// </summary>
        public static int Update(Auction auction)
        {
            Database db;
            db = new Database();
            db.Connect();


            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, auction);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        /// <summary>
        /// Select records
        /// </summary>
        /// <param name="iduser">Owner of transactions</param>
        public static Collection<Auction> Select(int iduser)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            command.Parameters.AddWithValue("@owner", iduser);
            SqlDataReader reader = db.Select(command);

            Collection<Auction> auctions = Read(reader, false);
            reader.Close();
            db.Close();
            return auctions;

        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        public static int Delete(int idAuction)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@idAuction", idAuction);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;

        }

        /// <summary>
        /// Select the record.
        /// </summary>
        public static Auction SelectOne(int idAuction)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@idAuction", idAuction);
            SqlDataReader reader = db.Select(command);

            Collection<Auction> auctions = Read(reader, true);
            Auction auction = null;
            if (auctions.Count == 1)
            {
                auction = auctions[0];
            }
            reader.Close();
            db.Close();
            return auction;

        }

        /// <summary>
        /// Prepare a command.
        /// </summary>
        private static void PrepareCommand(SqlCommand command, Auction auction)
        {
            command.Parameters.AddWithValue("@idAuction", auction.IdAuction);
            command.Parameters.AddWithValue("@owner", auction.IdOwner);
            command.Parameters.AddWithValue("@name", auction.Name);
            command.Parameters.AddWithValue("@description", auction.Description);
            //handle nullable column. If property is null then store DBNull value otherwise cast property to object.
            command.Parameters.AddWithValue("@description_detail", auction.DescriptionDetail == null ? DBNull.Value : (object)auction.DescriptionDetail);
            command.Parameters.AddWithValue("@creation", auction.Creation);
            command.Parameters.AddWithValue("@end", auction.End);
            command.Parameters.AddWithValue("@category", auction.IdCategory);
            command.Parameters.AddWithValue("@min_bid_value", auction.MinBidValue.HasValue ? (object)auction.MinBidValue.Value : DBNull.Value);
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="complete">true - all attribute values must be read</param>
        private static Collection<Auction> Read(SqlDataReader reader, bool complete)
        {
            Collection<Auction> auctions = new Collection<Auction>();

            while (reader.Read())
            {
                Auction auction = new Auction();
                int i = -1;
                auction.IdAuction = reader.GetInt32(++i);
                if (complete)
                {
                    auction.IdOwner = reader.GetInt32(++i);
                }
                auction.Name = reader.GetString(++i);
                auction.Description = reader.GetString(++i);
                if (complete)
                {
                    if (!reader.IsDBNull(++i))
                    {
                        auction.DescriptionDetail = reader.GetString(i); // desc detail is not always read
                    }
                }
                auction.Creation = reader.GetDateTime(++i);
                auction.End = reader.GetDateTime(++i);
                auction.IdCategory = reader.GetInt32(++i);
                auction.Category = new Category();
                auction.Category.IdCategory = auction.IdCategory;

                // bad solution: 
                // auction.Category = new CategoryTable().Select(auction.IdCategory); // read the record with the 1:1 relationship
                if (!reader.IsDBNull(++i))
                {
                    auction.MinBidValue = reader.GetInt32(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    auction.MaxBidValue = reader.GetInt32(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    auction.IdMaxBidUser = reader.GetInt32(i);
                }

                auction.Category.Name = reader.GetString(++i);

                //info about max bidder (if any)
                if (!reader.IsDBNull(++i))
                {
                    auction.MaxBidder = new User();
                    auction.MaxBidder.Id = auction.IdMaxBidUser.Value;
                    auction.MaxBidder.Login = reader.GetString(i);

                }

                if (complete)
                {
                    auction.Owner = new User();
                    auction.Owner.Id = auction.IdOwner;
                    auction.Owner.Login = reader.GetString(++i);

                }

                auctions.Add(auction);
            }
            return auctions;
        }
    }
}