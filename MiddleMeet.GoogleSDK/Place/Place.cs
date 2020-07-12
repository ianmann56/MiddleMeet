using System.Collections.Generic;
using MiddleMeet.GoogleSDK.Geocode;

namespace MiddleMeet.GoogleSDK.Place
{
  public class Place
  {
    public Geometry Geometry { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Types { get; set; }
    public string Vicinity { get; set; }
  }
}