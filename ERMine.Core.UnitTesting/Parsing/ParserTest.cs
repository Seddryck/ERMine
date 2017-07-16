using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERMine.Core.Parsing;
using Sprache;
using System.IO;
using System.Reflection;
using ERMine.Core.Modeling;

namespace ERMine.UnitTesting.Core.Parsing
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void Parse_University_Correct()
        {
            var text = string.Empty;
            using (var stream = Assembly.GetExecutingAssembly()
                                           .GetManifestResourceStream("ERMine.UnitTesting.Core.Parsing.Resources.University.txt"))
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }

            var parser = new Parser();
            var model = parser.Parse(text);

            var student = model.Entities.SingleOrDefault(e => e.Label == "Student");
            Assert.IsNotNull(student);
            Assert.AreEqual(4, student.Attributes.Count());
            Assert.IsFalse(student.IsWeak);

            var course = model.Entities.SingleOrDefault(e => e.Label == "Course");
            Assert.IsNotNull(course);
            Assert.AreEqual(4, course.Attributes.Count());
            Assert.AreEqual("CourseType", course.Attributes[3].DataType);
            Assert.AreEqual("CourseType", course.Attributes[3].Domain.Label);
            Assert.IsNull(course.Attributes[2].Domain);

            Assert.AreEqual("follow", model.Relationships[0].Label);
            Assert.AreEqual("Binary", model.Relationships[0].Kind);

            var evaluation = model.Entities.SingleOrDefault(e => e.Label == "Evaluation");
            Assert.IsNotNull(evaluation);
            Assert.IsTrue(evaluation.IsWeak);
            Assert.AreEqual(5, evaluation.Attributes.Count());

            Assert.AreEqual(1, model.Domains.Count);
            Assert.AreEqual("CourseType", model.Domains[0].Label);
            Assert.AreEqual(2, model.Domains[0].Values.Count);

            var foreignStudent = model.Entities.SingleOrDefault(e => e.Label == "Foreign student");
            Assert.IsNotNull(foreignStudent);

            Assert.AreEqual(1, model.IsaRelationships.Count);
            Assert.AreEqual("Student", model.IsaRelationships[0].SuperClass.Label);
            Assert.AreEqual("Foreign student", model.IsaRelationships[0].SubClasses[0].Label);
        }

        [TestMethod]
        public void Parse_Engineering_Correct()
        {
            var text = string.Empty;
            using (var stream = Assembly.GetExecutingAssembly()
                                           .GetManifestResourceStream("ERMine.UnitTesting.Core.Parsing.Resources.Engineering.txt"))
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            var parser = new Parser();
            var model = parser.Parse(text);
            
            Assert.AreEqual(7, model.Entities.Count);
            Assert.AreEqual(3, model.IsaRelationships.Count);
        }

        [TestMethod]
        public void Parse_Part_Correct()
        {
            var text = string.Empty;
            using (var stream = Assembly.GetExecutingAssembly()
                                           .GetManifestResourceStream("ERMine.UnitTesting.Core.Parsing.Resources.Part.txt"))
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            var parser = new Parser();
            var model = parser.Parse(text);

            Assert.AreEqual(3, model.Entities.Count);
            Assert.AreEqual(1, model.IsaRelationships.Count);

            var part = model.Entities.Single(e => e.Label == "Part");
            Assert.AreEqual(2, part.Attributes.Count);

            var manufactured = model.Entities.Single(e => e.Label == "Manufactured part");
            Assert.AreEqual(3, manufactured.Attributes.Count);

            Assert.AreEqual(part, manufactured.IsA[0].SuperClass);
            Assert.AreEqual("Overlapping", manufactured.IsA[0].Kind);
        }
    }
}
