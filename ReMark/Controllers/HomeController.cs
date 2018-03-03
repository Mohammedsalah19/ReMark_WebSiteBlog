using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReMark.Models;
using System.Data.SqlClient;
using System.Net;
namespace ReMark.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection(@"Data Source=M-SALAH\MSSQSERVER2;Initial Catalog=Remark;Integrated Security=True");
        private RemarkEntities9 re = new RemarkEntities9();
        private RemarkEntities9 re3 = new RemarkEntities9();



        public ActionResult Index()
        {
            return View(re3.NewBlogs.Where(m=>m.State==true).ToList());
        }

        #region signup

        public ActionResult signup(string input_username, string input_Pass, string input_Email, string input_Department)
        {
            if (Request.HttpMethod == "POST")
            {
                SqlCommand cmd = new SqlCommand("insert into Login(Username,Password,Department,Email) values('" + input_username + "','" + input_Pass + "'" +
                    ",'" + input_Department + "','" + input_Email + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return View();
        }
        #endregion


        #region login
        public ActionResult Login(string input_username, string input_Pass)
        {
            if (Request.HttpMethod == "POST")
            {
                SqlCommand cmd2 = new SqlCommand("select Username,Password,Department from Login", con);
                con.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["Username"].ToString() == input_username && dr["Password"].ToString() == input_Pass)
                    {
                        Session["Username"] = dr["Username"];
                        Session["Department"] = dr["Department"];
                        return View("index", re3.NewBlogs.Where(m => m.State == true).ToList());
                    }
                }
                dr.Close();
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            return View("Login");
        }

        #endregion

        #region NewBlog
        [ValidateInput(false)]
        public ActionResult NewBlog(string editor, string input_Blogtitle)
        {
            if (Session["Username"] != null)
            {
                if (Request.HttpMethod == "POST")
                {
                    ModelState.Clear();                   
                    string sub = Server.HtmlDecode(editor);
             //       editor = ValueProvider.GetValue(nameof(ReMark.Models.NewBlog.Subject)).AttemptedValue;
                    SqlCommand cmd3 = new SqlCommand("insert into NewBlog(Subject,Username,Department,Blog_Title,date,State) values('"+ sub + "','" + Session["Username"] + "','" + Session["Department"] + "','" + input_Blogtitle + "','" + DateTime.Now + "',0)", con);
                    con.Open();
                    cmd3.ExecuteNonQuery();
                    con.Close();
                    return View("index", re3.NewBlogs.Where(m => m.State == true).ToList());

                   
                }
            }
            else
            {
                return View("Login");
            }
            return View("NewBlog");
        }
        #endregion

        
        public ActionResult Blog(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewBlog NewBlogs = re3.NewBlogs.Find(id);
            if (NewBlogs == null)
            {
                return HttpNotFound();
            }
            return View("Blog",NewBlogs);
        }
    }
}
