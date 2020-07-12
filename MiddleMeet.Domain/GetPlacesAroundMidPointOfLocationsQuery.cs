using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiddleMeet.GoogleSDK.Geocode;
using MiddleMeet.GoogleSDK.Place;

namespace MiddleMeet.Domain
{
  public class GetPlacesAroundMidPointOfLocationsQuery
  {
    private const double MetersIn1Mile = 1609.34;
    private IGeocodeApi geocodeApi;
    private IPlaceApi placeApi;
    private IEnumerable<string> addresses;
    private double radiusMiles;

    private string[] PlaceTypesOfInterest = new string[]
    {
      "amusement_park",
      "bowling_alley",
      "cafe",
      "campground",
      "lodging",
      "movie_theater",
      "museum",
      "park",
      "restaurant",
      "shopping_mall",
      "zoo"
    };

    public GetPlacesAroundMidPointOfLocationsQuery(IGeocodeApi geocodeApi, IPlaceApi placeApi, IEnumerable<string> addresses, double radiusMiles)
    {
      this.geocodeApi = geocodeApi;
      this.placeApi = placeApi;
      this.addresses = addresses;
      this.radiusMiles = radiusMiles;
    }

    public async Task<IEnumerable<Location>> Run()
    {
      List<Coordinate> coordinates = new List<Coordinate>();
      double latitudeSum = 0;
      double longitudeSum = 0;

      foreach (string address in this.addresses)
      {
        IEnumerable<Geocode> geocodeLocations = await this.geocodeApi.GetGeocodesForAddress(address);
        Coordinate coordinate = geocodeLocations.Single().Geometry.Location;
        coordinates.Add(coordinate);
        latitudeSum += coordinate.Lat;
        longitudeSum += coordinate.Lng;
      }

      double midLatitude = latitudeSum / coordinates.Count();
      double midLongitude = longitudeSum / coordinates.Count();

      double radiusMeters = radiusMiles * MetersIn1Mile;

      IEnumerable<Place> nearbyPlaces = await this.placeApi.GetPlacesNearCoordinate(midLatitude, midLongitude, radiusMeters);

      nearbyPlaces = this.FilterForInterestedLocationTypes(nearbyPlaces, PlaceTypesOfInterest);

      return nearbyPlaces.Select(p => new Location(p.Name, p.Vicinity, p.Geometry.Location.Lat, p.Geometry.Location.Lng));
    }

    private IEnumerable<Place> FilterForInterestedLocationTypes(IEnumerable<Place> original, IEnumerable<string> placeTypesOfInterest)
    {
      return original.Where(place => place.Types.Any(type => placeTypesOfInterest.Contains(type)));
    }
  }
}
