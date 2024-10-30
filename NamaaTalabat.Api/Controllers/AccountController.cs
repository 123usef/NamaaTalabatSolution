using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NamaaTalabat.Api.DTOS;
using NamaaTalabat.Api.Errors;
using NamaaTalabat.Core.Entities.Identity;

namespace NamaaTalabat.Api.Controllers
{

    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser>   _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
          var CurrentUser =  await _userManager.FindByEmailAsync(model.Email);

            if (CurrentUser is null)
            {
                return BadRequest(new ApiResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(CurrentUser, model.Password , false);
            //var result = await _userManager.CheckPasswordAsync(CurrentUser, model.Password );

            if (result.Succeeded is false)
            {
                return BadRequest(new ApiResponse(401)); 
            }

            return Ok(new UserDto()
            {
                DisplayName= CurrentUser.Displayname,
                Email= CurrentUser.Email,
                Token = "here i will assign jwt Token "
            });


        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Displayname = model.DisplayName,
            };

            var cret = await _userManager.CreateAsync(user , model.Password );

            if (!cret.Succeeded)
                return BadRequest(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplayName = user.Displayname,
                Email = user.Email,
                Token = "token is here if i want to redirect to the personal Account"

            }); 

        }


    }
}
/*
 {
  "displayName": "ahemd",
  "email": "ahmed@gmail.com",
  "phoneNumber": "0123256465",
  "userName": "ahmed.gamal",
  "password": "UsEr@12345678"
}
 
 
 
 */