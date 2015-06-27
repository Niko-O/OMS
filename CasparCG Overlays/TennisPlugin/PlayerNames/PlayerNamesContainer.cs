using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using OnUtils;
using OnUtils.Extensions;
using OnUtils.Wpf;

namespace TennisPlugin.PlayerNames
{
    public class PlayerNamesContainer : ViewModelBase
    {

        private static PlayerNamesContainer _Instance = null;
        public static PlayerNamesContainer Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PlayerNamesContainer();
                }
                return _Instance;
            }
        }

        public IEnumerable<TennisNameData.IPlayerName> Names
        {
            get
            {
                return Sources.SelectMany(i => i.Names);
            }
        }

        private List<TennisNameData.IPlayerNameSource> Sources = new List<TennisNameData.IPlayerNameSource>();
        private CompositionTarget Target = new CompositionTarget();

        private PlayerNamesContainer()
        {
            PluginInterfaces.PublicProviders.MefCompositor.Compose(Target);
            PluginInterfaces.PublicProviders.MefCompositor.CatalogChanged += (sender, e) => UpdateSources();
            UpdateSources();
        }

        private void UpdateSources()
        {
            if (Target.Sources != null)
            {
                Sources.Synchronize(
                    Target.Sources,
                    (Left, Right) => Left == Right,
                    Item =>
                    {
                        Item.Value.NamesChanged += new EventHandler(SourceNamesChanged);
                        return Item.Value;
                    },
                    Item => Item.NamesChanged -= new EventHandler(SourceNamesChanged)
                );
            }
            OnPropertyChanged("Names");
        }

        private void SourceNamesChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("Names");
        }

        private class CompositionTarget
        {
            [ImportMany(typeof(TennisNameData.IPlayerNameSource), AllowRecomposition = true)]
            public IEnumerable<Lazy<TennisNameData.IPlayerNameSource, TennisNameData.EmptyMetadata>> Sources = null;
        }

    }
}
