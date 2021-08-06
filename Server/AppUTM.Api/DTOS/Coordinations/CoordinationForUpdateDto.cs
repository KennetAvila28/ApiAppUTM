using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppUTM.Core.Models;

namespace AppUTM.Api.DTOS.Coordinations
{
    public class CoordinationForUpdateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}