using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput.NunitTests {
    [TestFixture]
    public class CastingTests {
        [Test]
        public void CastingTests1() {
            // TODO: Add your test code here
            var test1 = Casting.GetDate("1/1/2000");
            var test2 = Casting.GetDate("not a date");

            Assert.AreEqual(test1.HasValue, true);
            Assert.AreEqual(test1.Value, new DateTime(2000,1,1));
            Assert.AreEqual(test2.HasValue, false);

            var test3 = Casting.GetDecimal("100");
            var test4 = Casting.GetDecimal("100.01");
            var test5 = Casting.GetDecimal("not a decimal");

            Assert.AreEqual(test3.HasValue, true);
            Assert.AreEqual(test3.Value, 100m);
            Assert.AreEqual(test4.HasValue, true);
            Assert.AreEqual(test4.Value, 100.01m);
            Assert.AreEqual(test5.HasValue, false);

            var test6 = Casting.GetInt("100");
            var test7 = Casting.GetInt("100.01");
            var test8 = Casting.GetInt("not an int");

            Assert.AreEqual(test6.HasValue, true);
            Assert.AreEqual(test6.Value, 100);
            Assert.AreEqual(test7.HasValue, false);
            Assert.AreEqual(test8.HasValue, false);

            var test9 = Casting.GetIPAddress("127.0.0.1");
            var test10 = Casting.GetIPAddress("999.0.0.0");
            var test11 = Casting.GetIPAddress("Not an IP");

            byte[] ipBytes = { 0x7f, 0x0, 0x0, 0x1 };
            Assert.AreEqual(test9, new System.Net.IPAddress(ipBytes));
            Assert.AreEqual(test10 == null, true);
            Assert.AreEqual(test11 == null, true);

            var test12 = Casting.GetUri("http://127.0.0.1");
            Assert.AreEqual(test12, new Uri("http://127.0.0.1"));

        }
    }
}
