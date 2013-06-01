using StyleMVVM.DependencyInjection;
using StyleMVVM.View;
using StyleMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using StyleMVVM.Messenger;
using Memo.View;
using Windows.Storage;
using System.IO;
using System.Xml.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Memo
{
    public class MainPageViewModel : PageViewModel
    {
        [ActivationComplete]
        public void Activated()
        {
            //ListNote = new ObservableCollection<Note>(new List<Note> { new Note { Title = "Test", Description = "Test" }, new Note { Title = "Test", Description = "Test" }, new Note { Title = "Test", Description = "Test" } });
            string elem = ApplicationData.Current.RoamingSettings.Values["notepad"] as string;
            if (elem == null) ListNote = new ObservableCollection<Note>();
            else
            {
                List<Note> list = DeserializeFromXml(elem);
                ListNote = new ObservableCollection<Note>(list);
            }
            
        }

        [Import]
        public ICharmService CharmService { get; set; }

        public void OpenAddGroupCharm()
        {
            CharmService.OpenCharm("AddNote");
        }

        private ObservableCollection<Note> _listNote;

        public ObservableCollection<Note> ListNote
        {
            get { return _listNote; }
            set { SetProperty(ref _listNote, value); }
        }

        [MessageHandler]
        public void MessageHandler(Note message)
        {
            ListNote.Add(message);

            ApplicationData.Current.RoamingSettings.Values["notepad"] = SerializeToXml(ListNote.ToList());
        }

        string SerializeToXml(List<Note> p)
        {
            StringWriter writer = new StringWriter();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Note>));
            serializer.Serialize(writer, p);

            return writer.ToString();
        }
        List<Note> DeserializeFromXml(string p)
        {
            StringReader reader = new StringReader(p);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Note>));
            List<Note> person = (List<Note>)serializer.Deserialize(reader);

            return person;
        }

        public void ButtonClickHandler(FrameworkElement sender, RoutedEventArgs clickEventArgs)
        {
            string title = (string)((Button)sender).CommandParameter;
            ListNote.Remove(ListNote.Where(x=>x.Title == title).FirstOrDefault());
            ApplicationData.Current.RoamingSettings.Values["notepad"] = SerializeToXml(ListNote.ToList());
        }
    }
}
