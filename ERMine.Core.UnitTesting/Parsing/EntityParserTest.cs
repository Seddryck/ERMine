using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ERMine.Core.Parsing;
using Sprache;

namespace ERMine.UnitTesting.Core.Parsing
{
    [TestClass]
    public class EntityParserTest
    {
        [TestMethod]
        public void entity_OneWord_Label()
        {
            var input = "[Student]";
            var entity = EntityParser.Entity.Parse(input);

            Assert.AreEqual("Student", entity.Label);

        }

        [TestMethod]
        public void entity_SeveralWord_Label()
        {
            var input = "[Student of University]";
            var entity = EntityParser.Entity.Parse(input);

            Assert.AreEqual("Student of University", entity.Label);

        }

        [TestMethod]
        public void entity_SeveralWordLineEnd_Label()
        {
            var input = "[Student of University]\r\n******";
            var entity = EntityParser.Entity.Parse(input);

            Assert.AreEqual("Student of University", entity.Label);
        }

        [TestMethod]
        public void entity_LabelAndAttributes_LabelAndAttributes()
        {
            var input = "[Student of University]\r\nAge int?\r\nSalary decimal(10,2)";
            var entity = EntityParser.Entity.Parse(input);

            Assert.AreEqual("Student of University", entity.Label);
            Assert.AreEqual(2, entity.Attributes.Count);
        }

        [TestMethod]
        public void entity_LabelAndAttributesAndKey_LabelAndAttributesAndKey()
        {
            var input = "[Student of University]\r\n*StudentNr varchar(20)\r\nAge int?\r\nSalary decimal(10,2)";
            var entity = EntityParser.Entity.Parse(input);

            Assert.AreEqual("Student of University", entity.Label);
            Assert.AreEqual(3, entity.Attributes.Count);
            Assert.AreEqual("StudentNr", entity.Key.Attributes.ElementAt(0).Label);
        }

        [TestMethod]
        public void Parse_EntityAndRelationship_Label()
        {
            var input = "[Student]" + "\r\n";
            input += "*StudentNr char(10)" + "\r\n";
            input += "Email varchar(50)?" + "\r\n";
            input += "Name varchar(50)" + "\r\n";
            input += "\r\n";

            var entity = EntityParser.Entity.Parse(input);

            Assert.AreEqual(3, entity.Attributes.Count());

        }


    }
}
