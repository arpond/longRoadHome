using System;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests_LongRoadHome.LocationTests
{
    [TestClass]
    public class TSublocationFactory
    {
        [TestCategory("Location"), TestCategory("SublocationFactory"), TestMethod()]
        public void SublocationFactory_Residential()
        {
            Residential.RegisterSublocation();
            SubLocationFactory sf = new SubLocationFactory();
            Sublocation sl = sf.CreateSubLocation(Residential.TYPE, 1,1,1);
            Assert.IsInstanceOfType(sl, typeof(Residential));
        }

        [TestCategory("Location"), TestCategory("SublocationFactory"), TestMethod()]
        public void SublocationFactory_Commercial()
        {
            Commercial.RegisterSublocation();
            SubLocationFactory sf = new SubLocationFactory();
            Sublocation sl = sf.CreateSubLocation(Commercial.TYPE, 1, 1, 1);
            Assert.IsInstanceOfType(sl, typeof(Commercial));
        }

        [TestCategory("Location"), TestCategory("SublocationFactory"), TestMethod()]
        public void SublocationFactory_Civic()
        {
            Civic.RegisterSublocation();
            SubLocationFactory sf = new SubLocationFactory();
            Sublocation sl = sf.CreateSubLocation(Civic.TYPE, 1, 1, 1);
            Assert.IsInstanceOfType(sl, typeof(Civic));
        }
    }
}
