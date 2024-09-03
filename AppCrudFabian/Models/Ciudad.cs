namespace AppCrudFabian.Models
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int DepartamentoId { get; set; } 

        public Departamento Departamento { get; set; }

    }
}
