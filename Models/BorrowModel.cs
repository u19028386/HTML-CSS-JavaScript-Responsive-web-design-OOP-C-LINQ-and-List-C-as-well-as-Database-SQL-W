using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDUDB1INF272.Models
{
    public class BorrowModel
    {
        public int ID { get; set; }
        public DateTime TakenDate { get; set; }
        public DateTime BroughtDate { get; set; }
        public string BorrowedBy { get; set; }

        public BorrowModel(int id, DateTime takendate, DateTime broughtdate, string borrowedby)
        {
            ID = id;
            TakenDate = takendate;
            BroughtDate = broughtdate;
            BorrowedBy = borrowedby;

        }
        public BorrowModel()
        {

        }
    }
}