using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeremyOne.UserInput;

namespace JeremyOne.UserInputTests {

    [TestFixture]
    public class ArgumentTests {

        [Test]
        public void ArgumentTest1() {
            var a = new Argument("name1", "value", true);
            var b = new Argument("name2", "99", true);
            var c = new Argument("name3", "99.99", true);
            var d = new Argument("name4", "123456789123", true);
            var e = new Argument("name5", "01/01/2000", true);
            var f = new Argument("name5", "01/01/2000 06:00:00", true);
            string[] ga = { "value1", "value2", "3", "4.44", "555555555555", "06/06/2006" };
            var g = new Argument("name6", ga, true);

            Assert.AreEqual(a.Name, "name1", "Name is unchanged");
            Assert.AreEqual(a.Value, "value", "Value is unchanged");
            Assert.AreEqual(a.ValueInt, null, "Unparseable int returns null");
            Assert.AreEqual(a.ValueDecimal, null, "Unparseable decimal returns null");
            Assert.AreEqual(a.ValueLong, null, "Unparseable long returns null");
            Assert.AreEqual(a.ValueDateTime, null, "Unparseable DateTime returns null");
            Assert.AreEqual(a.ValueIntOrDefault(11), 11, "Unparseable int returns default");
            Assert.AreEqual(a.ValueDecimalOrDefault(11.1M), 11.1M, "Unparseable Decimal returns default");
            Assert.AreEqual(a.ValueLongOrDefault(987654321987), 987654321987, "Unparseable long returns default");
            Assert.AreEqual(a.ValueDateTimeOrDefault(new DateTime(2000, 01, 01)), new DateTime(2000, 01, 01), "Unparseable date returns default");
            Assert.AreEqual(a.IsArray(), false, "Array is not identified");

            Assert.AreEqual(b.Value, "99", "Int readable as string");
            Assert.AreEqual(b.ValueInt, 99, "Int readable as int");
            Assert.AreEqual(b.ValueDecimal, 99.0, "Int readable as decimal");
            Assert.AreEqual(b.ValueIntOrDefault(11), 99.0, "Valid int readable as with default");

            Assert.AreEqual(c.ValueInt, null, "Decimal not readable as int");
            Assert.AreEqual(c.ValueDecimal, 99.99, "Decimal readable as decimal");
            Assert.AreEqual(c.ValueDecimalOrDefault(11.1m), 99.99, "Valid decimal readable as with default");

            Assert.AreEqual(d.ValueLong, 123456789123, "Long readable as long");
            Assert.AreEqual(d.ValueLongOrDefault(987654321987), 123456789123, "Valid long readable as with default");
            Assert.AreEqual(d.ValueInt, null);

            Assert.AreEqual(e.ValueDateTime, new DateTime(2000, 01, 01), "Date readable as date");
            Assert.AreEqual(e.ValueDateTimeOrDefault(new DateTime(2012, 12, 12)), new DateTime(2000, 01, 01), "Valid DateTime readable as with default");
            Assert.AreEqual(f.ValueDateTime, new DateTime(2000, 01, 01, 6, 0, 0), "Date with time readable as DateTime");

            Assert.AreEqual(g.IsArray(), true, "Array argument is identified");
            Assert.AreEqual(g[0], "value1", "Array 1st value is unchanged");
            Assert.AreEqual(g[1], "value2", "Array 2nd value is unchanged");
            Assert.AreEqual(g.GetItem(2).ValueInt, 3, "Array int value is readable as int");
            Assert.AreEqual(g.GetItem(3).ValueDecimal, 4.44m, "Array decimal value is readable as decimal");
            Assert.AreEqual(g.GetItem(4).ValueLong, 555555555555, "Array long value is readable as long");
            Assert.AreEqual(g.GetItem(5).ValueDateTime, new DateTime(2006, 06, 06), "Array date value is readable as date");

            var h = new Argument("name7", true);
            Assert.AreEqual(h.HasValue(), false, "Argument with no value is identified");
            Assert.AreEqual(h.IsArray(), false, "Argument with no value is identified as not array");
            h.Add("1");
            Assert.AreEqual(h.HasValue(), true, "Argument with 1 value is identified");
            Assert.AreEqual(h.IsArray(), false, "Argument with 1 value is not identified as array");
            h.Add("2");
            Assert.AreEqual(h.IsArray(), true, "Argument with 2 values is identified as array");

            string[] ia = { "1", "2", "3", "4", "5", "6", "SEVEN" };
            var i = new Argument("name8", ia, true);
            var iList = i.ToIntList();
            Assert.AreEqual(iList[0], 1);
            Assert.AreEqual(iList[1], 2);
            Assert.AreEqual(iList[2], 3);
            Assert.AreEqual(iList[3], 4);
            Assert.AreEqual(iList[4], 5);
            Assert.AreEqual(iList[5], 6);
            Assert.AreEqual(iList[6], null);

            string[] ja = { "1.1", "2.2", "3.3", "4.4", "5.5", "6.6", "SEVEN" };
            var j = new Argument("name9", ja, true);
            var jList = j.ToDecimalList();
            Assert.AreEqual(jList[0], 1.1M);
            Assert.AreEqual(jList[1], 2.2M);
            Assert.AreEqual(jList[2], 3.3M);
            Assert.AreEqual(jList[3], 4.4M);
            Assert.AreEqual(jList[4], 5.5M);
            Assert.AreEqual(jList[5], 6.6M);
            Assert.AreEqual(jList[6], null);

            string[] ka = { "111111111111", "222222222222", "333333333333", "444444444444", "555555555555", "666666666666", "SEVEN" };
            var k = new Argument("name10", ka, true);
            var kList = k.ToLongList();
            Assert.AreEqual(kList[0], 111111111111);
            Assert.AreEqual(kList[1], 222222222222);
            Assert.AreEqual(kList[2], 333333333333);
            Assert.AreEqual(kList[3], 444444444444);
            Assert.AreEqual(kList[4], 555555555555);
            Assert.AreEqual(kList[5], 666666666666);
            Assert.AreEqual(kList[6], null);


        }

    }
}
