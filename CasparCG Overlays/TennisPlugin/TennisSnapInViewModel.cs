using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils;
using OnUtils.Extensions;
using OnUtils.Wpf;

namespace TennisPlugin
{
    class TennisSnapInViewModel : ViewModelBase
    {

        private bool _CanSelectTemplate = true;
        public bool CanSelectTemplate
        {
            get
            {
                return _CanSelectTemplate;
            }
            set
            {
                ChangeIfDifferent(ref _CanSelectTemplate, value, "CanSelectTemplate");
            }
        }

        private IEnumerable<ITennisTemplate> _AvailableTennisTemplates;
        public IEnumerable<ITennisTemplate> AvailableTennisTemplates
        {
            get 
            {
                return _AvailableTennisTemplates;
            }
            set
            {
                ChangeIfDifferent(ref _AvailableTennisTemplates, value, "AvailableTennisTemplates");
            }
        }

        public ITennisTemplate _SelectedTennisTemplate;
        public ITennisTemplate SelectedTennisTemplate
        {
            get
            {
                return _SelectedTennisTemplate;
            }
            set
            {
                ChangeIfDifferent(ref _SelectedTennisTemplate, value, "SelectedTennisTemplate");
            }
        }

        [Dependency("ScoreboardIsVisible")]
        public bool CanLoadTemplate
        {
            get
            {
                return !_ScoreboardIsVisible && PluginInterfaces.PublicProviders.CasparServer.IsConnected;
            }
        }

        private bool _ScoreboardIsVisible = false;
        public bool ScoreboardIsVisible
        {
            get
            {
                return _ScoreboardIsVisible;
            }
            set
            {
                ChangeIfDifferent(ref _ScoreboardIsVisible, value, "ScoreboardIsVisible");
            }
        }

        private String _TeamNameOne = "";
        public String TeamNameOne
        {
            get
            {
                return _TeamNameOne;
            }
            set
            {
                ChangeIfDifferent(ref _TeamNameOne, value, "TeamNameOne");
            }
        }

        private String _TeamNameTwo = "";
        public String TeamNameTwo
        {
            get
            {
                return _TeamNameTwo;
            }
            set
            {
                ChangeIfDifferent(ref _TeamNameTwo, value, "TeamNameTwo");
            }
        }

        private String _TeamOnePoints = "0";
        public String TeamOnePoints
        {
            get
            {
                return _TeamOnePoints;
            }
            set
            {
                ChangeIfDifferent(ref _TeamOnePoints, value, "TeamOnePoints");
            }
        }

        private String _TeamOneGames = "0";
        public String TeamOneGames
        {
            get
            {
                return _TeamOneGames;
            }
            set
            {
                ChangeIfDifferent(ref _TeamOneGames, value, "TeamOneGames");
            }
        }

        private String _TeamOneSets = "0";
        public String TeamOneSets
        {
            get
            {
                return _TeamOneSets;
            }
            set
            {
                ChangeIfDifferent(ref _TeamOneSets, value, "TeamOneSets");
            }
        }

        private String _TeamTwoPoints = "0";
        public String TeamTwoPoints
        {
            get
            {
                return _TeamTwoPoints;
            }
            set
            {
                ChangeIfDifferent(ref _TeamTwoPoints, value, "TeamTwoPoints");
            }
        }

        private String _TeamTwoGames = "0";
        public String TeamTwoGames
        {
            get
            {
                return _TeamTwoGames;
            }
            set
            {
                ChangeIfDifferent(ref _TeamTwoGames, value, "TeamTwoGames");
            }
        }

        private String _TeamTwoSets = "0";
        public String TeamTwoSets
        {
            get
            {
                return _TeamTwoSets;
            }
            set
            {
                ChangeIfDifferent(ref _TeamTwoSets, value, "TeamTwoSets");
            }
        }

        [Dependency("TeamNameOne", "TeamNameTwo")]
        public bool CanShowGraphics 
        { 
            get 
            { 
                return !String.IsNullOrWhiteSpace(_TeamNameOne) && !String.IsNullOrWhiteSpace(_TeamNameTwo);
            } 
        }

        public TennisSnapInViewModel() : base(true)
        {
            AvailableTennisTemplates = new [] {new DefaultTennisTemplate()};
            SelectedTennisTemplate = AvailableTennisTemplates.First();
            PluginInterfaces.PublicProviders.CasparServer.PropertyChanged += (sender, e) => { OnPropertyChanged("CanLoadTemplate"); };
        }

    }
}
