using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistance;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    class GigsControllerTests
    {
        private ApplicationDbContext _context;
        private GigsController _gigsController;

        [SetUp]
        public void Setup()
        {
            _context = new ApplicationDbContext();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            //Arrange
            var user = _context.Users.First();
            var genre = _context.Genres.First();
            _context.Gigs.Add(new Gig
            {
                DateTime = DateTime.Now.AddDays(1),
                Artist = user,
                Genre = genre,
                Venue = "-"                                            
            });
            _context.SaveChanges();

            _gigsController = new GigsController(new UnitOfWork());
            _gigsController.MockCurrentUser(user.Id, user.UserName);

            //Act
            var result = _gigsController.Mine();

            //Assert            
            (((ViewResult) result).ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }
    }
}
