using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils;
using OnUtils.Extensions;
using OnUtils.Wpf;
using System.Collections.ObjectModel;

namespace TennisPlugin
{
    public class TennisSnapInViewModel : ViewModelBase
    {
         
        private bool _TemplateIsLoaded = false;
        public bool TemplateIsLoaded
        {
            get
            {
                return _TemplateIsLoaded;
            }
            set
            {
                ChangeIfDifferent(ref _TemplateIsLoaded, value, "TemplateIsLoaded");
            }
        }

        private bool _CanSelectTemplate = true;
        [Dependency("TemplateIsLoaded")]
        public bool CanSelectTemplate
        {
            get
            {
                return _CanSelectTemplate;
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

        [Dependency("TeamNameOne", "TeamNameTwo", "TemplateIsLoaded")]
        public bool CanShowGraphics
        {
            get
            {
                return PluginInterfaces.PublicProviders.CasparServer.IsConnected && _TemplateIsLoaded && !String.IsNullOrWhiteSpace(_TeamNameOne) && !String.IsNullOrWhiteSpace(_TeamNameTwo);
            }
        }

        #region Scoreboard
        
        private Scoring.UndoStateList _StateList = null;
        public Scoring.UndoStateList StateList
        {
            get
            {
                return _StateList;
            }
            set
            {
                ChangeIfDifferent(ref _StateList, value, "StateList");
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
         
        private bool _CanUndoScoring = false;
        public bool CanUndoScoring
        {
            get
            {
                return _CanUndoScoring;
            }
            set
            {
                ChangeIfDifferent(ref _CanUndoScoring, value, "CanUndoScoring");
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
        
        //[Dependency("")]
        public bool LowerThirdSettingsAreValid
        {
            get
            {
                //ToDo
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

        private bool _ApplyLowerThirdVisibilityDuration = false;
        public bool ApplyLowerThirdVisibilityDuration
        {
            get
            {
                return _ApplyLowerThirdVisibilityDuration;
            }
            set
            {
                ChangeIfDifferent(ref _ApplyLowerThirdVisibilityDuration, value, "ApplyLowerThirdVisibilityDuration");
            }
        }

        private string _LowerThirdVisibilityDurationString = "0";
        public string LowerThirdVisibilityDurationString
        {
            get
            {
                return _LowerThirdVisibilityDurationString;
            }
            set
            {
                if (ChangeIfDifferent(ref _LowerThirdVisibilityDurationString, value))
                {
                    _LowerThirdVisibilityDurationIsValid = int.TryParse(_LowerThirdVisibilityDurationString, out _LowerThirdVisibilityDuration) && _LowerThirdVisibilityDuration > 0;
                    OnPropertyChanged("LowerThirdVisibilityDurationString");
                }
            }
        }

        private bool _LowerThirdVisibilityDurationIsValid = false;
        [Dependency("LowerThirdVisibilityDurationString")]
        public bool LowerThirdVisibilityDurationIsValid
        {
            get
            {
                return _LowerThirdVisibilityDurationIsValid;
            }
        }

        private int _LowerThirdVisibilityDuration = -1;
        [Dependency("LowerThirdVisibilityDurationString")]
        public int LowerThirdVisibilityDuration
        {
            get
            {
                return _LowerThirdVisibilityDuration;
            }
        }

        private int _LowerThirdTextInputCount = 0;
        public int LowerThirdTextInputCount
        {
            get
            {
                return _LowerThirdTextInputCount;
            }
            set
            {
                if (ChangeIfDifferent(ref _LowerThirdTextInputCount, value))
                {
                    while (_LowerThirdTextInputs.Count < _LowerThirdTextInputCount)
                    {
                        var Temp = new LowerThirdTextInput();
                        Temp.IsSelected = _LowerThirdTextInputs.Count == 0;
                        Temp.Selected += new EventHandler(TextInputSelected);
                        _LowerThirdTextInputs.Add(Temp);
                    }
                    while (_LowerThirdTextInputs.Count > _LowerThirdTextInputCount)
                    {
                        _LowerThirdTextInputs[_LowerThirdTextInputs.Count - 1].Selected -= new EventHandler(TextInputSelected);
                        _LowerThirdTextInputs.RemoveAt(_LowerThirdTextInputs.Count - 1);
                    }
                    OnPropertyChanged("TextInputCount");
                }
            }
        }
        
        private ObservableCollection<LowerThirdTextInput> _LowerThirdTextInputs = new ObservableCollection<LowerThirdTextInput>();
        public ObservableCollection<LowerThirdTextInput> LowerThirdTextInputs
        {
            get
            {
                return _LowerThirdTextInputs;
            }
        }

        private char _LowerThirdTextSeparatorChar = '#';
        public char LowerThirdTextSeparatorChar
        {
            get
            {
                return _LowerThirdTextSeparatorChar;
            }
            set
            {
                ChangeIfDifferent(ref _LowerThirdTextSeparatorChar, value, "LowerThirdTextSeparatorChar");
            }
        }
        
        private bool _LowerThirdTextInputIsLocked = false;
        public bool LowerThirdTextInputIsLocked
        {
            get
            {
                return _LowerThirdTextInputIsLocked;
            }
            set
            {
                if (ChangeIfDifferent(ref _LowerThirdTextInputIsLocked, value))
                {
                    foreach (var i in _LowerThirdTextInputs)
                    {
                        i.RadioButtonIsEnabled = !_LowerThirdTextInputIsLocked;
                        i.TextBoxIsEnabled = !i.IsSelected || !_LowerThirdTextInputIsLocked;
                    }
                    OnPropertyChanged("LowerThirdTextInputIsLocked");
                }
            }
        }
        
        #endregion

        public TennisSnapInViewModel()
            : base(true)
        {
            AddExternalPropertyDependency("CanLoadTemplate", PluginInterfaces.PublicProviders.CasparServer, "IsConnected");
            AddExternalPropertyDependency("ToggleLowerThirdVisibilityButtonEnabled", PluginInterfaces.PublicProviders.CasparServer, "IsConnected");
            AddExternalPropertyDependency("CanShowGraphics", PluginInterfaces.PublicProviders.CasparServer, "IsConnected");
            LowerThirdTextInputCount = 5;
        }

        private void TextInputSelected(object sender, EventArgs e)
        {
            foreach (var i in _LowerThirdTextInputs)
            {
                if (i != sender)
                {
                    ((LowerThirdTextInput)i).IsSelected = false;
                }
            }
        }

    }
}
