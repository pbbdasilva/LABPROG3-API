using System.Text;
using back_labprog.Contracts.Database;
using back_labprog.Contracts.Frontend;
using back_labprog.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace back_labprog.Business;

public interface IDiseaseBO
{
    public IEnumerable<Disease> GetDiseasesAsync();
    public void AddDisease(Disease newDisease);
    public bool DeleteDisease(Disease disease);
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

    public bool DeleteDisease(Disease disease)
    {
        var collection = GetCollection();
        var filter = Builders<DiseaseDTO>.Filter.Eq("name", disease.Name);
        var result = collection.DeleteOne(filter);
        return result.DeletedCount > 0;
    }
}