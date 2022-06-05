using MongoDB.Bson;
using MongoDB.Driver;
namespace back_labprog.Services;

public static class DatabaseConnector
{
    private static string connectionUsername = "labprog";
    private static string connectionPassword = "sYJ4KLacrTQ5vYd";
    private static string connectionUrl = $"mongodb+srv://{connectionUsername}:{connectionPassword}@labprog.w5ihu.mongodb.net/?retryWrites=true&w=majority&connect=replicaSet";

    private static MongoClient GetConnection()
    {
        var settings = MongoClientSettings.FromConnectionString(connectionUrl);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client =  new MongoClient(connectionUrl);
        return client;
    }

    public static IMongoCollection<T> GetCollection<T>(string dbName, string collectionName) 
        => GetConnection().GetDatabase(dbName).GetCollection<T>(collectionName);
    }