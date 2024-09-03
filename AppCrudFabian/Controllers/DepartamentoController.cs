using AppCrudFabian.Data;
using AppCrudFabian.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppCrudFabian.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public DepartamentoController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListaDepartamentos()
        {
            List<Departamento> lista = await _appDbContext.Departamentos.ToListAsync();
            return View("./Views/Departamento/ListaDepartamentos.cshtml", lista);
        }

        public async Task<IActionResult> ListaCiudades(int IdDepartamento)
        {
            Departamento Departamento = await _appDbContext.Departamentos.Include(d => d.Ciudades).FirstAsync(d => d.Id == IdDepartamento);
            ViewData["DepartamentoNombre"] = Departamento.Nombre;
            ViewData["DepartamentoCiudades"] = Departamento.Ciudades;
            return View("./Views/Departamento/ListaCiudades.cshtml");
        }

    }
}
