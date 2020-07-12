using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiddleMeet.GoogleSDK.Client;

namespace MiddleMeet.GoogleSDK.Geocode
{
  public class GeocodeApi : IGeocodeApi
  {
    private string GeocodeApiUri = "geocode";

    private GoogleApiClient client;

    public GeocodeApi(GoogleApiClient client)
    {
      this.client = client;
    }

    public async Task<IEnumerable<Geocode>> GetGeocodesForAddress(string address)
    {
      GoogleApiRequest request = new GoogleApiRequestBuilder().ToApi(GeocodeApiUri).WithQueryParam("address", address).Build();
      GoogleApiResponse response = await this.client.Get(request);
      return response.ConvertResults<Geocode>();
    }

    public async Task<IEnumerable<Geocode>> GetGeocodesForCoordinate(double latitude, double longitude)
    {
      string latlngArgument = $"{latitude},{longitude}";
      GoogleApiRequest request = new GoogleApiRequestBuilder().ToApi(GeocodeApiUri).WithQueryParam("latlng", latlngArgument).Build();
      GoogleApiResponse response = await this.client.Get(request);
      return response.ConvertResults<Geocode>();
    }
  }
}