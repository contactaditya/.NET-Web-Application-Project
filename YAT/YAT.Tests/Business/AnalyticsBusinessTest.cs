using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YAT;
using YAT.Controllers;
using BusinessLayer;

namespace YAT.Tests.Business
{
    [TestClass]
    public class AnalyticsBusinessTest
    {
        Builder b = new Builder();
        Analytics a = new Analytics();

        [TestMethod]
        public void movieRank()
        {
            IEnumerable<StringRow> ranks = a.movieRank();
            Assert.IsNotNull(ranks);
        }
        public void Address()
        {
            IEnumerable<StringRow> ranks = a.address();
            Assert.IsNotNull(ranks);
        }
        public void registrationMonths()
        {
            IEnumerable<IntRow> ranks = a.registrationMonths();
            Assert.IsNotNull(ranks);
        }
        public void ageRank()
        {
            IEnumerable<IntRow> ranks = a.ageRank();
            Assert.IsNotNull(ranks);
        }
        public void genderCount()
        {
            IEnumerable<BoolRow> ranks = a.genderCount();
            Assert.IsNotNull(ranks);
        }
    }
}
