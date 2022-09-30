using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CDUDB1INF272.Models;

namespace CDUDB1INF272.Models
{
    public class DataService
    {
        private SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
        private SqlConnection currConnection;

        private static DataService instance;
        public static DataService getDataService()
        {
            if (instance == null)
            {
                instance = new DataService();
            }
            return instance;
        }


        public string getConnectionString()
        {
            stringBuilder["Data Source"] = "LAPTOP-KED1PJ97\\SQLEXPRESS";
            stringBuilder["Integrated Security"] = "true";
            stringBuilder["Initial Catalog"] = "Library";

            return stringBuilder.ToString();

        }

        public bool openConnection()
        {
            bool status = true;
            try
            {
                String conString = getConnectionString();
                currConnection = new SqlConnection(conString);
                currConnection.Open();
            }
            catch (Exception exc)
            {

                status = false;
            }
            return status;
        }

        public bool closeConnection()
        {
            if (currConnection != null)
            {
                currConnection.Close();
            }
            return true;
        }





        public List<DestinationModel> getDest()
        {
            List<DestinationModel> destinations = new List<DestinationModel>();
            try
            {
                openConnection();
                SqlCommand command = new SqlCommand("select books.pagecount,books.point, books.name,books.bookId,authors.name as author, types.name as tname from books inner join authors on books.authorID = authors.authorID inner join types on books.typeId = types.typeID", currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DestinationModel tmpDest = new DestinationModel();
                        tmpDest.Name = reader["name"].ToString();
                        tmpDest.Author = reader["author"].ToString();
                        tmpDest.ID = Convert.ToInt32(reader["bookid"]);
                        tmpDest.PageCount = Convert.ToInt32(reader["pagecount"]);
                        tmpDest.Points = Convert.ToInt32(reader["point"]);
                        //tmpDest.Available = Convert.ToBoolean(reader["Available"]);
                        tmpDest.Type = reader["tname"].ToString();

                        destinations.Add(tmpDest);
                    }
                }
                closeConnection();
            }
            catch
            {

            }
            return destinations;
        }

        public List<BorrowModel> getBorrow()
        {
            List<BorrowModel> borrows = new List<BorrowModel>();
            try
            {
                openConnection();
                SqlCommand command = new SqlCommand("select borrowId,takenDate, broughtDate, students.name as sname from borrows inner join students on borrows.studentId = students.studentId", currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BorrowModel tmpBorrows = new BorrowModel();
                        tmpBorrows.ID = Convert.ToInt32(reader["borrowId"]);
                        tmpBorrows.BroughtDate = Convert.ToDateTime(reader["broughtDate"]);
                        tmpBorrows.TakenDate = Convert.ToDateTime(reader["takenDate"]);
                        tmpBorrows.BorrowedBy = reader["sname"].ToString();
                        borrows.Add(tmpBorrows);
                    }
                }
                closeConnection();
            }
            catch
            {

            }
            return borrows;
        }

        public bool getB(int id)
        {
            List<BorrowModel> borrows = new List<BorrowModel>();
            bool status = false;
            try
            {
                openConnection();
                SqlCommand command = new SqlCommand("select borrowId,takenDate, broughtDate, students.name as sname from borrows inner join students on borrows.studentId = students.studentId where borrowId = " + id + ";", currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BorrowModel tmpBorrows = new BorrowModel();
                        tmpBorrows.ID = Convert.ToInt32(reader["borrowId"]);
                        tmpBorrows.BroughtDate = Convert.ToDateTime(reader["broughtDate"]);
                        tmpBorrows.TakenDate = Convert.ToDateTime(reader["takenDate"]);
                        tmpBorrows.BorrowedBy = reader["sname"].ToString();
                        borrows.Add(tmpBorrows);
                    }
                }
                closeConnection();
                status = true;
            }
            catch (Exception e)



            {
                status = false;
            }
            finally
            {
                closeConnection();
            }
            return status;
        }

        public DestinationModel getDestById(int id)
        {
            DestinationModel dest = null;
            List<DestinationModel> dests = getDest();

            if (dests.Any(d => d.ID == id))
            {
                int index = dests.FindIndex(d => d.ID == id);
                dest = dests[index];
            }

            return dest;
        }

        public BorrowModel getBById(int id)
        {
            BorrowModel b = null;
            List<BorrowModel> bb = getBorrow();

            if (bb.Any(d => d.ID == id))
            {
                int index = bb.FindIndex(d => d.ID == id);
                b = bb[index];
            }

            return b;
        }




        // new
        //public bool deleteDest(int id)
        //{
        //    bool status = false;
        //    try
        //    {
        //        openConnection();
        //        SqlCommand command = new SqlCommand("delete from TouristSites where id = " + id + ";", currConnection);
        //        command.ExecuteNonQuery();
        //        closeConnection();
        //        status = true;
        //    }
        //    catch (Exception e)
        //    {
        //        status = false;
        //    }
        //    finally
        //    {
        //        closeConnection();
        //    }
        //    return status;
        //}

        //public bool updateDest(DestinationModel someDest)
        //{
        //    bool status = false;
        //    try
        //    {
        //        openConnection();
        //        String cmd = "update TouristSites set Name = '" + someDest.Name + "', Website = '" + someDest.Website + "' where id = " + someDest.ID + ";";
        //        SqlCommand command = new SqlCommand(cmd, currConnection);
        //        command.ExecuteNonQuery();
        //        closeConnection();
        //        status = true;

        //    }
        //    catch (Exception e)
        //    {
        //        status = false;
        //    }
        //    finally
        //    {
        //        closeConnection();
        //    }
        //    return status;
        //}

        //public bool createDest(DestinationModel someDest)
        //{
        //    bool status = false;

        //    try
        //    {
        //        openConnection();
        //        String cmd = "INSERT INTO TouristSites(Name, Website) VALUES('" + someDest.Name + "', '" + someDest.Website + "');";
        //        SqlCommand command = new SqlCommand(cmd, currConnection);
        //        command.ExecuteNonQuery();
        //        closeConnection();
        //        status = true;
        //    }
        //    catch (Exception err)
        //    {
        //        status = false;
        //    }
        //    finally
        //    {
        //        closeConnection();
        //    }
        //    return status;
        //}

        //public List<DestinationModel> getDest()
        //{
        //    List<DestinationModel> destinations = new List<DestinationModel>();
        //    try
        //    {
        //        openConnection();
        //        SqlCommand command = new SqlCommand("select * from TouristSites", currConnection);
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                DestinationModel tmpDest = new DestinationModel();
        //                tmpDest.Name = reader["Name"].ToString();
        //                tmpDest.Website = reader["Website"].ToString();
        //                tmpDest.ID = Convert.ToInt32(reader["id"]);

        //                destinations.Add(tmpDest);
        //            }
        //        }
        //        closeConnection();
        //    }
        //    catch
        //    {

        //    }
        //    return destinations;
        //}






    }

}
