using GerryBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerryBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Singleton.Instance.Posts);
        }

        [HttpPost]
        public ActionResult Index(string action, string body = null, int postid = 0, string comment = null)
        {
            if (action == "deletePost")
            {
                Singleton.Instance.Posts.Remove(Singleton.Instance.Posts.Where(p => p.Id == postid).FirstOrDefault());
            }

            if (action == "addPost")
            {
                Singleton.Instance.Posts.Add(new Post(body));
            }
            if (action == "addComment")
            {
                Singleton.Instance.Posts.Where(p => p.Id == postid).FirstOrDefault().Comments.Add(comment);
            }

            return View(Singleton.Instance.Posts);
        }
    }
}