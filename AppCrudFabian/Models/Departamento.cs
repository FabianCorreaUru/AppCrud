namespace AppCrudFabian.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Superficie { get; set; }
        public string Poblacion {  get; set; }
        public List<Ciudad> Ciudades { get; set; }
        public Departamento() {
            Ciudades = new List<Ciudad>();
        }
             
    }
}
