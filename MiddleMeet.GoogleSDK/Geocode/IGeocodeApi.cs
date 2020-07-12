using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiddleMeet.GoogleSDK.Geocode
{
  public interface IGeocodeApi
  {
    Task<IEnumerable<Geocode>> GetGeocodesForAddress(string address);
    Task<IEnumerable<Geocode>> GetGeocodesForCoordinate(double latitude, double longitude);
  }
}