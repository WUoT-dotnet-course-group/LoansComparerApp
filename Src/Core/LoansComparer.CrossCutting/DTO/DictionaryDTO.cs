using System.Text.Json.Serialization;

namespace LoansComparer.CrossCutting.DTO
{
    public class DictionaryDTO
    {
        [JsonPropertyName("typeId")]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
