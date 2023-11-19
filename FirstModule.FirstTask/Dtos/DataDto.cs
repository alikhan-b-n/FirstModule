using System.Text.Json.Serialization;

namespace FirstModule.Dtos;

public class DataDto
{
    [JsonPropertyName("full_name")] public string FullName { get; set; }

    [JsonPropertyName("skills")] public string Skills { get; set; }
}