using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClone.Entities
{
    public class TweetModel
    {
        public Int32 tweet_Id { get; set; }
        public string user_Id { get; set; }

        [DataType(DataType.MultilineText)]

        [StringLength(150)]
        public string message { get; set; }
        public DateTime CreatedDate { get; set; }

       

    }
}
