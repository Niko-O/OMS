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

        public bool CasparServerIsConnected
        {
            get
            {
                return PluginInterfaces.PublicProviders.CasparServer.IsConnected;
            }
        }

        [Dependency("CasparServerIsConnected", "ScoreboardIsVisible")]
        public bool CanLoadTemplate
        {
            get
            {
                return !_ScoreboardIsVisible && PluginInterfaces.PublicProviders.CasparServer.IsConnected;
            }
        }

        [Dependency("CasparServerIsConnected", "PlayerNameOne", "PlayerNameTwo", "TemplateIsLoaded")]
        public bool CanShowGraphics
        {
            get
            {
                return PluginInterfaces.PublicProviders.CasparServer.IsConnected && _TemplateIsLoaded && !String.IsNullOrWhiteSpace(_PlayerNameOne) && !String.IsNullOrWhiteSpace(_PlayerNameTwo);
            }
        }

        #region Scoreboard

        private DelegateCommand _Player1ScoredCommand;
        public DelegateCommand Player1ScoredCommand
        {
            get
            {
                return _Player1ScoredCommand;
            }
        }

        private DelegateCommand _Player1ReducedCommand;
        public DelegateCommand Player1ReducedCommand
        {
            get
            {
                return _Player1ReducedCommand;
            }
        }

        private DelegateCommand _Player2ScoredCommand;
        public DelegateCommand Player2ScoredCommand
        {
            get
            {
                return _Player2ScoredCommand;
            }
        }

        private DelegateCommand _Player2ReducedCommand;
        public DelegateCommand Player2ReducedCommand
        {
            get
            {
                return _Player2ReducedCommand;
            }
        }

        private DelegateCommand _UndoCommand;
        public DelegateCommand UndoCommand
        {
            get
            {
                return _UndoCommand;
            }
        }

        public bool CanToggleIsTieBreakEnabled
        {
            get
            {
                if (_StateList == null) return false;
                if (IsTieBreakEnabled)
                {
                    return _StateList.CanProcess(Scoring.ScoringStrategyAction.DisableTieBreak);
                }
                else
                {
                    return _StateList.CanProcess(Scoring.ScoringStrategyAction.EnableTieBreak);
                }
            }
        }

        public bool IsTieBreakEnabled
        {
            get
            {
                if (_StateList == null) return false;
                return _StateList.CurrentState.IsTieBreakEnabled;
            }
            set
            {
                if (_StateList == null) return;
                if (value == IsTieBreakEnabled) return;
                if (value)
                {
                    _StateList.Process(Scoring.ScoringStrategyAction.EnableTieBreak);
                }
                else
                {
                    _StateList.Process(Scoring.ScoringStrategyAction.DisableTieBreak);
                }
                OnPropertyChanged("IsTieBreakEnabled");
            }
        }

        private Scoring.UndoStateList _StateList = null;
        public Scoring.UndoStateList StateList
        {
            get
            {
                return _StateList;
            }
            set
            {
                if (_StateList != value)
                {
                    if (_StateList != null)
                    {
                        _StateList.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(StateListPropertyChanged);
                    }
                    _StateList = value;
                    if (_StateList != null)
                    {
                        _StateList.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(StateListPropertyChanged);
                    }
                    OnPropertyChanged("StateList");
                    UpdateStateListCommands();
                }
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

        private String _PlayerNameOne = "";
        public String PlayerNameOne
        {
            get
            {
                return _PlayerNameOne;
            }
            set
            {
                ChangeIfDifferent(ref _PlayerNameOne, value, "PlayerNameOne");
            }
        }

        private String _PlayerNameTwo = "";
        public String PlayerNameTwo
        {
            get
            {
                return _PlayerNameTwo;
            }
            set
            {
                ChangeIfDifferent(ref _PlayerNameTwo, value, "PlayerNameTwo");
            }
        }

        #endregion

        #region TheOtherInserts

        private int _TheOtherInsertsTextInputCount = 0;
        public int TheOtherInsertsTextInputCount
        {
            get
            {
                return _TheOtherInsertsTextInputCount;
            }
            set
            {
                if (ChangeIfDifferent(ref _TheOtherInsertsTextInputCount, value))
                {
                    while (_TheOtherInsertsTextInputs.Count < _TheOtherInsertsTextInputCount)
                    {
                        var Temp = new TheOtherInsertsTextInput();
                        if (_TheOtherInsertsTextInputs.Count == 0)
                        {
                            Temp.IsSelected = true;
                            SelectedTextInput = Temp;
                        }
                        Temp.Selected += new EventHandler(TextInputSelected);
                        _TheOtherInsertsTextInputs.Add(Temp);
                    }
                    while (_TheOtherInsertsTextInputs.Count > _TheOtherInsertsTextInputCount)
                    {
                        _TheOtherInsertsTextInputs[_TheOtherInsertsTextInputs.Count - 1].Selected -= new EventHandler(TextInputSelected);
                        _TheOtherInsertsTextInputs.RemoveAt(_TheOtherInsertsTextInputs.Count - 1);
                    }
                    OnPropertyChanged("TextInputCount");
                }
            }
        }

        private ObservableCollection<TheOtherInsertsTextInput> _TheOtherInsertsTextInputs = new ObservableCollection<TheOtherInsertsTextInput>();
        public ObservableCollection<TheOtherInsertsTextInput> TheOtherInsertsTextInputs
        {
            get
            {
                return _TheOtherInsertsTextInputs;
            }
        }

        private char _TheOtherInsertsTextSeparatorChar = '#';
        public char TheOtherInsertsTextSeparatorChar
        {
            get
            {
                return _TheOtherInsertsTextSeparatorChar;
            }
            set
            {
                ChangeIfDifferent(ref _TheOtherInsertsTextSeparatorChar, value, "TheOtherInsertsTextSeparatorChar");
            }
        }

        private bool _TheOtherInsertsTextInputIsLocked = false;
        public bool TheOtherInsertsTextInputIsLocked
        {
            get
            {
                return _TheOtherInsertsTextInputIsLocked;
            }
            set
            {
                if (ChangeIfDifferent(ref _TheOtherInsertsTextInputIsLocked, value))
                {
                    foreach (var i in _TheOtherInsertsTextInputs)
                    {
                        i.RadioButtonIsEnabled = !_TheOtherInsertsTextInputIsLocked;
                        i.TextBoxIsEnabled = !i.IsSelected || !_TheOtherInsertsTextInputIsLocked;
                    }
                    OnPropertyChanged("TheOtherInsertsTextInputIsLocked");
                }
            }
        }

        private void TextInputSelected(object sender, EventArgs e)
        {
            foreach (var i in _TheOtherInsertsTextInputs)
            {
                if (i != sender)
                {
                    ((TheOtherInsertsTextInput)i).IsSelected = false;
                }
            }
            SelectedTextInput = (TheOtherInsertsTextInput)sender;
        }

        private TheOtherInsertsTextInput _SelectedTextInput = null;
        public TheOtherInsertsTextInput SelectedTextInput
        {
            get
            {
                return _SelectedTextInput;
            }
            set
            {
                if (_SelectedTextInput != null)
                {
                    _SelectedTextInput.TextChanged -= new EventHandler(TextInputTextChanged);
                }
                _SelectedTextInput = value;
                if (_SelectedTextInput != null)
                {
                    _SelectedTextInput.TextChanged += new EventHandler(TextInputTextChanged);
                }
                OnPropertyChanged("SelectedTextInput");
            }
        }

        private void TextInputTextChanged(object sender, EventArgs e)
        {
            SelectedTextInputText = _SelectedTextInput.Text;
        }

        private string _SelectedTextInputText = null;
        public string SelectedTextInputText
        {
            get
            {
                return _SelectedTextInputText;
            }
            set
            {
                ChangeIfDifferent(ref _SelectedTextInputText, value, "SelectedTextInputText");
            }
        }

        [Dependency("TheOtherInsertsTextSeparatorChar", "SelectedTextInput", "SelectedTextInputText")]
        public bool TheOtherInsertsSettingsAreValid
        {
            get
            {
                return _TheOtherInsertsTextSeparatorChar != '\0' && SelectedTextInput != null && !string.IsNullOrEmpty(SelectedTextInputText);
            }
        }

        #endregion

        #region Serve

        private bool _IsPlayerOneServe = false;
        public bool IsPlayerOneServe
        {
            get
            {
                return _IsPlayerOneServe;
            }
            set
            {
                ChangeIfDifferent(ref _IsPlayerOneServe, value, "IsPlayerOneServe");
            }
        }

        private bool _IsPlayerTwoServe = false;
        public bool IsPlayerTwoServe
        {
            get
            {
                return _IsPlayerTwoServe;
            }
            set
            {
                ChangeIfDifferent(ref _IsPlayerTwoServe, value, "IsPlayerTwoServe");
            }
        }

        [Dependency("IsPlayerOneServe")]
        public string ToggleServePlayerOneButtonText
        {
            get
            {
                return _IsPlayerOneServe ? "Kein Aufschlag" : "Aufschlag";
            }
        }

        [Dependency("IsPlayerTwoServe")]
        public string ToggleServePlayerTwoButtonText
        {
            get
            {
                return _IsPlayerTwoServe ? "Kein Aufschlag" : "Aufschlag";
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

        [Dependency("CasparServerIsConnected", "LowerThirdIsVisible", "CrawlerTopIsVisible", "CrawlerBottomIsVisible", "TheOtherInsertsSettingsAreValid")]
        public bool CanToggleLowerThirdVisibility
        {
            get
            {
                return CasparServerIsConnected && (_LowerThirdIsVisible || (!_CrawlerTopIsVisible && !CrawlerBottomIsVisible && TheOtherInsertsSettingsAreValid));
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

        #region Crawler Top

        private bool _CrawlerTopIsVisible = false;
        public bool CrawlerTopIsVisible
        {
            get
            {
                return _CrawlerTopIsVisible;
            }
            set
            {
                ChangeIfDifferent(ref _CrawlerTopIsVisible, value, "CrawlerTopIsVisible");
            }
        }

        [Dependency("CasparServerIsConnected", "LowerThirdIsVisible", "CrawlerTopIsVisible", "CrawlerBottomIsVisible", "TheOtherInsertsSettingsAreValid")]
        public bool CanToggleCrawlerTopVisibility
        {
            get
            {
                return CasparServerIsConnected && (_CrawlerTopIsVisible || (!LowerThirdIsVisible && !CrawlerBottomIsVisible && TheOtherInsertsSettingsAreValid));
            }
        }

        [Dependency("CrawlerTopIsVisible")]
        public string ToggleCrawlerTopVisibilityButtonText
        {
            get
            {
                return _CrawlerTopIsVisible ? "Ausblenden" : "Einblenden";
            }
        }

        #endregion

        #region Crawler Bottom

        private bool _CrawlerBottomIsVisible = false;
        public bool CrawlerBottomIsVisible
        {
            get
            {
                return _CrawlerBottomIsVisible;
            }
            set
            {
                ChangeIfDifferent(ref _CrawlerBottomIsVisible, value, "CrawlerBottomIsVisible");
            }
        }

        [Dependency("CasparServerIsConnected", "LowerThirdIsVisible", "CrawlerTopIsVisible", "CrawlerBottomIsVisible", "TheOtherInsertsSettingsAreValid")]
        public bool CanToggleCrawlerBottomVisibility
        {
            get
            {
                return CasparServerIsConnected && (_CrawlerBottomIsVisible || (!LowerThirdIsVisible && !CrawlerTopIsVisible && TheOtherInsertsSettingsAreValid));
            }
        }

        [Dependency("CrawlerBottomIsVisible")]
        public string ToggleCrawlerBottomVisibilityButtonText
        {
            get
            {
                return _CrawlerBottomIsVisible ? "Ausblenden" : "Einblenden";
            }
        }

        #endregion

        public TennisSnapInViewModel()
            : base(true)
        {
            _Player1ScoredCommand = new DelegateCommand(() => _StateList.Process(Scoring.ScoringStrategyAction.Player1Scored), () => _StateList != null && _StateList.CanProcess(Scoring.ScoringStrategyAction.Player1Scored));
            _Player1ReducedCommand = new DelegateCommand(() => _StateList.Process(Scoring.ScoringStrategyAction.Player1Reduced), () => _StateList != null && _StateList.CanProcess(Scoring.ScoringStrategyAction.Player1Reduced));
            _Player2ScoredCommand = new DelegateCommand(() => _StateList.Process(Scoring.ScoringStrategyAction.Player2Scored), () => _StateList != null && _StateList.CanProcess(Scoring.ScoringStrategyAction.Player2Scored));
            _Player2ReducedCommand = new DelegateCommand(() => _StateList.Process(Scoring.ScoringStrategyAction.Player2Reduced), () => _StateList != null && _StateList.CanProcess(Scoring.ScoringStrategyAction.Player2Reduced));
            _UndoCommand = new DelegateCommand(() => _StateList.Undo(), () => _StateList != null && _StateList.CanUndo);
            AddExternalPropertyDependency("CasparServerIsConnected", PluginInterfaces.PublicProviders.CasparServer, "IsConnected");
            TheOtherInsertsTextInputCount = 5;
            if (IsInDesignMode)
            {
                _StateList = new Scoring.UndoStateList(new Scoring.V1.TennisScoringStrategyV1());
            }
        }

        private void StateListPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentState":
                    UpdateStateListCommands();
                    break;
            }
        }

        private void UpdateStateListCommands()
        {
            _Player1ScoredCommand.OnCanExecuteChanged();
            _Player1ReducedCommand.OnCanExecuteChanged();
            _Player2ScoredCommand.OnCanExecuteChanged();
            _Player2ReducedCommand.OnCanExecuteChanged();
            _UndoCommand.OnCanExecuteChanged();
            OnPropertyChanged("CanToggleIsTieBreakEnabled");
            OnPropertyChanged("IsTieBreakEnabled");
        }

    }
}
