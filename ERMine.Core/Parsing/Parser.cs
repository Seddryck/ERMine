using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace ERMine.Core.Parsing
{
    public class Parser
    {
        private readonly ModelRepository repository = new ModelRepository();

        public Model Parse(string text)
        {
            var members = ModelParser.EntityRelationships.Parse(text);
            foreach (var member in members)
                repository.Merge(member);

            return repository.Get();
        }
    }
}
