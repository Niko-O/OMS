using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils;
using OnUtils.Extensions;
using OnUtils.Wpf;

namespace TennisPlugin
{
    public class TennisSnapInViewModel : ViewModelBase
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

        [Dependency("TeamNameOne", "TeamNameTwo")]
        public bool CanShowGraphics
        {
            get
            {
                return !String.IsNullOrWhiteSpace(_TeamNameOne) && !String.IsNullOrWhiteSpace(_TeamNameTwo);
            }
        }

        #region Scoreboard

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

        #endregion

        #region Lower Third
         
        private bool _LowerThirdIsVisible = false;
        public bool LowerThirdIsVisible
        {
            get
            {
                return _LowerThirdIsVisible;
            }
            set
            {
                ChangeIfDifferent(ref _LowerThirdIsVisible, value, "LowerThirdIsVisible");
            }
        }
        
        private DisplayItem<LowerThirdTextEffect>[] _LowerThirdTextEffects =
        {
            DisplayItem.Create("Statischer Text", LowerThirdTextEffect.StaticText),
            DisplayItem.Create("Rechts nach links scrollen", LowerThirdTextEffect.ScrollRightToLeft)
        };
        public IEnumerable<DisplayItem<LowerThirdTextEffect>> LowerThirdTextEffects
        {
            get
            {
                return _LowerThirdTextEffects;
            }
        }

        private DisplayItem<LowerThirdTextEffect> _SelectedLowerThirdTextEffect;
        public DisplayItem<LowerThirdTextEffect> SelectedLowerThirdTextEffect
        {
            get
            {
                return _SelectedLowerThirdTextEffect;
            }
            set
            {
                ChangeIfDifferent(ref _SelectedLowerThirdTextEffect, value, "SelectedLowerThirdTextEffect");
            }
        }

        [Dependency("SelectedLowerThirdTextEffect")]
        public bool LowerThirdSettingsAreValid
        {
            get
            {
                if (_SelectedLowerThirdTextEffect == null) return false;
                return true;
            }
        }

        [Dependency("LowerThirdIsVisible", "LowerThirdSettingsAreValid")]
        public bool ToggleLowerThirdVisibilityButtonEnabled
        {
            get
            {
                return PluginInterfaces.PublicProviders.CasparServer.IsConnected && (_LowerThirdIsVisible || LowerThirdSettingsAreValid);
            }
        }

        [Dependency("LowerThirdIsVisible")]
        public string ToggleLowerThirdVisibilityButtonText
        {
            get
            {
                return _LowerThirdIsVisible ? "Ausblenden" : "Einblenden";
            }
        }

        #endregion

        public TennisSnapInViewModel()
            : base(true)
        {
            _SelectedLowerThirdTextEffect = _LowerThirdTextEffects[0];
            AvailableTennisTemplates = new[] { new DefaultTennisTemplate() };
            SelectedTennisTemplate = AvailableTennisTemplates.First();
            PluginInterfaces.PublicProviders.CasparServer.PropertyChanged += (sender, e) => {
                if (e.PropertyName == "IsConnected")
                {
                    OnPropertyChanged("CanLoadTemplate", "ToggleLowerThirdVisibilityButtonEnabled");
                }
            };
        }

        #region Test

        private string[] _Test_LowerThirdTexts = { "Hallo", "Wegen Regenwetters verschoben", "Lol", "Heute gibt es am Buffet Kuchen", "KUCHEN" };
        public string[] Test_LowerThirdTexts
        {
            get
            {
                return _Test_LowerThirdTexts;
            }
        }

        private string _Test_SelectedLowerThirdText = "Wegen Regenwetters verschoben";
        public string Test_SelectedLowerThirdText
        {
            get
            {
                return _Test_SelectedLowerThirdText;
            }
            set
            {
                ChangeIfDifferent(ref _Test_SelectedLowerThirdText, value, "Test_SelectedLowerThirdText");
            }
        }

        #endregion

    }
}
