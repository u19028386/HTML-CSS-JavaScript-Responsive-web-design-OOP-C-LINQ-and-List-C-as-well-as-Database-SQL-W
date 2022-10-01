using CDUDB1INF272.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

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
        public ActionResult ComplexSearch(string id)
        {
            try
            {
                SqlCommand myComplexSearch;
                myComplexSearch = new SqlCommand("select books.pagecount,books.point, books.name,books.bookId,authors.name as author, types.name as tname from books inner join authors on books.authorID = authors.authorID inner join types on books.typeId = types.typeID WHERE authors.name =" + id , myConnection);

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
                    //tmpDest.Available = Convert.ToBoolean(reader["Available"]);
                    book.Type = myReader["tname"].ToString();

                   
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
            return View("Index",Globals.complexList);
        }
        public ActionResult Index()            
        {
               
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

        [HttpGet]
            public ActionResult MView(int id)
            {
            BorrowModel foundB = dataService.getBById(id);
                return View(foundB);
            }

            [HttpPost]
            public ActionResult MView(BorrowModel someB)
            {
                dataService.BooksInfo(someB);
                return View();
            }


        // new 
        //    [HttpGet]
        //    public ActionResult Update(int id)
        //    {
        //        DestinationModel foundDest = dataService.getDestById(id);
        //        return View(foundDest);
        //    }

        //    [HttpPost]
        //    public ActionResult Update(DestinationModel someDest)
        //    {
        //        dataService.updateDest(someDest);
        //        return RedirectToAction("Index");
        //    }

        //    [HttpGet]
        //    public ActionResult Add()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    public ActionResult Add(DestinationModel someDest)
        //    {
        //        ViewBag.Message = dataService.createDest(someDest);
        //        return RedirectToAction("Index");


        //    }

        //    public ActionResult Delete(int id)
        //    {
        //        dataService.deleteDest(id);
        //        return RedirectToAction("Index");
        //    }





    }


}



