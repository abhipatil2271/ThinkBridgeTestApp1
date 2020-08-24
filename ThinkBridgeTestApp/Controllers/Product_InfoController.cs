using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThinkBridgeTestApp.Models;

namespace ThinkBridgeTestApp.Controllers
{
    public class Product_InfoController : Controller
    {
        private TestDBEntities db = new TestDBEntities();

        // GET: Product_Info
        public ActionResult Index()
        {
            return View(db.Product_Info.ToList());
        }

        // GET: Product_Info/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Info product_Info = db.Product_Info.Find(id);
            if (product_Info == null)
            {
                return HttpNotFound();
            }
            return View(product_Info);
        }

        // GET: Product_Info/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product_Info product_Info, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    db.Product_Info.Add(new Product_Info
                    {
                        Product_Id = Guid.NewGuid(),
                        Name = product_Info.Name,
                        Description = product_Info.Description,
                        Price = product_Info.Price,
                        Image = "Images/" + file.FileName
                    });
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    //string path = Path.Combine(Server.MapPath("~/Images"));
                    string path = "Images/DefaultImg.png";
                    db.Product_Info.Add(new Product_Info
                    {
                        Product_Id = Guid.NewGuid(),
                        Name = product_Info.Name,
                        Description = product_Info.Description,
                        Price = product_Info.Price,
                        Image = path
                    });
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(product_Info);
        }

        // GET: Product_Info/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Info product_Info = db.Product_Info.Find(id);
            if (product_Info == null)
            {
                return HttpNotFound();
            }
            return View(product_Info);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product_Info product_Info, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (product_Info != null)
                {
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);

                        Product_Info product = new Product_Info();

                        product.Product_Id = product_Info.Product_Id;
                        product.Name = product_Info.Name;
                        product.Description = product_Info.Description;
                        product.Price = product_Info.Price;
                        product.Image = "Images/" + file.FileName;

                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        //string path = Path.Combine(Server.MapPath("~/Images"), "DefaultImg.png");
                        string path = "Images/DefaultImg.png";
                        //file.SaveAs(path);

                        Product_Info product = new Product_Info();

                        product.Product_Id = product_Info.Product_Id;
                        product.Name = product_Info.Name;
                        product.Description = product_Info.Description;
                        product.Price = product_Info.Price;
                        product.Image = path;

                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(product_Info);
        }

        // GET: Product_Info/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Info product_Info = db.Product_Info.Find(id);
            if (product_Info == null)
            {
                return HttpNotFound();
            }
            return View(product_Info);
        }

        // POST: Product_Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product_Info product_Info = db.Product_Info.Find(id);
            db.Product_Info.Remove(product_Info);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
