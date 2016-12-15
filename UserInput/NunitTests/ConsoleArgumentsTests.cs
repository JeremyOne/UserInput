using System;
using NUnit.Framework;
using JeremyOne.UserInput;

namespace JeremyOne.UserInputTests
{
    [TestFixture]
    public class ConsoleArgumentsTests {

        [Test]
        public void ConsoleArgumentsTest1() {
            string[] a1 = {
                "value1",
                "value2",
                "value3",
                "/name1:value",
                "/name2;value",
                "-name3:value",
                "-name4;value",
                "--name5:value",
                "--name6;value",
                "/name7:",
                "/name8",
                "-name9",
                "--name10;",
                "--name11",
                "name11-value",
                "/name12:",
                "name12-value1",
                "name12-value2",
                "name12-value3",
                "/name13:value1:value2:value3",
                "/name14:\"value with spaces\"",
                "/name15:'value with spaces'",
                "/name16",
                "'value with spaces'",
                "\"value with spaces\""
            };

            string[] names = { "/", "-", "--" };
            char[] seperators = { ':', ';'};

            var c1 = new ConsoleArguments(a1);
            var c2 = new ConsoleArguments(a1, names, seperators);

            Assert.AreEqual(c1.Get("").HasValue(), true, "Unnamed argument has value");
            Assert.AreEqual(c1.Default.HasValue(), true, "Unnamed argument has value, using .Default");

            Assert.AreEqual(c1.Default.HasValue(), true, "Unnamed argument has value");
            Assert.AreEqual(c1.Default.Exists, true, "Unnamed argument exists");
            Assert.AreEqual(c1.Default.IsArray(), true, "Unnamed argument flagged as array");
            Assert.AreEqual(c1.Default.GetItem(0).Value, "value1", "Unnamed argument (1) accessed with indexer has value");
            Assert.AreEqual(c1.Default.GetItem(1).Value, "value2", "Unnamed argument (2) accessed with indexer has value");
            Assert.AreEqual(c1.Default.GetItem(2).Value, "value3", "Unnamed argument (3) accessed with indexer has value");

            Assert.AreEqual(c1.Get("name1").HasValue(), true, "Existing argument has value");
            Assert.AreEqual(c1.Get("name1").Exists, true, "Existing argument flagged correctly");
            Assert.AreEqual(c1.Get("name1").Value, "value", "Value is unchanged");
            Assert.AreEqual(c1.Get("name3").Value, null, "Incorrect name indicator returns null");

            Assert.AreEqual(c1["name1"].HasValue(), true, "Existing argument has value, using array accessor");
            Assert.AreEqual(c1["name1"].Exists, true, "Existing argument flagged correctly, using array accessor");
            Assert.AreEqual(c1["name1"].Value, "value", "Value is unchanged, using array accessor");
            Assert.AreEqual(c1["name3"].Value, null, "Incorrect name indicator returns null, using array accessor");

            Assert.AreEqual(c1.Get("1eman").HasValue(), false, "Non-existent argument does not have value");
            Assert.AreEqual(c1.Get("1eman").Exists, false, "Non-existent argument flagged correctly");

            Assert.AreEqual(c2.Get("name3").Value, "value", "Correct name indicator returns value");
            Assert.AreEqual(c2.Get("name4").Value, "value", "Correct name indicator returns value");
            Assert.AreEqual(c2.Get("name5").Value, "value", "Correct name indicator returns value");
            Assert.AreEqual(c2.Get("name6").Value, "value", "Correct name indicator returns value");

            Assert.AreEqual(c2.Get("name7").Exists, true, "Correct name indicator (no value) exists");
            Assert.AreEqual(c2.Get("name8").Exists, true, "Correct name indicator (no value) exists");
            Assert.AreEqual(c2.Get("name9").Exists, true, "Correct name indicator (no value) exists");
            Assert.AreEqual(c2.Get("name10").Exists, true, "Correct name indicator (no value) exists");

            Assert.AreEqual(c2.Get("name11").Value, "name11-value", "Multi parameter argument read correctly");

            Assert.AreEqual(c2.Get("name12").Value, "name12-value1", "Multi pram array argument read correctly");
            Assert.AreEqual(c2.Get("name12").IsArray(), true, "Multi pram array argument identified correctly");
            Assert.AreEqual(c2.Get("name12")[0], "name12-value1", "Multi pram array argument access with indexer correct");
            Assert.AreEqual(c2.Get("name12")[1], "name12-value2", "Multi pram array argument access with indexer correct");
            Assert.AreEqual(c2.Get("name12")[2], "name12-value3", "Multi pram array argument access with indexer correct");

            Assert.AreEqual(c2.Get("name13").Value, "value1", "Multi array argument read correctly");
            Assert.AreEqual(c2.Get("name13").IsArray(), true, "Multi array argument identified correctly");
            Assert.AreEqual(c2.Get("name13")[0], "value1", "Multi array argument access with indexer correct");
            Assert.AreEqual(c2.Get("name13")[1], "value2", "Multi array argument access with indexer correct");
            Assert.AreEqual(c2.Get("name13")[2], "value3", "Multi array argument access with indexer correct");

            Assert.AreEqual(c2.Get("name14").Value, "value with spaces", "Value with spaces read correctly");
            Assert.AreEqual(c2.Get("name15").Value, "value with spaces", "Value with spaces read correctly, single quote");
            Assert.AreEqual(c2.Get("name16").Value, "value with spaces", "Value with spaces read correctly, multi parameter");
            Assert.AreEqual(c2.Get("name16")[1], "value with spaces", "Value with spaces in array read correctly, multi parameter");
        }
    }
}