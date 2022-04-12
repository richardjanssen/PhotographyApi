using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotographyApi.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var a = 1;
            a.Should().Be(1);
        }
    }
}