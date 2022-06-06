using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back_labprog.Contracts.Database;

public class GeoInfoDTO
{
    [BsonId]
    public ObjectId? Id { get; set; }
    [BsonElement("uf")]
    public string Uf { get; set; }
    [BsonElement("municipio")]
    public string Municipio { get; set; }
    [BsonElement("ibge")]
    public string Ibge { get; set; }
    [BsonElement("latitude")]
    public float Latitude { get; set; }
    [BsonElement("longitude")]
    public float Longitude { get; set; }
    [BsonElement("regiao")]
    public string Regiao { get; set; }
    [BsonElement("populacao")]
    public int Populacao { get; set; }
}