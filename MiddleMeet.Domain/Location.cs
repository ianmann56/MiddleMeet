namespace MiddleMeet.Domain
{
  public class Location
  {
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }

    public Location(string name, string address, double lat, double lng)
    {
      this.Name = name;
      this.Address = address;
      this.Latitude = lat;
      this.Longitude = lng;
    }

    public override string ToString()
    {
      return $"{this.Name} @ {this.Address}";
    }
  }
}