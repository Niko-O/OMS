using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin
{
    public class TemplateFilter
    {

        private string _SelectedDirectory;
        public string SelectedDirectory
        {
            get
            {
                return _SelectedDirectory;
            }
        }

        public TemplateFilter(string NewSelectedDirectory)
        {
            _SelectedDirectory = NewSelectedDirectory;
        }

        public IEnumerable<CasparServerCommands.TlsCommand.TemplatePath> Filter(IEnumerable<CasparServerCommands.TlsCommand.TemplatePath> Paths)
        {
            return Paths.Where(i => i.Directory.StartsWith(_SelectedDirectory, StringComparison.InvariantCultureIgnoreCase));
        }

    }
}
