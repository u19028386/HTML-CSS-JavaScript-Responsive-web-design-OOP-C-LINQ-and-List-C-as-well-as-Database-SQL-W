using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDUDB1INF272.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CClass { get; set; }
        public int Points { get; set; }

        public Student(int bookid, string name, string surname, string cclass, int point)
        {
            ID = bookid;
            Name = name;
            Surname = surname;
            CClass = cclass;
            Points = point;
            
        }

        public Student()
        {

        }
    }
}