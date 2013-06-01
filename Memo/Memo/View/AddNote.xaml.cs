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
using StyleMVVM.Messenger;

namespace Memo.View
{
    [CharmInfo]
    public class AddNoteInfo : CharmInfo<AddNote>
    {
        public override string SettingsText
        {
            get
            {
                return "Dodaj notke";
            }
        }
    }

    [Export]
    public sealed partial class AddNote : CharmControl
    {
        public AddNote()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DispatchedMessenger.Instance.Send(new Note { Title = Title.Text, Description = Desc.Text }); 
        }
    }

    public class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
