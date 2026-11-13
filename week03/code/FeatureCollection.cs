public class FeatureCollection
{
    public List<Feature> Features { get; set; }
}

public class Feature
{
    public Property Properties { get; set; }
}

public class Property
{
    public string Place { get; set; }
    public double? Mag { get; set; }
}

