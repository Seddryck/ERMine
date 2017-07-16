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
            var input = "[Student] --)- [Freshman]";
            var isaRelationship = IsaRelationshipParser.IsaRelationship.Parse(input);

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClass.Label);
        }

        [TestMethod]
        public void IsaRelationship_Reverse_TwoEntities()
        {
            var input = "[Freshman] -(-- [Student]";
            var isaRelationship = IsaRelationshipParser.IsaRelationship.Parse(input);

            Assert.AreEqual("Student", isaRelationship.SuperClass.Label);
            Assert.AreEqual("Freshman", isaRelationship.SubClass.Label);
        }

    }
}
