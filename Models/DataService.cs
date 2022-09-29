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
                SqlCommand command = new SqlCommand("select * from books", currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DestinationModel tmpDest = new DestinationModel();
                        tmpDest.Name = reader["name"].ToString();
                        //tmpDest.Author = reader["author"].ToString();
                        //tmpDest.ID = Convert.ToInt32(reader["id"]);
                        //tmpDest.PageCount = Convert.ToInt32(reader["Pagecount"]);
                        //tmpDest.Points = Convert.ToInt32(reader["Points"]);
                        //tmpDest.Available = Convert.ToBoolean(reader["Available"]);

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

    }







    }
