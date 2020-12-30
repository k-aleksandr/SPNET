﻿using Newtonsoft.Json;

namespace LabClassLibrary.Models
{
    public class Customer
    {
        [JsonProperty("Name")]
        public string Name { get; set;}

        [JsonProperty("Address")]
        public string Address { get; set; }
    }
}
