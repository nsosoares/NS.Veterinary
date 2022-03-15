using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NS.Veterinary.Api.Helpers;
using NS.Veterinary.Api.Extensions;
using NS.Veterinary.Api.Notifications;
using NS.Veterinary.Api.ViewModels;

namespace NS.Veterinary.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthController(
            INotifier notifier
            , SignInManager<IdentityUser> signInManager
            , UserManager<IdentityUser> userManager
            , IOptions<JwtSettings> jwtSettings) : base(notifier)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseApi>> Post([FromBody] RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState, registerUserViewModel);

            var user = new IdentityUser
            {
                UserName = registerUserViewModel.Email,
                Email = registerUserViewModel.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUserViewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return CustomResponse(JwtGenerator.GenerateJwt(_jwtSettings));
            }

            foreach (var error in result.Errors)
            {
                Notify($"{error.Code} - {error.Description}");
            }

            return CustomResponse(registerUserViewModel);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseApi>> Login([FromBody] LoginUserViewModel loginUserViewModel)
        {
            if(!ModelState.IsValid)
                return CustomResponse(ModelState, loginUserViewModel);

            var result = await _signInManager.PasswordSignInAsync(loginUserViewModel.Email, loginUserViewModel.Password, false, true);

            if(result.Succeeded) return CustomResponse(JwtGenerator.GenerateJwt(_jwtSettings));

            if (result.IsLockedOut)
            {
                Notify(ErrorMessage.GetErrorMessageIsLockedOut());
                return CustomResponse(loginUserViewModel);
            }
            Notify(ErrorMessage.GetErrorMessageloginFailure());
            return CustomResponse(loginUserViewModel);
        }
    }
}
