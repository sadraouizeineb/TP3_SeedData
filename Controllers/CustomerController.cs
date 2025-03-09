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
            //if (customer.MembershiptypeID == null)
            //{
            //    ModelState.AddModelError("MembershiptypeID", "Le type d'adhésion est obligatoire.");
            //}

            //if (ModelState.IsValid)
            //{
            // Vérifier si le type d'adhésion existe
            customer.MembershipType = _context.MemberShipType
                                        .FirstOrDefault(m => m.Id == customer.MembershiptypeID);

            //if (customer.MembershipType == null)
            //{
            //    ModelState.AddModelError("MembershipType", "Le type d'adhésion sélectionné est invalide.");
            //}

            if (ModelState.IsValid)
            {
                customer.CustomerId = Guid.NewGuid(); // Générer un nouvel ID
                _context.Add(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //}

            // Stocker les erreurs dans ViewBag
            //ViewBag.Errors = ModelState.Values
            //    .SelectMany(v => v.Errors)
            //    .Select(e => e.ErrorMessage)
            //    .ToList();

            // Recharger la liste des types d'adhésion pour éviter une erreur dans la vue
            //var members = _context.MembershipTypes.ToList();
            //ViewBag.member = new SelectList(members, "Id", "Name", customer.MembershiptypeID);

            return View(customer);
        }

        // Liste des clients
        public IActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }
    }
}
