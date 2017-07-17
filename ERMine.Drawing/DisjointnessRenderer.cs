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
    class DisjointnessRenderer : IAttributeRenderer
    {
        public string ToString(object obj)
        {
            switch ((DisjointnessType)obj)
            {
                case DisjointnessType.Disjoint: return "d";
                case DisjointnessType.Overlapping: return "o";
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
