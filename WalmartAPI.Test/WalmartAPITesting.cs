using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WalmartAPI;
using System.IO;
using System.Xml.Serialization;
using WalmartAPI.Classes;
using WalmartAPI.Classes.Walmart.mp;

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

        [Theory]
        [InlineData("PrivateKey.cer")]
        public void testFeeds(string privateKeyFile)
        {
            var key = "";
            using (var stream = new StreamReader(privateKeyFile))
            {
                key = stream.ReadToEnd();
            }

            var auth = new WalmartAPI.Classes.Authentication()
            {
                consumerId = "bfcfcaac-433d-42b1-adee-3e8e81486cd0",
                privateKey =key,
                httpRequestMethod="GET"
            };
            var x = new WalmartAPI.Classes.wmRequest(@"https://marketplace.walmartapis.com/v2/feeds", auth);
            var sut = x.getWMresponse<feedRecordResponse>();

            Assert.IsType(typeof(feedRecordResponse), sut);
        }

        [Theory]
        [InlineData(@"mock\FeedRecordResponse.xml",typeof(feedRecordResponse))]
        [InlineData(@"mock\XMLFile.xml",typeof(feedRecordResponse))]
        [InlineData(@"mock\OrdersResponse.xml", typeof(WalmartAPI.Classes.Walmart.Orders.ordersListType))]
        public void testDeserialization(string file,Type type)
        {
            using (var str = File.OpenRead(file))
            {
                var xs = new XmlSerializer(type);

                var sut = xs.Deserialize(str);

                Assert.IsType(type, sut);
            }
        }

    }
}
