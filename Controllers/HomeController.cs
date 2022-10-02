using CDUDB1INF272.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CDUDB1INF272.Controllers
{
    public class HomeController : Controller
    {
        private SqlConnection myConnection = new SqlConnection(Globals.ConnectionString);

        private DataService dataService = DataService.getDataService();
        public ActionResult BookInfo()
        {
            try
            {
                SqlCommand myComplexSearch;
                myComplexSearch = new SqlCommand("select borrowId,takenDate, broughtDate, students.name as sname from borrows inner join students on borrows.studentId = students.studentId", myConnection);

                myConnection.Open();


                SqlDataReader myReader = myComplexSearch.ExecuteReader();
                Globals.borrowList.Clear();
                while (myReader.Read())
                {
                   
                    BorrowModel tmpBorrows = new BorrowModel();
                    tmpBorrows.ID = Convert.ToInt32(myReader["borrowId"]);
                    tmpBorrows.BroughtDate = Convert.ToDateTime(myReader["broughtDate"]);
                    tmpBorrows.TakenDate = Convert.ToDateTime(myReader["takenDate"]);
                    tmpBorrows.BorrowedBy = myReader["sname"].ToString();
                    


                    Globals.borrowList.Add(tmpBorrows);
                }

            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }
            return View(Globals.borrowList);
        }

        public ActionResult ViewStudent()
        {
            //dropdown begin
            SqlCommand myComplexSearch2;
            myComplexSearch2 = new SqlCommand("select * from students", myConnection);

            myConnection.Open();
            SqlDataAdapter datasetA2 = new SqlDataAdapter(myComplexSearch2);
            DataSet datas2 = new DataSet();
            datasetA2.Fill(datas2);
            ViewBag.classname = datas2.Tables[0];
            List<SelectListItem> getClassname = new List<SelectListItem>();

            foreach (System.Data.DataRow dr1 in ViewBag.classname.Rows)
            {
                getClassname.Add(new SelectListItem { Text = dr1["class"].ToString(), Value = dr1["class"].ToString() });
            }

            ViewBag.Classes = getClassname;
            myConnection.Close();
            //end


            try
            {
                SqlCommand myComplexSearch;
                myComplexSearch = new SqlCommand("select studentId,name, surname,class,point  from students" , myConnection);

                myConnection.Open();
                

                SqlDataReader myReader = myComplexSearch.ExecuteReader();
                Globals.studentList.Clear();
                while (myReader.Read())
                {
                    Student stud = new Student();

                    stud.Name = myReader["name"].ToString();
                    stud.Surname = myReader["surname"].ToString();
                    stud.ID = Convert.ToInt32(myReader["studentId"]);
                    stud.CClass = myReader["class"].ToString();
                    stud.Points = Convert.ToInt32(myReader["point"]);
                    


                    Globals.studentList.Add(stud);
                }

                

            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }
            return View( Globals.studentList);
        }
        public ActionResult ComplexSearch(string bookname)
        {
            try
            {
                SqlCommand myComplexSearch;
                myComplexSearch = new SqlCommand("select books.pagecount,books.point, books.name,books.bookId,authors.name as author, types.name as tname from books inner join authors on books.authorID = authors.authorID inner join types on books.typeId = types.typeID inner join borrows on books.bookId= borrows.bookId WHERE books.name =" + bookname , myConnection);

                myConnection.Open();
              

                SqlDataReader myReader = myComplexSearch.ExecuteReader();
                Globals.complexList.Clear();
                while (myReader.Read())
                {
                    DestinationModel book = new DestinationModel();
                    
                    book.Name = myReader["name"].ToString();
                    book.Author = myReader["author"].ToString();
                    book.ID = Convert.ToInt32(myReader["bookid"]);
                    book.PageCount = Convert.ToInt32(myReader["pagecount"]);
                    book.Points = Convert.ToInt32(myReader["point"]);
                    book.Available = myReader["Available"].ToString();
                    book.Type = myReader["tname"].ToString();
                    book.mDate = Convert.ToDateTime(myReader["borrows.broughtDate"]);
                    if (book.mDate == null)
                    {
                        book.Available = "Available";
                    }
                    else book.Available = "Out";


                    Globals.complexList.Add(book);
                }
                ViewBag.SearchStatus = 2;
                ViewBag.SearchText = "Advanced search results";
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }
            return View(Globals.complexList);
        }

        public ActionResult ComplexS(string studentname)
        {
            try
            {
                SqlCommand myComplexSearch;
                myComplexSearch = new SqlCommand("select studentId,name, surname,class,point from students WHERE name =" + studentname, myConnection);

                myConnection.Open();


                SqlDataReader myReader = myComplexSearch.ExecuteReader();
                Globals.studentList.Clear();
                while (myReader.Read())
                {
                    Student stud = new Student();

                    stud.Name = myReader["name"].ToString();
                    stud.Surname = myReader["surname"].ToString();
                    stud.ID = Convert.ToInt32(myReader["studentId"]);
                    stud.CClass = myReader["class"].ToString();
                    stud.Points = Convert.ToInt32(myReader["point"]);



                    Globals.studentList.Add(stud);
                }
            
              
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }
            return View(Globals.studentList);
        }
        public ActionResult Index()            
        {
            SqlCommand myComplexSearch;
            myComplexSearch = new SqlCommand("select * from authors", myConnection);

            myConnection.Open();
            SqlDataAdapter datasetA = new SqlDataAdapter(myComplexSearch);
            DataSet datas = new DataSet();
            datasetA.Fill(datas);
            ViewBag.authorname = datas.Tables[0];
            List<SelectListItem> getAuthorname = new List<SelectListItem>();

            foreach (System.Data.DataRow dr in ViewBag.authorname.Rows)
            {
                getAuthorname.Add(new SelectListItem { Text = dr["name"].ToString(), Value = dr["name"].ToString() });
            }

            ViewBag.Authors = getAuthorname;
            myConnection.Close();

            //BEGIN
            SqlCommand myComplexSearch1;
            myComplexSearch1 = new SqlCommand("select * from types", myConnection);

            myConnection.Open();
            SqlDataAdapter datasetA1 = new SqlDataAdapter(myComplexSearch1);
            DataSet datas1 = new DataSet();
            datasetA1.Fill(datas1);
            ViewBag.typename = datas1.Tables[0];
            List<SelectListItem> getTypename = new List<SelectListItem>();

            foreach (System.Data.DataRow dr1 in ViewBag.typename.Rows)
            {
                getTypename.Add(new SelectListItem { Text = dr1["name"].ToString(), Value = dr1["name"].ToString() });
            }

            ViewBag.Types = getTypename;
            myConnection.Close();

            //END 

            List<DestinationModel> databaseDest = dataService.getDest();
            if (databaseDest.Count == 0)
            {
                ViewBag.Message = "Database not found";
            }
                 return View(databaseDest);
        }

        

       
      public ActionResult BookIndex()
       {
            List<BorrowModel> databaseBorrow = dataService.getBorrow();
            if (databaseBorrow.Count == 0)
            {
                ViewBag.Message = "Database not found";
            }
            return View(databaseBorrow);
        }

        [HttpGet]
        public ActionResult Me(int id)
        {
            BorrowModel foundborrow = dataService.getBById(id);
            return View(foundborrow);
        }

        [HttpPost]
        public ActionResult Me(BorrowModel someBorrow)
        {
            
            return RedirectToAction("BookIndex");
        }

        
            

        public ActionResult Dropdown1()
        {
            SqlCommand myComplexSearch;
            myComplexSearch = new SqlCommand("select * from authors", myConnection);

            myConnection.Open();
            SqlDataAdapter datasetA = new SqlDataAdapter(myComplexSearch);
            DataSet datas = new DataSet();
            datasetA.Fill(datas);
            ViewBag.authorname = datas.Tables[0];
            List<SelectListItem> getAuthorname = new List<SelectListItem>();

            foreach(System.Data.DataRow dr in ViewBag.authorname.Rows)
            {
                getAuthorname.Add(new SelectListItem { Text = dr["name"].ToString(), Value = dr["name"].ToString() });
            }

            ViewBag.Authors = getAuthorname;
            myConnection.Close();
            return View();

        }


        





    }


}



