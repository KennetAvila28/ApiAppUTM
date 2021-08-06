using System.Collections.Generic;
using AppUTM.Models.Events;

namespace AppUTM.Models.Coordinations
{
    public class CoordinationReturn
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<EventReturn> Events { get; set; }
    }
}