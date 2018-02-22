using GigHub.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Claims;
using System.Security.Principal;

namespace Gighub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {


        public GigsControllerTests()
        {
            var identity = new GenericIdentity("user1@domain.com");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "user1@domain.com"));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));

            var principal = new GenericPrincipal(identity, null);

            var mockUoW = new Mock<IUnitOfWork>();
            


        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
