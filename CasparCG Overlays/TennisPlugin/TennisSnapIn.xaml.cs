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

        private TennisSnapInViewModel ViewModel;
        private TeamStats TeamOneStats = new TeamStats();
        private TeamStats TeamTwoStats = new TeamStats();

        public TennisSnapIn()
        {
            InitializeComponent();
            ViewModel = (TennisSnapInViewModel)this.DataContext;
        }

        public void Unload()
        {
            if (ViewModel.ScoreboardIsVisible)
            {
                ViewModel.ScoreboardIsVisible = false;
                ViewModel.SelectedTennisTemplate.HideScoreboard();
            }
            if (ViewModel.LowerThirdIsVisible)
            {
                ViewModel.LowerThirdIsVisible = false;
                ViewModel.SelectedTennisTemplate.HideLowerThird();
            }
            ViewModel.CanSelectTemplate = true;
            if (PluginInterfaces.PublicProviders.CasparServer.IsConnected)
            {   
                PluginInterfaces.PublicProviders.CasparServer.UnloadTemplate(ViewModel.SelectedTennisTemplate);
            }
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
            ViewModel.ScoreboardIsVisible = !ViewModel.ScoreboardIsVisible;
            if (ViewModel.ScoreboardIsVisible)
            {
                UpdatePlayerNames();
                UpdatePlayerStats();
                ViewModel.SelectedTennisTemplate.ShowScoreboard();
            }
            else
            {
                ViewModel.SelectedTennisTemplate.HideScoreboard();
            }
        }

        private void IncrementTeamOnePoints(object sender, RoutedEventArgs e)
        {
            TeamOneStats.IncrementPoints();
            UpdatePlayerStats();
        }

        private void DecrementTeamOnePoints(object sender, RoutedEventArgs e)
        {
            TeamOneStats.DecrementPoints();
            UpdatePlayerStats();
        }

        private void IncrementTeamTwoPoints(object sender, RoutedEventArgs e)
        {
            TeamTwoStats.IncrementPoints();
            UpdatePlayerStats();
        }

        private void DecrementTeamTwoPoints(object sender, RoutedEventArgs e)
        {
            TeamTwoStats.DecrementPoints();
            UpdatePlayerStats();
        }

        private void UpdatePlayerStats()
        {
            ViewModel.SelectedTennisTemplate.SetPointsOne(TeamOneStats.PointsAsString);
            ViewModel.SelectedTennisTemplate.SetGamesOne(TeamOneStats.Games);
            ViewModel.SelectedTennisTemplate.SetSetsOne(TeamOneStats.Sets);
            ViewModel.SelectedTennisTemplate.SetPointsTwo(TeamTwoStats.PointsAsString);
            ViewModel.SelectedTennisTemplate.SetGamesTwo(TeamTwoStats.Games);
            ViewModel.SelectedTennisTemplate.SetSetsTwo(TeamTwoStats.Sets);
            ViewModel.TeamOnePoints = TeamOneStats.PointsAsString;
            ViewModel.TeamOneGames = TeamOneStats.Games.ToString();
            ViewModel.TeamOneSets = TeamOneStats.Sets.ToString();
            ViewModel.TeamTwoPoints = TeamTwoStats.PointsAsString;
            ViewModel.TeamTwoGames = TeamTwoStats.Games.ToString();
            ViewModel.TeamTwoSets = TeamTwoStats.Sets.ToString();
        }

        private void UpdatePlayerNames()
        {
            ViewModel.SelectedTennisTemplate.SetTeamNameOne(ViewModel.TeamNameOne);
            ViewModel.SelectedTennisTemplate.SetTeamNameTwo(ViewModel.TeamNameTwo);
        }

        private void ToggleLowerThirdVisibility(object sender, RoutedEventArgs e)
        {
            ViewModel.LowerThirdIsVisible = !ViewModel.LowerThirdIsVisible;
            if (ViewModel.LowerThirdIsVisible)
            {
                UpdateLowerThirdParameters();
                ViewModel.SelectedTennisTemplate.ShowLowerThird();
            }
            else
            {
                ViewModel.SelectedTennisTemplate.HideLowerThird();
            }
        }

        private void UpdateLowerThirdParameters()
        {
            //ViewModel.SelectedTennisTemplate.SetLowerThirdTitleText(...);
            //ViewModel.SelectedTennisTemplate.SetLowerThirdSubtitleText(...);
            ViewModel.SelectedTennisTemplate.SetLowerThirdEffects(ViewModel.SelectedLowerThirdTextEffect.Value == LowerThirdTextEffect.ScrollRightToLeft);
        }

    }
}
