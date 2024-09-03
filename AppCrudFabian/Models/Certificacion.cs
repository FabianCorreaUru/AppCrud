namespace AppCrudFabian.Models
{
    public class Certificacion
    {
        public int IdCertificacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public IEnumerable<EmpleadosCertificaciones> EmpleadosCertificaciones { get; set; } 

    }
}
