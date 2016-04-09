using Antlr4.StringTemplate;
using ERMine.Core.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ERMine.Drawing
{
    class CardinalityRenderer : IAttributeRenderer
    {
        public string ToString(object obj)
        {
            switch ((Cardinality)obj)
            {
                case Cardinality.ZeroOrOne: return "0..1";
                case Cardinality.ExactyOne: return "1..1";
                case Cardinality.ZeroOrMore: return "0..n";
                case Cardinality.OneOrMore: return "1..n";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string ToString(object obj, string formatString, CultureInfo culture)
        {
            return ToString(obj);
        }
    }
}
