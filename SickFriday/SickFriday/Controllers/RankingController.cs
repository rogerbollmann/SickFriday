using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SickFriday.Models;

namespace SickFriday.Controllers
{
    public class RankingController : Controller
    {
        private SickFridayDB db = new SickFridayDB();

        // GET: /Ranking/
        public ActionResult Index(string searchString)
        {
            //string searchString = id;

            var UserList = new List<string>();

            var UserQuery = from d in db.Users
                            orderby d.Name
                            select d.Name;

            UserList.AddRange(UserQuery.Distinct());
            ViewBag.searchString = new SelectList(UserList);

            
            if (!String.IsNullOrEmpty(searchString))
            {
                var ranking = from m in db.Rankings
                              where m.First == searchString
                              || m.Second == searchString
                              || m.Third == searchString
                              || m.Fourth == searchString
                              || m.Fifth == searchString
                              || m.Sixth == searchString
                              || m.Seventh == searchString
                              || m.Eight == searchString
                              || m.Ninth == searchString
                              select m;

                return View(ranking.OrderBy(d => d.Date));

            }
            else
            {
                return View(db.Rankings.ToList().OrderBy(d => d.Date));
            }



            
            //return View(db.Rankings.ToList().OrderBy(d => d.Date));
        }

        // GET: /Ranking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranking ranking = db.Rankings.Find(id);
            if (ranking == null)
            {
                return HttpNotFound();
            }
            return View(ranking);
        }

