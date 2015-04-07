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

        private bool _GraphicsIsVisible = false;
        public bool GraphicsIsVisible
        {
            get
            {
                return _GraphicsIsVisible;
            }
            set
            {
                ChangeIfDifferent(ref _GraphicsIsVisible, value, "GraphicsIsVisible");
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
        }

    }
}
