using Laptopy.DTOs;
using Laptopy.Models;
using Laptopy.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Laptopy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,SignInManager<ApplicationUser> signInManager )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(ApplicationUserDTO userDTO)
        {
            if (roleManager.Roles.IsNullOrEmpty())
            {
                await roleManager.CreateAsync(new(SD.adminRole));
                await roleManager.CreateAsync(new(SD.customerRole));
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = userDTO.FirstName + userDTO.LastName,
                    Email = userDTO.Email,
                   
                };

                var result = await userManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SD.customerRole);
                    await signInManager.SignInAsync(user, false);

                    return Ok();
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(userDTO);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);

            if (user != null)
            {
                var result = await userManager.CheckPasswordAsync(user, loginDTO.Password);

                if (result)
                {
                    await signInManager.SignInAsync(user, loginDTO.RemeberMe);

                    return Ok();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There are errors");
                }
            }

            return NotFound();
        }

        [HttpDelete("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
        //[HttpPost("forgot-password")]

        //public async Task<IActionResult> ForgotPassword([FromBody] SendOtpRequestDto dto)
        //{

        //    var response = await userManager.sen.SendOtpForPasswordReset(dto);
        //    if (!response.IsSucceeded)
        //    {
        //        return BadRequest(new { response.Message, StatusCode = 400 });
        //    }
        //    return Ok(new { response.Message, statusCode = 200 });
        //}

    }
}
