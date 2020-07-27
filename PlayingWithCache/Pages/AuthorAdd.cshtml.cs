using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using PlayingWithCache.Data;
using PlayingWithCache.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlayingWithCache.Pages
{
    public class AuthorAddPage : PageModel
    {
        private readonly IMemoryCache _cache;
        private readonly IRepository<Author> _repository;

        public AuthorAddPage(
            IMemoryCache cache,
            IRepository<Author> repository)
        {
            _cache = cache;
            _repository = repository;
        }

        public void OnGet() => Page();

        [BindProperty]
        public Author AuthorModel { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync(CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                Message = "Operation failed!";
                return Page();
            }

            await _repository.AddAsync(AuthorModel, ct);

            _cache.Remove(CacheKeys.AuthorsCacheKey);

            return RedirectToPage("./Authors");
        }
    }
}
