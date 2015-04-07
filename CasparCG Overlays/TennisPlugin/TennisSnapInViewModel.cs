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

        public IEnumerable<ITennisTemplate> AvailableTennisTemplates { get; set; }
        public ITennisTemplate SelectedTennisTemplate { get; set; }

        public TennisSnapInViewModel()
        {
            AvailableTennisTemplates = new [] {new DefaultTennisTemplate()};
            SelectedTennisTemplate = AvailableTennisTemplates.First();
        }

    }
}
