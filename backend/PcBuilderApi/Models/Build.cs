﻿using System.Text.Json.Serialization;

namespace PcBuilderApi.Models
{
    public class Build
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Component>? Components { get; set; }
    }
}
