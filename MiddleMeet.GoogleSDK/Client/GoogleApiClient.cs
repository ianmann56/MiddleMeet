using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MiddleMeet.GoogleSDK.Client
{
  public class GoogleApiClient
  {
    private const string GoogleApiUrlBase = "https://maps.googleapis.com/maps/api/";
    private string apiKey;
    private HttpClient client;

    public GoogleApiClient(GoogleApiConfig googleApiConfig)
    {
      this.apiKey = googleApiConfig.ApiKey;
      this.client = new HttpClient();
    }

    public async Task<GoogleApiResponse> Get(GoogleApiRequest request)
    {
      string url = this.BuildRequestUrl(request);
      HttpResponseMessage response = await this.client.GetAsync(url);
      response.EnsureSuccessStatusCode();
      string responseBody = await response.Content.ReadAsStringAsync();
      
      GoogleApiResponse jsonResponse = JsonConvert.DeserializeObject<GoogleApiResponse>(responseBody);
      return jsonResponse;
    }

    private string BuildRequestUrl(GoogleApiRequest request)
    {
      string url = $"{GoogleApiUrlBase}{request.Api}/";
      if (!string.IsNullOrEmpty(request.SubApi))
      {
        url = url + request.SubApi + "/";
      }

      url = $"{url}json?key={this.apiKey}";

      for (int i = 0; i < request.QueryParams.Count(); i ++)
      {
        string queryParamKey = request.QueryParams.Keys.ToList()[i];
        url = $"{url}&{queryParamKey}={request.QueryParams[queryParamKey]}";
      }

      return url;
    }
  }
}