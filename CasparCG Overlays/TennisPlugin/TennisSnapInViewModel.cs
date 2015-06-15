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

        [Dependency("PlayerNameOne", "PlayerNameTwo", "TemplateIsLoaded")]
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
            _Player1ScoredCommand = new DelegateCommand(() => _StateList.Process(Scoring.ScoringStrategyAction.Player1Scored), () => _StateList != null && _StateList.CanProcess(Scoring.ScoringStrategyAction.Player1Scored));
            _Player1ReducedCommand = new DelegateCommand(() => _StateList.Process(Scoring.ScoringStrategyAction.Player1Reduced), () => _StateList != null && _StateList.CanProcess(Scoring.ScoringStrategyAction.Player1Reduced));
            _Player2ScoredCommand = new DelegateCommand(() => _StateList.Process(Scoring.ScoringStrategyAction.Player2Scored), () => _StateList != null && _StateList.CanProcess(Scoring.ScoringStrategyAction.Player2Scored));
            _Player2ReducedCommand = new DelegateCommand(() => _StateList.Process(Scoring.ScoringStrategyAction.Player2Reduced), () => _StateList != null && _StateList.CanProcess(Scoring.ScoringStrategyAction.Player2Reduced));
            _UndoCommand = new DelegateCommand(() => _StateList.Undo(), () => _StateList != null && _StateList.CanUndo);
            AddExternalPropertyDependency("CanLoadTemplate", PluginInterfaces.PublicProviders.CasparServer, "IsConnected");
            AddExternalPropertyDependency("ToggleLowerThirdVisibilityButtonEnabled", PluginInterfaces.PublicProviders.CasparServer, "IsConnected");
            AddExternalPropertyDependency("CanShowGraphics", PluginInterfaces.PublicProviders.CasparServer, "IsConnected");
            LowerThirdTextInputCount = 5;
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
