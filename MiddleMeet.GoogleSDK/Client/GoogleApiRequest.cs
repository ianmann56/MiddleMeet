using System.Collections.Generic;

namespace MiddleMeet.GoogleSDK.Client
{
  public class GoogleApiRequest
  {
    public string Api { get; private set; }
    public string SubApi { get; private set; }
    public Dictionary<string, string> QueryParams { get; private set; }

    public GoogleApiRequest(string api, string subApi, Dictionary<string, string> queryParams)
    {
      this.Api = api;
      this.SubApi = subApi;
      this.QueryParams = queryParams;
    }
  }
}