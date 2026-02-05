using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        private RepositoryDoctoresSQLServer repo;

        public DoctoresController(RepositoryDoctoresSQLServer repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Doctor> doctors = this.repo.GetDoctores();
            return View(doctors);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doc)
        {
            await this.repo.CreateDoctorAsync(doc.IdDoctor, doc.Apellido, doc.Especialidad, doc.Salario, doc.IdHospital);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
