using Antlr4.StringTemplate;
using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Factory;
using ERMine.Core.Modeling.Repository;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Drawing
{
    public class Class1
    {
        public static void Main()
        {
            var studentNr = new ERMine.Core.Modeling.Attribute() { Label = "StudentNr", IsPartOfPrimaryKey = true };
            var firstName = new ERMine.Core.Modeling.Attribute() { Label = "FirstName" };
            var lastName = new ERMine.Core.Modeling.Attribute() { Label = "LastName" };
            var fullName = new ERMine.Core.Modeling.Attribute() { Label = "FullName", IsDerived=true };
            var email = new ERMine.Core.Modeling.Attribute() { Label = "Email", IsMultiValued = true, IsNullable=true };
            var studentAttributes = new List<ERMine.Core.Modeling.Attribute>() { studentNr, firstName, lastName, fullName, email };
            var student = new EntityFactory().Create("Student", studentAttributes);

            var courseCode = new ERMine.Core.Modeling.Attribute() { Label = "CourseCode", IsPartOfPrimaryKey = true };
            var title = new ERMine.Core.Modeling.Attribute() { Label = "Title" };
            var courseAttributes = new List<ERMine.Core.Modeling.Attribute>() { courseCode, title };
            var course = new EntityFactory().Create("Course", courseAttributes);

            var follow = new RelationshipFactory().Create("follow", "Student", Cardinality.OneOrMore, "Course", Cardinality.OneOrMore);

            var position = new ERMine.Core.Modeling.Attribute() { Label = "Position", IsPartOfPartialKey = true };
            var orderLineAttributes = new List<ERMine.Core.Modeling.Attribute>() { position };
            var orderLine = new EntityFactory().Create("OrderLine", orderLineAttributes);

            var orderNumber = new ERMine.Core.Modeling.Attribute() { Label = "OrderNumber", IsPartOfPrimaryKey = true };
            var orderAttributes = new List<ERMine.Core.Modeling.Attribute>() { orderNumber };
            var order = new EntityFactory().Create("Order", orderAttributes);

            var contains = new RelationshipFactory().Create("contains", "Order", Cardinality.OneOrMore, "OrderLine", Cardinality.ExactyOne);

            var list = new List<IEntityRelationship>() { student, course, follow, orderLine, order, contains };

            var repository = new ModelRepository();
            repository.Merge(list);
            var model = repository.Get();

            var text = string.Empty;
            using (var stream = Assembly.GetExecutingAssembly()
                                           .GetManifestResourceStream("ERMine.Drawing.DefaultTemplate.txt"))
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            var group = new TemplateGroup('$', '$');
            group.RegisterRenderer(typeof(string), new StringRenderer());
            group.RegisterRenderer(typeof(Cardinality), new CardinalityRenderer());
            var template = new Template(group, text);
            template.Add("entities", model.Entities);
            template.Add("relationships", model.Relationships);
            var dot = template.Render();
            Console.WriteLine(dot);

            //// These three instances can be injected via the IGetStartProcessQuery, 
            ////                                               IGetProcessStartInfoQuery and 
            ////                                               IRegisterLayoutPluginCommand interfaces

            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);

            //// GraphGeneration can be injected via the IGraphGeneration interface

            var wrapper = new GraphGeneration(getStartProcessQuery,
                                              getProcessStartInfoQuery,
                                              registerLayoutPluginCommand);
            wrapper.RenderingEngine = Enums.RenderingEngine.Neato;
            byte[] output = wrapper.GenerateGraph(dot, Enums.GraphReturnType.Png);

            File.WriteAllBytes("toto.png", output);

        }
    }
}
