using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoDesk_Group1.Repositories.Interfaces;
using NoDeskGroup1.Models;

namespace NoDeskGroup1.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketRepository _repo;
        private static readonly string[] Statuses = { "Open", "Resolved", "ClosedNoResolve" };
        private static readonly string[] Priorities = { "Low", "Medium", "High", "Urgent" };

        public TicketController(ITicketRepository repo) => _repo = repo;

        public async Task<IActionResult> Index() => View(await _repo.GetAllAsync());

        public IActionResult Create()
        {
            ViewBag.Statuses = Statuses;
            ViewBag.Priorities = Priorities;
            return View(new Ticket());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket m)
        {
            if (!ModelState.IsValid) { ViewBag.Statuses = Statuses; ViewBag.Priorities = Priorities; return View(m); }
            await _repo.AddAsync(m);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _repo.GetByIdAsync(id);
            if (model == null) return NotFound();
            ViewBag.Statuses = Statuses;
            ViewBag.Priorities = Priorities;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Ticket m)
        {
            if (!ModelState.IsValid) { ViewBag.Statuses = Statuses; ViewBag.Priorities = Priorities; return View(m); }
            await _repo.UpdateAsync(id, m);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> StatusSummary()
        {
            var data = await _repo.CountByStatusAsync();
            return View(data);
        }
    }
}
