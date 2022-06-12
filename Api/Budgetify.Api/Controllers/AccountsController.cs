namespace Budgetify.Api.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/accounts")]
    [ApiController]
    [AllowAnonymous]
    public class AccountsController : ControllerBase
    {
        public IActionResult SignIn([FromRoute] string scheme)
        {
            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
            string? redirectUrl = Url.Content("~/");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, scheme);
        }
    }
}
