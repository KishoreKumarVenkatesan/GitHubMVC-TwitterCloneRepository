using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Entities;

namespace TwitterClone.DAL
{
    public class TwitterDAL
    {
        public bool SetTweetMessage(TweetModel model)
        {
            bool result = false;
            try
            {
                using (var Context = new PODbEntities())
                {
                    Tweet t = new Tweet { user_id = model.user_Id, message = model.message, CREATED = DateTime.Now };
                    Context.Tweets.Add(t);
                    Context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public List<TweetModel> GetAllTweetMessage(string userId)
        {
            PODbEntities db = new PODbEntities();
            Tweet t = new Tweet();
            List<TweetModel> lst = new List<TweetModel>();
            try
            {
                lst = (from e in db.Tweets
                        //join f in db.People on e.user_id equals f.followingPerson
                       where e.user_id == userId
                       select new TweetModel
                       {
                           user_Id = e.user_id,
                           message = e.message,
                           CreatedDate = e.CREATED
                       }).ToList();

            }
            catch (Exception ex)
            {

            }

            return lst;

        }

        public int GetFollowingCount(string userId)
        {
            int count = 0;
            PODbEntities db = new PODbEntities();
            using (var Context = new PODbEntities())
            {
                var e = (from d in db.People
                         where d.user_id == userId
                         select d).ToList();
                count = e[0].People.Count();
            }
            return count;
        }

        public int GetFollowerCount(string userId)
        {
            int count = 0;
            PODbEntities db = new PODbEntities();
            using (var Context = new PODbEntities())
            {
                var e = (from d in db.People
                         where d.user_id == userId
                         select d).ToList();
                count = e[0].followingPerson.Count();

            }
            return count;
        }

        public List<FollowingUser> GetFollowingUsers(string userId)
        {
            PODbEntities db = new PODbEntities();            
            List<FollowingUser> lst = new List<FollowingUser>();
            try
            {
                var e = (from d in db.People
                         where d.user_id == userId
                       select d).ToList();
                lst = (from g in e[0].People
                       select new FollowingUser
                       {
                           fullName = g.fullName,
                           email = g.email
                       }
                     ).ToList();
                
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public List<FollowingUser> GetFollowers(string userId)
        {
            PODbEntities db = new PODbEntities();
            //Person p = new Person();
            List<FollowingUser> lst = new List<FollowingUser>();
            try
            {
                var e = (from d in db.People
                         where d.user_id == userId
                         select d).ToList();
                lst = (from g in e[0].followingPerson
                       select new FollowingUser
                       {
                           fullName = g.fullName,
                           email = g.email
                       }
                     ).ToList();

            }
            catch (Exception ex)
            {

            }
            return lst;
        }
        public List<ManageProfile> GetSearchPersonDetails(string name)
        {
            PODbEntities db = new PODbEntities();
            List<ManageProfile> lst = new List<ManageProfile>();
            try
            {
                lst = (from e in db.People
                       where e.fullName.StartsWith(name)
                       select new ManageProfile
                       {
                           fullName = e.fullName,
                           email = e.email
                       }).ToList();

            }
            catch (Exception ex)
            {

            }

            return lst;

        }

        public bool AddFollowingUsers(string followingUserName,string user_Id)
        {
            bool result = false;
            try
            {
                PODbEntities db = new PODbEntities();
                string followingUserId = string.Empty;
                using (var Context = new PODbEntities())
                {
                    followingUserId = (from d in db.People
                                       where d.fullName == followingUserName
                                       select d.user_id).FirstOrDefault();

                   
                    string query= "insert into[dbo].[Following]([following_id],[user_id]) values("+"'"+ followingUserId +"', '"+ user_Id +"'"+")";


                    int noOfRowInserted = Context.Database.ExecuteSqlCommand(query);
                if(noOfRowInserted > 0)
                    {
                        result = true;
                    }
                else
                    {
                        result = false; 
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }


}
