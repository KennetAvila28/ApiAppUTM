using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppUTM.Core.Models
{
    public class Coordination : BaseEntity
    {
        public string Nombre { get; set; }

        [JsonIgnore]
        public List<Event> Events { get; set; }
    }
}