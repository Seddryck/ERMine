using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Parsing
{
    class Keyword
    {
        public static readonly Parser<string> IsNullable = Parse.String("?").Text().Or(Parse.String("NULL").Text().Token());
        public static readonly Parser<string> IsPartOfKey = Parse.String("*").Text().Or(Parse.String("PK").Text());
    }
}
