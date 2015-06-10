using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TennisPlugin
{
    public partial class FilterTemplatesDialog : Window
    {

        public TemplateFilter Filter
        {
            get
            {
                if (ViewModel.SelectedTemplateDirectory == null)
                {
                    return null;
                }
                else
                {
                    return new TemplateFilter(ViewModel.SelectedTemplateDirectory.BuildPath());
                }
            }
        }

        private FilterTemplatesDialogViewModel ViewModel;

        public FilterTemplatesDialog()
        {
            InitializeComponent();
            ViewModel = (FilterTemplatesDialogViewModel)this.DataContext;
            LoadTemplates();
        }

        private void RefreshTemplates(object sender, RoutedEventArgs e)
        {
            LoadTemplates();
        }

        private void LoadTemplates()
        {
            IEnumerable<CasparServerCommands.TlsCommand.TemplatePath> Paths;
            if (PluginInterfaces.PublicProviders.CasparServer.IsConnected)
            {
                Paths = CasparServerCommands.TlsCommand.ParseResponse(PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.TlsCommand()));
            }
            else
            {
                Paths = new List<CasparServerCommands.TlsCommand.TemplatePath>()
                {
                    new CasparServerCommands.TlsCommand.TemplatePath("1/1.1/1.1.1/FILE_1.1.1", 1, "20141234"),
                    new CasparServerCommands.TlsCommand.TemplatePath("1/1.1/1.1.2/FILE_1.1.2", 2, "20141234"),
                    new CasparServerCommands.TlsCommand.TemplatePath("1/1.2/1.2.1/FILE_1.2.1", 3, "20141234"),
                    new CasparServerCommands.TlsCommand.TemplatePath("1/1.2/1.2.2/FILE_1.2.2", 4, "20141234"),
                    new CasparServerCommands.TlsCommand.TemplatePath("2/2.1/2.1.1/FILE_2.1.1", 5, "20141234"),
                    new CasparServerCommands.TlsCommand.TemplatePath("2/2.1/2.1.2/FILE_2.1.2", 6, "20141234"),
                    new CasparServerCommands.TlsCommand.TemplatePath("2/2.2/2.2.1/FILE_2.2.1", 7, "20141234"),
                    new CasparServerCommands.TlsCommand.TemplatePath("2/2.2/2.2.2/FILE_2.2.2", 8, "20141234")
                };
                //return;
            }
            var InvisibleRootDirectory = TemplateDirectory.RootDirectory;
            foreach (var i in Paths)
            {
                if (!String.IsNullOrEmpty(i.Directory))
                {
                    var PathParts = i.Directory.Split('/');

                    var CurrentParentDirectory = InvisibleRootDirectory;
                    TemplateDirectory CurrentDirectory;

                    foreach (var PathPart in PathParts)
                    {
                        CurrentDirectory = CurrentParentDirectory.Children.SingleOrDefault(j => j.Name == PathPart);
                        if (CurrentDirectory == null)
                        {
                            CurrentDirectory = new TemplateDirectory(PathPart);
                            CurrentParentDirectory.Add(CurrentDirectory);
                        }
                        CurrentParentDirectory = CurrentDirectory;
                    }
                }
            }
            ViewModel.TemplateDirectories = InvisibleRootDirectory.Children;
        }

        private void CloseOk(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CloseNoFilter(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedTemplateDirectory = null;
            this.DialogResult = true;
            this.Close();
        }

        private void CloseCancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void MainTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ViewModel.SelectedTemplateDirectory = (TemplateDirectory)MainTreeView.SelectedItem;
        }

    }
}
