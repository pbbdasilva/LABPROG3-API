using back_labprog.Contracts.Database;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back_labprog.Contracts.Frontend;

public class Disease
{
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("transmissivity")]
    public string Rate { get; set; }
    [BsonElement("description")]
    public string Description { get; set; }

    public static implicit operator DiseaseDTO(Disease d)
        => new DiseaseDTO
        {
            Name = d.Name,
            Rate = d.Rate,
            Description = d.Description,
            Id = ObjectId.GenerateNewId()
        };
}