        // GET: /Ranking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Ranking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,First,Second,Third,Fourth,Fifth,Sixth,Seventh,Eight,Ninth,Date,BuyIn,WinFirst,WinSecond,WinThird")] Ranking ranking, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                
                //update Ranking table
                db.Rankings.Add(ranking);
                db.SaveChanges();
                CreateOrUpdateUserRanking(ranking);
                //Redirect to Index
                return RedirectToAction("Index");

            }

            return View(ranking);
        }



        // GET: /Ranking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranking ranking = db.Rankings.Find(id);
            if (ranking == null)
            {
                return HttpNotFound();
            }
            return View(ranking);
        }

        // POST: /Ranking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,First,Second,Third,Fourth,Fifth,Sixth,Seventh,Eight,Ninth,Date,BuyIn,WinFirst,WinSecond,WinThird")] Ranking ranking)
        {


            if (ModelState.IsValid)
            {
                //Problem with ObjectManger because of 2 object with the same key couldn't be handlet
                //therefor I user AsNoTracking()
                Ranking ranking1 = db.Rankings.AsNoTracking().First(p => p.ID == ranking.ID);
                
                Ranking rankingToDelete = new Ranking
                {
                    //ID= ranking1.ID,
                    First = ranking1.First,
                    Second = ranking1.Second,
                    Third = ranking1.Third,
                    Fourth = ranking1.Fourth,
                    Fifth = ranking1.Fifth,
                    Sixth = ranking1.Sixth,
                    Seventh = ranking1.Seventh,
                    Eight = ranking1.Eight,
                    BuyIn = ranking1.BuyIn,
                    Date = ranking1.Date,
                    WinFirst = ranking1.WinFirst,
                    WinSecond = ranking1.WinSecond,
                    WinThird = ranking1.WinThird
                };

                //Ranking ranktingToDelete = db.Rankings.Find(ranking.ID);

                ranking1 = null;
                
                
                db.Entry(ranking).State = EntityState.Modified;
                db.SaveChanges();

                Ranking rankingToCreate = db.Rankings.Find(ranking.ID);
                DeleteAndUpdateUserTable(rankingToDelete);
                CreateOrUpdateUserRanking(rankingToCreate);

                return RedirectToAction("Index");
            }
            return View(ranking);
        }



        // GET: /Ranking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranking ranking = db.Rankings.Find(id);
            if (ranking == null)
            {
                return HttpNotFound();
            }
            return View(ranking);
        }

        // POST: /Ranking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ranking ranking = db.Rankings.Find(id);
            DeleteAndUpdateUserTable(ranking);
            db.Rankings.Remove(ranking);
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

        private void CreateOrUpdateUserRanking(Ranking ranking)
        {
            //update User table
            string first = ranking.First;
            string second = ranking.Second;
            string third = ranking.Third;
            string fourth = ranking.Fourth;
            string fifth = ranking.Fifth;
            string sixth = ranking.Sixth;
            string seventh = ranking.Seventh;
            string eight = ranking.Eight;

            int firstWin = ranking.WinFirst;
            int secondWin = ranking.WinSecond;
            int thirdWin = ranking.WinThird;

            /**
            var findUser = (from m in db.Users
                           where m.Name == first
                           || m.Name == second
                           || m.Name == third
                           || m.Name == fourth
                           || m.Name == fifth
                           || m.Name == sixth
                           || m.Name == seventh
                           || m.Name == eight
                           select m).ToList();

            **/


            if (!string.IsNullOrEmpty(first))
            {

                var findUserFirst = (from m in db.Users
                                     where m.Name == first
                                     select m).ToList();

                if (findUserFirst.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == first);
                    user1.Input = user1.Input + ranking.BuyIn;
                    user1.Presence = user1.Presence + 1;
                    user1.Points = user1.Points + 8;
                    user1.Win = user1.Win + firstWin;


                    db.SaveChanges();
                }
                else
                {
                    User userNew = new User();
                    userNew.Name = ranking.First;
                    userNew.Presence = 1;
                    userNew.Points = 8;
                    userNew.Input = ranking.BuyIn;
                    userNew.Win = firstWin;
                    db.Users.Add(userNew);
                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(second))
            {

                var findUserSecond = (from m in db.Users
                                      where m.Name == second
                                      select m).ToList();

                if (findUserSecond.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == second);
                    user1.Input = user1.Input + ranking.BuyIn;
                    user1.Presence = user1.Presence + 1;
                    user1.Points = user1.Points + 7;
                    user1.Win = user1.Win + secondWin;


                    db.SaveChanges();
                }
                else
                {
                    User userNew = new User();
                    userNew.Name = ranking.Second;
                    userNew.Presence = 1;
                    userNew.Points = 7;
                    userNew.Input = ranking.BuyIn;
                    userNew.Win = secondWin;
                    db.Users.Add(userNew);
                    db.SaveChanges();
                }
            }


            if (!string.IsNullOrEmpty(third))
            {

                var findUserThird = (from m in db.Users
                                     where m.Name == third
                                     select m).ToList();

                if (findUserThird.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == third);
                    user1.Input = user1.Input + ranking.BuyIn;
                    user1.Presence = user1.Presence + 1;
                    user1.Points = user1.Points + 6;
                    user1.Win = user1.Win + thirdWin;

                    db.SaveChanges();
                }
                else
                {
                    User userNew = new User();
                    userNew.Name = ranking.Third;
                    userNew.Presence = 1;
                    userNew.Points = 6;
                    userNew.Input = ranking.BuyIn;
                    userNew.Win = thirdWin;
                    db.Users.Add(userNew);
                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(fourth))
            {

                var findUserFourth = (from m in db.Users
                                      where m.Name == fourth
                                      select m).ToList();

                if (findUserFourth.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == fourth);
                    user1.Input = user1.Input + ranking.BuyIn;
                    user1.Presence = user1.Presence + 1;
                    user1.Points = user1.Points + 5;

                    db.SaveChanges();
                }
                else
                {
                    User userNew = new User();
                    userNew.Name = ranking.Fourth;
                    userNew.Presence = 1;
                    userNew.Points = 5;
                    userNew.Input = ranking.BuyIn;
                    db.Users.Add(userNew);
                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(fifth))
            {

                var findUserFifth = (from m in db.Users
                                     where m.Name == fifth
                                     select m).ToList();

                if (findUserFifth.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == fifth);
                    user1.Input = user1.Input + ranking.BuyIn;
                    user1.Presence = user1.Presence + 1;
                    user1.Points = user1.Points + 4;

                    db.SaveChanges();
                }
                else
                {
                    User userNew = new User();
                    userNew.Name = ranking.Fifth;
                    userNew.Presence = 1;
                    userNew.Points = 4;
                    userNew.Input = ranking.BuyIn;
                    db.Users.Add(userNew);
                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(sixth))
            {

                var findUserSixth = (from m in db.Users
                                     where m.Name == sixth
                                     select m).ToList();

                if (findUserSixth.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == sixth);
                    user1.Input = user1.Input + ranking.BuyIn;
                    user1.Presence = user1.Presence + 1;
                    user1.Points = user1.Points + 3;

                    db.SaveChanges();
                }
                else
                {
                    User userNew = new User();
                    userNew.Name = ranking.Sixth;
                    userNew.Presence = 1;
                    userNew.Points = 3;
                    userNew.Input = ranking.BuyIn;
                    db.Users.Add(userNew);
                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(seventh))
            {

                var findUserSeventh = (from m in db.Users
                                       where m.Name == seventh
                                       select m).ToList();

                if (findUserSeventh.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == seventh);
                    user1.Input = user1.Input + ranking.BuyIn;
                    user1.Presence = user1.Presence + 1;
                    user1.Points = user1.Points + 2;

                    db.SaveChanges();
                }
                else
                {
                    User userNew = new User();
                    userNew.Name = ranking.Seventh;
                    userNew.Presence = 1;
                    userNew.Points = 2;
                    userNew.Input = ranking.BuyIn;
                    db.Users.Add(userNew);
                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(eight))
            {

                var findUserEight = (from m in db.Users
                                     where m.Name == eight
                                     select m).ToList();

                if (findUserEight.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == eight);
                    user1.Input = user1.Input + ranking.BuyIn;
                    user1.Presence = user1.Presence + 1;
                    user1.Points = user1.Points + 1;

                    db.SaveChanges();
                }
                else
                {
                    User userNew = new User();
                    userNew.Name = ranking.Eight;
                    userNew.Presence = 1;
                    userNew.Points = 1;
                    userNew.Input = ranking.BuyIn;
                    db.Users.Add(userNew);
                    db.SaveChanges();
                }
            }
            
        }

        private void DeleteAndUpdateUserTable(Ranking ranking)
        {
            //update User table
            string first = ranking.First;
            string second = ranking.Second;
            string third = ranking.Third;
            string fourth = ranking.Fourth;
            string fifth = ranking.Fifth;
            string sixth = ranking.Sixth;
            string seventh = ranking.Seventh;
            string eight = ranking.Eight;

            int firstWin = ranking.WinFirst;
            int secondWin = ranking.WinSecond;
            int thirdWin = ranking.WinThird;

            /**
            var findUser = (from m in db.Users
                           where m.Name == first
                           || m.Name == second
                           || m.Name == third
                           || m.Name == fourth
                           || m.Name == fifth
                           || m.Name == sixth
                           || m.Name == seventh
                           || m.Name == eight
                           select m).ToList();

            **/


            if (!string.IsNullOrEmpty(first))
            {

                var findUserFirst = (from m in db.Users
                                     where m.Name == first
                                     select m).ToList();

                if (findUserFirst.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == first);
                    user1.Input = user1.Input - ranking.BuyIn;
                    user1.Presence = user1.Presence - 1;
                    user1.Points = user1.Points - 8;
                    user1.Win = user1.Win - firstWin;


                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(second))
            {

                var findUserSecond = (from m in db.Users
                                      where m.Name == second
                                      select m).ToList();

                if (findUserSecond.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == second);
                    user1.Input = user1.Input - ranking.BuyIn;
                    user1.Presence = user1.Presence - 1;
                    user1.Points = user1.Points - 7;
                    user1.Win = user1.Win - secondWin;


                    db.SaveChanges();
                }
            }


            if (!string.IsNullOrEmpty(third))
            {

                var findUserThird = (from m in db.Users
                                     where m.Name == third
                                     select m).ToList();

                if (findUserThird.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == third);
                    user1.Input = user1.Input - ranking.BuyIn;
                    user1.Presence = user1.Presence - 1;
                    user1.Points = user1.Points - 6;
                    user1.Win = user1.Win - thirdWin;

                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(fourth))
            {

                var findUserFourth = (from m in db.Users
                                      where m.Name == fourth
                                      select m).ToList();

                if (findUserFourth.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == fourth);
                    user1.Input = user1.Input - ranking.BuyIn;
                    user1.Presence = user1.Presence - 1;
                    user1.Points = user1.Points - 5;

                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(fifth))
            {

                var findUserFifth = (from m in db.Users
                                     where m.Name == fifth
                                     select m).ToList();

                if (findUserFifth.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == fifth);
                    user1.Input = user1.Input - ranking.BuyIn;
                    user1.Presence = user1.Presence - 1;
                    user1.Points = user1.Points - 4;

                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(sixth))
            {

                var findUserSixth = (from m in db.Users
                                     where m.Name == sixth
                                     select m).ToList();

                if (findUserSixth.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == sixth);
                    user1.Input = user1.Input - ranking.BuyIn;
                    user1.Presence = user1.Presence - 1;
                    user1.Points = user1.Points - 3;

                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(seventh))
            {

                var findUserSeventh = (from m in db.Users
                                       where m.Name == seventh
                                       select m).ToList();

                if (findUserSeventh.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == seventh);
                    user1.Input = user1.Input - ranking.BuyIn;
                    user1.Presence = user1.Presence - 1;
                    user1.Points = user1.Points - 2;

                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(eight))
            {

                var findUserEight = (from m in db.Users
                                     where m.Name == eight
                                     select m).ToList();

                if (findUserEight.Count > 0)
                {

                    var user1 = db.Users.Single(u => u.Name == eight);
                    user1.Input = user1.Input - ranking.BuyIn;
                    user1.Presence = user1.Presence - 1;
                    user1.Points = user1.Points - 1;

                    db.SaveChanges();
                }
            }
        }
    }
}
