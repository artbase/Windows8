using StyleMVVM.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Syndication;
using StyleMVVM.View;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WalutyTemplate
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    /// 
    [StartPage]
    public sealed partial class MainPage : StyleMVVM.View.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected  override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            

            double count;
            string tekst2 = Count.Text.Replace(".", ",");
            if (double.TryParse(tekst2, out count))
            {
                HttpClient client = new HttpClient();
                Task<string> result;
                 result =  client.GetStringAsync("http://youbookmarks.com/feed/waluty_api.php?kod=usd&limit=1");
                 if (result.Result != null)
                 {
                     XDocument document = XDocument.Parse(result.Result);
                     var elem = (from d in document.Descendants("item") select d).ToList();
                     string tekst = elem[1].Element("description").Value;
                     string[] lista = tekst.Split('|');
                     lista[1] = lista[1].Replace(".", ",");
                     double przelicznik = double.Parse(lista[1]);

                     Result.Text = "Tyle dolarów kosztuje : " + (count * przelicznik).ToString() + " złotych";
                 }
                
            }
            else
            {
                Result.Text = "Źle wpisana liczba";
            }


        }
    }
}
