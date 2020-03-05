using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFBanco.Models;

namespace EFBanco.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(double? balance, string nombre)
        {
            var applicationDbContext = _context.Cliente.Include(c => c.Sucursal).ThenInclude(c => c.Banco);
            //Carga inicial
            if (balance == null && string.IsNullOrEmpty(nombre))
            {
                applicationDbContext = _context.Cliente.Include(c => c.Sucursal).ThenInclude(c => c.Banco);
            }
            else if (balance != null && !string.IsNullOrEmpty(nombre))
            {

                applicationDbContext = _context.Cliente.Where(cliente => cliente.Nombre.ToLower() == nombre.ToLower() && Convert.ToDouble(cliente.Balance) == balance).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
            }
            else if (string.IsNullOrEmpty(nombre) && balance != null)
            {
                applicationDbContext = _context.Cliente.Where(cliente => Convert.ToDouble(cliente.Balance) == balance).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
            }
            else if (!string.IsNullOrEmpty(nombre) && balance==null)
            {
                applicationDbContext = _context.Cliente.Where(cliente => cliente.Nombre.ToLower() == nombre.ToLower()).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
            }
            else
            {
                applicationDbContext = _context.Cliente.Include(c => c.Sucursal).ThenInclude(c => c.Banco);
            }
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexV2(double? balance, string nombre, string rad)
        {
            var applicationDbContext = _context.Cliente.Include(c => c.Sucursal).ThenInclude(c => c.Banco);
            //Si mayor o menor
            if (rad=="menor")
            {

                if (balance == null && string.IsNullOrEmpty(nombre))
                {
                    applicationDbContext = _context.Cliente.Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else if (balance != null && !string.IsNullOrEmpty(nombre))
                {

                    applicationDbContext = _context.Cliente.Where(cliente => cliente.Nombre.ToLower() == nombre.ToLower() && Convert.ToDouble(cliente.Balance) < balance).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else if (string.IsNullOrEmpty(nombre) && balance != null)
                {
                    applicationDbContext = _context.Cliente.Where(cliente => Convert.ToDouble(cliente.Balance) < balance).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else if (!string.IsNullOrEmpty(nombre) && balance == null)
                {
                    applicationDbContext = _context.Cliente.Where(cliente => cliente.Nombre.ToLower() == nombre.ToLower()).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else
                {
                    applicationDbContext = _context.Cliente.Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
            }
            else if(rad == "mayor")
            {
                if (balance == null && string.IsNullOrEmpty(nombre))
                {
                    applicationDbContext = _context.Cliente.Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else if (balance != null && !string.IsNullOrEmpty(nombre))
                {

                    applicationDbContext = _context.Cliente.Where(cliente => cliente.Nombre.ToLower() == nombre.ToLower() && Convert.ToDouble(cliente.Balance) >= balance).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else if (string.IsNullOrEmpty(nombre) && balance != null)
                {
                    applicationDbContext = _context.Cliente.Where(cliente => Convert.ToDouble(cliente.Balance) >= balance).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else if (!string.IsNullOrEmpty(nombre) && balance == null)
                {
                    applicationDbContext = _context.Cliente.Where(cliente => cliente.Nombre.ToLower() == nombre.ToLower()).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else
                {
                    applicationDbContext = _context.Cliente.Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }

            }
            else
            {
                if (balance == null && string.IsNullOrEmpty(nombre))
                {
                    applicationDbContext = _context.Cliente.Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else if (balance != null && !string.IsNullOrEmpty(nombre))
                {

                    applicationDbContext = _context.Cliente.Where(cliente => cliente.Nombre.ToLower() == nombre.ToLower() && Convert.ToDouble(cliente.Balance) == balance).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else if (string.IsNullOrEmpty(nombre) && balance != null)
                {
                    applicationDbContext = _context.Cliente.Where(cliente => Convert.ToDouble(cliente.Balance) == balance).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else if (!string.IsNullOrEmpty(nombre) && balance == null)
                {
                    applicationDbContext = _context.Cliente.Where(cliente => cliente.Nombre.ToLower() == nombre.ToLower()).Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
                else
                {
                    applicationDbContext = _context.Cliente.Include(c => c.Sucursal).ThenInclude(c => c.Banco);
                }
            }
            //Carga inicial
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Sucursal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }


        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Direccion");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Pasas varios atributos del objeto com un unico atributo con el Bind 
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Iban,Balance,SucursalId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Direccion", cliente.SucursalId);
            return View(cliente);
        }
        // GET: Clientes/Sacar Dinero/5
        public async Task<IActionResult> SacarDinero(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["Nombre"] = new SelectList(_context.Cliente, "Id", "Nombre", cliente.Nombre);
            ViewData["Apellido"] = new SelectList(_context.Cliente, "Id", "Apellido", cliente.Apellido);
            ViewData["Iban"] = new SelectList(_context.Cliente, "Id", "Iban", cliente.Iban);
            return View(cliente);
        }

        // POST: Clientes/SacarDinero/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SacarDinero(int id, [Bind("Id,Nombre,Apellido,Iban,Balance,SucursalId")] Cliente cliente, string degreso)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                cliente.Balance -= Convert.ToDouble(degreso);
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Direccion", cliente.SucursalId);
            return View(cliente);
        }
        // GET: Clientes/Sacar Dinero/5
        public async Task<IActionResult> IntroducirDinero(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            //ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Direccion", cliente.SucursalId);
            ViewData["Nombre"] = new SelectList(_context.Cliente, "Id", "Nombre", cliente.Nombre);
            ViewData["Apellido"] = new SelectList(_context.Cliente, "Id", "Apellido", cliente.Apellido);
            ViewData["Iban"] = new SelectList(_context.Cliente, "Id", "Iban", cliente.Iban);
            return View(cliente);
        }

        // POST: Clientes/SacarDinero/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IntroducirDinero(int id, [Bind("Id,Nombre,Apellido,Iban,Balance,SucursalId")] Cliente cliente, string ingreso)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                cliente.Balance += Convert.ToDouble(ingreso);
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            //ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Direccion", cliente.SucursalId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Direccion", cliente.SucursalId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Direccion", cliente.SucursalId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Iban,Balance,SucursalId")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Direccion", cliente.SucursalId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Sucursal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
