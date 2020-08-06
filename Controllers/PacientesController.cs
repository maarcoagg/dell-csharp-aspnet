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
    public class PacientesController : Controller
    {
        private readonly HospitalContext _context;

        public PacientesController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pacientes.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacientes = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Cpf == id);
            if (pacientes == null)
            {
                return NotFound();
            }

            return View(pacientes);
        }

        // GET: Pacientes/Consultas/id_pac
        [HttpGet("Consultas/{id}")]
        public async Task<IActionResult> Consulta(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var pacientes = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Cpf == id);
                
            if (pacientes == null)
            {
                return NotFound();
            }
            
            var consultas = await _context.Consultas
                .Include(c => c.CodTriagemNavigation)
                .Include(c => c.CorenNavigation)
                .Include(c => c.CpfNavigation)
                .Include(c => c.CrmNavigation)
                .FirstOrDefaultAsync(m => m.CodConsultas == id);
            if (consultas == null)
            {
                return NotFound();
            }

            return View(consultas);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cpf,Nome,Sexo")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pacientes);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacientes = await _context.Pacientes.FindAsync(id);
            if (pacientes == null)
            {
                return NotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cpf,Nome,Sexo")] Pacientes pacientes)
        {
            if (id != pacientes.Cpf)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacientesExists(pacientes.Cpf))
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
            return View(pacientes);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacientes = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Cpf == id);
            if (pacientes == null)
            {
                return NotFound();
            }

            return View(pacientes);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pacientes = await _context.Pacientes.FindAsync(id);
            _context.Pacientes.Remove(pacientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacientesExists(string id)
        {
            return _context.Pacientes.Any(e => e.Cpf == id);
        }
    }
}
