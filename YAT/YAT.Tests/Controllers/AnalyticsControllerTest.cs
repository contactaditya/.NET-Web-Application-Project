using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YAT;
using YAT.Controllers;

namespace YAT.Tests.Controllers
{
    [TestClass]
    public class AnalyticsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            AnalyticsController controller = new AnalyticsController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
