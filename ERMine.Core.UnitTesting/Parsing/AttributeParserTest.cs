using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ERMine.Core.Parsing;
using Sprache;

namespace ERMine.UnitTesting.Core.Parsing
{
    [TestClass]
    public class AttributeParserTest
    {
        [TestMethod]
        public void Attribute_OneWord_Label()
        {
            var input = "LastName";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("LastName", attribute.Label);

        }

        [TestMethod]
        public void Attribute_OneWordLineFeed_Label()
        {
            var input = "LastName\r\n";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("LastName", attribute.Label);

        }


        [TestMethod]
        public void Attribute_WithDataType_LabelAndDataType()
        {
            var input = "Age int";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
        }

        [TestMethod]
        public void Attribute_WithDataTypeLength_LabelAndDataType()
        {
            var input = "LastName varchar(50)";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("LastName", attribute.Label);
            Assert.AreEqual("varchar(50)", attribute.DataType);
        }

        [TestMethod]
        public void Attribute_WithDataTypePrecision_LabelAndDataType()
        {
            var input = "Salary decimal(10,2)";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("Salary", attribute.Label);
            Assert.AreEqual("decimal(10,2)", attribute.DataType);
        }

        [TestMethod]
        public void Attribute_WithDataTypeNotNullable_LabelAndDataTypeAndNullable()
        {
            var input = "Age int";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
            Assert.IsFalse(attribute.IsNullable);
        }

        [TestMethod]
        public void Attribute_WithDataTypeNullable_LabelAndDataTypeAndNullable()
        {
            var input = "Age int?";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
            Assert.IsTrue(attribute.IsNullable);
        }

        [TestMethod]
        public void Attribute_WithDataTypeSpaceNullable_LabelAndDataTypeAndNullable()
        {
            var input = "Age int ? ";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
            Assert.IsTrue(attribute.IsNullable);
        }

        [TestMethod]
        public void Attribute_Key_LabelAndDataTypeAndNullableAndKey()
        {
            var input = "*Age int?";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
            Assert.IsTrue(attribute.IsNullable);
            Assert.IsTrue(attribute.IsPartOfKey);
        }

        [TestMethod]
        public void Attribute_KeySpace_LabelAndDataTypeAndNullableAndKey()
        {
            var input = "* Age int?";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
            Assert.IsTrue(attribute.IsNullable);
            Assert.IsTrue(attribute.IsPartOfKey);
        }

        [TestMethod]
        public void Attribute_KeyFullSpace_LabelAndDataTypeAndNullableAndKey()
        {
            var input = "PK Age int?";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
            Assert.IsTrue(attribute.IsNullable);
            Assert.IsTrue(attribute.IsPartOfKey);
        }

        [TestMethod]
        public void Attribute_KeyFullNoSpace_LabelAndDataTypeAndNullableAndKey()
        {
            var input = "PKAge int?";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
            Assert.IsTrue(attribute.IsNullable);
            Assert.IsTrue(attribute.IsPartOfKey);
        }

        [TestMethod]
        public void Attribute_Nullabe_LabelAndDataTypeAndNullable()
        {
            var input = "Age int NULL";
            var attribute = AttributeParser.Attribute.Parse(input);

            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
            Assert.IsTrue(attribute.IsNullable);
        }
        
        [TestMethod]
        public void Attributes_One_UniqueAttribute()
        {
            var input = "Age int?\r\n";
            var attributes = AttributeParser.Attributes.Parse(input);
            Assert.AreEqual(attributes.Count(), 1);

            var attribute = attributes.ElementAt(0);
            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
            Assert.IsTrue(attribute.IsNullable);
        }

        [TestMethod]
        public void Attributes_Many_TwoAttributes()
        {
            var input = "Age int?\r\nSalary decimal(10,2)";
            var attributes = AttributeParser.Attributes.Parse(input);
            Assert.AreEqual(attributes.Count(), 2);

            var attribute = attributes.ElementAt(0);
            Assert.AreEqual("Age", attribute.Label);
            Assert.AreEqual("int", attribute.DataType);
            Assert.IsTrue(attribute.IsNullable);

            attribute = attributes.ElementAt(1);
            Assert.AreEqual("Salary", attribute.Label);
            Assert.AreEqual("decimal(10,2)", attribute.DataType);
            Assert.IsFalse(attribute.IsNullable);
        }

    }
}
