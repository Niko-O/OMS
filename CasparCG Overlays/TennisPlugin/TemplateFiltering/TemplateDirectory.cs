using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin
{
    public class TemplateDirectory
    {

        public static TemplateDirectory RootDirectory
        {
            get
            {
                return new PRootDirectory("Root");
            }
        }

        private TemplateDirectory Parent = null;

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        private List<TemplateDirectory> _Children = new List<TemplateDirectory>();
        public IEnumerable<TemplateDirectory> Children
        {
            get
            {
                return _Children;
            }
        }

        public TemplateDirectory(string NewName, params TemplateDirectory[] NewChildren)
        {
            _Name = NewName;
            foreach (var i in NewChildren)
            {
                _Children.Add(i);
            }
        }

        public void Add(TemplateDirectory Child)
        {
            _Children.Add(Child);
            Child.Parent = this;
        }

        public string BuildPath()
        {
            if (Parent == null || Parent is PRootDirectory)
            {
                return _Name;
            }
            else
            {
                return Parent.BuildPath() + "/" + _Name;
            }
        }

        private class PRootDirectory : TemplateDirectory
        {

            public PRootDirectory(string NewName, params TemplateDirectory[] NewChildren)
                : base(NewName, NewChildren)
        {
        }

        }

    }
}
