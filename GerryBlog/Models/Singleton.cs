using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerryBlog.Models
{
    public class Singleton
    {
        private static Singleton _instance;
        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }
        public static int IdCount;
        public List<Post> Posts;
        private Singleton() {
            IdCount = 1;
            Posts = new List<Post>();
            Posts.Add(new Post("This is an example of a Blog created in Visual Studio to practice the basic elements of OOP. For this webpage, each post is an instance of a base class, there are some properties in each Post object, one of them is a List that contains all the comments that belong to that particular Post. An MVC approach is used to manipulate the DOM and show new elements added (posts or comments). There is no real database in this project, but the posts and comments are persistent, even after a hard reload, thanks to the as long as we keep the server running in Visual Studio. This was achieved by using the Singleton pattern. And of course, if you made it reading this far, you noticed I used bootstrap for the style."));
            Posts.Add(new Post("Wikipedia is a collaboratively edited, multilingual, free-access, free content Internet encyclopedia that is supported and hosted by the non-profit Wikimedia Foundation. Volunteers worldwide collaboratively write Wikipedia's 30 million articles in 287 languages, including over 4.5 million in the English Wikipedia. Anyone who can access the site can edit almost any of its articles, which collectively make up the Internet's largest and most popular general reference work. In February 2014, The New York Times reported that Wikipedia is ranked fifth globally among all websites stating, With 18 billion page views and nearly 500 million unique visitors a month..., Wikipedia trails just Yahoo, Facebook, Microsoft and Google, the largest with 1.2 billion unique visitors."));
            Posts.Add(new Post(".NET Framework (pronounced dot net) is a software framework developed by Microsoft that runs primarily on Microsoft Windows. It includes a large class library known as Framework Class Library (FCL) and provides language interoperability (each language can use code written in other languages) across several programming languages. Programs written for .NET Framework execute in a software environment (as contrasted to hardware environment), known as Common Language Runtime (CLR), an application virtual machine that provides services such as security, memory management, and exception handling. FCL and CLR together constitute .NET Framework."));
            Posts.Add(new Post("JavaScript (JS) is a dynamic computer programming language. It is most commonly used as part of web browsers, whose implementations allow client-side scripts to interact with the user, control the browser, communicate asynchronously, and alter the document content that is displayed. It is also being used in server-side network programming (with Node.js), game development and the creation of desktop and mobile applications."));
            Posts[0].Comments.Add("Source: Wikipedia");
            Posts[1].Comments.Add("Source: Wikipedia");
            Posts[2].Comments.Add("Source: Wikipedia");
            Posts[3].Comments.Add("Source: Wikipedia");
        }
    }
}