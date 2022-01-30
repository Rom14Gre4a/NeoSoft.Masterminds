using System.Text.Json.Serialization;

namespace NeoSoft.Masterminds.Models
{
    public class TokenApiModel
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
