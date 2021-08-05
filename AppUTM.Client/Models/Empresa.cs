using Microsoft.AspNetCore.Http;

namespace AppUTM.Client.Models
{
    public class Empresa
    {
        public int EmpresaId { get; set; }
        public string RFC { get; set; }
        public string Nombre { get; set; }
        public string Alias { get; set; }
        public string Tipo { get; set; }
        public string Direccion { get; set; }
        public bool Activa { get; set; }
        public string Telefono { get; set; }
        public string ImagenEmpresa { get; set; }
        public IFormFile Foto { get; set; }
        public string Domain { get; set; }

        //Propiedades auxiliares
        public bool Cupones { get; set; } = false;
    }
}
