using back_labprog.Contracts.Frontend;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back_labprog.Contracts.Database;

public class DiseaseOccurrenceDTO
{
    [BsonId]
    public ObjectId? Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("latitude")]
    public float Latitude { get; set; }
    [BsonElement("longitude")]
    public float Longitude { get; set; }
    [BsonElement("state")] 
    public string State { get; set; }

    public DiseaseOccurrence ConvertToDiseaseOccurrence()
    {
        return new DiseaseOccurrence
        {
            Name = this.Name,
            Latitude = this.Latitude,
            Longitude = this.Longitude,
            State = this.State
        };
    }
}