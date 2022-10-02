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

        public string Available { get; set; }

        public BorrowModel SStatus { get; set; }
        public BorrowModel RDate { get; set; }

        public DateTime mDate { get; set; }




        public DestinationModel(int bookid, string name, string author, string type, int pageCount, int point, string available, DateTime sdateTime)
        {
            ID = bookid;
            Name = name;
            Author = author;
            Type = type;
            PageCount = pageCount;
            Points = point;
            Available = available;
            mDate = sdateTime;
            SStatus = new BorrowModel();
            RDate = new BorrowModel();
        }

        public DestinationModel()
        {

        }
    }
}