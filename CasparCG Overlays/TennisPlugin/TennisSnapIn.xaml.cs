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
        private Scoring.UndoStateList StateList;
        //private TeamStats TeamOneStats = new TeamStats();
        //private TeamStats TeamTwoStats = new TeamStats();

        public TennisSnapIn()
        {
            InitializeComponent();
            ViewModel = (TennisSnapInViewModel)this.DataContext;
            StateList = new Scoring.UndoStateList(new Scoring.V1.TennisScoringStrategyV1());
            ViewModel.StateList = StateList;
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
            //TeamOneStats.IncrementPoints();
            StateList.Process(Scoring.ScoringStrategyAction.Player1Scored);
            UpdatePlayerStats();
        }

        private void DecrementTeamOnePoints(object sender, RoutedEventArgs e)
        {
            //TeamOneStats.DecrementPoints();
            //UpdatePlayerStats();
        }

        private void IncrementTeamTwoPoints(object sender, RoutedEventArgs e)
        {
            //TeamTwoStats.IncrementPoints();
            StateList.Process(Scoring.ScoringStrategyAction.Player2Scored);
            UpdatePlayerStats();
        }

        private void DecrementTeamTwoPoints(object sender, RoutedEventArgs e)
        {
            //TeamTwoStats.DecrementPoints();
            //UpdatePlayerStats();
        }

        private void UndoScoring(object sender, RoutedEventArgs e)
        {
            StateList.Undo();
            UpdatePlayerStats();
        }

        private void UpdatePlayerStats()
        {
            //ViewModel.SelectedTennisTemplate.SetPointsOne(TeamOneStats.PointsAsString);
            //ViewModel.SelectedTennisTemplate.SetGamesOne(TeamOneStats.Games);
            //ViewModel.SelectedTennisTemplate.SetSetsOne(TeamOneStats.Sets);
            //ViewModel.SelectedTennisTemplate.SetPointsTwo(TeamTwoStats.PointsAsString);
            //ViewModel.SelectedTennisTemplate.SetGamesTwo(TeamTwoStats.Games);
            //ViewModel.SelectedTennisTemplate.SetSetsTwo(TeamTwoStats.Sets);
            //ViewModel.TeamOnePoints = TeamOneStats.PointsAsString;
            //ViewModel.TeamOneGames = TeamOneStats.Games.ToString();
            //ViewModel.TeamOneSets = TeamOneStats.Sets.ToString();
            //ViewModel.TeamTwoPoints = TeamTwoStats.PointsAsString;
            //ViewModel.TeamTwoGames = TeamTwoStats.Games.ToString();
            //ViewModel.TeamTwoSets = TeamTwoStats.Sets.ToString();
            if (PluginInterfaces.PublicProviders.CasparServer.IsConnected)
            {
                ViewModel.SelectedTennisTemplate.SetPointsOne(StateList.CurrentState.Player1Point.ToDisplayString());
                ViewModel.SelectedTennisTemplate.SetGamesOne(StateList.CurrentState.Player1Game);
                ViewModel.SelectedTennisTemplate.SetSetsOne(StateList.CurrentState.Player1Set);
                ViewModel.SelectedTennisTemplate.SetPointsTwo(StateList.CurrentState.Player2Point.ToDisplayString());
                ViewModel.SelectedTennisTemplate.SetGamesTwo(StateList.CurrentState.Player2Game);
                ViewModel.SelectedTennisTemplate.SetSetsTwo(StateList.CurrentState.Player2Set);
            }
            ViewModel.TeamOnePoints = StateList.CurrentState.Player1Point.ToDisplayString();
            ViewModel.TeamOneGames = StateList.CurrentState.Player1Game.ToString();
            ViewModel.TeamOneSets = StateList.CurrentState.Player1Set.ToString();
            ViewModel.TeamTwoPoints = StateList.CurrentState.Player2Point.ToDisplayString();
            ViewModel.TeamTwoGames = StateList.CurrentState.Player2Game.ToString();
            ViewModel.TeamTwoSets = StateList.CurrentState.Player2Set.ToString();
            ViewModel.CanUndoScoring = StateList.CanUndo;
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
            var SelectedTextLine = ViewModel.LowerThirdTextInputs.SingleOrDefault(i => i.IsSelected);
            if (SelectedTextLine == null)
            {
                ViewModel.SelectedTennisTemplate.SetLowerThirdTitleText("");
                ViewModel.SelectedTennisTemplate.SetLowerThirdSubtitleText("");
            }
            else
            {
                int IndexOfFirstSeparator = SelectedTextLine.Text.IndexOf(ViewModel.LowerThirdTextSeparatorChar);
                if (IndexOfFirstSeparator == -1 || IndexOfFirstSeparator == SelectedTextLine.Text.Length - 1)
                {
                    ViewModel.SelectedTennisTemplate.SetLowerThirdTitleText(SelectedTextLine.Text);
                    ViewModel.SelectedTennisTemplate.SetLowerThirdSubtitleText("");
                }
                else if (IndexOfFirstSeparator == 0)
                {
                    ViewModel.SelectedTennisTemplate.SetLowerThirdTitleText("");
                    ViewModel.SelectedTennisTemplate.SetLowerThirdSubtitleText(SelectedTextLine.Text);
                }
                else
                {
                    ViewModel.SelectedTennisTemplate.SetLowerThirdTitleText(SelectedTextLine.Text.Remove(IndexOfFirstSeparator));
                    ViewModel.SelectedTennisTemplate.SetLowerThirdSubtitleText(SelectedTextLine.Text.Substring(IndexOfFirstSeparator + 1));
                }
            }
            //ViewModel.SelectedTennisTemplate.SetLowerThirdEffects(ViewModel.SelectedLowerThirdTextEffect.Value == LowerThirdTextEffect.ScrollRightToLeft);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(1, 1, "foo", FooBox.Text));
        }

    }
}
