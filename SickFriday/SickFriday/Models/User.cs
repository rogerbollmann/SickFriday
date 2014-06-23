using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SickFriday.Models
{
    public class User
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Punkte")]
        public int Points { get; set; }

        [Display(Name = "Einsatz")]
        public int Input { get; set; }

        [Display(Name = "Gewonnen")]
        public int Win { get; set; }

        [Display(Name = "Anwesenheit")]
        public int Presence { get; set; }


    }

}