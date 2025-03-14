using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp3dotnet.Data;
using tp3dotnet.Models;

namespace tp3dotnet.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            var members = _context.MemberShipType.ToList();
            ViewBag.member = new SelectList(members, "Id", "Name");
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CustomerId,Name,MembershiptypeID")] Customer customer)
        {
            // Vérifier si MembershiptypeID est null
            if (customer.MembershiptypeID == null)
            {
                ModelState.AddModelError("MembershiptypeID", "Le type d'adhésion est obligatoire.");
            }


            if (ModelState.IsValid)
            {
                // Vérifier si le type d'adhésion existe
                customer.MembershipType = _context.MemberShipType
                                            .FirstOrDefault(m => m.Id == customer.MembershiptypeID);

                if (customer.MembershipType == null)
                {
                    ModelState.AddModelError("MembershipType", "Le type d'adhésion sélectionné est invalide.");
                }

                if (ModelState.IsValid)
                {
                    customer.CustomerId = Guid.NewGuid(); // Générer un nouvel ID
                    _context.Add(customer);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            // Stocker les erreurs dans ViewBag
            ViewBag.Errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            // Recharger la liste des types d'adhésion pour éviter une erreur dans la vue
            var members = _context.MemberShipType.ToList();
            ViewBag.member = new SelectList(members, "Id", "Name", customer.MembershiptypeID);

            return View(customer);
        }


        // Liste des clients
        public IActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }
        // GET: Customers/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .FirstOrDefault(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .FirstOrDefault(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Customers/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            // Charger la liste des types d'adhésion pour le dropdown
            var members = _context.MemberShipType.ToList();
            ViewBag.member = new SelectList(members, "Id", "Name", customer.MembershiptypeID);

            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("CustomerId,Name,MembershiptypeID")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (customer.MembershiptypeID == null)
            {
                ModelState.AddModelError("MembershiptypeID", "Le type d'adhésion est obligatoire.");
            }

            if (ModelState.IsValid)
            {
                //try
                {
                    // Vérifier que le type d'adhésion existe
                    customer.MembershipType = _context.MemberShipType
                                                      .FirstOrDefault(m => m.Id == customer.MembershiptypeID);

                    if (customer.MembershipType == null)
                    {
                        ModelState.AddModelError("MembershipType", "Le type d'adhésion sélectionné est invalide.");
                        var members = _context.MemberShipType.ToList();
                        ViewBag.member = new SelectList(members, "Id", "Name", customer.MembershiptypeID);
                        return View(customer);
                    }

                    _context.Update(customer);
                    _context.SaveChanges();
                }
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!_context.Customers.Any(e => e.CustomerId == customer.CustomerId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }

            // Recharger la liste des types d'adhésion en cas d'erreur
            var membersList = _context.MemberShipType.ToList();
            ViewBag.member = new SelectList(membersList, "Id", "Name", customer.MembershiptypeID);

            return View(customer);
        }

    }
}

