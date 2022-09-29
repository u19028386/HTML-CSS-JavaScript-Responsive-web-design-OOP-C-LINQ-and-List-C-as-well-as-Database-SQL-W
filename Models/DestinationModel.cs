using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDUDB1INF272.Models
{
    public class DestinationModel
    {
        public int ID { get; set; }
        public string Name { get; set;}
        public string Author { get; set; }
        public string Type { get; set; }

        public int PageCount { get; set; }

        public int Points { get; set; }

        public bool Available { get; set; }



        public DestinationModel(int bookid, string name, string author, string type, int pageCount, int points, bool available)
        {
            ID = bookid;
            Name = name;
            Author = author;
            Type = type;
            PageCount = pageCount;
            Points = points;
            Available = available;
        }

        public DestinationModel()
        {

        }
    }
}