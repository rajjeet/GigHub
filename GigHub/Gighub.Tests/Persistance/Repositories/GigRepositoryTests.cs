using FluentAssertions;
using Gighub.Tests.Controllers.Extensions;
using GigHub.Core.Models;
using GigHub.Persistance;
using GigHub.Persistance.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace Gighub.Tests.Persistance.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigsRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private string _artistId;
        private Gig[] gigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            var mockContext =  new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            _artistId = "2";
            _repository = new GigsRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetGigsForArtist_GigsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1"};
            _mockGigs.SetSource(new[] { gig } );
            var result = _repository.GetGigsForArtist("1");
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsForArtist_GigsIsCancelled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1"};
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig } );
            var result = _repository.GetGigsForArtist("1");
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsForArtist_GigIsForDifferentArtist_ShouldNotBeReturned()
        {            
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = _artistId };        

            _mockGigs.SetSource(new[] { gig });
            var result = _repository.GetGigsForArtist(gig.ArtistId + "-");
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsForArtist_GigIsForGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = _artistId };

            gigs = new[] { gig };
            _mockGigs.SetSource(gigs);
            var result = _repository.GetGigsForArtist(gig.ArtistId);
            result.Should().Equal(gigs);
        }

    }
}
