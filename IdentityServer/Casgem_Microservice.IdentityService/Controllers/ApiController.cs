using Casgem_Microservice.IdentityService.Dtos;
using Casgem_Microservice.IdentityService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Casgem_Microservice.IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApiController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser()
            {
                UserName = signUpDto.UserName,
                Email =signUpDto.Email,
                City= signUpDto.City
            };
            var result=await _userManager.CreateAsync(user,signUpDto.Password);

            if (result.Succeeded)
            {
                return Ok("Kullanıcı kaydı oluşturuldu!");
            }
            return Ok("Hata oluştu!");
        }
    }
}
