namespace AppCrudFabian.Models
{
    public class Empleado
    {

        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public DateOnly FechaContrato { get; set; }
        public bool Activo { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; }

        public IEnumerable<EmpleadosCertificaciones> EmpleadosCertificaciones { get; set; }

    }
}
