using FluentAssertions;
using Gighub.Tests.Controllers.Extensions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace Gighub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigsRepository> _mockIGigRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockIGigRepository = new Mock<IGigsRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockIGigRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigExistsWithGivenId_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCancelled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();
            _mockIGigRepository.Setup(gr => gr.GetGigByGigId(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();

        }

        [TestMethod]
        public void Cancel_UserCancellingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig() {ArtistId = _userId + "-"};
            _mockIGigRepository.Setup(gr => gr.GetGigByGigId(1)).Returns(gig);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();

        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig() { ArtistId = _userId };
            _mockIGigRepository.Setup(gr => gr.GetGigByGigId(1)).Returns(gig);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

    }
}
