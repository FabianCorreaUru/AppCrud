using AppCrudFabian.Models;

namespace AppCrudFabian.Servicios
{
    public interface IServicioApi
    {
        Task<List<Vehiculo>> Lista();

        Task<Vehiculo> Obtener(int IdVehiculo);

        Task<bool> Guardar(Vehiculo objeto);

        Task<bool> Editar(Vehiculo objeto);

        Task<bool> Eliminar(int IdVehiculo);
    }
}
