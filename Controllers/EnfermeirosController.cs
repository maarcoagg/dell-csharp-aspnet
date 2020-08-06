using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projetos.Models;

namespace Projetos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnfermeirosController : Controller
    {
        private readonly HospitalContext _context;

        public EnfermeirosController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Enfermeiros
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enfermeiros.ToListAsync());
        }

        // GET: Enfermeiros/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiros = await _context.Enfermeiros
                .FirstOrDefaultAsync(m => m.Coren == id);
            if (enfermeiros == null)
            {
                return NotFound();
            }

            return View(enfermeiros);
        }

        // GET: Enfermeiros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enfermeiros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Coren,Nome")] Enfermeiros enfermeiros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermeiros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enfermeiros);
        }

        // GET: Enfermeiros/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiros = await _context.Enfermeiros.FindAsync(id);
            if (enfermeiros == null)
            {
                return NotFound();
            }
            return View(enfermeiros);
        }

        // POST: Enfermeiros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Coren,Nome")] Enfermeiros enfermeiros)
        {
            if (id != enfermeiros.Coren)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermeiros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermeirosExists(enfermeiros.Coren))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(enfermeiros);
        }

        // GET: Enfermeiros/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiros = await _context.Enfermeiros
                .FirstOrDefaultAsync(m => m.Coren == id);
            if (enfermeiros == null)
            {
                return NotFound();
            }

            return View(enfermeiros);
        }

        // POST: Enfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var enfermeiros = await _context.Enfermeiros.FindAsync(id);
            _context.Enfermeiros.Remove(enfermeiros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermeirosExists(string id)
        {
            return _context.Enfermeiros.Any(e => e.Coren == id);
        }
    }
}
