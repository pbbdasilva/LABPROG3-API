using back_labprog.Contracts.Database;
using back_labprog.Contracts.Frontend;
using back_labprog.Services;
using MongoDB.Driver;

namespace back_labprog.Business;

public interface IDiseaseBO
{
    public IEnumerable<Disease> GetDiseasesAsync();
    public void AddDisease(Disease newDisease);
}

public class DiseaseBO : IDiseaseBO
{
    private IMongoCollection<DiseaseDTO> GetCollection() 
        => DatabaseConnector.GetCollection<DiseaseDTO>("labprog", "diseases");
    
    public IEnumerable<Disease> GetDiseasesAsync()
    {
        var collection = GetCollection();
        return collection.AsQueryable().ToList().Select(x => x.ConvertToDisease());
    }

    public async void AddDisease(Disease newDisease)
    {
        var collection = GetCollection();
        var result = await collection.CountDocumentsAsync(disease => disease.Name == newDisease.Name);
        
        if (result == 0) await collection.InsertOneAsync(newDisease);
    }
}