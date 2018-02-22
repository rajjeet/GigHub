using FluentAssertions;
using Gighub.Tests.Controllers.Extensions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace Gighub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        GigsController _controller;

        public GigsControllerTests()
        {
            var mockIGigRepository = new Mock<IGigsRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(mockIGigRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            _controller.MockCurrentUser("1", "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigExistsWithGivenId_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
