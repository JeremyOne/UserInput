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
            Assert.AreEqual(g[0].Value, "value1", "Array 1st value is unchanged");
            Assert.AreEqual(g[1].Value, "value2", "Array 2nd value is unchanged");
            Assert.AreEqual(g[2].ValueInt, 3, "Array int value is readable as int");
            Assert.AreEqual(g[3].ValueDecimal, 4.44m, "Array decimal value is readable as decimal");
            Assert.AreEqual(g[4].ValueLong, 555555555555, "Array long value is readable as long");
            Assert.AreEqual(g[5].ValueDateTime, new DateTime(2006, 06, 06), "Array date value is readable as date");

            Assert.Throws<IndexOutOfRangeException>(delegate {
                var outOfRange = g[6];
            }, "Array out of bounds throws index error");
            Assert.Throws<ArgumentOutOfRangeException>(delegate {
                var outOfRange = g[-1].Value;
            }, "Array negative throws argument error");

            var h = new Argument("name7", true);
            Assert.AreEqual(h.HasValue(), false, "Argument with no value is identified");
            Assert.AreEqual(h.IsArray(), false, "Argument with no value is identified as not array");
            h.AddValue("1");
            Assert.AreEqual(h.HasValue(), true, "Argument with 1 value is identified");
            Assert.AreEqual(h.IsArray(), false, "Argument with 1 value is not identified as array");
            h.AddValue("2");
            Assert.AreEqual(h.IsArray(), true, "Argument with 2 values is identified as array");

        }

    }
}
