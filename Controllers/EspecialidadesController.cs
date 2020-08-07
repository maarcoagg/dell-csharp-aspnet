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
    public class EspecialidadesController : Controller
    {
        private readonly HospitalContext _context;

        public EspecialidadesController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Especialidades
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Especialidades.ToListAsync());
        }

        // GET: Especialidades/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var especialidades = await _context.Especialidades
                .FirstOrDefaultAsync(m => m.CodEspecialidade == id);
                
            if (especialidades == null)
            {
                return NotFound();
            }

            return View(especialidades);
        }

        [HttpGet("Medicos/{id}")]
        public async Task<IActionResult> Medicos(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidade = await _context.Especialidades
                .FirstOrDefaultAsync(e => e.Nome == id);

            // var medicos = await _context.Medicos
            //     .FirstOrDefaultAsync(m => m.CodEspecialidade == especialidade.CodEspecialidade); 

            var medicos = from m in _context.Medicos
                          where m.CodEspecialidade == especialidade.CodEspecialidade
                          select m;

            if (medicos == null)
            {
                return NotFound(await medicos.ToListAsync());
            }

            return View();
        }
        
        // GET: Especialidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Especialidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,CodEspecialidade,ValorConsulta,Descricao")] Especialidades especialidades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especialidades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialidades);
        }

        // GET: Especialidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidades = await _context.Especialidades.FindAsync(id);
            if (especialidades == null)
            {
                return NotFound();
            }
            return View(especialidades);
        }

        // POST: Especialidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,CodEspecialidade,ValorConsulta,Descricao")] Especialidades especialidades)
        {
            if (id != especialidades.CodEspecialidade)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialidades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadesExists(especialidades.CodEspecialidade))
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
            return View(especialidades);
        }

        // GET: Especialidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidades = await _context.Especialidades
                .FirstOrDefaultAsync(m => m.CodEspecialidade == id);
            if (especialidades == null)
            {
                return NotFound();
            }

            return View(especialidades);
        }

        // POST: Especialidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var especialidades = await _context.Especialidades.FindAsync(id);
            _context.Especialidades.Remove(especialidades);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialidadesExists(int id)
        {
            return _context.Especialidades.Any(e => e.CodEspecialidade == id);
        }
    }
}
