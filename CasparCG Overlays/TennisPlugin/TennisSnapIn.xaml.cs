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
    /// <summary>
    /// Interaction logic for TennisSnapIn.xaml
    /// </summary>
    public partial class TennisSnapIn : UserControl
    {

        TennisSnapInViewModel ViewModel;

        public TennisSnapIn()
        {
            InitializeComponent();
            ViewModel = (TennisSnapInViewModel)this.DataContext;
        }

        private void LoadAndLockSelectedTemplate(object sender, RoutedEventArgs e)
        {
            ViewModel.CanSelectTemplate = false;
            PluginInterfaces.PublicProviders.CasparServer.LoadTemplate(ViewModel.SelectedTennisTemplate);
        }

        private void UnlockSelectedTemplate(object sender, RoutedEventArgs e)
        {
            ViewModel.CanSelectTemplate = true;
            PluginInterfaces.PublicProviders.CasparServer.UnloadTemplate(ViewModel.SelectedTennisTemplate);
        }

        private void ToggleScoreboardVisibility(object sender, RoutedEventArgs e)
        {
            ViewModel.GraphicsIsVisible = !ViewModel.GraphicsIsVisible;
            var SelectedTemplate = ViewModel.SelectedTennisTemplate;
            if (ViewModel.GraphicsIsVisible)
            {
                SelectedTemplate.ShowScoreboard();
            }
            else
            {
                SelectedTemplate.HideScoreboard();
            }
        }

    }
}
