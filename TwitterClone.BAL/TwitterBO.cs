using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DAL;
using TwitterClone.Entities;

namespace TwitterClone.BAL
{
    public class TwitterBO
    {
        public bool SetTweetMessage(TweetModel model)
        {
            TwitterDAL t = new TwitterDAL();
            bool result = false;
            if (model != null)
            {
                result = t.SetTweetMessage(model);
            }
            return result;
        }

        public List<TweetModel> GetAllTweetMessage(string userId)
        {
            TwitterDAL t = new TwitterDAL();
            List<TweetModel> lsttweet = new List<TweetModel>();
            if (!string.IsNullOrEmpty(userId))
            {
                lsttweet = t.GetAllTweetMessage(userId);
            }
            return lsttweet;
        }
        public int GetFollowingCount(string userId)
        {
            int count = 0;
            TwitterDAL t = new TwitterDAL();
            if (!string.IsNullOrEmpty(userId))
            {
                count = t.GetFollowingCount(userId);
            }
            return count;
        }
        public int GetFollowerCount(string userId)
        {
            int count = 0;
            TwitterDAL t = new TwitterDAL();
            if (!string.IsNullOrEmpty(userId))
            {
                count = t.GetFollowerCount(userId);
            }
            return count;
        }
        public List<FollowingUser> GetFollowingUsers(string userId)
        {
            TwitterDAL t = new TwitterDAL();
            List<FollowingUser> lst = new List<FollowingUser>();
            if (!string.IsNullOrEmpty(userId))
            {
                lst = t.GetFollowingUsers(userId);
            }
            return lst;
       }

        public List<FollowingUser> GetFollowers(string userId)
        {
            TwitterDAL t = new TwitterDAL();
            List<FollowingUser> lst = new List<FollowingUser>();
            if (!string.IsNullOrEmpty(userId))
            {
                lst = t.GetFollowers(userId);
            }
            return lst;
        }
    
        public List<ManageProfile> GetSearchPersonDetails(string name)
        {
            TwitterDAL t = new TwitterDAL();
            List<ManageProfile> lstPerson = new List<ManageProfile>();
            if (!string.IsNullOrEmpty(name))
            {
                lstPerson = t.GetSearchPersonDetails(name);
            }
            return lstPerson;
        }

        public bool AddFollowingUsers(string followingUserName, string user_Id)
        {
            TwitterDAL t = new TwitterDAL();
            bool result = false;
            if (!string.IsNullOrEmpty(followingUserName) && !string.IsNullOrEmpty(user_Id))
            {
                result = t.AddFollowingUsers(followingUserName, user_Id);
            }
            return false;
        }
    }
}
