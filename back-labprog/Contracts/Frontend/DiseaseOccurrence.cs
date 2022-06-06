using MongoDB.Bson.Serialization.Attributes;

namespace back_labprog.Contracts.Frontend;

public class DiseaseOccurrence
{
    [BsonElement("latitude")]
    public float Latitude { get; set; }
    [BsonElement("longitude")]
    public float Longitude { get; set; }
    [BsonElement("state")] 
    public string State { get; set; }
    [BsonElement("disease")]
    public string Disease { get; set; }
}