using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;  // for SelectList
using NoDesk_Group1.Repositories.Interfaces;
using NoDeskGroup1.Models;

namespace NoDeskGroup1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repo;
        private static readonly string[] Roles = { "ServiceDesk", "Manager", "Technician" };

        public EmployeeController(IEmployeeRepository repo) => _repo = repo;

        public async Task<IActionResult> Index() => View(await _repo.GetAllAsync());

        public IActionResult Create()
        {
            ViewBag.Roles = Roles;
            return View(new Employee());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee m)
        {
            if (!ModelState.IsValid) { ViewBag.Roles = Roles; return View(m); }
            await _repo.AddAsync(m);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _repo.GetByIdAsync(id);
            if (model == null) return NotFound();
            ViewBag.Roles = Roles;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Employee m)
        {
            if (!ModelState.IsValid) { ViewBag.Roles = Roles; return View(m); }
            await _repo.UpdateAsync(id, m);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
