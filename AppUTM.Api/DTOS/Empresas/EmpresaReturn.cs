using Microsoft.AspNetCore.Http;

namespace AppUTM.Api.DTOS.Empresas
{
    public class EmpresaReturn
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
        public string Domain { get; set; }
    }

    public class EmpresasUTM
    {
        public string IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string CorreoEmpresa { get; set; }
        public string RFC { get; set; }
        public string PersonaResponsable { get; set; }
    }
}