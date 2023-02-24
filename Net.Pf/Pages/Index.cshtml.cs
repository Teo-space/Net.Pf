using Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Net.Pf.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;

        public readonly IUserAccessor userAccessor;

        public IndexModel(ILogger<IndexModel> logger, IUserAccessor userAccessor)
        {
            this.logger = logger;
            this.userAccessor = userAccessor;
        }


        public void OnGet()
        {

        }


    }


}

