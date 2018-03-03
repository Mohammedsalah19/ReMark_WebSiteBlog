using ReMark.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ReMark.Controllers
{
    public class AdminRoleController : Controller
    {
        private RemarkEntities9 re3 = new RemarkEntities9();


        // GET: AdminRole
        public ActionResult Index()
        {
            
              return View(re3.NewBlogs.ToList());
        }


            #region Show or Disappear Blog 
        public ActionResult Statue(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewBlog NewBlog = re3.NewBlogs.Find(id);
            if (NewBlog == null)
            {
                return HttpNotFound();
            }
            if(NewBlog.State==false)
            {
                NewBlog.State = true;
            }
            else
            {
                NewBlog.State = false;

            }
            re3.Entry(NewBlog).State = EntityState.Modified;
            re3.SaveChanges();
            return RedirectToAction("Index");
        }
            #endregion

           #region delete Blog form admin control
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewBlog NewBlog = re3.NewBlogs.Find(id);
            if (NewBlog == null)
            {
                return HttpNotFound();
            }
            NewBlog NewBlog2 = re3.NewBlogs.Find(id);
            re3.NewBlogs.Remove(NewBlog2);
            re3.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion


        


    }
}