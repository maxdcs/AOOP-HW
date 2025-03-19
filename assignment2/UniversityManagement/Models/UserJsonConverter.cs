using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UniversityManagement.Models
{
  public class UserJsonConverter : JsonConverter<User>
  {
    public override User Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      // Save reader state
      var readerAtStart = reader;
      
      // Read the JSON document into a JsonDocument
      using JsonDocument doc = JsonDocument.ParseValue(ref reader);
      var root = doc.RootElement;
      
      // Get the Role property to determine the concrete type
      string role = root.GetProperty("Role").GetString() ?? string.Empty;
      
      // Get other common properties
      Guid id = root.GetProperty("Id").GetGuid();
      string? name = root.GetProperty("Name").GetString();
      string? username = root.GetProperty("Username").GetString();
      string? password = root.GetProperty("Password").GetString();
      
      // Create the appropriate type
      User user = role switch
      {
        "Teacher" => new Teacher(name ?? string.Empty, username ?? string.Empty, password ?? string.Empty) { Id = id },
        "Student" => new Student(name ?? string.Empty, username ?? string.Empty, password ?? string.Empty) { Id = id },
        _ => throw new JsonException($"Unknown user role: {role}")
      };
      
      return user;
    }
    
    public override void Write(Utf8JsonWriter writer, User value, JsonSerializerOptions options)
    {
      writer.WriteStartObject();
      
      writer.WriteString("Id", value.Id);
      writer.WriteString("Name", value.Name);
      writer.WriteString("Username", value.Username);
      writer.WriteString("Password", value.Password);
      writer.WriteString("Role", value.Role);
      
      writer.WriteEndObject();
    }
  }
}