using System.Text;
using back_labprog.Contracts.Database;
using back_labprog.Contracts.Frontend;
using back_labprog.Services;
using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Driver;

namespace back_labprog.Business;

public interface IMapBO
{
    public IEnumerable<DiseaseOccurrence> GetOccurrences(FilterRequest filter);
    public void UploadCsv(Stream ms);
}

public class MapBO : IMapBO
{
    private IMongoCollection<DiseaseOccurrenceDTO> GetCollection() 
        => DatabaseConnector.GetCollection<DiseaseOccurrenceDTO>("labprog", "occurrences");
    public IEnumerable<DiseaseOccurrence> GetOccurrences(FilterRequest filter)
    {
        var collection = GetCollection();
        return collection.AsQueryable().ToList()
            .Where(x => filter.SelectedStates.Contains(x.State) && filter.SelectTedDiseases.Contains(x.Disease))
            .Select(x => x.ConvertToDiseaseOccurrence());
    }
    
    public void UploadCsv(Stream ms)
    {
        var collection = GetCollection();
        var occurenceList = ReadRawOccurrenceStream(ms);
        collection.InsertMany(occurenceList);
    }

    private List<DiseaseOccurrenceDTO> ReadRawOccurrenceStream(Stream ms)
    {
        var occurenceList = new List<DiseaseOccurrenceDTO>();
        using (var reader = new StreamReader(ms, Encoding.UTF8))
        {
            var line = reader.ReadLine();
            if (line == null) return occurenceList;
            
            var uploadData = JsonConvert.DeserializeObject<UploadData>(line);
            var rows = uploadData.Payload.Split('\n');
            foreach (var row in rows)
            {
                var ocurrence = ParseDiseaseOccurence(row);
                if (ocurrence != null) occurenceList.Add(ocurrence);
            }
        }

        return occurenceList;
    }

    private DiseaseOccurrenceDTO? ParseDiseaseOccurence(string line)
    {
        var values = line.Split('\t');
        if (values.Count() < 7) return null;
        values[6].Remove(values.Length - 1);
        return new DiseaseOccurrenceDTO
        {
            Id = ObjectId.GenerateNewId(),
            State = values[0],
            Latitude = float.Parse(values[3]),
            Longitude = float.Parse(values[4]),
            Disease = values[5]
        };
    }
}