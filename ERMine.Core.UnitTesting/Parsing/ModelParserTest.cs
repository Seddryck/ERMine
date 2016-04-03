using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ERMine.Core.Parsing;
using Sprache;
using ERMine.Core.Modeling;

namespace ERMine.UnitTesting.Core.Parsing
{
    [TestClass]
    public class ModelParserTest
    {
        [TestMethod]
        public void Parse_UniqueEntity_Label()
        {
            var input = "[Student]" + "\r\n";
            input += "*StudentNr char(10)" + "\r\n";
            input += "LastName varchar(50)" + "\r\n";
            input += "FirstName varchar(50)" + "\r\n";
            input += "Email varchar(50)?";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(1, entityRelationships.Count());
            Assert.AreEqual(4, (entityRelationships.ElementAt(0) as Entity).Attributes.Count());

        }

        [TestMethod]
        public void Parse_EntityAndRelationship_Label()
        {
            var input = "[Student]" + "\r\n";
            input += "*StudentNr char(10)" + "\r\n";
            input += "LastName varchar(50)" + "\r\n";
            input += "FirstName varchar(50)" + "\r\n";
            input += "Email varchar(50)?" + "\r\n";
            input += "\r\n";
            input += "[Student] *-follow-* [Course]\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(2, entityRelationships.Count());

        }

        [TestMethod]
        public void Parse_EntityAndRelationshipAndEntity_Label()
        {
            var input = "[Student]" + "\r\n";
            input += "*StudentNr char(10)" + "\r\n";
            input += "LastName varchar(50)" + "\r\n";
            input += "FirstName varchar(50)" + "\r\n";
            input += "Email varchar(50)?" + "\r\n";
            input += "\r\n";
            input += "[Student] *-follow-* [Course]\r\n";
            input += "[Course]" + "\r\n";
            input += "* CourseCode char(10)" + "\r\n";
            input += "Title varchar(255)" + "\r\n";
            input += "Credit int" + "\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(3, entityRelationships.Count());

        }

        [TestMethod]
        public void Parse_TwoEntitiesAndRelationship_Label()
        {
            var input = "[Student]" + "\r\n";
            input += "*StudentNr char(10)" + "\r\n";
            input += "LastName varchar(50)" + "\r\n";
            input += "FirstName varchar(50)" + "\r\n";
            input += "Email varchar(50)?" + "\r\n";
            input += "\r\n";
            input += "[Course]" + "\r\n";
            input += "* CourseCode char(10)" + "\r\n";
            input += "Title varchar(255)" + "\r\n";
            input += "Credit int" + "\r\n";
            input += "[Student] *-follow-* [Course]\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(3, entityRelationships.Count());

        }
    }
}
