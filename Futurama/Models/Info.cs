// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var info = Info.FromJson(jsonString);

using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProblemDetailsApiDemo.Futurama.Models
{
    public partial class Info
    {
        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }

        [JsonProperty("yearsAired")]
        public string YearsAired { get; set; }

        [JsonProperty("creators")]
        public Creator[] Creators { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public partial class Creator
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Info
    {
        public static Info[] FromJson(string json) => JsonConvert.DeserializeObject<Info[]>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Info[] self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}