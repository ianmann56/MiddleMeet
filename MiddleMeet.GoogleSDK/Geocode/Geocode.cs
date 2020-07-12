using Newtonsoft.Json;

namespace MiddleMeet.GoogleSDK.Geocode
{
  public class Geocode
  {
    public Geometry Geometry { get; set; }
    
    [JsonProperty("formatted_address")]
    public string FormattedAddress { get; set; }
  }
}