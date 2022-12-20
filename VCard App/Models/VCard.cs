using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCard_App.Models;

public class VCard
{
    [JsonIgnore]
    public string GuId { get; set; }

    [JsonProperty("name")]
    public Name Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("location")]
    public Location Location { get; set; }
}
