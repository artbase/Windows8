using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace PinterestTemplate
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class MainPage : PinterestTemplate.Common.LayoutAwarePage
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
        protected async override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
            allPinInfo result = await GetAll();
            List<elemTab> list = (from s in result.body
                                  select new elemTab { Title = s.board, Subtitle = s.desc, Image = new BitmapImage(new Uri(s.src)) }).ToList();
            itemsViewSource.Source = list;
            
        }

        private const string RestServiceUrl = "http://pinterestapi.co.uk/namibia/";

        public async Task<allPinInfo> GetAll()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RestServiceUrl)
            };

            //// I want to add and accept a header for JSON
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //// Retrieve all the groups with their items
            var response = await client.GetAsync("pins");

            //// Throw an exception if something went wrong
            response.EnsureSuccessStatusCode();

            //// In case you need date and time properties
            const string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ";
            var jsonSerializerSettings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat(dateTimeFormat)
            };

            var jsonSerializer = new DataContractJsonSerializer(typeof(allPinInfo), jsonSerializerSettings);

            var stream = await response.Content.ReadAsStreamAsync();
            allPinInfo elem = (allPinInfo)jsonSerializer.ReadObject(stream);
            return elem;
        }
    }

    public class elemTab    
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public ImageSource Image { get; set; }
    }

    public class pinInfo
    {
        public string href { get; set; }
        public string src { get; set; }
        public string desc { get; set; }
        public string board { get; set; }
        public string attrib { get; set; }
    }
    public class meta
    {
        public string count { get; set; }
    }

    public class allPinInfo
    {
        public pinInfo[] body { get; set; }
        public meta meta { get; set; }
    }
}
