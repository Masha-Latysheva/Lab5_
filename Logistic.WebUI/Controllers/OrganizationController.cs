using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logistic.WebUI.Controllers
{
    public class OrganizationController : BaseController<IOrganizationRepository, Organization>
    {
        public OrganizationController(IOrganizationRepository repository) : base(repository)
        {
        }

        protected override Expression<Func<Organization, bool>> SearchExpression(string searchString)
        {
            return organization => organization.Name.ToLower().Contains(searchString.ToLower().Trim());
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var items = await GetSearchQuery(searchString)
                .ToListAsync();

            return View(ToPagedList(items, page));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await Repository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await Repository.GetEntityById(id);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Organization item)
        {
            await Repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Organization item)
        {
            await Repository.Add(item);

            return RedirectToAction(nameof(Index));
        }
    }
}