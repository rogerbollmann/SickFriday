using SickFriday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SickFriday.Controllers
{
    public class ChartsController : Controller
    {
        //
        // GET: /Charts/
        private SickFridayDB db = new SickFridayDB();
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Points()
        {
            List<string> xval = new List<string>();
            List<string> yval = new List<string>();


            foreach (var points in db.Users.ToList().OrderByDescending(c => c.Points).Take(9))
            {
                xval.Add(points.Name);
                yval.Add(points.Points.ToString());

            }

            string[] _xval = xval.ToArray();
            string[] _yval = yval.ToArray();

            var chartsPoint = new Chart(width: 1000, height: 400, theme: ChartTheme.Yellow) //,theme: ChartTheme.Yellow
            .AddSeries(
                xValue: _xval,
                yValues: _yval);

            return File(chartsPoint.GetBytes("png"), "image/png");

        }

        public ActionResult Presence()
        {
            List<string> xval = new List<string>();
            List<string> yval = new List<string>();

            foreach (var points in db.Users.ToList().OrderByDescending(c => c.Presence).Take(9))
            {
                xval.Add(points.Name);
                yval.Add(points.Presence.ToString());

            }

            string[] _xval = xval.ToArray();
            string[] _yval = yval.ToArray();

            var chartsPoint = new Chart(width: 1000, height: 400, theme: ChartTheme.Green) //,theme: ChartTheme.Green
            .AddSeries(
            name: "Anwesenheiten",
                xValue: _xval,
                yValues: _yval);

            return File(chartsPoint.GetBytes("png"), "image/png");

        }
        public ActionResult Input()
        {
            List<string> xval = new List<string>();
            List<string> yval = new List<string>();
            List<string> yval2 = new List<string>();

            foreach (var points in db.Users.ToList().OrderByDescending(c => c.Win).Take(9))
            {
                xval.Add(points.Name);
                yval.Add(points.Input.ToString());
                yval2.Add(points.Win.ToString());

            }

            string[] _xval = xval.ToArray();
            string[] _yval = yval.ToArray();
            string[] _yval2 = yval2.ToArray();

            var chartsPoint = new Chart(width: 1000, height: 400, theme: ChartTheme.Blue) //,theme: ChartTheme.Blue
            .AddSeries(
                name: "Buy-In",
                xValue: _xval,
                yValues: _yval
                )
            .AddSeries(
                name:"Gewonnen",
                xValue: _xval,
                yValues: _yval2)
            .AddLegend();

            return File(chartsPoint.GetBytes("png"), "image/png");

        }

	}
}