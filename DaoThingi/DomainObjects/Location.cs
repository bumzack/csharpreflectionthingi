using DaoThingi.Reflection;

namespace DaoThingi.DomainObjects
{
    [Table]
    public class Location
    {
        [Id]
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Community { get; set; }
        public string Province { get; set; }

        public Location(double v1, double v2, string v3, string v4)
        {
            Lat = v1;
            Lng = v2;
            Community = v3;
            Province = v4;
        }
    }
}