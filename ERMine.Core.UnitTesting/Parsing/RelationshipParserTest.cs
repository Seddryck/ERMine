using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ERMine.Core.Parsing;
using ERMine.Core.Modeling;
using Sprache;

namespace ERMine.UnitTesting.Core.Parsing
{
    [TestClass]
    public class RelationshipParserTest
    {
        [TestMethod]
        public void Relationship_Binary_WithLabel()
        {
            var input = "[Student] +-follow-* [Course]";
            var relationship = RelationshipBinaryParser.Relationship.Parse(input);

            Assert.AreEqual("follow", relationship.Label);
            Assert.AreEqual("Student", relationship.Entities.ElementAt(0).Label);
            Assert.AreEqual(Cardinality.OneOrMore, relationship.Cardinality.ElementAt(0));
            Assert.AreEqual("Course", relationship.Entities.ElementAt(1).Label);
            Assert.AreEqual(Cardinality.ZeroOrMore, relationship.Cardinality.ElementAt(1));
        }

        [TestMethod]
        public void Relationship_Binary_WithoutLabel()
        {
            var input = "[Post]*--1[Comment]";
            var relationship = RelationshipBinaryParser.Relationship.Parse(input);

            Assert.AreEqual(null, relationship.Label);
            Assert.AreEqual("Post", relationship.Entities.ElementAt(0).Label);
            Assert.AreEqual(Cardinality.ZeroOrMore, relationship.Cardinality.ElementAt(0));
            Assert.AreEqual("Comment", relationship.Entities.ElementAt(1).Label);
            Assert.AreEqual(Cardinality.ExactyOne, relationship.Cardinality.ElementAt(1));
        }

        [TestMethod]
        public void Relationship_Binary_Self()
        {
            var input = "[Employee] *-manages-1 [Employee]";
            var relationship = RelationshipBinaryParser.Relationship.Parse(input);

            Assert.AreEqual("Employee", relationship.Entities.ElementAt(0).Label);
            Assert.AreEqual("Employee", relationship.Entities.ElementAt(1).Label);
            Assert.IsTrue(relationship.Entities.ElementAt(0).Label == relationship.Entities.ElementAt(1).Label);
        }

        [TestMethod]
        public void Relationship_Ternary_Simple()
        {
            var input = "-SupplySchedule- [Vendor]* [Part]+ [Warehouse]+";
            var relationship = RelationshipNAryParser.Relationship.Parse(input);

            Assert.AreEqual("Vendor", relationship.Entities.ElementAt(0).Label);
            Assert.AreEqual(Cardinality.ZeroOrMore, relationship.Cardinality.ElementAt(0));
            Assert.AreEqual("Part", relationship.Entities.ElementAt(1).Label);
            Assert.AreEqual(Cardinality.OneOrMore, relationship.Cardinality.ElementAt(1));
            Assert.AreEqual("Warehouse", relationship.Entities.ElementAt(2).Label);
            Assert.AreEqual(Cardinality.OneOrMore, relationship.Cardinality.ElementAt(2));

        }

        [TestMethod]
        public void Relationship_BinaryAsNAry_Simple()
        {
            var input = "-has- [Post]* [Comment]1";
            var relationship = RelationshipNAryParser.Relationship.Parse(input);

            Assert.AreEqual("Post", relationship.Entities.ElementAt(0).Label);
            Assert.AreEqual(Cardinality.ZeroOrMore, relationship.Cardinality.ElementAt(0));
            Assert.AreEqual("Comment", relationship.Entities.ElementAt(1).Label);
            Assert.AreEqual(Cardinality.ExactyOne, relationship.Cardinality.ElementAt(1));
        }


        [TestMethod]
        public void Relationship_5ary_Simple()
        {
            var input = "-has- [Employee]* [Comment]1 [Date]1 [Witness]* [Scope]?";
            var relationship = RelationshipNAryParser.Relationship.Parse(input);

            Assert.AreEqual(5, relationship.Entities.Count());
        }

    }
}
