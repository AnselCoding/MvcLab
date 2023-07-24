using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace MvcLab.Models.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IConfiguration _configuration;
        public UnitTest1(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var svc = new SimpleWeatherService(_configuration);
            Assert.AreEqual(1, 1);
        }
    }
}
