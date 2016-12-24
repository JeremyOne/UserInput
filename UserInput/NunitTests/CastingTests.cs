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
        }
    }
}
