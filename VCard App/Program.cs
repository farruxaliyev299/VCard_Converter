using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using VCard_App.Helpers;
using VCard_App.Models;

namespace VCard_App
{
    public class VCards 
    {
        [JsonProperty("results")]
        public IEnumerable<VCard> Vcards { get; set; }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            CreateVCard();
        }

        static void CreateVCard()
        {
            string responseBody = VCardHelper.HttpGetContentAsync(@"https://randomuser.me/api?results=2").Result;

            VCards vCards = VCardHelper.DeserializeWithSetting<VCards>(responseBody);

            vCards = VCardHelper.CardIndexer(vCards);

            Dictionary<string, string> vCardDictionary = VCardHelper.GetVCards(vCards);

            //Saves Cards into 
            VCardHelper.SaveVCards(vCardDictionary, "VCards");
        }
    }
}