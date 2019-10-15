using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TwitterClone.BAL;
using TwitterClone.Entities;


namespace TwitterClone.UI.Controllers
{
    public class AccountController : Controller
    {
        
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            //this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            bool isSuccess = false;
            try
            {
                AccountBO accBAL = new AccountBO();

                if (this.ModelState.IsValid)
                {
                    model.password = MD5Hash(model.password);
                    isSuccess = accBAL.VerifyLoginUser(model);
                }
                if(isSuccess)
                {
                   Session["UserFullName"] = model.userId;
                }
                else
                {
                   ViewBag.Error= "Username or Password is invalid";
                }

            }
            catch (Exception ex)
            {
                
            }
            return (isSuccess == true) ? RedirectToAction("Index", "TweetHome") : RedirectToAction("Login", "Account");
        }
   
        public ActionResult SignOut()
        {
            Session["UserFullName"] = null;
           return RedirectToAction("Login");
        }
        [AllowAnonymous]

        public ActionResult SignUp()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignupModel model)
        {


            bool isSuccess = false;
            try
            {
                AccountBO accBAL = new AccountBO();
                
                if (this.ModelState.IsValid)
                {
                    model.password = MD5Hash(model.password);
                    model.active = true;
                    isSuccess = accBAL.SignUp(model);
                }
                
                
                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("error", "Signup is not done pls validate your details");
            }
            return (isSuccess == true) ? RedirectToAction("Index", "TweetHome") : RedirectToAction("Index","Home");
        }
      
        public ActionResult EditProfile()
        {
           // AccountBO acc = new AccountBO();

           // string userID = string.Empty;
           // ManageProfile m = new ManageProfile();
           // if (Session["UserFullName"] != null)
           // {
           //     userID = Session["UserFullName"].ToString();
           //     List<ManageProfile> lst = acc.GetUserInfo(userID);
               
           //     m = lst.FirstOrDefault();
           //}
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ManageProfile model)
        {
           
                AccountBO accBAL = new AccountBO();
                bool isSuccess = false;
                string userID = string.Empty;
                if (Session["UserFullName"] != null)
                {
                    userID = Session["UserFullName"].ToString();
                }
            try
            {
                if (this.ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(model.password))
                    {
                        model.password = MD5Hash(model.password);
                    }
                    isSuccess = accBAL.UpdateDeleteUserInfo(model, userID);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("error", "Profile is not updated successfully");
            }
            return (isSuccess == true) ? RedirectToAction("Index", "TweetHome") : RedirectToAction("EditProfile", "Account");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public static string MD5Hash(string text)


        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

       
    }
}