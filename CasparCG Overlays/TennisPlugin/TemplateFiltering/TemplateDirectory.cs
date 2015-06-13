using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils;
using OnUtils.Wpf;

namespace TennisPlugin
{
    public class TemplateDirectory : NotifyPropertyChanged
    {

        public static TemplateDirectory RootDirectory
        {
            get
            {
                return new PRootDirectory("Root");
            }
        }
         
        private bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                ChangeIfDifferent(ref _IsSelected, value, "IsSelected");
            }
        }

        private bool _IsExpanded = false;
        public bool IsExpanded
        {
            get
            {
                return _IsExpanded;
            }
            set
            {
                ChangeIfDifferent(ref _IsExpanded, value, "IsExpanded");
            }
        }
         
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

        private TemplateDirectory Parent = null;
        
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
