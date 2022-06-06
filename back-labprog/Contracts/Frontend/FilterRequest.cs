using MongoDB.Bson.Serialization.Attributes;

namespace back_labprog.Contracts.Frontend;

public class FilterRequest
{
    [BsonElement("state")]
    public List<string> State { get; set; }
    [BsonElement("disease")]
    public List<string> DiseaseName { get; set; }
}