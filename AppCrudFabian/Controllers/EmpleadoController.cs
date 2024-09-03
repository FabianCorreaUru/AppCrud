using Microsoft.AspNetCore.Mvc;
using AppCrudFabian.Data;
using AppCrudFabian.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using NuGet.Protocol;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppCrudFabian.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public EmpleadoController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Lista()
        {
            List<Empleado> lista = await _appDbContext.Empleados.Include(e => e.Area).ToListAsync();
            return View("./Views/Empleado/Lista.cshtml",lista);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            Empleado empleado = new Empleado();
            if (id != 0)
            {
                empleado = await _appDbContext.Empleados.Include(e => e.EmpleadosCertificaciones).FirstAsync(e => e.IdEmpleado == id);

                List<EmpleadosCertificaciones> certificaciones = await _appDbContext.EmpleadosCertificaciones.Where(ec => ec.EmpleadoId == id).Include(ec => ec.Certificacion).ToListAsync();
                ViewData["cert"] = certificaciones;
                
            }

            return View(empleado);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            List<Area> listaAreas = _appDbContext.Areas.ToList();
            ViewBag.listaAreas = new SelectList(listaAreas,"Id","Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(Empleado empleado)
        {
            if (empleado.Correo == null)
            {
                ViewData["ErrorCorreo"] = "Ingresa un correo";
                return View();
            }
            await _appDbContext.Empleados.AddAsync(empleado);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Empleado empleado = await _appDbContext.Empleados.FirstAsync(e => e.IdEmpleado == id);
            List<Area> listaAreas = _appDbContext.Areas.ToList();
            ViewBag.listaAreas = new SelectList(listaAreas, "Id", "Nombre");
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Empleado empleado)
        {
            _appDbContext.Empleados.Update(empleado);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Empleado empleado = await _appDbContext.Empleados.FirstAsync(e => e.IdEmpleado == id);
            _appDbContext.Empleados.Remove(empleado);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

    }
}
