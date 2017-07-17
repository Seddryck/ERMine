using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ERMine.Core.Parsing;
using ERMine.Core.Modeling;
using Sprache;

namespace ERMine.UnitTesting.Core.Parsing
{
    [TestClass]
    public class IsaUnionRelationshipParserTest
    {
        [TestMethod]
        public void IsaRelationship_Simple_TwoEntities()
        {
            var input = "[Student] --|>- [Freshman]";
            var isaRelationship = IsaUnionRelationshipParser.IsaRelationship.Parse(input) as IsaRelationship;

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
        }

        [TestMethod]
        public void IsaRelationship_Reverse_TwoEntities()
        {
            var input = "[Freshman] -<|-- [Student]";
            var isaRelationship = IsaUnionRelationshipParser.IsaRelationship.Parse(input) as IsaRelationship;

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
        }
        
        [TestMethod]
        public void IsaRelationship_Disjoint_TwoEntities()
        {
            var input = "[Student] -(d)-|>- [Freshman]";
            var isaRelationship = IsaUnionRelationshipParser.IsaRelationship.Parse(input) as IsaRelationship;

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
            Assert.IsInstanceOfType(isaRelationship, typeof(IsaPartialDisjointRelationship));
        }

        [TestMethod]
        public void IsaRelationship_NamedDisjoint_TwoEntities()
        {
            var input = "[Student] -(d #1)-|>- [Freshman]";
            var isaRelationship = IsaUnionRelationshipParser.IsaRelationship.Parse(input) as IsaRelationship;

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
            var isaRelationship = IsaUnionRelationshipParser.IsaRelationship.Parse(input) as IsaRelationship;

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
            Assert.IsInstanceOfType(isaRelationship, typeof(IsaPartialOverlappingRelationship));
        }

        [TestMethod]
        public void IsaRelationship_NamedOverlapping_TwoEntities()
        {
            var input = "[Student] -(o #alpha)-|>- [Freshman]";
            var isaRelationship = IsaUnionRelationshipParser.IsaRelationship.Parse(input) as IsaRelationship;

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClasses[0].Label);
            Assert.IsInstanceOfType(isaRelationship, typeof(IsaPartialOverlappingRelationship));
            var isaOverlappingRelationship = isaRelationship as IsaPartialOverlappingRelationship;
            Assert.AreEqual("alpha", isaOverlappingRelationship.Label);
        }

        [TestMethod]
        public void UnionRelationship_Unnamed_TwoEntities()
        {
            var input = "[Person] -(u)-|>- [Owner]";
            var unionRelationship = IsaUnionRelationshipParser.IsaRelationship.Parse(input) as UnionRelationship;

            Assert.AreEqual("Person", unionRelationship.SuperClasses[0].Label);
            Assert.AreEqual("Owner", unionRelationship.SubClass.Label);
            Assert.IsInstanceOfType(unionRelationship, typeof(UnionPartialRelationship));
        }

        [TestMethod]
        public void UnionRelationship_Named_TwoEntities()
        {
            var input = "[Person] -(u #1)-|>- [Owner]";
            var unionRelationship = IsaUnionRelationshipParser.IsaRelationship.Parse(input) as UnionRelationship;

            Assert.AreEqual("Person", unionRelationship.SuperClasses[0].Label);
            Assert.AreEqual("Owner", unionRelationship.SubClass.Label);
            Assert.AreEqual("1", unionRelationship.Label);
            Assert.IsInstanceOfType(unionRelationship, typeof(UnionPartialRelationship));
        }

    }
}
