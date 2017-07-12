using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ERMine.Core.Parsing;
using Sprache;

namespace ERMine.UnitTesting.Core.Parsing
{
    [TestClass]
    public class DomainParserTest
    {
        [TestMethod]
        public void Domain_OneWord_Label()
        {
            var input = "<Weekday>";
            var domain = DomainParser.Domain.Parse(input);

            Assert.AreEqual("Weekday", domain);

        }

        [TestMethod]
        public void Domain_SeveralWord_Label()
        {
            var input = "<Week of the day>";
            var domain = DomainParser.Domain.Parse(input);

            Assert.AreEqual("Week of the day", domain);
        }

        [TestMethod]
        public void DomainValue_OneWord_Label()
        {
            var input = "Monday";
            var domainValue = DomainParser.DomainValue.Parse(input);

            Assert.AreEqual("Monday", domainValue.Label);

        }

        [TestMethod]
        public void DomainValue_OneWordLineFeed_Label()
        {
            var input = "Monday\r\n";
            var domainValue = DomainParser.DomainValue.Parse(input);

            Assert.AreEqual("Monday", domainValue.Label);

        }


        [TestMethod]
        public void DomainValue_WithQuote_Label()
        {
            var input = "'Monday'";
            var domainValue = DomainParser.DomainValue.Parse(input);

            Assert.AreEqual("Monday", domainValue.Label);
        }

        [TestMethod]
        public void DomainValue_WithQuoteAndSpace_Label()
        {
            var input = "'First day of week'";
            var domainValue = DomainParser.DomainValue.Parse(input);

            Assert.AreEqual("First day of week", domainValue.Label);
        }


    }
}
