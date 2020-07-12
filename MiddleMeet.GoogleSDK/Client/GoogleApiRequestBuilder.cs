using System.Collections.Generic;

namespace MiddleMeet.GoogleSDK.Client
{
  public class GoogleApiRequestBuilder
  {
    private string api;
    private string subApi;
    private Dictionary<string, string> queryParams;

    public GoogleApiRequestBuilder()
    {
      this.queryParams = new Dictionary<string, string>();
    }

    public GoogleApiRequestBuilder ToApi(string api)
    {
      this.api = api;
      return this;
    }

    public GoogleApiRequestBuilder ToSubApi(string subApi)
    {
      this.subApi = subApi;
      return this;
    }

    public GoogleApiRequestBuilder WithQueryParam(string name, string value)
    {
      this.queryParams[name] = value;
      return this;
    }

    public GoogleApiRequest Build()
    {
      return new GoogleApiRequest(this.api, this.subApi, this.queryParams);
    }
  }
}