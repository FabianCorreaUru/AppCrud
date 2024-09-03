namespace AppCrudFabian.Models
{
    public class EmpleadosCertificaciones
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        public int CertificacionId { get; set; }
        public Certificacion Certificacion { get; set; }

    }
}
