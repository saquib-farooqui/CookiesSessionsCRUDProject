using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using WebApplication33.DatabaseCon;
using WebApplication33.Models;

namespace WebApplication33.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        DatabaseContext db = new DatabaseContext();
        BookViewModel vm = new BookViewModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Registration register)
        {
          
            if (ModelState.IsValid)
            {
                if (checkifuserexist(register) == false)
                {


                    db.Registration.Add(register);
                    int rec = db.SaveChanges();

                    if (rec > 0)
                    {
                        ViewBag.Success = "Registration Successfull";
                    }
                    else
                    {
                        ViewBag.Error = "Registration Failed";
                    }
                }
                else
                {
                    ViewBag.Error = "Username Exist";
                }
            }
            return View();
        }

        public bool checkifuserexist(Registration register)
        {
            var user = db.Registration.FirstOrDefault(r => r.Email == register.Email);

            if(user == null)
            {
                return false;
            }
            return true;



         }

        public ActionResult Login()
        {
            string savedEmail = Request.Cookies["UserEmail"].Value;

            // Pass the saved email to the view using ViewBag
            ViewBag.SavedEmail = savedEmail;

            // Return the Login view
            return View();

        }

        [HttpPost]
        public ActionResult Login(Registration login, bool RememberMe)
        {
            var valuser = db.Registration.FirstOrDefault(l => l.Email == login.Email && l.Password == login.Password);
      
            if (valuser != null)
            {
                if (RememberMe == true)
                {
                    HttpCookie cookie = new HttpCookie("UserEmail", login.Email);
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookie);
                }
                Session["User"] = login.Email;
                return RedirectToAction("BookView");
            }
    
            ViewBag.Error = "Invalid User or Password";
            return View();
        }

       public ActionResult BookView()
        {
            if (Session["User"] != null)
            {
              
                var ses = (string)Session["User"];
                vm.listofbooks = db.Book.Where(u => u.UserId == ses).ToList();
                return View(vm);
                
            }
            var sess = (string)Session["User"];
            vm.listofbooks = db.Book.Where(r => r.UserId == sess).ToList();
            return View(vm);
        }
        [HttpPost]
        public ActionResult BookView(BookViewModel vam)
        {
             if (Session["User"] == null)
                {
                    return RedirectToAction("Login");
                }
            if (ModelState.IsValid)
            {
                var ses = (string)Session["User"];

                vam.addBook.UserId = ses;
                db.Book.Add(vam.addBook);
                int rec = db.SaveChanges();

                if (rec > 0)
                {
                    ViewBag.Message = "Success";
                }
                else
                {
                    ViewBag.Message = "error";
                }
                // Return the updated view with the list of books
                return RedirectToAction("BookView");
            }
            var sess = (string)Session["User"];
            vm.listofbooks = db.Book.Where(r => r.UserId == sess).ToList();
            return View(vm);

           
        }

        public ActionResult Edit(int id) {

            var record = db.Book.FirstOrDefault(r => r.Id == id);
            if(record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        
        
        
        }
        [HttpPost]
        public ActionResult Edit(Books edit)
        {
            var editrecord = db.Book.FirstOrDefault(r => r.Id == edit.Id);
           
            if(editrecord != null)
            {
                editrecord.BookName = edit.BookName;
                editrecord.Author = edit.Author;
                editrecord.PublishingYear = edit.PublishingYear;
                db.SaveChanges();
                return RedirectToAction("BookView");
            }
            return HttpNotFound();
        }

        public ActionResult Delete(int id)
        {
            var record = db.Book.FirstOrDefault(r => r.Id == id);
            if(record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        [HttpPost]
        public ActionResult Delete(Books delete)
        {
            var delrecord = db.Book.FirstOrDefault(r => r.Id == delete.Id);
            if(delrecord == null)
            {
                return HttpNotFound();
            }
            db.Book.Remove(delrecord);
            db.SaveChanges();
            return RedirectToAction("BookView");
        }
    }
}