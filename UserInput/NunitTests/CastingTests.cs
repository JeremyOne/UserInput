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
            //Define dates
            var test1 = Casting.GetDate("1/1/2000");
            var test2 = Casting.GetDate("not a date");

            //Test dates
            Assert.AreEqual(test1.HasValue, true, "Standard format has value");
            Assert.AreEqual(test1.Value, new DateTime(2000,1,1), "Standard format date is correct");
            Assert.AreEqual(test2.HasValue, false, "Invalid format has no value");

            //Define decimals
            var test3 = Casting.GetDecimal("100");
            var test4 = Casting.GetDecimal("100.01");
            var test5 = Casting.GetDecimal("not a decimal");

            //Test Decimals
            Assert.AreEqual(test3.HasValue, true, "100 has value");
            Assert.AreEqual(test3.Value, 100m, "100 is correct");
            Assert.AreEqual(test4.HasValue, true, "100.01 has value");
            Assert.AreEqual(test4.Value, 100.01m, "100.01 is correct");
            Assert.AreEqual(test5.HasValue, false, "Invalid decimal has no value");

            //Define Ints
            var test6 = Casting.GetInt("100");
            var test7 = Casting.GetInt("100.01");
            var test8 = Casting.GetInt("not an int");

            //Test Ints
            Assert.AreEqual(test6.HasValue, true, "100 has value");
            Assert.AreEqual(test6.Value, 100, "100 value is correct");
            Assert.AreEqual(test7.HasValue, false, "100.01 (decimal) has no value");
            Assert.AreEqual(test8.HasValue, false, "invalid int has no value");

            //define IP
            var test9 = Casting.GetIPAddress("127.0.0.1");
            var test10 = Casting.GetIPAddress("999.0.0.0");
            var test11 = Casting.GetIPAddress("Not an IP");

            //test IP
            byte[] ipBytes = { 0x7f, 0x0, 0x0, 0x1 };
            Assert.AreEqual(test9, new System.Net.IPAddress(ipBytes), "IP bytes are correct");
            Assert.Null(test10, "Out of range IP is null");
            Assert.Null(test11, "Invalid data null");

            //test URI
            var test12 = Casting.GetUri("http://127.0.0.1");
            Assert.AreEqual(test12, new Uri("http://127.0.0.1"), "Uri data is correct");

        }
    }
}
