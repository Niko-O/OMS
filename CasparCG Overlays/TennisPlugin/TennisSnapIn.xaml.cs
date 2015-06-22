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
using OnUtils;
using OnUtils.Extensions;

namespace TennisPlugin
{
    /// <summary>
    /// Interaction logic for TennisSnapIn.xaml
    /// </summary>
    public partial class TennisSnapIn : UserControl
    {

        private TennisSnapInViewModel ViewModel;
        private Scoring.UndoStateList StateList;

        private string _CurrentFilter = null;
        public string CurrentFilter
        {
            get
            {
                return _CurrentFilter;
            }
            set
            {
                _CurrentFilter = value;
                LoadTemplatesFromServer();
            }
        }

        public TennisSnapIn()
        {
            InitializeComponent();
            ViewModel = (TennisSnapInViewModel)this.DataContext;
            StateList = new Scoring.UndoStateList(new Scoring.V1.V1State());
            StateList.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(StateListPropertyChanged);
            ViewModel.StateList = StateList;
            PluginInterfaces.PublicProviders.CasparServer.IsConnectedChanged += () => LoadTemplatesFromServer();
            LoadTemplatesFromServer();
        }

        public void Unload()
        {
            if (ViewModel.ScoreboardIsVisible)
            {
                ViewModel.ScoreboardIsVisible = false;
                if (PluginInterfaces.PublicProviders.CasparServer.IsConnected)
                {
                    ViewModel.SelectedTennisTemplate.HideScoreboard();
                }
            }
            if (ViewModel.LowerThirdIsVisible)
            {
                ViewModel.LowerThirdIsVisible = false;
                if (PluginInterfaces.PublicProviders.CasparServer.IsConnected)
                {
                    ViewModel.SelectedTennisTemplate.HideLowerThird();
                }
            }
            if (ViewModel.TemplateIsLoaded && PluginInterfaces.PublicProviders.CasparServer.IsConnected)
            {
                PluginInterfaces.PublicProviders.CasparServer.UnloadTemplate(ViewModel.SelectedTennisTemplate);
            }
            ViewModel.TemplateIsLoaded = false;
        }

        private void EditTemplateFilter(object sender, RoutedEventArgs e)
        {
            var Dlg = new FilterTemplatesDialog();
            Dlg.Filter = _CurrentFilter;
            if ((bool)Dlg.ShowDialog())
            {
                CurrentFilter = Dlg.Filter;
            }
        }

        private void LoadTemplatesFromServer()
        {
            IEnumerable<CasparServerCommands.TlsCommand.TemplatePath> TemplatePaths;
            if (PluginInterfaces.PublicProviders.CasparServer.IsConnected)
            {
                TemplatePaths = CasparServerCommands.TlsCommand.ParseResponse(PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.TlsCommand()));
            }
            else
            {
                TemplatePaths = new List<CasparServerCommands.TlsCommand.TemplatePath>()
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
                return;
            }
            var Filtered = _CurrentFilter == null ? TemplatePaths : TemplatePaths.Where(i => i.Directory.StartsWith(_CurrentFilter, StringComparison.InvariantCultureIgnoreCase)); ;

            var NewTemplates = new List<ServerStoredTennisTemplate>();

            var PreviouslyLoadedTemplate = ViewModel.TemplateIsLoaded ? ViewModel.SelectedTennisTemplate : null;
            var PreviouslySelectedTemplate = ViewModel.SelectedTennisTemplate as ServerStoredTennisTemplate;

            foreach (var i in Filtered)
            {
                NewTemplates.Add(new ServerStoredTennisTemplate(i));
            }

            ViewModel.AvailableTennisTemplates = PreviouslyLoadedTemplate == null ? NewTemplates : NewTemplates.Prepend(PreviouslyLoadedTemplate);

            if (PreviouslyLoadedTemplate != null)
            {
                ViewModel.SelectedTennisTemplate = PreviouslyLoadedTemplate;
            }
            else if (PreviouslySelectedTemplate != null)
            {
                ViewModel.SelectedTennisTemplate = NewTemplates.SingleOrDefault(i => i.Path.FullPath == PreviouslySelectedTemplate.Path.FullPath);
            }
        }

        private void LoadAndLockSelectedTemplate(object sender, RoutedEventArgs e)
        {
            ViewModel.TemplateIsLoaded = true;
            PluginInterfaces.PublicProviders.CasparServer.LoadTemplate(ViewModel.SelectedTennisTemplate);
        }

        private void UnlockSelectedTemplate(object sender, RoutedEventArgs e)
        {
            ViewModel.TemplateIsLoaded = false;
            PluginInterfaces.PublicProviders.CasparServer.UnloadTemplate(ViewModel.SelectedTennisTemplate);
        }

        #region Scoreboard

