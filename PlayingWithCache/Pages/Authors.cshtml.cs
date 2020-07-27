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
    public class AuthorsPage : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRepository<Author> _repository;

        public AuthorsPage(
            IMemoryCache memoryCache,
            IRepository<Author> repository)
        {
            _memoryCache = memoryCache;
            _repository = repository;
        }

        public List<Author> Authors { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            /// Method A:
            //if (!_memoryCache.TryGetValue<List<Author>>(CacheKeys.AuthorsCacheKey, out var _))
            //{
            //    Authors = await _repository.ListAsync(ct);

            //    var cacheOptions = new MemoryCacheEntryOptions()
            //        .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

            //    _memoryCache.Set(CacheKeys.AuthorsCacheKey, Authors, cacheOptions);
            //    Message = " database.";
            //}
            //else
            //{
            //    Authors = _memoryCache.Get<List<Author>>(CacheKeys.AuthorsCacheKey);
            //    Message = " memory cache";
            //}

            /// Method B:
            Authors = await await _memoryCache
                .GetOrCreateAsync(CacheKeys.AuthorsCacheKey, e =>
                {
                    e.SlidingExpiration = TimeSpan.FromSeconds(30);
                    e.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                    return Task.FromResult(_repository.ListAsync(ct));
                });

            return Page();
        }
    }
}
