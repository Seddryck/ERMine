﻿using System;
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
        

    }
}
