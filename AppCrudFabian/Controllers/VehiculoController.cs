using Microsoft.AspNetCore.Mvc;
using AppCrudFabian.Models;
using AppCrudFabian.Servicios;


namespace AppCrudFabian.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly IServicioApi _servicioAPI;

        public VehiculoController(IServicioApi servicioAPI)
        {
            _servicioAPI = servicioAPI;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Lista()
        {
            Console.WriteLine("LLAMO LISTA Fabian");
            List<Vehiculo> Lista = await _servicioAPI.Lista();
            ViewBag.Action = "Listado Vehiculos";
            return View(Lista);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            Console.WriteLine("LLAMO DETALLE");
            Vehiculo Vehiculo = new Vehiculo();

            if (id != 0)
            {
                Vehiculo = await _servicioAPI.Obtener(id);
                ViewBag.Action = "Detalle Vehiculo";
            }

            return View(Vehiculo);
        }

        public async Task<IActionResult> Vehiculo(int id)
        {
            Vehiculo Vehiculo = new Vehiculo();
            
            if(id != 0)
            {
                Vehiculo = await _servicioAPI.Obtener(id);
                ViewBag.Action = "Editar Vehiculo";
            }
            else
            {
                ViewBag.Action = "Nuevo Vehiculo";
            }

            return View(Vehiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(Vehiculo Vehiculo)
        {
            bool respuesta; 
            
            if (Vehiculo.id == 0)
            {
                respuesta = await _servicioAPI.Guardar(Vehiculo);
            }
            else
            {
                respuesta = await _servicioAPI.Editar(Vehiculo);
            }
            Console.WriteLine(respuesta);

            if (respuesta) {
                return RedirectToAction("Lista");
            }
            else {
                return NoContent();
            }            
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var respuesta = await _servicioAPI.Eliminar(id);

            if (respuesta)
            {
                return RedirectToAction("Lista");
            }
            else
            {
                return NoContent();
            }
        }

    }
}
