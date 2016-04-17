using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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

        public Model ParseFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(string.Format("File not found '{0}'", path));

            var text = File.ReadAllText(path);

            return Parse(text);
        }

        public Model ParseFiles(IEnumerable<string> paths)
        {
            foreach (var path in paths)
                ParseFile(path);
            return repository.Get();
        }
    }
}
