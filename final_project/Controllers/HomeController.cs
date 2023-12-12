using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final_project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Login Page.";

            return View();
        }

        public ActionResult AddUserToDatabase(FormCollection fc)
        {
            String firstName = fc["firstname"];
            String lastName = fc["lastname"];
            String email = fc["email"];
            String diko = fc["password"];

            user use = new user();
            use.firstname = firstName;
            use.lastname = lastName;
            use.email = email;
            use.password = diko;
            use.roleID = 1;

            friendsEntities1 fe = new friendsEntities1();
            fe.users.Add(use);
            fe.SaveChanges();

            //insert the code that will save these information to the DB

            return RedirectToAction("Login");
        }

        public ActionResult UserUpdate()
        {
            friendsEntities1 rdbe = new friendsEntities1();
            user u = (from a in rdbe.users
                      where a.userId == 1
                      select a).FirstOrDefault();
            u.firstname = "Carmel";
            u.lastname = "Sombilon";
            u.email = "mae@gmail.com";
            u.password = "guapa kaayo";
            u.roleID = 1;
            rdbe.SaveChanges();

            return View();
        }

        public ActionResult UserDelete()
        {
            friendsEntities1 rdbe = new friendsEntities1();
            user u = (from a in rdbe.users
                      where a.userId == 1
                      select a).FirstOrDefault();
            rdbe.users.Remove(u);
            rdbe.SaveChanges();

            return View();
        }

        public ActionResult ShowUser()
        {
            friendsEntities1 fe = new friendsEntities1();
            var userList = (from a in fe.users
                            select a).ToList();

            ViewData["UserList"] = userList;
            return View();
        }
        public ActionResult UserUpdate(int id)
        {
            friendsEntities1 rdbe = new friendsEntities1();
            user u = rdbe.users.Find(id);

            if (u == null)
            {
                return HttpNotFound();
            }

            // You can pass the user object to the view for editing
            return View(u);
        }

      

        [HttpPost]

        public ActionResult Delete(int id)
        {
            friendsEntities1 rdbe = new friendsEntities1();
            user u = (from a in rdbe.users
                      where a.userId == id
                      select a).FirstOrDefault();
            rdbe.users.Remove(u);
            rdbe.SaveChanges();

            return RedirectToAction("ShowUser");
        }
        [HttpPost]
        public ActionResult Update(int id)
        {
            int x = id;


            friendsEntities1 user = new friendsEntities1();

            var selectedUser = (from a in user.users where a.userId == x select a).ToList();


            ViewData["User"] = selectedUser;

            return View();
            //  return RedirectToAction("UserUpdate");  // Redirect to the appropriate action or view
        }

        public ActionResult UpdateUser(FormCollection fc, int id)
        {
            friendsEntities1 rdbe = new friendsEntities1();
            user u = (from a in rdbe.users
                      where a.userId == id
                      select a).FirstOrDefault();

            String new_first_name = fc["new_firstname"];
            String new_last_name = fc["new_lastname"];
            String new_email = fc["new_email"];
            String new_password = fc["new_password"];

            u.firstname = new_first_name;
            u.lastname = new_last_name;
            u.email = new_email;
            u.password = new_password;
          

            rdbe.SaveChanges();

            return RedirectToAction("ShowUser");
        }

    }
}