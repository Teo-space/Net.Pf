using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Net.Pf.Pages.AdminPanel.Infrastructure;


public class ErrrorHandleModel : PageModel
{
    //public void OnGet()
    public async Task OnGet()
    {
        await Task.Delay(1);

        throw new Exception("Testing Exception");
    }
}

