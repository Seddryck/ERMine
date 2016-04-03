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
            var relationship = RelationshipParser.Relationship.Parse(input);

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
            var relationship = RelationshipParser.Relationship.Parse(input);

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
            var relationship = RelationshipParser.Relationship.Parse(input);

            Assert.AreEqual("Employee", relationship.Entities.ElementAt(0).Label);
            Assert.AreEqual("Employee", relationship.Entities.ElementAt(1).Label);
            Assert.IsTrue(relationship.Entities.ElementAt(0).Label == relationship.Entities.ElementAt(1).Label);
        }

    }
}
