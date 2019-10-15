using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TwitterClone.UI.Models
{
    public class TweetModel
    {
        public Int32 tweet_Id { get; set; }
        public string user_Id { get; set; }
        public string message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}