using back_labprog.Contracts.Database;
using back_labprog.Contracts.Frontend;
using back_labprog.Services;
using MongoDB.Driver;

namespace back_labprog.Business;

public interface IUserBO
{
    public User? VerifyLogin(User userCredentials);
    public bool VerifyMail(User userCredentials);
    public User Register(User userCredentials);
    public IEnumerable<User> GetUsers();
    public bool DeleteUser(User userCredentials);
}

public class UserBO : IUserBO
{
    private IMongoCollection<UserDTO> GetCollection() 
        => DatabaseConnector.GetCollection<UserDTO>("labprog", "users");
    public User? VerifyLogin(User userCredentials)
    {
        var collection = GetCollection();
        var response = collection.AsQueryable().FirstOrDefault(u => u.Email == userCredentials.Email && 
                                                                    u.Password == userCredentials.Password);
        return response?.ConvertToUser();
    }

    public bool VerifyMail(User userCredentials)
    {
        var collection = GetCollection();
        var response = collection.AsQueryable().FirstOrDefault(u => u.Email == userCredentials.Email);
        return response != null;
    }

    public User Register(User userCredentials)
    {
        var collection = GetCollection();
        collection.InsertOne(userCredentials);
        return userCredentials;
    }

    public IEnumerable<User> GetUsers()
    {
        var collection = GetCollection();
        return collection.AsQueryable().ToList().Select(u => u.ConvertToUser());
    }

    public bool DeleteUser(User userCredentials)
    {
        var collection = GetCollection();
        var filter = Builders<UserDTO>.Filter.Eq("email", userCredentials.Email);
        var result = collection.DeleteOne(filter);
        return result.DeletedCount > 0;
    }
}