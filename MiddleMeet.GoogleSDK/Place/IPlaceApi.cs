using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiddleMeet.GoogleSDK.Place
{
  public interface IPlaceApi
  {
    Task<IEnumerable<Place>> GetPlacesNearCoordinate(double latitude, double longitude, double radiusMeters);
  }
}