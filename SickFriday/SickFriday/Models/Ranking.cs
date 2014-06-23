using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SickFriday.Models
{
    public class Ranking
    {
        public int ID { get; set; }

        [Display(Name = "1.Platz")]
        public string First { get; set; }

        [Display(Name = "2.Platz")]
        public string Second { get; set; }

        [Display(Name = "3.Platz")]
        public string Third { get; set; }

        [Display(Name = "4.Platz")]
        public string Fourth { get; set; }

        [Display(Name = "5.Platz")]
        public string Fifth { get; set; }

        [Display(Name = "6.Platz")]
        public string Sixth { get; set; }

        [Display(Name = "7.Platz")]
        public string Seventh { get; set; }

        [Display(Name = "8.Platz")]
        public string Eight { get; set; }

        [Display(Name = "9.Platz")]
        public string Ninth { get; set; }

        [Display(Name = "Datum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Buy-In")]
        public int BuyIn { get; set; }

        [Display(Name = "1.Platz gewonnen")]
        public int WinFirst { get; set; }

        [Display(Name = "2.Platz gewonnen")]
        public int WinSecond { get; set; }

        [Display(Name = "3.Platz gewonnen")]
        public int WinThird { get; set; }

    }

    public class SickFridayDB : DbContext
    {
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<User> Users { get; set; }
    }


}