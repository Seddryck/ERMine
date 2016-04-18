using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Drawing
{
    class CommandLineOptions
    {

        [Option('f', "file", Required = true,
          HelpText = "Input file to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('t', "template", Required = false, 
          HelpText = "Template to use during the process.")]
        public string Template { get; set; }
    }
}
