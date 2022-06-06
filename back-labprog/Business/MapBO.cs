using back_labprog.Contracts.Database;
using back_labprog.Contracts.Frontend;
using back_labprog.Services;
using MongoDB.Driver;

namespace back_labprog.Business;

public interface IMapBO
{
    IEnumerable<DiseaseOccurrence> GetOccurrences(FilterRequest filter);
}

public class MapBO : IMapBO
{
    private IMongoCollection<DiseaseOccurrenceDTO> GetCollection() 
        => DatabaseConnector.GetCollection<DiseaseOccurrenceDTO>("labprog", "occurrences");
    public IEnumerable<DiseaseOccurrence> GetOccurrences(FilterRequest filter)
    {
        var collection = GetCollection();
        return collection.AsQueryable().ToList()
            .Where(x => filter.State.Contains(x.State) && filter.DiseaseName.Contains(x.Name))
            .Select(x => x.ConvertToDiseaseOccurrence());
    }
}