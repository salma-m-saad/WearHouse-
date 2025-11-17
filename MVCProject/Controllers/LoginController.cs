using Microsoft.AspNetCore.Mvc;
using MVCProject.Filters;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    
    public class LoginController : Controller
    {
        Context context = new Context();
        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        public IActionResult Login(Account account)
        {
            Account acc = context.Accounts.FirstOrDefault(a => a.Email == account.Email && a.Password == account.Password);
            if (acc != null)
            {
                if (acc.IsAdmin)
                    HttpContext.Session.SetString("IsAdmin","Admin");
                else
                    HttpContext.Session.SetString("IsAdmin", "NotAdmin");

                HttpContext.Session.SetString("Name", acc.Name);


                return RedirectToAction("Index", "HomePage");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(Account account,string ConfPass)
        {
            if (account.Name != null && account.Email != null && account.Password != null&& ConfPass==account.Password) 
            {

                account.IsAdmin = false;

                context.Accounts.Add(account);
                context.SaveChanges();
               
                HttpContext.Session.SetString("IsAdmin", "NotAdmin");
                HttpContext.Session.SetString("Name", account.Name);


                return RedirectToAction("Index", "HomePage");
                
            }
            
            return View();
        }


    }
}
