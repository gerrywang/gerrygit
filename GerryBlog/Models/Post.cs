using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerryBlog.Models
{
    public class Post
    {
        public string Body { get; set; }
        public List<string> Comments { get; set; }
        public int Id { get; set; }
        public Post(string post)
        {
            this.Body = post;
            this.Id = Singleton.IdCount++;
            this.Comments = new List<string>();
        }
    }
}