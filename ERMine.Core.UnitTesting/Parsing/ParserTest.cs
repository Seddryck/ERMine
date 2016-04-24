using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERMine.Core.Parsing;
using Sprache;
using System.IO;
using System.Reflection;

namespace ERMine.UnitTesting.Core.Parsing
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void Parse_Simple_Correct()
        {
            var text = string.Empty;
            using (var stream = Assembly.GetExecutingAssembly()
                                           .GetManifestResourceStream("ERMine.UnitTesting.Core.Parsing.Resources.Model.txt"))
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }

            var parser = new Parser();
            var model = parser.Parse(text);

            Assert.AreEqual("Student", model.Entities[0].Label);
            Assert.AreEqual(4, model.Entities[0].Attributes.Count());
            Assert.IsFalse(model.Entities[0].IsWeak);
            Assert.AreEqual("Course", model.Entities[1].Label);
            Assert.AreEqual(3, model.Entities[1].Attributes.Count());
            Assert.AreEqual("follow", model.Relationships[0].Label);
            Assert.AreEqual("Binary", model.Relationships[0].Kind);
            Assert.AreEqual("Evaluation", model.Entities[2].Label);
            Assert.IsTrue(model.Entities[2].IsWeak);
            Assert.AreEqual(5, model.Entities[2].Attributes.Count());

        }
    }
}
