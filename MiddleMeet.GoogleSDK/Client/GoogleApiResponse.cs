using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MiddleMeet.GoogleSDK.Client
{
  public class GoogleApiResponse
  {
    [JsonProperty("next_page_token")]
    public string NextPageToken { get; set; }
    public JArray Results { get; set; }

    public IEnumerable<T> ConvertResults<T>()
    {
      IEnumerable<T> resultsArray = this.Results.ToObject<IEnumerable<T>>();
      return resultsArray;
    }
  }
}