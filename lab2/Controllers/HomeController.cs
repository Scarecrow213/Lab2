using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using lab2.Models;

namespace lab2.Controllers
{
    public class HomeController : Controller
    {
        PartContext db = new PartContext();
        public ActionResult Index(int? shop, string kind)
        {
            IQueryable<Part> parts = db.Parts.Include(p => p.Shop);
            if (shop != null && shop != 0)
            {
                parts = parts.Where(p => p.ShopId == shop);
            }
            if (!String.IsNullOrEmpty(kind) && !kind.Equals("Все"))
            {
                parts = parts.Where(p => p.Kind == kind);
            }

            List<Shop> shops = db.Shops.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех 
            shops.Insert(0, new Shop { NameShop = "Все", ShopId = 0 });
            PartListViewModel blvm = new PartListViewModel
            {
                Parts = parts.ToList(),
                Shops = new SelectList(shops, "ShopId", "NameShop"),
                Kind = new SelectList(new List<string>()
                {
                "Все",
                "Видеокарта",
                "Процессор",
                "Оперативная память",
                "Материнская плата"
                })
            };
            return View(blvm);
        }
        //добавление магазина 
        [HttpGet]
        public ActionResult CreateShop()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateShop(Shop shop)
        {
            db.Shops.Add(shop);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        //Создание новой комплектующей 
        [HttpGet]
        public ActionResult CreatePart()
        {
            // Формируем список магазинов для передачи в представление 
            SelectList shops = new SelectList(db.Shops, "ShopId", "NameShop");
            SelectList Kind = new SelectList(new List<string>()
                {
                "Видеокарта",
                "Процессор",
                "Оперативная память",
                "Материнская плата"
                });
            ViewBag.Kinds = Kind;
            ViewBag.Shops = shops;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePart(Part part)
        {
            //Добавляем комплектующую в таблицу 
            db.Parts.Add(part);
            db.SaveChanges();
            // перенаправляем на главную страницу 
            return RedirectToAction("Index");
        }

        //Редактирование записи 
        [HttpGet]
        public ActionResult EditPart(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд комплектующую 
            Part part = db.Parts.Find(id);
            if (part != null)
            {
                // Создаем список магазинов для передачи в представление 
                SelectList shops = new SelectList(db.Shops, "ShopId", "NameShop", part.ShopId);
                SelectList Kind = new SelectList(new List<string>()
                {
                "Видеокарта",
                "Процессор",
                "Оперативная память",
                "Материнская плата"
                });
                ViewBag.Shops = shops;
                ViewBag.Kinds = Kind;
                return View(part);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EditPart(Part part)
        {
            db.Entry(part).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //удаление комплектующей 
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Part b = db.Parts.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            Shop shop = db.Shops.Find(b.ShopId);
            ViewBag.Shop = shop.NameShop;
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Part b = db.Parts.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Parts.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Удаление магазина
        [HttpGet]
        public ActionResult Delete_shop()
        {
            // Формируем список магазинов для передачи в представление
            SelectList shops = new SelectList(db.Shops, "ShopId", "NameShop");
            ViewBag.Shops = shops;
            return View();
        }

        [HttpPost, ActionName("Delete_shop")]
        public ActionResult DeleteConfirmed_shop(Shop shop)
        {
            Shop s = db.Shops.Find(shop.ShopId);
            if (s == null)
            {
                return HttpNotFound();
            }
            db.Shops.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PartView(int id)
        {
            Part b = db.Parts.Find(id);
            return View(b);
        }

        
    }
}