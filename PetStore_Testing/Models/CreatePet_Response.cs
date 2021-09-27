using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PetStore_Testing.Models
{
    public class CreatePet_Response
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photoUrls")]
        public String[] PhotoUrls { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("status")]
        public String Status { get; set; }

    }
}
