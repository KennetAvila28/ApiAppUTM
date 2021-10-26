using AppUTM.Models.Events;
using System.Collections.Generic;

namespace AppUTM.Models.Coordinations
{
    public class CoordinationReturn
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<EventReturn> Events { get; set; }
    }
}