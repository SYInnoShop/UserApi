using Newtonsoft.Json;

namespace AzureFunctions.Models;

public class UserData
{
    [JsonProperty("displayName")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonProperty("extension_8f934461e7d3407587bf5f7b1d21817e_UserRole")]
    public string Role { get; set; } = string.Empty;
}