using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppUTM.Core.Models;

namespace AppUTM.Api.DTOS.Coordinations
{
    public class CoordinationReturn
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Event> Events { get; set; }
    }
}