using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCard_App.Helpers
{
    public static class VCardHelper
    {
        public static async Task<string> HttpGetContentAsync(string stream)
        {
            HttpResponseMessage response;
            using (HttpClient client = new HttpClient())
            {
                do
                {
                    response = await client.GetAsync(stream);
                    if (!response.IsSuccessStatusCode) Console.WriteLine("Getting Content Failed !");
                    Task.Delay(2000);
                }
                while (!response.IsSuccessStatusCode);
            }

            return await response.Content.ReadAsStringAsync();
        }
        public static T DeserializeWithSetting<T>(string responseBody)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var obj = JsonConvert.DeserializeObject<T>(responseBody, settings);

            return obj;
        }
        public static VCards CardIndexer(VCards cards)
        {
            foreach (var card in cards.Vcards)
            {
                card.GuId = card.Name.First + "_" + Guid.NewGuid().ToString();
            }

            return cards;
        }
        public static Dictionary<string, string> GetVCards(VCards model)
        {
            Dictionary<string, string> cards = new();
            foreach (var card in model.Vcards)
            {
                int id = 1;
                string fullName = card.Name.First + " " + card.Name.Last;
                cards
                    .Add(card.GuId, $"BEGIN:VCARD\r\n VERSION:4.0\r\n FN:{fullName}\r\n N:{card.Name.First};{card.Name.Last};\r\n EMAIL;TYPE=work:{card.Email};\r\n ADR;TYPE=home:;;{card.Location.City};{card.Location.Country};\r\n TEL;TYPE=cell:{card.Phone};\r\n UID:urn:uuid:{card.GuId};\r\n END:VCARD");
            }

            return cards;
        }
        public static void SaveVCards(Dictionary<string, string> cards, string folderName)
        {
            string dir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + $@"\{folderName}";

            if (!Directory.Exists(dir))
            {
                Console.WriteLine("Directory doesn't exist");
                return;
            }
            foreach (var card in cards)
            {
                using (FileStream fs = new(dir + $@"\{card.Key}.vcf", FileMode.Create))
                {
                    using StreamWriter sr = new(fs);
                    sr.WriteLine(card.Value);
                }
            }
            Console.WriteLine("Card saved in the directory !");
        }
    }
}
