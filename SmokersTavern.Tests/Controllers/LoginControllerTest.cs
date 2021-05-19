using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokersTavern.Business;
using SmokersTavern.Controllers;
using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Net;
using System.Web;

namespace SmokersTavern.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void Login()
        {
            // Arrange
            LoginController controller = new LoginController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
