﻿using Casgem_Microservice.IdentityService.Dtos;
using Casgem_Microservice.IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Casgem_Microservice.IdentityService.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
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
