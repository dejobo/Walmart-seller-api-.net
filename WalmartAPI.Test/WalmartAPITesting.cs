using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WalmartAPI.Test
{
    public class WalmartAPITesting
    {
        [Fact(DisplayName ="Xunit is working")]
        public void test()
        {
            string sut = "tst";
            Assert.Equal("tst", sut);
        }
    }
}
