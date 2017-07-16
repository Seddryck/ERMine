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
            input += "\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(1, entityRelationships.Count());
            Assert.AreEqual(1, entityRelationships.Count(e => e is Entity));
            Assert.AreEqual(4, (entityRelationships.ElementAt(0) as Entity).SpecificAttributes.Count);

        }

        [TestMethod]
        public void Parse_TwoEntities_Label()
        {
            var input = "[Student]" + "\r\n";
            input += "*StudentNr char(10)" + "\r\n";
            input += "\r\n";
            input += "[Course]" + "\r\n";
            input += "*CourseNr char(10)" + "\r\n";
            input += "\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(2, entityRelationships.Count());
            Assert.AreEqual(2, entityRelationships.Count(e => e is Entity));
            Assert.AreEqual(1, (entityRelationships.ElementAt(0) as Entity).SpecificAttributes.Count);
            Assert.AreEqual(1, (entityRelationships.ElementAt(1) as Entity).SpecificAttributes.Count);

        }

        [TestMethod]
        public void Parse_UniqueDomain_Label()
        {
            var input = "<WeekDay>" + "\r\n";
            input += "Monday" + "\r\n";
            input += "'Tuesday'" + "\r\n";
            input += "Wednesday" + "\r\n";
            input += "Thursday";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(1, entityRelationships.Count());
            Assert.AreEqual(1, entityRelationships.Count(d => d is Domain));
            Assert.AreEqual(4, (entityRelationships.ElementAt(0) as Domain).Values.Count);

        }

        [TestMethod]
        public void Parse_TwoDomains_Label()
        {
            var input = "<WeekDay>" + "\r\n";
            input += "Monday" + "\r\n";
            input += "'Tuesday'" + "\r\n";
            input += "Wednesday" + "\r\n";
            input += "Thursday" +"\r\n";
            input += "Friday" + "\r\n";
            input += "\r\n";
            input += "<WeekendDay>" + "\r\n";
            input += "Saturday" + "\r\n";
            input += "'Sunday'" + "\r\n";


            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(2, entityRelationships.Count());
            Assert.AreEqual(2, entityRelationships.Count(d => d is Domain));
            Assert.AreEqual(5, (entityRelationships.ElementAt(0) as Domain).Values.Count);
            Assert.AreEqual(2, (entityRelationships.ElementAt(1) as Domain).Values.Count);

        }

        [TestMethod]
        public void Parse_EntityAndDomain_Label()
        {
            var input = "";
            input += "<Weekday>" + "\r\n";
            input += "Monday" + "\r\n";
            input += "Tuesday" + "\r\n";
            input += "\r\n";
            input += "[Student]" + "\r\n";
            input += "*StudentNr char(10)" + "\r\n";
            input += "LastName varchar(50)" + "\r\n";
            input += "FirstName varchar(50)" + "\r\n";
            input += "Email varchar(50)?" + "\r\n";
            input += "\r\n";
            

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(2, entityRelationships.Count());
            Assert.AreEqual(1, entityRelationships.Count(e => e is Entity));
            Assert.AreEqual(1, entityRelationships.Count(d => d is Domain));

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
            input += "[Student] *-follow-* [Course]";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(2, entityRelationships.Count());
            Assert.AreEqual(1, entityRelationships.Count(e => e is Entity));
            Assert.AreEqual(1, entityRelationships.Count(r => r is Relationship));

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
            input += "\r\n";
            input += "[Course]" + "\r\n";
            input += "* CourseCode char(10)" + "\r\n";
            input += "Title varchar(255)" + "\r\n";
            input += "Credit int" + "\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(3, entityRelationships.Count());
            Assert.AreEqual(2, entityRelationships.Count(e => e is Entity));
            Assert.AreEqual(1, entityRelationships.Count(r => r is Relationship));

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
            input += "Title varchar(255)#" + "\r\n";
            input += "Credit int" + "\r\n";
            input += "[Student] *-follow-* [Course]\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(3, entityRelationships.Count());
            Assert.AreEqual(2, entityRelationships.Count(e => e is Entity));
            Assert.AreEqual(1, entityRelationships.Count(r => r is Relationship));

        }

        [TestMethod]
        public void Parse_TwoRelationships_Label()
        {
            var input = "[Student] *-isfriend-* [Student]" + "\r\n";
            input += "[Student] *-follow-* [Course]\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(2, entityRelationships.Count());
            Assert.AreEqual(2, entityRelationships.Count(r => r is Relationship));

        }

        [TestMethod]
        public void Parse_OneTernary_Label()
        {
            var input = "-SupplySchedule- [Vendor]* [Part]+ [Warehouse]+\r\n";
            input += "[Student] *-follow-* [Course]";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(2, entityRelationships.Count());
            Assert.AreEqual(2, entityRelationships.Count(r => r is Relationship));
        }

        [TestMethod]
        public void Parse_ThreeRelationships_Label()
        {
            var input = "[Student] *-isfriend-* [Student]" + "\r\n";
            input += "[Student] *-follow-* [Course]\r\n";
            input += "-SupplySchedule- [Vendor]* [Part]+ [Warehouse]+";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(3, entityRelationships.Count());
            Assert.AreEqual(3, entityRelationships.Count(r => r is Relationship));

        }

        [TestMethod]
        public void Parse_ThreeEntitiesAndTernaryRelationship_Label()
        {
            var input = "[Warehouse]" + "\r\n";
            input += "-SupplySchedule- [Vendor]* [Part]+ [Warehouse]+" + "\r\n";
            input += "[Part]" + "\r\n";
            input += "[Vendor]" + "\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(3, entityRelationships.Count(e => e is Entity));
            Assert.AreEqual(1, entityRelationships.Count(r => r is Relationship));

        }

        [TestMethod]
        public void Parse_TwiceThreeRelationship_Label()
        {
            var input = "[Warehouse]" + "\r\n";
            input += "-SupplySchedule- [Vendor]* [Part]+ [Warehouse]+" + "\r\n";
            input += "[Part]" + "\r\n";
            input += "[Vendor]" + "\r\n";
            input += "[Student] *-isfriend-* [Student]" + "\r\n";
            input += "[Student] *-follow-* [Course]\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(3, entityRelationships.Count(e => e is Entity));
            Assert.AreEqual(3, entityRelationships.Count(r => r is Relationship));

        }

        [TestMethod]
        public void Parse_TwiceThree2Relationship_Label()
        {
            var input = "[Warehouse]" + "\r\n";
            input += "-SupplySchedule- [Vendor]* [Part]+ [Warehouse]+" + "\r\n";
            input += "\r\n";
            input += "[Student] *-isfriend-* [Student]" + "\r\n";
            input += "[Student] *-follow-* [Course]\r\n";
            input += "[Part]" + "\r\n";
            input += "[Vendor]";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(3, entityRelationships.Count(e => e is Entity));
            Assert.AreEqual(3, entityRelationships.Count(r => r is Relationship));

        }

        [TestMethod]
        public void Parse_TwoSuperClasses_AttributesCount()
        {
            var input = "[Student]" + "\r\n";
            input += "StudentNo" + "\r\n";
            input += "\r\n";
            input += "[Freshman]" + "\r\n";
            input += "Motivation";
            input += "\r\n";
            input += "[Foreign Student]" + "\r\n";
            input += "Country" + "\r\n";
            input += "\r\n";
            input += "[Foreign Student] -<|-- [Freshman]" + "\r\n";
            input += "[Student] -<|-- [Freshman]" + "\r\n";

            var entityRelationships = ModelParser.EntityRelationships.Parse(input);

            Assert.AreEqual(3, entityRelationships.Count(e => e is Entity));
            Assert.AreEqual(2, entityRelationships.Count(r => r is IsaRelationship));
        }

    }
}
