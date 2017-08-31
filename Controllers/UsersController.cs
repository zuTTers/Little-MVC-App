using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.ViewModels;

namespace TestWeb.Controllers
{
    public class UsersController : Controller
    {
        int defaultPageSize = 5;
        // GET: Users

        public ActionResult Index(int? p, string orderby, string otype,string filter)
        {
            if (p == null)
                p = 1;

            List<Users> users = null;
            UserListViewModel userlist = new UserListViewModel();

            using (var db = new testEntities2())
            {
                IQueryable<Users> query = null;
                if (string.IsNullOrEmpty(filter))
                {
                    query = db.Users.Where(x=> 1==1);
                }
                else
                {
                    query = db.Users.Where(x => x.FirstName.Contains(filter) || x.LastName.Contains(filter));
                }

                if (string.IsNullOrEmpty(orderby))
                {
                    users = query.OrderByDescending(x => x.UserID).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                }
                else if (orderby == "FirstName")
                {
                    if (string.IsNullOrEmpty(otype) || otype == "asc")
                    {
                        users =query.OrderBy(x => x.FirstName).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                    }
                    else if (otype == "desc")
                    {
                        users =query.OrderByDescending(x => x.FirstName).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                    }
                }
                else if (orderby == "LastName")
                {
                    if (string.IsNullOrEmpty(otype) || otype == "asc")
                    {
                        users =query.OrderBy(x => x.LastName).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                    }
                    else if (otype == "desc")
                    {
                        users =query.OrderByDescending(x => x.LastName).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                    }
                }
                else if (orderby == "Gender")
                {
                    if (string.IsNullOrEmpty(otype) || otype == "asc")
                    {
                        users =query.OrderBy(x => x.Gender).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                    }
                    else if (otype == "desc")
                    {
                        users =query.OrderByDescending(x => x.Gender).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                    }
                }
                else if (orderby == "CreatedDate")
                {
                    if (string.IsNullOrEmpty(otype) || otype == "asc")
                    {
                        users =query.OrderBy(x => x.CreatedDate).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                    }
                    else if (otype == "desc")
                    {
                        users =query.OrderByDescending(x => x.CreatedDate).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                    }
                }
                else if (orderby == "UserName")
                {
                    if (string.IsNullOrEmpty(otype) || otype == "asc")
                    {
                        users =query.OrderBy(x => x.UserName).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                    }
                    else if (otype == "desc")
                    {
                        users =query.OrderByDescending(x => x.UserName).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                    }
                }

                userlist.Users = users;
                userlist.CurrentPage = p.Value;
                userlist.TotalCount =query.Count();
                if ((userlist.TotalCount % defaultPageSize) == 0)
                {
                    userlist.TotalPage = userlist.TotalCount / defaultPageSize;
                }
                else
                {
                    userlist.TotalPage = (userlist.TotalCount / defaultPageSize) + 1;
                }

            }
            ViewBag.orderby = orderby;
            ViewBag.otype = otype;


            return View(userlist);
        }

        

        public ActionResult UserDetail(int id)
        {
            Users userdetail = null;
            using (var db = new testEntities2())
            {
                userdetail = db.Users.Where(d => d.UserID == id).First();
                
            }

            return View(userdetail);
        }

        public ActionResult UserEdit(int id)
        {
            Users user = null;
            using (var db = new testEntities2())
            {
                user = db.Users.Where(e => e.UserID == id).First();
            }

            return View(user);
        }

        public ActionResult UserUpdate(Users user)
        {
            Users userupdate = null;

            using (var db = new testEntities2())
            {
                userupdate = db.Users.Where(d => d.UserID == user.UserID).First();
                userupdate.FirstName = user.FirstName;
                userupdate.LastName = user.LastName;
                userupdate.CreatedDate = user.CreatedDate;
                userupdate.Gender = user.Gender;
                userupdate.UserName = user.UserName;
                userupdate.Password = user.Password;
                db.SaveChanges();
            }
            return RedirectToAction("UserDetail", new { id = user.UserID });

        }

        public ActionResult UserAdd()
        {           
            return View();
        }

        public ActionResult UserAdder(Users user)
        {
            Users useradd = null;

            using (var db = new testEntities2())
            {
                useradd = db.Users.Add(user);
                db.SaveChanges();
            }

            return RedirectToAction("UserDetail", new { id = user.UserID });
            
        }

        public ActionResult UserDelete(int id)
        {
            Users userdelete = null;

            using (var db= new testEntities2())
            {
                userdelete = db.Users.Where(x => x.UserID == id).First();
                userdelete = db.Users.Remove(userdelete);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        
    }
}