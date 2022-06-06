using back_labprog.Contracts.Frontend;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back_labprog.Contracts.Database;

public class DiseaseDTO
{
    [BsonId]
    public ObjectId? Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("rate")]
    public string Rate { get; set; }
    [BsonElement("description")]
    public string Description { get; set; }

    public Disease ConvertToDisease()
    {
        return new Disease
        {
            Name = this.Name,
            Rate = this.Rate,
            Description = this.Description
        };
    }
}