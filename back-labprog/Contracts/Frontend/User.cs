using back_labprog.Contracts.Database;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back_labprog.Contracts.Frontend;

public class User
{
    [BsonElement("name")]
    public string? Name { get; set; }
    
    [BsonElement("lastname")]
    public string? Lastname { get; set; }
    
    [BsonElement("email")]
    public string Email { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; }
    
    [BsonElement("token")]
    public Guid? Token { get; set; } 
    
    public static implicit operator UserDTO(User u)
        => new UserDTO
        {
            Name = u.Name,
            LastName = u.Lastname,
            Email = u.Email,
            Password = u.Password,
            Token = u.Token,
            Id = ObjectId.GenerateNewId()
        };
}