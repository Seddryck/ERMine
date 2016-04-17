using Antlr4.StringTemplate;
using CommandLine;
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
    public class Sample
    {
        public static void Main(string[] args)
        {
            var source = Parser.Default.ParseArguments<CommandLineOptions>(args).MapResult(o => { return o.InputFiles.ElementAt(0); }, (x) => { return null; });
            //var template = Parser.Default.ParseArguments<CommandLineOptions>(args).MapResult(o => { return o.Template; }, (x) => { return null; });

            var parser = new Core.Parsing.Parser();
            var model = parser.ParseFile(source);

            var text = string.Empty;
            using (var stream = Assembly.GetExecutingAssembly()
                                           .GetManifestResourceStream("ERMine.Drawing.DefaultTemplate.st"))
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

            File.WriteAllBytes("toto2.png", output);

        }
    }
}
