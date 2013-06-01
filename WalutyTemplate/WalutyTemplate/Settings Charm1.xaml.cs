using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StyleMVVM.DependencyInjection;
using StyleMVVM.View;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WalutyTemplate
{
    [CharmInfo]
    public class Settings_Charm1Info : CharmInfo<Settings_Charm1>
    {
        public override string SettingsText
        {
            get
            {
                return "Polityka prywatności";
            }
        }
    }

    [Export]
    public sealed partial class Settings_Charm1 : CharmControl
    {
        public Settings_Charm1()
        {
            this.InitializeComponent();
        }
    }
}
