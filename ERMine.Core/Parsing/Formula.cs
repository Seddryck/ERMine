using ERMine.Core.Modeling;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Parsing
{
    class Formula
    {
        public static readonly Parser<string> Derived = Parse.CharExcept("{%}").AtLeastOnce().Text().Contained(Parse.String("{%"), Parse.String("%}")).Token();
    }
}
