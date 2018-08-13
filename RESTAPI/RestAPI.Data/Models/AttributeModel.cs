using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPI.Data.Models
{
    public class AttributeModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Environment Name")]
        public string Env_Name { get; set; }

        [JsonProperty("Attribute Name")]
        public string AttributeName { get; set; }

        [JsonProperty("Attribute Description")]
        public string AttributeDescription { get; set; }

        [JsonProperty("Attribute Value")]
        public string AttributeValue { get; set; }
    }
}
