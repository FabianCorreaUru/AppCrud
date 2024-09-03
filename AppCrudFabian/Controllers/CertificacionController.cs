using Microsoft.AspNetCore.Mvc;
using AppCrudFabian.Data;
using AppCrudFabian.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using NuGet.Protocol;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AppCrudFabian.Controllers
{
    public class CertificacionController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public CertificacionController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Lista()
        {
            List<Certificacion> lista = await _appDbContext.Certificaciones.ToListAsync();
            return View("./Views/Certificacion/Lista.cshtml", lista);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(Certificacion certificacion)
        {
            if (certificacion.Nombre == null)
            {
                ViewData["ErrorCorreo"] = "Ingresa un Nombre";
                return View();
            }
            else if (certificacion.Descripcion == null)
            {
                ViewData["ErrorCorreo"] = "Ingresa una Descripcion";
                return View();
            }
            await _appDbContext.Certificaciones.AddAsync(certificacion);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Certificacion certificacion = await _appDbContext.Certificaciones.FirstAsync(e => e.IdCertificacion == id);
            return View(certificacion);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Certificacion certificacion)
        {
            _appDbContext.Certificaciones.Update(certificacion);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Certificacion certificacion = await _appDbContext.Certificaciones.FirstAsync(e => e.IdCertificacion == id);
            _appDbContext.Certificaciones.Remove(certificacion);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> AgregarCertificacionEmpleado()
        {
            List<Empleado> listaEmpleados = _appDbContext.Empleados.ToList();
            ViewBag.listaEmpleados = new SelectList(listaEmpleados, "IdEmpleado", "NombreCompleto");

            List<Certificacion> listaCertificaciones = _appDbContext.Certificaciones.ToList();
            ViewBag.listaCertificaciones = new SelectList(listaCertificaciones, "IdCertificacion", "Nombre");

            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> AgregarCertificacionEmpleado(EmpleadosCertificaciones empleadoCertificacion)
        {
     
            await _appDbContext.EmpleadosCertificaciones.AddAsync(empleadoCertificacion);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

    }
}
