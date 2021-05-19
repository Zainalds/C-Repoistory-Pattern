using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokersTavern.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmokersTavern.Tests.Controllers
{
    [TestClass]
    public class RegisterControllerTest
    {
        [TestMethod]
        public void Register()
        {
            // Arrange
            RegisterController controller = new RegisterController();

            // Act
            ViewResult result = controller.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
