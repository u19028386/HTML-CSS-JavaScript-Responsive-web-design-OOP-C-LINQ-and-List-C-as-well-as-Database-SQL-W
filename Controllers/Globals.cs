using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CDUDB1INF272.Models;

namespace CDUDB1INF272.Controllers
{
    
    
       
        public static class Globals
        {
            public static string ConnectionString = "Data Source=LAPTOP-KED1PJ97\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";
            public static List<BorrowModel> borrowList = new List<BorrowModel>();
            public static List<DestinationModel> bookList = new List<DestinationModel>();
        public static List<DestinationModel> complexList = new List<DestinationModel>();
        public static List<Student> studentList = new List<Student>();
    }
    
}