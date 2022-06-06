using MongoDB.Bson.Serialization.Attributes;

namespace back_labprog.Contracts.Frontend;

public class FilterRequest
{
    [BsonElement("selectedStates")]
    public List<string> SelectedStates { get; set; }
    [BsonElement("selectedDiseases")]
    public List<string> SelectTedDiseases { get; set; }
}