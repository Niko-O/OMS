using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils.Wpf;

namespace TennisPlugin
{
    public class TheOtherInsertsTextInput : NotifyPropertyChanged
    {

        public event EventHandler Selected;
        public event EventHandler TextChanged;
        
        private bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (ChangeIfDifferent(ref _IsSelected, value, "IsSelected"))
                {
                    if (_IsSelected)
                    {
                        if (Selected != null)
                        {
                            Selected(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }
  
        private string _Text = "";
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (ChangeIfDifferent(ref _Text, value, "Text"))
                {
                    if (_IsSelected)
                    {
                        if (Selected != null)
                        {
                            TextChanged(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        private bool _RadioButtonIsEnabled = true;
        public bool RadioButtonIsEnabled
        {
            get
            {
                return _RadioButtonIsEnabled;
            }
            set
            {
                ChangeIfDifferent(ref _RadioButtonIsEnabled, value, "RadioButtonIsEnabled");
            }
        }
         
        private bool _TextBoxIsEnabled = true;
        public bool TextBoxIsEnabled
        {
            get
            {
                return _TextBoxIsEnabled;
            }
            set
            {
                ChangeIfDifferent(ref _TextBoxIsEnabled, value, "TextBoxIsEnabled");
            }
        }

        public TheOtherInsertsTextInput()
        {
        }

    }
}
