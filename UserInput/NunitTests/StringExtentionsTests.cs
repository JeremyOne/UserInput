using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeremyOne.UserInput;

namespace JeremyOne.UserInput.NunitTests {
    [TestFixture]
    public class StringExtentionsTests {
        [Test]
        public void StringExtentionsTestMethod() {
            string test1 = "test,'$1,234.56'";
            string[] result1 = test1.Split(',');
            string[] result2 = test1.SmartSplit(',', '\'');

            Assert.AreEqual(result1.Length, 3);
            Assert.AreEqual(result2.Length, 2);
            Assert.AreEqual(result1[1], "$1,234.56");

            string test2 = "test,'$1,234.56',',,,,', '',last item";
            string[] result3 = test2.SmartSplit(',', '\'');

            Assert.AreEqual(result3.Length, 5);
            Assert.AreEqual(result3[4], "last item");
        }
    }
}
