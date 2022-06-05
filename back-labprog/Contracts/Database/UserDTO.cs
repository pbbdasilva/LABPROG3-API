using back_labprog.Contracts.Frontend;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back_labprog.Contracts.Database;

public class UserDTO
{
    [BsonId]
    public ObjectId? Id { get; set; }
    [BsonElement("name")]
    public string? Name { get; set; }
    [BsonElement("lastname")]
    public string? LastName { get; set; }
    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("password")]
    public string Password { get; set; }
    [BsonElement("token")]
    public Guid? Token { get; set; }
    
    public User ConvertToUser()
    {
        return new User
        {
            Name = this.Name,
            Lastname = this.LastName,
            Password = this.Password,
            Email = this.Email,
            Token = this.Token
        };
    }
}