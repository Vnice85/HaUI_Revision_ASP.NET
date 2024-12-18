using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Revision.Models;

namespace Revision.Controllers
{
    public class HocSinhsController : Controller
    {
        private HocSinhDB db = new HocSinhDB();

        // GET: HocSinhs
        public ActionResult Xemdanhsach()
        {
            var hocSinhs = db.HocSinhs.Include(h => h.LopHoc);
            return View(hocSinhs.ToList());
        }

        // GET: HocSinhs/Details/5
        public ActionResult ChiTiet(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // GET: HocSinhs/Create
        public ActionResult Themdulieu()
        {
            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop");
            return View();
        }

        // POST: HocSinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Themdulieu(HocSinh hs, HttpPostedFileBase anhduthi)
        {
            var student = new HocSinh();
            if (anhduthi != null && anhduthi.ContentLength > 0)
            {
                string root = Server.MapPath("/Images/");
                string path = Path.Combine(root, anhduthi.FileName);
                anhduthi.SaveAs(path);

            }
            else return View(hs);
            if (ModelState.IsValid)
            {
                student.sbd = hs.sbd;
                student.hoten = hs.hoten;
                student.anhduthi = anhduthi.FileName;
                student.malop = hs.malop;
                student.diemthi = hs.diemthi;
                db.HocSinhs.Add(student);
                db.SaveChanges();
                return RedirectToAction("Xemdanhsach");
            }
            return View();
        }

       
        public ActionResult Edit(string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop", hocSinh.malop);
            return View(hocSinh);
        }

        // POST: HocSinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HocSinh hs, HttpPostedFileBase anhduthi)
        {
            
            var student =db.HocSinhs.Find(hs.sbd);
            if (anhduthi != null && anhduthi.ContentLength > 0)
            {
                string root = Server.MapPath("/Images/");
                string path = Path.Combine(root, anhduthi.FileName);
                anhduthi.SaveAs(path);
                student.anhduthi = anhduthi.FileName;

            }
            if (ModelState.IsValid)
            {
                student.sbd = hs.sbd;
                student.hoten = hs.hoten;
                
                student.malop = hs.malop;
                student.diemthi = hs.diemthi;
                db.SaveChanges();
                return RedirectToAction("Xemdanhsach");
            }
          

            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop", hs.malop);
            return View(student);
        }

        // GET: HocSinhs/Delete/5
        public ActionResult Xoadulieu(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // POST: HocSinhs/Delete/5
        [HttpPost, ActionName("Xoadulieu")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HocSinh hocSinh = db.HocSinhs.Find(id);
            db.HocSinhs.Remove(hocSinh);
            db.SaveChanges();
            return RedirectToAction("Xemdanhsach");
        }

        public ActionResult TimKiem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TimKiem(string str)
        {
            var ds = db.HocSinhs.Where(x=>x.sbd == str || x.hoten == str).ToList();
            return View(ds);
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
