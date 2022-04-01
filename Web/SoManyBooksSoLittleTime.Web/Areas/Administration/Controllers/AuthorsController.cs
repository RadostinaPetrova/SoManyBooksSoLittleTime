namespace SoManyBooksSoLittleTime.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;

    public class AuthorsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Author> dataRepository;

        public AuthorsController(IDeletableEntityRepository<Author> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Authors
        public async Task<IActionResult> Index()
        {
            return this.View(await this.dataRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var author = await this.dataRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return this.NotFound();
            }

            return this.View(author);
        }

        // GET: Administration/Authors/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Author author)
        {
            if (this.ModelState.IsValid)
            {
                await this.dataRepository.AddAsync(author);
                await this.dataRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(author);
        }

        // GET: Administration/Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var author = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            if (author == null)
            {
                return this.NotFound();
            }

            return this.View(author);
        }

        // POST: Administration/Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FullName,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Author author)
        {
            if (id != author.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(author);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.AuthorExists(author.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(author);
        }

        // GET: Administration/Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var author = await this.dataRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return this.NotFound();
            }

            return this.View(author);
        }

        // POST: Administration/Authors/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            this.dataRepository.Delete(author);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool AuthorExists(int id)
        {
            return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
