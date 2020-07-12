using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiddleMeet.GoogleSDK.Client;

namespace MiddleMeet.GoogleSDK.Place
{
  public class PlaceApi : IPlaceApi
  {
    private string PlaceApiUri = "place";
    private string NearbySearchSubApiUri = "nearbysearch";
    
    private GoogleApiClient client;

    public PlaceApi(GoogleApiClient client)
    {
      this.client = client;
    }

    public Task<IEnumerable<Place>> GetPlacesNearCoordinate(double latitude, double longitude, double radiusMeters)
    {
      return this.GetPlacesNearCoordinateWithPageToken(latitude, longitude, radiusMeters, null);
    }

    private async Task<IEnumerable<Place>> GetPlacesNearCoordinateWithPageToken(double latitude, double longitude, double radiusMeters, string pageToken)
    {
      string locationArgument = $"{latitude},{longitude}";
      GoogleApiRequestBuilder requestBuilder = new GoogleApiRequestBuilder()
                                      .ToApi(PlaceApiUri)
                                      .ToSubApi(NearbySearchSubApiUri)
                                      .WithQueryParam("location", locationArgument)
                                      .WithQueryParam("radius", radiusMeters.ToString());
      if (pageToken != null)
      {
        requestBuilder = requestBuilder.WithQueryParam("pagetoken", pageToken);
      }
      GoogleApiRequest request = requestBuilder.Build();

      GoogleApiResponse response = await this.client.Get(request);
      IEnumerable<Place> places = response.ConvertResults<Place>();

      if (!string.IsNullOrEmpty(response.NextPageToken))
      {
        await Task.Delay(2000); // I've found that you have to wait a little bit to get the next page. Otherwise, it gives back an "invalid request" response.
        places = places.Concat(await GetPlacesNearCoordinateWithPageToken(latitude, longitude, radiusMeters, response.NextPageToken));
      }

      return places;
    }
  }
}