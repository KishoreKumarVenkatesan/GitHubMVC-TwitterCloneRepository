using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TwitterClone.BAL;
using TwitterClone.Entities;

namespace TwitterClone.UI.Controllers
{
    public class TweetHomeController : Controller
    {
        // GET: TweetHome
        public ActionResult Index()
        {
            if (Session["UserFullName"] != null)
            {
                ViewBag.UserFullName = Session["UserFullName"];
            }
            TwitterBO t = new TwitterBO();
            List<TweetModel> lstTweet = new List<TweetModel>();
            lstTweet = t.GetAllTweetMessage(ViewBag.UserFullName);
            ViewBag.TweetComments = lstTweet;
            int followingCount = t.GetFollowingCount(ViewBag.UserFullName);
            int followerCount = t.GetFollowerCount(ViewBag.UserFullName);
            ViewBag.TweetCount = lstTweet.Count();
            ViewBag.FollowingCount = followingCount;
            ViewBag.FollowerCount = followerCount;
            if (Session["SearchPerson"] != null)
            {
                ViewBag.PersonList = Session["SearchPerson"];
            }
            if(Session["FollowingUsers"]!=null)
            {
                ViewBag.FollowingUsers = Session["FollowingUsers"];
            }
            Session["SearchPerson"] = null;
            Session["FollowingUsers"] = null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetTweetMessage(TweetModel model)
        {
            bool isSuccess = false;
            try
            {
                if (this.ModelState.IsValid)
                {
                    TwitterBO accBAL = new TwitterBO();
                    if (Session["UserFullName"] != null)
                    {
                        model.user_Id = Session["UserFullName"].ToString();
                    }
                    isSuccess = accBAL.SetTweetMessage(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Tweet message is not updated successfully");
            }
            return RedirectToAction("Index", "TweetHome");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchPerson(string name)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    TwitterBO accBAL = new TwitterBO();

                    List<ManageProfile> lstPerson = new List<ManageProfile>();
                    lstPerson = accBAL.GetSearchPersonDetails(name);
                    Session["SearchPerson"] = lstPerson;

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Person name is not found");
            }
            return RedirectToAction("Index", "TweetHome");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFollowers(string followerName)
        {
            bool result = false;
            string user_Id = string.Empty;
            try
            {
                if (this.ModelState.IsValid)
                {
                    TwitterBO accBAL = new TwitterBO();
                    if (Session["UserFullName"] != null)
                    {
                        user_Id = Session["UserFullName"].ToString();
                    }
                    result = accBAL.AddFollowingUsers(followerName, user_Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Profile is not updated successfully");
            }
            return RedirectToAction("Index", "TweetHome");
        }

        public ActionResult GetFollowingUsers()
        {

            string user_Id = string.Empty;
            List<FollowingUser> lstFollowingUsers = new List<FollowingUser>();
            try
            {
                if (this.ModelState.IsValid)
                {
                    TwitterBO accBAL = new TwitterBO();
                    if (Session["UserFullName"] != null)
                    {
                        user_Id = Session["UserFullName"].ToString();
                    }
                    lstFollowingUsers = accBAL.GetFollowingUsers(user_Id);
                    Session["FollowingUsers"] = lstFollowingUsers;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Error in getting following user details please contact admin");
            }
            return RedirectToAction("Index", "TweetHome");
        }

        public ActionResult GetFollowers()
        {

            string user_Id = string.Empty;
            List<FollowingUser> lstFollowingUsers = new List<FollowingUser>();
            try
            {
                if (this.ModelState.IsValid)
                {
                    TwitterBO accBAL = new TwitterBO();
                    if (Session["UserFullName"] != null)
                    {
                        user_Id = Session["UserFullName"].ToString();
                    }
                    lstFollowingUsers = accBAL.GetFollowers(user_Id);
                    Session["FollowingUsers"] = lstFollowingUsers;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Error in getting follower details please contact admin");
            }
            return RedirectToAction("Index", "TweetHome");
        }

      
    }
}