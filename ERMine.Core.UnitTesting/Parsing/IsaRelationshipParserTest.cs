using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ERMine.Core.Parsing;
using ERMine.Core.Modeling;
using Sprache;

namespace ERMine.UnitTesting.Core.Parsing
{
    [TestClass]
    public class IsaRelationshipParserTest
    {
        [TestMethod]
        public void IsaRelationship_Simple_TwoEntities()
        {
            var input = "[Student] --|>- [Freshman]";
            var isaRelationship = IsaRelationshipParser.IsaRelationship.Parse(input);

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
        }

        [TestMethod]
        public void IsaRelationship_Reverse_TwoEntities()
        {
            var input = "[Freshman] -<|-- [Student]";
            var isaRelationship = IsaRelationshipParser.IsaRelationship.Parse(input);

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
        }
        
        [TestMethod]
        public void IsaRelationship_Disjoint_TwoEntities()
        {
            var input = "[Student] -(d)-|>- [Freshman]";
            var isaRelationship = IsaRelationshipParser.IsaRelationship.Parse(input);

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
            Assert.IsInstanceOfType(isaRelationship, typeof(IsaPartialDisjointRelationship));
        }

        [TestMethod]
        public void IsaRelationship_NamedDisjoint_TwoEntities()
        {
            var input = "[Student] -(d #1)-|>- [Freshman]";
            var isaRelationship = IsaRelationshipParser.IsaRelationship.Parse(input);

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
            Assert.IsInstanceOfType(isaRelationship, typeof(IsaPartialDisjointRelationship));
            var isaDisjointRelationship = isaRelationship as IsaPartialDisjointRelationship;
            Assert.AreEqual("1", isaDisjointRelationship.Label);
        }

        [TestMethod]
        public void IsaRelationship_Overlapping_TwoEntities()
        {
            var input = "[Student] -(o)-|>- [Freshman]";
            var isaRelationship = IsaRelationshipParser.IsaRelationship.Parse(input);

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
            Assert.IsInstanceOfType(isaRelationship, typeof(IsaPartialOverlappingRelationship));
        }

        [TestMethod]
        public void IsaRelationship_NamedOverlapping_TwoEntities()
        {
            var input = "[Student] -(o #alpha)-|>- [Freshman]";
            var isaRelationship = IsaRelationshipParser.IsaRelationship.Parse(input);

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
            Assert.IsInstanceOfType(isaRelationship, typeof(IsaPartialOverlappingRelationship));
            var isaOverlappingRelationship = isaRelationship as IsaPartialOverlappingRelationship;
            Assert.AreEqual("alpha", isaOverlappingRelationship.Label);
        }

    }
}
