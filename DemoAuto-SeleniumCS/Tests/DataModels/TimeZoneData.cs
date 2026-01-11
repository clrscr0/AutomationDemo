using System.Collections.Generic;
using Newtonsoft.Json; // Install Newtonsoft.Json via NuGet

public class TimeZoneData
{
    [JsonProperty("timeZoneId")]
    public int TimeZoneId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("resourceId")]
    public int ResourceId { get; set; }

    [JsonProperty("registryId")]
    public string? RegistryId { get; set; }

    [JsonProperty("modifiedDate")]
    public string? ModifiedDate { get; set; }

    [JsonProperty("ianaName")]
    public string? IanaName { get; set; }

    [JsonProperty("offset")]
    public double Offset { get; set; }

    public TimeZoneInfo ToTimeZoneInfo()
    {
        string id = RegistryId != null ? RegistryId : "";
        return TimeZoneInfo.FindSystemTimeZoneById(id);
    }
}