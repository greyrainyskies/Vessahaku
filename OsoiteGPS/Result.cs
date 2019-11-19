using System;
using System.Collections.Generic;
using System.Text;

namespace OsoiteGPS
{
    public class ParsedText
    {
        public string Name { get; set; }
        public string AdminParts { get; set; }
        public string Postalcode { get; set; }
        public IList<string> Regions { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }

    public class Query
    {
        public string Text { get; set; }
        public int Size { get; set; }
        public bool Private { get; set; }
        public string Lang { get; set; }
        public int QuerySize { get; set; }
        public string Parser { get; set; }
        public ParsedText ParsedText { get; set; }
    }

    public class Engine
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
    }

    public class Geocoding
    {
        public string Version { get; set; }
        public string Attribution { get; set; }
        public Query Query { get; set; }
        public Engine Engine { get; set; }
        public long Timestamp { get; set; }
    }

    public class Geometry
    {
        public string Type { get; set; }
        public IList<double> Coordinates { get; set; }
    }

    public class Properties
    {
        public string Id { get; set; }
        public string Gid { get; set; }
        public string Layer { get; set; }
        public string Source { get; set; }
        public string SourceId { get; set; }
        public string Name { get; set; }
        public string Housenumber { get; set; }
        public string Street { get; set; }
        public string Postalcode { get; set; }
        public string PostalcodeGid { get; set; }
        public decimal Confidence { get; set; }
        public string Accuracy { get; set; }
        public string Country { get; set; }
        public string CountryGid { get; set; }
        public string CountryA { get; set; }
        public string Region { get; set; }
        public string RegionGid { get; set; }
        public string Localadmin { get; set; }
        public string LocaladminGid { get; set; }
        public string Locality { get; set; }
        public string LocalityGid { get; set; }
        public string Neighbourhood { get; set; }
        public string NeighbourhoodGid { get; set; }
        public string Label { get; set; }
    }

    public class Feature
    {
        public string Type { get; set; }
        public Geometry Geometry { get; set; }
        public Properties Properties { get; set; }
    }

    public class Result
    {
        public Geocoding Geocoding { get; set; }
        public string Type { get; set; }
        public IList<Feature> Features { get; set; }
        public IList<double> Bbox { get; set; }
    }
}
