using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using OnUtils;
using OnUtils.Wpf;
using OnUtils.Extensions;

namespace TennisPlugin
{
    public class FilterTemplatesDialogViewModel : ViewModelBase
    {

        public bool CasparServerIsConnected
        {
            get
            {
                return (bool)(object)true || PluginInterfaces.PublicProviders.CasparServer.IsConnected;
            }
        }

        [Dependency("CasparServerIsConnected")]
        public Visibility TreeViewVisibility
        {
            get
            {
                return CasparServerIsConnected.ToVisibility();
            }
        }

        [Dependency("CasparServerIsConnected")]
        public Visibility NotConnectedMessageVisibility
        {
            get
            {
                return (!CasparServerIsConnected).ToVisibility();
            }
        }

        private IEnumerable<TemplateDirectory> _TemplateDirectories = null;
        public IEnumerable<TemplateDirectory> TemplateDirectories
        {
            get
            {
                return _TemplateDirectories;
            }
            set
            {
                ChangeIfDifferent(ref _TemplateDirectories, value, "TemplateDirectories");
            }
        }
         
        private TemplateDirectory _SelectedTemplateDirectory = null;
        public TemplateDirectory SelectedTemplateDirectory
        {
            get
            {
                return _SelectedTemplateDirectory;
            }
            set
            {
                ChangeIfDifferent(ref _SelectedTemplateDirectory, value, "SelectedTemplateDirectory");
            }
        }

        [Dependency("SelectedTemplateDirectory")]
        public bool CanCloseOk
        {
            get
            {
                return _SelectedTemplateDirectory != null;
            }
        }

        public FilterTemplatesDialogViewModel()
            : base(true)
        {
            AddExternalPropertyDependency("CasparServerIsConnected", PluginInterfaces.PublicProviders.CasparServer, "IsConnected");
            _TemplateDirectories = new[] {
                new TemplateDirectory("1",
                    new TemplateDirectory("1.1"),
                    new TemplateDirectory("1.2") { IsSelected = true, IsExpanded = true }
                ) { IsExpanded = true },
                new TemplateDirectory("2",
                    new TemplateDirectory("2.1",
                        new TemplateDirectory("2.1.1"),
                        new TemplateDirectory("2.1.2")
                    ),
                    new TemplateDirectory("2.2")
                ),
                new TemplateDirectory("3")
            };
        }

    }
}
