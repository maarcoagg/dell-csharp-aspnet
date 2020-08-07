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
    public class TriagemController : Controller
    {
        private readonly HospitalContext _context;

        public TriagemController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Triagem
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.Triagem.Include(t => t.CorenNavigation).Include(t => t.CpfNavigation);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: Triagem/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var triagem = await _context.Triagem
                .Include(t => t.CorenNavigation)
                .Include(t => t.CpfNavigation)
                .FirstOrDefaultAsync(m => m.CodTriagem == id);
            if (triagem == null)
            {
                return NotFound();
            }

            return View(triagem);
        }

        [HttpGet("Prioridade/{id}")]
        public async Task<IActionResult> Prioridade(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var triagem = await _context.Triagem
                .FirstOrDefaultAsync(t => t.Prioridade == id);

            var pacientes = from p in _context.Pacientes
                          where p.Cpf == triagem.Cpf
                          orderby p.Nome
                          select p;

            if (pacientes == null)
            {
                return NotFound();
            }

            return View(await pacientes.ToListAsync());
        }

        // GET: Triagem/Create
        public IActionResult Create()
        {
            ViewData["Coren"] = new SelectList(_context.Enfermeiros, "Coren", "Coren");
            ViewData["Cpf"] = new SelectList(_context.Pacientes, "Cpf", "Cpf");
            return View();
        }

        // POST: Triagem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodTriagem,Cpf,Coren,DataConsulta,DescricaoPaciente,Prioridade")] Triagem triagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(triagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Coren"] = new SelectList(_context.Enfermeiros, "Coren", "Coren", triagem.Coren);
            ViewData["Cpf"] = new SelectList(_context.Pacientes, "Cpf", "Cpf", triagem.Cpf);
            return View(triagem);
        }

        // GET: Triagem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var triagem = await _context.Triagem.FindAsync(id);
            if (triagem == null)
            {
                return NotFound();
            }
            ViewData["Coren"] = new SelectList(_context.Enfermeiros, "Coren", "Coren", triagem.Coren);
            ViewData["Cpf"] = new SelectList(_context.Pacientes, "Cpf", "Cpf", triagem.Cpf);
            return View(triagem);
        }

        // POST: Triagem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodTriagem,Cpf,Coren,DataConsulta,DescricaoPaciente,Prioridade")] Triagem triagem)
        {
            if (id != triagem.CodTriagem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(triagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TriagemExists(triagem.CodTriagem))
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
            ViewData["Coren"] = new SelectList(_context.Enfermeiros, "Coren", "Coren", triagem.Coren);
            ViewData["Cpf"] = new SelectList(_context.Pacientes, "Cpf", "Cpf", triagem.Cpf);
            return View(triagem);
        }

        // GET: Triagem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var triagem = await _context.Triagem
                .Include(t => t.CorenNavigation)
                .Include(t => t.CpfNavigation)
                .FirstOrDefaultAsync(m => m.CodTriagem == id);
            if (triagem == null)
            {
                return NotFound();
            }

            return View(triagem);
        }

        // POST: Triagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var triagem = await _context.Triagem.FindAsync(id);
            _context.Triagem.Remove(triagem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TriagemExists(int id)
        {
            return _context.Triagem.Any(e => e.CodTriagem == id);
        }
    }
}
