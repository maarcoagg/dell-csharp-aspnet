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
    public class ConsultasController : Controller
    {
        private readonly HospitalContext _context;

        public ConsultasController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Consultas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.Consultas.Include(c => c.CodTriagemNavigation).Include(c => c.CorenNavigation).Include(c => c.CpfNavigation).Include(c => c.CrmNavigation);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: Consultas/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
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

        // GET: Consultas/Create
        public IActionResult Create()
        {
            ViewData["CodTriagem"] = new SelectList(_context.Triagem, "CodTriagem", "Coren");
            ViewData["Coren"] = new SelectList(_context.Enfermeiros, "Coren", "Coren");
            ViewData["Cpf"] = new SelectList(_context.Pacientes, "Cpf", "Cpf");
            ViewData["Crm"] = new SelectList(_context.Medicos, "Crm", "Crm");
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cpf,Crm,Coren,DataConsulta,CodTriagem,CodConsultas")] Consultas consultas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodTriagem"] = new SelectList(_context.Triagem, "CodTriagem", "Coren", consultas.CodTriagem);
            ViewData["Coren"] = new SelectList(_context.Enfermeiros, "Coren", "Coren", consultas.Coren);
            ViewData["Cpf"] = new SelectList(_context.Pacientes, "Cpf", "Cpf", consultas.Cpf);
            ViewData["Crm"] = new SelectList(_context.Medicos, "Crm", "Crm", consultas.Crm);
            return View(consultas);
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultas = await _context.Consultas.FindAsync(id);
            if (consultas == null)
            {
                return NotFound();
            }
            ViewData["CodTriagem"] = new SelectList(_context.Triagem, "CodTriagem", "Coren", consultas.CodTriagem);
            ViewData["Coren"] = new SelectList(_context.Enfermeiros, "Coren", "Coren", consultas.Coren);
            ViewData["Cpf"] = new SelectList(_context.Pacientes, "Cpf", "Cpf", consultas.Cpf);
            ViewData["Crm"] = new SelectList(_context.Medicos, "Crm", "Crm", consultas.Crm);
            return View(consultas);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cpf,Crm,Coren,DataConsulta,CodTriagem,CodConsultas")] Consultas consultas)
        {
            if (id != consultas.CodConsultas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultasExists(consultas.CodConsultas))
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
            ViewData["CodTriagem"] = new SelectList(_context.Triagem, "CodTriagem", "Coren", consultas.CodTriagem);
            ViewData["Coren"] = new SelectList(_context.Enfermeiros, "Coren", "Coren", consultas.Coren);
            ViewData["Cpf"] = new SelectList(_context.Pacientes, "Cpf", "Cpf", consultas.Cpf);
            ViewData["Crm"] = new SelectList(_context.Medicos, "Crm", "Crm", consultas.Crm);
            return View(consultas);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
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

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultas = await _context.Consultas.FindAsync(id);
            _context.Consultas.Remove(consultas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultasExists(int id)
        {
            return _context.Consultas.Any(e => e.CodConsultas == id);
        }
    }
}
