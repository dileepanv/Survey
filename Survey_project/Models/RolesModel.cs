using Newtonsoft.Json;

namespace Survey_project.Models
{
   
    public class RoleModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("roleName")]
        public string RoleName { get; set; }
    }
}