        private void StateListPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentState":
                    UpdateStandings();
                    break;
            }
        }

        private void ToggleScoreboardVisibility(object sender, RoutedEventArgs e)
        {
            ViewModel.ScoreboardIsVisible = !ViewModel.ScoreboardIsVisible;
            if (ViewModel.ScoreboardIsVisible)
            {
                UpdatePlayerNames();
                UpdateStandings();
                ViewModel.SelectedTennisTemplate.ShowScoreboard();
            }
            else
            {
                ViewModel.SelectedTennisTemplate.HideScoreboard();
            }
        }

        private void UpdatePlayerNames()
        {
            if (ViewModel.HasPlayerNames)
            {
                ViewModel.SelectedTennisTemplate.SetPlayerNames(ViewModel.SelectedPlayerOneName.ShortName, ViewModel.SelectedPlayerTwoName.ShortName);
            }
            else
            {
                ViewModel.SelectedTennisTemplate.SetPlayerNames(ViewModel.PlayerNameOne, ViewModel.PlayerNameTwo);
            }
        }

        private void UpdateStandings()
        {
            if (PluginInterfaces.PublicProviders.CasparServer.IsConnected)
            {
                ViewModel.SelectedTennisTemplate.SetStandings(StateList.CurrentState.Player1Set.ToString(), StateList.CurrentState.Player1Game.ToString(), StateList.CurrentState.Player1Point.ToString(),
                                                              StateList.CurrentState.Player2Set.ToString(), StateList.CurrentState.Player2Game.ToString(), StateList.CurrentState.Player2Point.ToString());
            }
        }

        #endregion

        #region Lower Third

        private void ToggleLowerThirdVisibility(object sender, RoutedEventArgs e)
        {
            ViewModel.LowerThirdIsVisible = !ViewModel.LowerThirdIsVisible;
            if (ViewModel.LowerThirdIsVisible)
            {
                int IndexOfFirstSeparator = ViewModel.SelectedTextInput.Text.IndexOf(ViewModel.TheOtherInsertsTextSeparatorChar);
                if (IndexOfFirstSeparator == -1 || IndexOfFirstSeparator == ViewModel.SelectedTextInput.Text.Length - 1)
                {
                    ViewModel.SelectedTennisTemplate.SetLowerThirdText(ViewModel.SelectedTextInput.Text, "");
                }
                else if (IndexOfFirstSeparator == 0)
                {
                    ViewModel.SelectedTennisTemplate.SetLowerThirdText("", ViewModel.SelectedTextInput.Text);
                }
                else
                {
                    ViewModel.SelectedTennisTemplate.SetLowerThirdText(ViewModel.SelectedTextInput.Text.Remove(IndexOfFirstSeparator), ViewModel.SelectedTextInput.Text.Substring(IndexOfFirstSeparator + 1));
                }
                ViewModel.SelectedTennisTemplate.ShowLowerThird();
            }
            else
            {
                ViewModel.SelectedTennisTemplate.HideLowerThird();
            }
        }

        #endregion

        #region Crawler Top

        private void ToggleCrawlerTopVisibility(object sender, RoutedEventArgs e)
        {
            ViewModel.CrawlerTopIsVisible = !ViewModel.CrawlerTopIsVisible;
            if (ViewModel.CrawlerTopIsVisible)
            {
                ViewModel.SelectedTennisTemplate.SetCrawlerText(ViewModel.SelectedTextInput.Text);
                ViewModel.SelectedTennisTemplate.ShowCrawlerTop();
            }
            else
            {
                ViewModel.SelectedTennisTemplate.HideCrawlerTop();
            }
        }

        #endregion

        #region Crawler Top

        private void ToggleCrawlerBottomVisibility(object sender, RoutedEventArgs e)
        {
            ViewModel.CrawlerBottomIsVisible = !ViewModel.CrawlerBottomIsVisible;
            if (ViewModel.CrawlerBottomIsVisible)
            {
                ViewModel.SelectedTennisTemplate.SetCrawlerText(ViewModel.SelectedTextInput.Text);
                ViewModel.SelectedTennisTemplate.ShowCrawlerBottom();
            }
            else
            {
                ViewModel.SelectedTennisTemplate.HideCrawlerBottom();
            }
        }

        #endregion

        #region Serve

        private void ToggleServePlayerOne(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsPlayerOneServe)
            {
                ViewModel.IsPlayerOneServe = false;
            }
            else
            {
                ViewModel.IsPlayerOneServe = true;
                ViewModel.IsPlayerTwoServe = false;
            }
        }

        private void ToggleServePlayerTwo(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsPlayerTwoServe)
            {
                ViewModel.IsPlayerTwoServe = false;
            }
            else
            {
                ViewModel.IsPlayerTwoServe = true;
                ViewModel.IsPlayerOneServe = false;
            }
        }

        #endregion

    }
}
