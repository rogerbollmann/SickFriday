namespace SickFriday.Migrations
{
    using SickFriday.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SickFriday.Models.SickFridayDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SickFriday.Models.SickFridayDB context)
        {
           
            //Add users
            context.Users.AddOrUpdate(i => i.ID,
                new User
                {
                    Name = "Roger",
                    Input = 0,
                    Points = 0,
                    Win = 0,
                    Presence = 0
                },
                new User
                {
                    Name = "Fabian",
                    Input = 0,
                    Points = 0,
                    Win = 0,
                    Presence = 0
                },
                new User
                {
                    Name = "Andrin",
                    Input = 0,
                    Points = 0,
                    Win = 0,
                    Presence = 0
                },
                new User
                {
                    Name = "Rocco",
                    Input = 0,
                    Points = 0,
                    Win = 0,
                    Presence = 0
                },
                new User
                {
                    Name = "Mario",
                    Input = 0,
                    Points = 0,
                    Win = 0,
                    Presence = 0
                },
                new User
                {
                    Name = "Mäde",
                    Input = 0,
                    Points = 0,
                    Win = 0,
                    Presence = 0
                },
                new User
                {
                    Name = "Sergio",
                    Input = 0,
                    Points = 0,
                    Win = 0,
                    Presence = 0
                },
                new User
                {
                    Name = "Yasar",
                    Input = 0,
                    Points = 0,
                    Win = 0,
                    Presence = 0
                });

             //Add users
            context.Rankings.AddOrUpdate(i => i.ID,
                new Ranking
                {
                    First = "Fabian",
                    Second = "Mario",
                    Third = "Mäde",
                    Fourth = "Yasar",
                    Fifth = "Andrin",
                    Sixth = "Roger",
                    Date = DateTime.Now,
                    BuyIn = 30,
                    WinFirst = 200,
                    WinSecond = 200,
                    WinThird = 100
                });

        }
    }
}
