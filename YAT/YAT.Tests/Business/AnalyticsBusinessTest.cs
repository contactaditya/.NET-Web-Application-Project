using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YAT;
using YAT.Controllers;
using BusinessLayer;

namespace YAT.Tests.Controllers
{
    [TestClass]
    public class AnalyticsBusinessTest
    {
        Builder b = new Builder();
        Analytics a = new Analytics();

        [TestMethod]
        public void Index()
        {
            IEnumerable<MovieRank> ranks = a.movieRank();
            
            // Assert
            Assert.IsNotNull(ranks);
        }

    }
}
